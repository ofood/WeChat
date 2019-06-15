/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：OAuthContainer.cs
    文件功能描述：用户OAuth容器，用于自动管理OAuth的AccessToken，如果过期会重新获取


    创建标识：Senparc - 20160801

    修改标识：Senparc - 20160803
    修改描述：v14.2.3 使用ApiUtility.GetExpireTime()方法处理过期
 
    修改标识：Senparc - 20160804
    修改描述：v14.2.4 增加TryGetOAuthAccessTokenAsync，GetOAuthAccessTokenAsync，GetOAuthAccessTokenResultAsync的异步方法

    修改标识：Senparc - 20160808
    修改描述：v14.3.0 删除 ItemCollection 属性，直接使用ContainerBag加入到缓存
        
    修改标识：Senparc - 20160813
    修改描述：v14.3.4 添加TryReRegister()方法，处理分布式缓存重启（丢失）的情况
    
    修改标识：Senparc - 20160813
    修改描述：v14.3.6 完善getNewToken参数传递

    修改标识：Senparc - 20180614
    修改描述：CO2NET v0.1.0 ContainerBag 取消属性变动通知机制，使用手动更新缓存

    修改标识：Senparc - 20180707
    修改描述：v15.0.9 Container 的 Register() 的微信参数自动添加到 Config.SenparcWeixinSetting.Items 下

    修改标识：Senparc - 20181226
    修改描述：v16.6.2 修改 DateTime 为 DateTimeOffset
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OFoodWeChat.MP.AdvancedAPIs;
using OFoodWeChat.MP.AdvancedAPIs.OAuth;
using OFoodWeChat.Core.Containers;
using OFoodWeChat.Core.Exceptions;
using OFoodWeChat.Infrastructure.Utilities;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure.Extensions;

namespace OFoodWeChat.MP.Containers
{
    /// <summary>
    /// OAuth包
    /// </summary>
    [Serializable]
    public class OAuthAccessTokenBag : BaseContainerBag, IBaseContainerBag_AppId
    {
        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public OAuthAccessTokenResult OAuthAccessTokenResult { get; set; }


        public DateTimeOffset OAuthAccessTokenExpireTime { get; set; }

        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        internal object Lock = new object();

        //private DateTimeOffset _oAuthAccessTokenExpireTime;
        //private OAuthAccessTokenResult _oAuthAccessTokenResult;
        //private string _appSecret;
        //private string _appId;
    }

    /// <summary>
    /// 用户OAuth容器，用于自动管理OAuth的AccessToken，如果过期会重新获取（测试中，暂时别用）
    /// </summary>
    public class OAuthAccessTokenContainer : BaseContainer<OAuthAccessTokenBag>
    {
        const string LockResourceName = "Core.OAuthAccessTokenContainer";

        #region 同步方法


        //static Dictionary<string, JsApiTicketBag> JsApiTicketCollection =
        //   new Dictionary<string, JsApiTicketBag>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket，
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="name">标记JsApiTicket名称（如微信公众号名称），帮助管理员识别。当 name 不为 null 和 空值时，本次注册内容将会被记录到 Senparc.Weixin.Config.SenparcWeixinSetting.Items[name] 中，方便取用。</param>
        /// 此接口不提供异步方法
        public static void Register(string appId, string appSecret, string name = null)
        {
            RegisterFunc = () =>
            {
                //using (FlushCache.CreateInstance())
                //{
                var bag = new OAuthAccessTokenBag()
                {
                    Name = name,
                    AppId = appId,
                    AppSecret = appSecret,
                    OAuthAccessTokenExpireTime = DateTimeOffset.MinValue,
                    OAuthAccessTokenResult = new OAuthAccessTokenResult()
                };
                Update(appId, bag, null);
                return bag;
                //}
            };
            RegisterFunc();

            if (!name.IsNullOrEmpty())
            {
                WxConfig.WeixinSetting.Items[name].WeixinAppId = appId;
                WxConfig.WeixinSetting.Items[name].WeixinAppSecret = appSecret;
            }
        }

