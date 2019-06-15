/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：JsApiTicketContainer.cs
    文件功能描述：通用接口JsApiTicket容器，用于自动管理JsApiTicket，如果过期会重新获取
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OFoodWeChat.Work.CommonAPIs;
using OFoodWeChat.Work.Entities;
using OFoodWeChat.Work.Exceptions;

using OFoodWeChat.Core.Containers;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure.Utilities;

namespace OFoodWeChat.Work.Containers
{
    /// <summary>
    /// JsApiTicketBag
    /// </summary>
    [Serializable]
    public class JsApiTicketBag : BaseContainerBag
    {
        /// <summary>
        /// CorpId
        /// </summary>
        public string CorpId { get; set; }
        public string CorpSecret { get; set; }


        public JsApiTicketResult JsApiTicketResult { get; set; }


        public DateTimeOffset ExpireTime { get; set; }

        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        internal object Lock = new object();
    }

    /// <summary>
    /// 通用接口JsApiTicket容器，用于自动管理JsApiTicket，如果过期会重新获取
    /// </summary>
    public class JsApiTicketContainer : BaseContainer<JsApiTicketBag>
    {
        private const string UN_REGISTER_ALERT = "此AppId尚未注册，JsApiTicketContainer.Register完成注册（全局执行一次即可）！";

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket，
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="name">标记JsApiTicket名称（如微信公众号名称），帮助管理员识别。当 name 不为 null 和 空值时，本次注册内容将会被记录到 Senparc.Weixin.Config.SenparcWeixinSetting.Items[name] 中，方便取用。</param>
        /// 此接口无异步方法
        private static string BuildingKey(string corpId, string corpSecret)
        {
            return corpId + corpSecret;
        }

        public static void Register(string corpId, string corpSecret, string name = null)
        {
            //记录注册信息，RegisterFunc委托内的过程会在缓存丢失之后自动重试
            RegisterFunc = () =>
            {
                //using (FlushCache.CreateInstance())
                //{
                var bag = new JsApiTicketBag()
                {
                    Name = name,
                    CorpId = corpId,
                    CorpSecret = corpSecret,
                    ExpireTime = DateTimeOffset.MinValue,
                    JsApiTicketResult = new JsApiTicketResult()
                };
                Update(BuildingKey(corpId, corpSecret), bag,null);
                return bag;
                //}
            };
            RegisterFunc();

            if (!name.IsNullOrEmpty())
            {
                WxConfig.WeixinSetting.Items[name].WeixinCorpId = corpId;
                WxConfig.WeixinSetting.Items[name].WeixinCorpSecret = corpSecret;
            }
        }

        #region 同步方法


        /// <summary>
        /// 使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetTicket(string appId, string appSecret, bool getNewTicket = false)
        {
            if (!CheckRegistered(BuildingKey(appId, appSecret)) || getNewTicket)
            {
                Register(appId, appSecret);
            }
            return GetTicket(appId, appSecret, getNewTicket);
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetTicket(string appId, string appSecret, bool getNewTicket = false)
        {
            return GetTicketResult(appId, appSecret, getNewTicket).ticket;
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static JsApiTicketResult GetTicketResult(string appId, string appSecret, bool getNewTicket = false)
        {
            if (!CheckRegistered(BuildingKey(appId, appSecret)))
            {
                throw new WeixinWorkException(UN_REGISTER_ALERT);
            }

            var jsApiTicketBag = TryGetItem(BuildingKey(appId, appSecret));
            lock (jsApiTicketBag.Lock)
            {
                if (getNewTicket || jsApiTicketBag.ExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    jsApiTicketBag.JsApiTicketResult = CommonApi.GetTicket(jsApiTicketBag.CorpId, jsApiTicketBag.CorpSecret);
                    jsApiTicketBag.ExpireTime = ApiUtility.GetExpireTime(jsApiTicketBag.JsApiTicketResult.expires_in);
                    Update(jsApiTicketBag, null);//更新到缓存
                }
            }
            return jsApiTicketBag.JsApiTicketResult;
        }

        ///// <summary>
        ///// 检查是否已经注册
        ///// </summary>
        ///// <param name="appId"></param>
        ///// <returns></returns>
        ///// 此接口无异步方法
        //public new static bool CheckRegistered(string appId)
        //{
        //    return Cache.CheckExisted(appId);
        //}

        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static async Task<string> TryGetTicketAsync(string appId, string appSecret, bool getNewTicket = false)
        {
            if (!CheckRegistered(BuildingKey(appId, appSecret)) || getNewTicket)
            {
                Register(appId, appSecret);
            }
            return await GetTicketAsync(appId, appSecret, getNewTicket);
        }

        /// <summary>
        /// 【异步方法】获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<string> GetTicketAsync(string appId, string appSecret, bool getNewTicket = false)
        {
            var result = await GetTicketResultAsync(appId, appSecret, getNewTicket);
            return result.ticket;
        }

        /// <summary>
        /// 【异步方法】获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<JsApiTicketResult> GetTicketResultAsync(string appId, string appSecret, bool getNewTicket = false)
        {
            if (!CheckRegistered(BuildingKey(appId, appSecret)))
            {
                throw new WeixinWorkException(UN_REGISTER_ALERT);
            }

            var jsApiTicketBag = TryGetItem(BuildingKey(appId, appSecret));
            //lock (jsApiTicketBag.Lock)
            {
                if (getNewTicket || jsApiTicketBag.ExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    var jsApiTicketResult = await CommonApi.GetTicketAsync(jsApiTicketBag.CorpId, jsApiTicketBag.CorpSecret);
                    jsApiTicketBag.JsApiTicketResult = jsApiTicketResult;
                    //jsApiTicketBag.JsApiTicketResult = CommonApi.GetTicket(jsApiTicketBag.AppId, jsApiTicketBag.AppSecret);
                    jsApiTicketBag.ExpireTime = ApiUtility.GetExpireTime(jsApiTicketBag.JsApiTicketResult.expires_in);
                    Update(jsApiTicketBag, null);//更新到缓存
                }
            }
            return jsApiTicketBag.JsApiTicketResult;
        }
        #endregion
    }
}
