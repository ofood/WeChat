﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：MobileApi.cs
    文件功能描述：移动端SDK
    
    
    创建标识：Senparc - 20181009
    
    
----------------------------------------------------------------*/

/*
    官方文档：https://work.weixin.qq.com/api/doc#10029
 */
using OFoodWeChat.Work.AdvancedAPIs.Mobile;
using System.Threading.Tasks;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Core;

namespace OFoodWeChat.Work.AdvancedAPIs
{
    /// <summary>
    /// 移动端SDK
    /// </summary>
    public static class MobileApi
    {
        #region 同步方法


        /// <summary>
        /// 获取电子发票ticket
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "MobileApi.GetTicket", true)]
        public static GetTicketResultJson GetTicket(string accessTokenOrAppKey, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/ticket/get?access_token={0}&type=wx_card", accessToken);

                return CommonJsonSend.Send<GetTicketResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);

        }

        /// <summary>
        /// 获取应用的jsapi_ticket
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "MobileApi.GetJsApiTicket", true)]
        public static GetTicketResultJson GetJsApiTicket(string accessTokenOrAppKey, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/ticket/get?access_token={0}&type=agent_config", accessToken);

                return CommonJsonSend.Send<GetTicketResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);

        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】获取电子发票ticket
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "MobileApi.GetTicketAsync", true)]
        public static async Task<GetTicketResultJson> GetTicketAsync(string accessTokenOrAppKey, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/ticket/get?access_token={0}&type=wx_card", accessToken);

                return await CommonJsonSend.SendAsync<GetTicketResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);

        }

        /// <summary>
        /// 【异步方法】获取应用的jsapi_ticket
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "MobileApi.GetJsApiTicketAsync", true)]
        public static async Task<GetTicketResultJson> GetJsApiTicketAsync(string accessTokenOrAppKey, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/ticket/get?access_token={0}&type=agent_config", accessToken);

                return await CommonJsonSend.SendAsync<GetTicketResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);

        }

        #endregion
#endif
    }
}
