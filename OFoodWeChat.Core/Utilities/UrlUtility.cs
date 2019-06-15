/*----------------------------------------------------------------
    文件名：UrlUtility.cs
    文件功能描述：URL工具类
    
    修改标识：Senparc - 20190123
    修改描述：v6.3.6 支持在子程序环境下获取 OAuth 回调地址
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OFoodWeChat.Infrastructure.Extensions;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using OFoodWeChat.Core.Exceptions;

namespace OFoodWeChat.Core.Utilities
{
    /// <summary>
    /// URL工具类
    /// </summary>
    public class UrlUtility
    {
        /// <summary>
        /// 生成OAuth用的CallbackUrl参数（原始状态，未整体进行UrlEncode）
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="oauthCallbackUrl"></param>
        /// <returns></returns>

        public static string GenerateOAuthCallbackUrl(HttpContext httpContext, string oauthCallbackUrl)
        {


            if (httpContext.Request == null)
            {
                throw new WeixinNullReferenceException("httpContext.Request 不能为null！", httpContext);
            }

            var request = httpContext.Request;
            //var location = new Uri($"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}");
            //var returnUrl = location.AbsoluteUri; //httpContext.Request.Url.ToString();    
            var returnUrl = request.AbsoluteUri();
            var urlData = httpContext.Request;
            var scheme = urlData.Scheme;//协议
            var host = urlData.Host.Host;//主机名（不带端口）
            var port = urlData.Host.Port ?? -1;//端口（因为从.NET Framework移植，因此不直接使用urlData.Host）
            string portSetting = null;//Url中的端口部分
            string schemeUpper = scheme.ToUpper();//协议（大写）
            string baseUrl = httpContext.Request.PathBase;//子站点应用路径

            if (port == -1 || //这个条件只有在 .net core 中， Host.Port == null 的情况下才会发生
                (schemeUpper == "HTTP" && port == 80) ||
                (schemeUpper == "HTTPS" && port == 443))
            {
                portSetting = "";//使用默认值
            }
            else
            {
                portSetting = ":" + port;//添加端口
            }

            //授权回调字符串
            var callbackUrl = string.Format("{0}://{1}{2}{6}{3}{4}returnUrl={5}",
                scheme,
                host,
                portSetting,
                oauthCallbackUrl,
                oauthCallbackUrl.Contains("?") ? "&" : "?",
                returnUrl.UrlEncode(),
                //添加应用目录：https://github.com/JeffreySu/WeiXinMPSDK/issues/1552
                baseUrl
            );
            return callbackUrl;
        }
    }
}
