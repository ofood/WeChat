/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：SenparcHttpClient.cs
    文件功能描述：SenparcHttpClient，用于提供 HttpClientFactory 的自定义类
----------------------------------------------------------------*/


using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace OFoodWeChat.Infrastructure.Http
{
    /// <summary>
    /// SenparcHttpClient，用于提供 HttpClientFactory 的自定义类
    /// </summary>
    public class SenparcHttpClient
    {
        /// <summary>
        /// HttpClient 对象
        /// </summary>
        public HttpClient Client { get; private set; }

        /// <summary>
        /// 从 HttpClientFactory 的唯一名称中获取 HttpClient 对象，并加载到 SenparcHttpClient 中
        /// </summary>
        /// <param name="httpClientName"></param>
        /// <returns></returns>
        public static SenparcHttpClient GetInstanceByName(string httpClientName)
        {
            if (!string.IsNullOrEmpty(httpClientName))
            {
                var clientFactory = OFoodDI.GetRequiredService<IHttpClientFactory>();
                var httpClient = clientFactory.CreateClient(httpClientName);
                return new SenparcHttpClient(httpClient);
            }

            return OFoodDI.GetRequiredService<SenparcHttpClient>(true);
        }

        /// <summary>
        /// SenparcHttpClient 构造函数
        /// </summary>
        /// <param name="httpClient"></param>
        public SenparcHttpClient(HttpClient httpClient)
        {
            //httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            //httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            Client = httpClient;
        }

        //public void SetHandler(HttpClientHandler handler)
        //{
        //}

        public void SetCookie(Uri uri, CookieContainer cookieContainer)
        {
            if (cookieContainer == null)
            {
                return;
            }

            var cookieHeader = cookieContainer.GetCookieHeader(uri);
            Client.DefaultRequestHeaders.Add(HeaderNames.Cookie, cookieHeader);
        }

    }
}