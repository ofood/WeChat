﻿/*----------------------------------------------------------------
    
    文件名：CommonApi.cs
    文件功能描述：通用基础API
    

----------------------------------------------------------------*/

/*
    获取AccessToken API地址：http://qydev.weixin.qq.com/wiki/index.php?title=%E4%B8%BB%E5%8A%A8%E8%B0%83%E7%94%A8
    获取微信服务器ip段 API地址：http://qydev.weixin.qq.com/wiki/index.php?title=%E5%9B%9E%E8%B0%83%E6%A8%A1%E5%BC%8F#.E8.8E.B7.E5.8F.96.E5.BE.AE.E4.BF.A1.E6.9C.8D.E5.8A.A1.E5.99.A8.E7.9A.84ip.E6.AE.B5
    获取调用微信JS接口的临时票据 API地址：http://qydev.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1JS%E6%8E%A5%E5%8F%A3
    userid转换成openid API地址:http://qydev.weixin.qq.com/wiki/index.php?title=Userid%E4%B8%8Eopenid%E4%BA%92%E6%8D%A2%E6%8E%A5%E5%8F%A3
 */

using System.Threading.Tasks;
using OFoodWeChat.Work.Containers;
using OFoodWeChat.Work.Entities;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Core;
using OFoodWeChat.Core.CommonAPIs;

namespace OFoodWeChat.Work.CommonAPIs
{
    /// <summary>
    /// 通用基础API
    /// </summary>
    public partial class CommonApi
    {
        //public static string _apiUrl = WxConfig.ApiMpHost + "/cgi-bin";

        #region 同步方法

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        [ApiBind(PlatformType.WeChat_Work, "CommonApi.GetToken", true)]
        public static AccessTokenResult GetToken(string corpId, string corpSecret)
        {
            #region 主动调用的频率限制
            /*
当你获取到AccessToken时，你的应用就可以成功调用企业号后台所提供的各种接口以管理或访问企业号后台的资源或给企业号成员发消息。

为了防止企业应用的程序错误而引发企业号服务器负载异常，默认情况下，每个企业号调用接口都有一定的频率限制，当超过此限制时，调用对应接口会收到相应错误码。

以下是当前默认的频率限制，企业号后台可能会根据运营情况调整此阈值：

基础频率
每企业调用单个cgi/api不可超过1000次/分，30000次/小时

每ip调用单个cgi/api不可超过2000次/分，60000次/小时

每ip获取AccessToken不可超过300次/小时

发消息频率
每企业不可超过200次/分钟；不可超过帐号上限数*30人次/天

创建帐号频率
每企业创建帐号数不可超过帐号上限数*3/月
*/
            #endregion

            var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/gettoken?corpid={0}&corpsecret={1}", corpId.AsUrlData(), corpSecret.AsUrlData());
            return CommonJsonSend.Send<AccessTokenResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 获取微信服务器的ip段
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "CommonApi.GetCallBackIp", true)]
        public static GetCallBackIpResult GetCallBackIp(string accessToken)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/getcallbackip?access_token={0}", accessToken.AsUrlData());