        #region OAuthAccessToken

        /// <summary>
        /// 使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetOAuthAccessToken(string appId, string appSecret, string code, bool getNewToken = false)
        {
            if (!CheckRegistered(appId) || getNewToken)
            {
                Register(appId, appSecret);
            }
            return GetOAuthAccessToken(appId, code, getNewToken);
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="getNewToken">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetOAuthAccessToken(string appId, string code, bool getNewToken = false)
        {
            return GetOAuthAccessTokenResult(appId, code, getNewToken).access_token;
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="getNewToken">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static OAuthAccessTokenResult GetOAuthAccessTokenResult(string appId, string code, bool getNewToken = false)
        {
            if (!CheckRegistered(appId))
            {
                throw new UnRegisterAppIdException(null, "此appId尚未注册，请先使用OAuthAccessTokenContainer.Register完成注册（全局执行一次即可）！");
            }

            var oAuthAccessTokenBag = TryGetItem(appId);
            using (Cache.BeginCacheLock(LockResourceName, appId))//同步锁
            {
                if (getNewToken || oAuthAccessTokenBag.OAuthAccessTokenExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    oAuthAccessTokenBag.OAuthAccessTokenResult = OAuthApi.GetAccessToken(oAuthAccessTokenBag.AppId, oAuthAccessTokenBag.AppSecret, code);
                    oAuthAccessTokenBag.OAuthAccessTokenExpireTime =
                        ApiUtility.GetExpireTime(oAuthAccessTokenBag.OAuthAccessTokenResult.expires_in);
                    Update(oAuthAccessTokenBag, null);
                }
            }
            return oAuthAccessTokenBag.OAuthAccessTokenResult;
        }

        #endregion
        #endregion

        #region 异步方法
        #region OAuthAccessToken

        /// <summary>
        /// 【异步方法】使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static async Task<string> TryGetOAuthAccessTokenAsync(string appId, string appSecret, string code, bool getNewToken = false)
        {
            if (!CheckRegistered(appId) || getNewToken)
            {
                Register(appId, appSecret);
            }
            return await GetOAuthAccessTokenAsync(appId, code, getNewToken);
        }

        /// <summary>
        /// 【异步方法】获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="getNewToken">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<string> GetOAuthAccessTokenAsync(string appId, string code, bool getNewToken = false)
        {
            var result = await GetOAuthAccessTokenResultAsync(appId, code, getNewToken);
            return result.access_token;
        }

        /// <summary>
        /// 【异步方法】获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="getNewToken">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<OAuthAccessTokenResult> GetOAuthAccessTokenResultAsync(string appId, string code, bool getNewToken = false)
        {
            if (!CheckRegistered(appId))
            {
                throw new UnRegisterAppIdException(null, "此appId尚未注册，请先使用OAuthAccessTokenContainer.Register完成注册（全局执行一次即可）！");
            }

            var oAuthAccessTokenBag = TryGetItem(appId);
            using (Cache.BeginCacheLock(LockResourceName, appId))//同步锁
            {
                if (getNewToken || oAuthAccessTokenBag.OAuthAccessTokenExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    var oAuthAccessTokenResult = await OAuthApi.GetAccessTokenAsync(oAuthAccessTokenBag.AppId, oAuthAccessTokenBag.AppSecret, code);
                    oAuthAccessTokenBag.OAuthAccessTokenResult = oAuthAccessTokenResult;
                    //oAuthAccessTokenBag.OAuthAccessTokenResult =  OAuthApi.GetAccessToken(oAuthAccessTokenBag.AppId, oAuthAccessTokenBag.AppSecret, code);
                    oAuthAccessTokenBag.OAuthAccessTokenExpireTime =
                        ApiUtility.GetExpireTime(oAuthAccessTokenBag.OAuthAccessTokenResult.expires_in);
                    Update(oAuthAccessTokenBag, null);
                }
            }
            return oAuthAccessTokenBag.OAuthAccessTokenResult;
        }

        #endregion
        #endregion
    }
}
