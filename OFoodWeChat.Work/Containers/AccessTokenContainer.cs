/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：AccessTokenContainer.cs
    文件功能描述：通用接口AccessToken容器，用于自动管理AccessToken，如果过期会重新获取

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OFoodWeChat.Core.Containers;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Infrastructure.Data.JsonResult;
using OFoodWeChat.Infrastructure.Utilities;
using OFoodWeChat.Work.CommonAPIs;
using OFoodWeChat.Work.Entities;

namespace OFoodWeChat.Work.Containers
{
   

    /// <summary>
    /// 通用接口AccessToken容器，用于自动管理AccessToken，如果过期会重新获取
    /// </summary>
    public class AccessTokenContainer : BaseContainer<AccessTokenBag>
    {
        private const string UN_REGISTER_ALERT = "此CorpId尚未注册，AccessTokenContainer.Register完成注册（全局执行一次即可）！";

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token。
        /// 执行此注册过程，会连带注册ProviderTokenContainer。
        /// </summary>
        /// <param name="corpId">corpId</param>
        /// <param name="corpSecret">corpSecret</param>
        /// 此接口无异步方法
        public static string BuildingKey(string corpId, string corpSecret)
        {
            return string.Format("{0}@{1}", corpId, corpSecret);
        }

        /// <summary>
        /// 根据Key获取corpId和corpSecret
        /// </summary>
        /// <param name="appKey">由BuildingKey()方法生成的Key</param>
        /// <param name="corpId">corpId</param>
        /// <param name="corpSecret">corpSecret</param>
        public static void GetCorpIdAndSecretFromKey(string appKey, out string corpId, out string corpSecret)
        {
            var keyArr = appKey.Split('@');
            corpId = keyArr[0];
            corpSecret = keyArr[1];
        }


        /// <summary>
        /// 注册每个corpId和corpSecret，在调用高级接口时可以仅使用AppKey（由 AccessTokenContainer.BuildingKey() 方法获得）
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别。当 name 不为 null 和 空值时，本次注册内容将会被记录到 Senparc.Weixin.Config.SenparcWeixinSetting.Items[name] 中，方便取用。</param>
        public static void Register(string corpId, string corpSecret, string name = null)
        {
            //记录注册信息，RegisterFunc委托内的过程会在缓存丢失之后自动重试
            RegisterFunc = () =>
            {
                //using (FlushCache.CreateInstance())
                //{
                var bag = new AccessTokenBag()
                {
                    Name = name,
                    CorpId = corpId,
                    CorpSecret = corpSecret,
                    ExpireTime = DateTimeOffset.MinValue,
                    AccessTokenResult = new AccessTokenResult()
                };
                Update(BuildingKey(corpId, corpSecret), bag, null);
                return bag;
                //}
            };
            RegisterFunc();

            if (!name.IsNullOrEmpty())
            {
                WxConfig.WeixinSetting.Items[name].WeixinCorpId = corpId;
                WxConfig.WeixinSetting.Items[name].WeixinCorpSecret = corpSecret;
            }

            JsApiTicketContainer.Register(corpId, corpSecret);//连带注册JsApiTicketContainer

            ProviderTokenContainer.Register(corpId, corpSecret);//连带注册ProviderTokenContainer
        }

        #region 同步方法


        /// <summary>
        /// 使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetToken(string corpId, string corpSecret, bool getNewToken = false)
        {
            if (!CheckRegistered(BuildingKey(corpId, corpSecret)) || getNewToken)
            {
                Register(corpId, corpSecret);
            }
            return GetToken(corpId, corpSecret, getNewToken);
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="appKey">由BuildingKey()方法生成的Key</param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetToken(string appKey, bool getNewToken = false)
        {
            return GetTokenResult(appKey, getNewToken).access_token;
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetToken(string corpId, string corpSecret, bool getNewToken = false)
        {
            var appKey = BuildingKey(corpId, corpSecret);
            return GetTokenResult(appKey, getNewToken).access_token;
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static AccessTokenResult GetTokenResult(string corpId, string corpSecret, bool getNewToken = false)
        {
            var appKey = BuildingKey(corpId, corpSecret);
            return GetTokenResult(appKey, getNewToken);
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="appKey">由BuildingKey()方法生成的Key</param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static AccessTokenResult GetTokenResult(string appKey, bool getNewToken = false)
        {
            if (!CheckRegistered(appKey))
            {
                string corpId;
                string corpSecret;
                GetCorpIdAndSecretFromKey(appKey, out corpId, out corpSecret);
                Register(corpId, corpSecret);
                //throw new WeixinWorkException(UN_REGISTER_ALERT);
            }

            var accessTokenBag = TryGetItem(appKey);
            lock (accessTokenBag.Lock)
            {
                if (getNewToken || accessTokenBag.ExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    accessTokenBag.AccessTokenResult = CommonApi.GetToken(accessTokenBag.CorpId,
                        accessTokenBag.CorpSecret);
                    accessTokenBag.ExpireTime = ApiUtility.GetExpireTime(accessTokenBag.AccessTokenResult.expires_in);
                    Update(accessTokenBag, null);//更新到缓存
                }
            }
            return accessTokenBag.AccessTokenResult;
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static async Task<string> TryGetTokenAsync(string corpId, string corpSecret, bool getNewToken = false)
        {
            if (!CheckRegistered(BuildingKey(corpId, corpSecret)) || getNewToken)
            {
                Register(corpId, corpSecret);
            }
            return await GetTokenAsync(corpId, corpSecret, getNewToken);
        }

        /// <summary>
        /// 【异步方法】获取可用Token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<string> GetTokenAsync(string corpId, string corpSecret, bool getNewToken = false)
        {
            var result = await GetTokenResultAsync(corpId, corpSecret, getNewToken);
            return result.access_token;
        }

        /// <summary>
        /// 【异步方法】获取可用Token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<IAccessTokenResult> GetTokenResultAsync(string corpId, string corpSecret, bool getNewToken = false)
        {
            var appKey = BuildingKey(corpId, corpSecret);
            return await GetTokenResultAsync(appKey, getNewToken);
        }


        /// <summary>
        /// 【异步方法】获取可用Token
        /// </summary>
        /// <param name="appKey">由BuildingKey()方法生成的Key</param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static async Task<IAccessTokenResult> GetTokenResultAsync(string appKey, bool getNewToken = false)
        {
            if (!CheckRegistered(appKey))
            {
                string corpId;
                string corpSecret;
                GetCorpIdAndSecretFromKey(appKey, out corpId, out corpSecret);

                Register(corpId, corpSecret);
                //throw new WeixinWorkException(UN_REGISTER_ALERT);
            }

            var accessTokenBag = TryGetItem(appKey);
            // lock (accessTokenBag.Lock)
            {
                if (getNewToken || accessTokenBag.ExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    var accessTokenResult = await CommonApi.GetTokenAsync(accessTokenBag.CorpId,
                        accessTokenBag.CorpSecret);
                    //accessTokenBag.AccessTokenResult = CommonApi.GetToken(accessTokenBag.CorpId,
                    //    accessTokenBag.CorpSecret);
                    accessTokenBag.AccessTokenResult = accessTokenResult;
                    accessTokenBag.ExpireTime = ApiUtility.GetExpireTime(accessTokenBag.AccessTokenResult.expires_in);
                    Update(accessTokenBag, null);//更新到缓存
                }
            }
            return accessTokenBag.AccessTokenResult;
        }
        #endregion

    }
}