            return CommonJsonSend.Send<GetCallBackIpResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "CommonApi.GetTicket", true)]
        public static JsApiTicketResult GetTicket(string corpId, string corpSecret)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/get_jsapi_ticket?access_token={0}",
                                       accessToken.AsUrlData());

                JsApiTicketResult result = CommonJsonSend.Send<JsApiTicketResult>(null, url, null, CommonJsonSendType.GET);
                return result;
            }, AccessTokenContainer.BuildingKey(corpId, corpSecret));
        }



        /// <summary>
        /// userid转换成openid接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userId">企业号内的成员id</param>
        /// <param name="agentId">需要发送红包的应用ID，若只是使用微信支付和企业转账，则无需该参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "CommonApi.ConvertToOpenId", true)]
        public static ConvertToOpenIdResult ConvertToOpenId(string accessToken, string userId, string agentId = null, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/user/convert_to_openid?access_token={0}",
                accessToken.AsUrlData());

            var data = new
            {
                userid = userId,
                agentid = agentId
            };
            return CommonJsonSend.Send<ConvertToOpenIdResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// openid转换成userid接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "CommonApi.ConvertToUserId", true)]
        public static ConvertToUserIdResult ConvertToUserId(string accessToken, string openId, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/user/convert_to_userid?access_token={0}",
                accessToken.AsUrlData());

            var data = new
            {
                openid = openId
            };

            return CommonJsonSend.Send<ConvertToUserIdResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        #endregion

        #region 异步方法
        /// 【异步方法】<summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        [ApiBind(PlatformType.WeChat_Work, "CommonApi.GetTokenAsync", true)]
        public static async Task<AccessTokenResult> GetTokenAsync(string corpId, string corpSecret)
        {
            #region 主动调用的频率限制
            /*
当你获取到AccessToken时，你的应用就可以成功调用企业号后台所提供的各种接口以管理或访问企业号后台的资源或给企业号成员发消息。

为了防止企业应用的程序错误而引发企业号服务器负载异常，默认情况下，每个企业号调用接口都有一定的频率限制，当超过此限制时，调用对应接口会收到相应错误码。

以下是当前默认的频率限制，企业号后台可能会根据运营情况调整此阈值：

基础频率
每企业调用单个cgi/api不可超过1000次/分，30000次/小时

每ip调用单个cgi/api不可超过2000次/分，60000次/小时

每ip获取AccessToken不可超过300次/小时

发消息频率
每企业不可超过200次/分钟；不可超过帐号上限数*30人次/天

创建帐号频率
每企业创建帐号数不可超过帐号上限数*3/月
*/
            #endregion

            var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/gettoken?corpid={0}&corpsecret={1}", corpId.AsUrlData(), corpSecret.AsUrlData());
            return await CommonJsonSend.SendAsync<AccessTokenResult>(null, url, null, CommonJsonSendType.POST);
        }

        /// <summary>
        /// 【异步方法】获取微信服务器的ip段
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "CommonApi.GetCallBackIpAsync", true)]
        public static async Task<GetCallBackIpResult> GetCallBackIpAsync(string accessToken)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/getcallbackip?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<GetCallBackIpResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 【异步方法】获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "CommonApi.GetTicketAsync", true)]
        public static async Task<JsApiTicketResult> GetTicketAsync(string corpId, string corpSecret)
        {

            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/get_jsapi_ticket?access_token={0}",
                                    accessToken.AsUrlData());

                return await CommonJsonSend.SendAsync<JsApiTicketResult>(null, url, null, CommonJsonSendType.GET);
            }, AccessTokenContainer.BuildingKey(corpId, corpSecret));
        }


        /// <summary>
        /// 【异步方法】userid转换成openid接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userId">企业号内的成员id</param>
        /// <param name="agentId">需要发送红包的应用ID，若只是使用微信支付和企业转账，则无需该参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "CommonApi.ConvertToOpenIdAsync", true)]
        public static async Task<ConvertToOpenIdResult> ConvertToOpenIdAsync(string accessToken, string userId, string agentId = null, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/user/convert_to_openid?access_token={0}",
                accessToken.AsUrlData());

            var data = new
            {
                userid = userId,
                agentid = agentId
            };

            return await CommonJsonSend.SendAsync<ConvertToOpenIdResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 【异步方法】openid转换成userid接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "CommonApi.ConvertToUserIdAsync", true)]
        public static async Task<ConvertToUserIdResult> ConvertToUserIdAsync(string accessToken, string openId, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/user/convert_to_userid?access_token={0}",
                accessToken.AsUrlData());

            var data = new
            {
                openid = openId
            };

            return await CommonJsonSend.SendAsync<ConvertToUserIdResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        #endregion
    }
}
