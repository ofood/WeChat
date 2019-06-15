/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：RequestUtility.Get.cs
    文件功能描述：获取请求结果（Get）

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.Infrastructure.Helpers;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using OFoodWeChat.Infrastructure.WebProxy;

namespace OFoodWeChat.Infrastructure.Http
{
    /// <summary>
    /// HTTP 请求工具类
    /// </summary>
    public static partial class RequestUtility
    {
        #region 公用静态方法

        /// <summary>
        /// .NET Core 版本的HttpWebRequest参数设置
        /// </summary>
        /// <returns></returns>
        private static HttpClient HttpGet_Common_NetCore(string url, CookieContainer cookieContainer = null,
            Encoding encoding = null, X509Certificate2 cer = null,
            string refererUrl = null, bool useAjax = false, int timeOut = OFoodConfig.TIME_OUT)
        {
            var handler = HttpClientHelper.GetHttpClientHandler(cookieContainer, RequestUtility.SenparcHttpClientWebProxy, DecompressionMethods.GZip);

            if (cer != null)
            {
                handler.ClientCertificates.Add(cer);
            }

            HttpClient httpClient = OFoodDI.GetRequiredService<SenparcHttpClient>().Client;
            HttpClientHeader(httpClient, refererUrl, useAjax, null, timeOut);

            return httpClient;
        }

        #endregion

        #region 同步方法

        /// <summary>
        /// 使用Get方法获取字符串结果（没有加入Cookie）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpGet(string url, Encoding encoding = null)
        {
            var handler = HttpClientHelper.GetHttpClientHandler(null, SenparcHttpClientWebProxy, DecompressionMethods.GZip);


            HttpClient httpClient = OFoodDI.GetRequiredService<SenparcHttpClient>().Client;

            return httpClient.GetStringAsync(url).Result;
        }

        /// <summary>
        /// 使用Get方法获取字符串结果（加入Cookie）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="encoding"></param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="refererUrl">referer参数</param>
        /// <param name="useAjax">是否使用Ajax</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string HttpGet(string url, CookieContainer cookieContainer = null, Encoding encoding = null, X509Certificate2 cer = null,
            string refererUrl = null, bool useAjax = false, int timeOut = OFoodConfig.TIME_OUT)
        {
            var httpClient = HttpGet_Common_NetCore(url, cookieContainer, encoding, cer, refererUrl, useAjax, timeOut);

            var response = httpClient.GetAsync(url).GetAwaiter().GetResult();//获取响应信息

            HttpClientHelper.SetResponseCookieContainer(cookieContainer, response);//设置 Cookie

            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 获取HttpWebResponse或HttpResponseMessage对象，本方法通常用于测试）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="encoding"></param>
        /// <param name="cer"></param>
        /// <param name="refererUrl"></param>
        /// <param name="useAjax">是否使用Ajax请求</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static HttpResponseMessage HttpResponseGet(string url, CookieContainer cookieContainer = null, Encoding encoding = null, X509Certificate2 cer = null,
   string refererUrl = null, bool useAjax = false, int timeOut = OFoodConfig.TIME_OUT)
        {
            var httpClient = HttpGet_Common_NetCore(url, cookieContainer, encoding, cer, refererUrl, useAjax, timeOut);
            var task = httpClient.GetAsync(url);
            HttpResponseMessage response = task.Result;

            HttpClientHelper.SetResponseCookieContainer(cookieContainer, response);//设置 Cookie

            return response;
        }


        #endregion

        #region 异步方法

        /// <summary>
        /// 使用Get方法获取字符串结果（没有加入Cookie）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> HttpGetAsync(string url, Encoding encoding = null)
        {
            var handler = new HttpClientHandler
            {
                UseProxy = SenparcHttpClientWebProxy != null,
                Proxy = SenparcHttpClientWebProxy,
            };

            HttpClient httpClient = OFoodDI.GetRequiredService<SenparcHttpClient>().Client;
            return await httpClient.GetStringAsync(url).ConfigureAwait(false);

        }

        /// <summary>
        /// 使用Get方法获取字符串结果（加入Cookie）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="encoding"></param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="timeOut"></param>
        /// <param name="refererUrl">referer参数</param>
        /// <returns></returns>
        public static async Task<string> HttpGetAsync(string url, CookieContainer cookieContainer = null, Encoding encoding = null, X509Certificate2 cer = null,
            string refererUrl = null, bool useAjax = false, int timeOut = OFoodConfig.TIME_OUT)
        {
            var httpClient = HttpGet_Common_NetCore(url, cookieContainer, encoding, cer, refererUrl, useAjax, timeOut);

            var response = await httpClient.GetAsync(url).ConfigureAwait(false);//获取响应信息

            HttpClientHelper.SetResponseCookieContainer(cookieContainer, response);//设置 Cookie

            var retString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return retString;
        }

        #endregion
    }
}
