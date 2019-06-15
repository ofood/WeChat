/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SnsApi.cs
    文件功能描述：小程序微信登录
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Threading.Tasks;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure.Enums;

namespace OFoodWeChat.Open.WxaAPIs.Sns
{
    /// <summary>
    /// 微信SNS接口
    /// </summary>
    public static class SnsApi
    {
        #region 同步方法

        /// <summary>
        /// code 换取 session_key
        /// </summary>
        /// <param name="appId">小程序的AppID</param>
        /// <param name="componentAppId">第三方平台appid</param>
        /// <param name="componentAccessToken">	第三方平台的component_access_token</param>
        /// <param name="jsCode">登录时获取的 code</param>
        /// <param name="grantType">保持默认：authorization_code</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "SnsApi.JsCode2Json", true)]
        public static JsCode2JsonResult JsCode2Json(string appId, string componentAppId, string componentAccessToken, string jsCode, string grantType = "authorization_code", int timeOut = WxConfig.TIME_OUT)
        {
            string urlFormat =
                WxConfig.ApiMpHost + "/sns/component/jscode2session?appid={0}&js_code={1}&grant_type={2}&component_appid={3}&component_access_token={4}";

            var url = string.Format(urlFormat, appId, jsCode, grantType, componentAppId, componentAccessToken);
            var result = CommonJsonSend.Send<JsCode2JsonResult>(null, url, null, CommonJsonSendType.GET);
            return result;
        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】code 换取 session_key
        /// </summary>
        /// <param name="appId">小程序的AppID</param>
        /// <param name="componentAppId">第三方平台appid</param>
        /// <param name="componentAccessToken">	第三方平台的component_access_token</param>
        /// <param name="jsCode">登录时获取的 code</param>
        /// <param name="grantType">保持默认：authorization_code</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "SnsApi.JsCode2JsonAsync", true)]
        public static async Task<JsCode2JsonResult> JsCode2JsonAsync(string appId, string componentAppId, string componentAccessToken, string jsCode, string grantType = "authorization_code", int timeOut = WxConfig.TIME_OUT)
        {
            string urlFormat =
                WxConfig.ApiMpHost + "/sns/component/jscode2session?appid={0}&js_code={1}&grant_type={2}&component_appid={3}&component_access_token={4}";

            var url = string.Format(urlFormat, appId, jsCode, grantType, componentAppId, componentAccessToken);
            var result = await CommonJsonSend.SendAsync<JsCode2JsonResult>(null, url, null, CommonJsonSendType.GET);
            return result;
        }

        #endregion
#endif
    }
}