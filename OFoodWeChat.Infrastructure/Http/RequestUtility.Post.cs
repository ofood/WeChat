/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：RequestUtility.Post.cs
    文件功能描述：获取请求结果（Post）


    创建标识：Senparc - 20171006

    修改描述：移植Post方法过来

    修改标识：Senparc - 20180516
    修改描述：v4.21.1-rc1  解决 RequestUtility.HttpResponsePost() 和 HttpPostAsync() 方法
                           在 .net core 下没有及时关闭 postStream 的问题

    修改标识：Senparc - 20180602
    修改描述：v4.22.2 完善 RequestUtility.HttpPost_Common_NetCore() 字符串信息提交过程

    -- CO2NET --

    修改标识：Senparc - 20181009
    修改描述：v0.2.15 Post 方法添加 headerAddition参数

    修改标识：Senparc - 20190429
    修改描述：v0.7.0 优化 HttpClient，重构 RequestUtility（包括 Post 和 Get），引入 HttpClientFactory 机制

    修改标识：Senparc - 20190521
    修改描述：v0.7.3 .NET Core 提供多证书注册功能

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
using OFoodWeChat.Infrastructure.Exceptions;
using System.Linq;

namespace OFoodWeChat.Infrastructure.Http
{
    /// <summary>
    /// HTTP 请求工具类
    /// </summary>
    public static partial class RequestUtility
    {
        #region 静态公共方法


        /// <summary>
        /// 给.NET Core使用的HttpPost请求公共设置方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="hc"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="postStream"></param>
        /// <param name="fileDictionary"></param>
        /// <param name="refererUrl"></param>
        /// <param name="encoding"></param>
        /// <param name="certName">证书唯一名称，如果不需要则保留null</param>
        /// <param name="useAjax"></param>
        /// <param name="headerAddition">header附加信息</param>
        /// <param name="timeOut"></param>
        /// <param name="checkValidationResult"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static HttpClient HttpPost_Common_NetCore(string url, out HttpContent hc, CookieContainer cookieContainer = null,
            Stream postStream = null, Dictionary<string, string> fileDictionary = null, string refererUrl = null,
            Encoding encoding = null, string certName = null, bool useAjax = false, Dictionary<string, string> headerAddition = null,
            int timeOut = OFoodConfig.TIME_OUT, bool checkValidationResult = false, string contentType = HttpClientHelper.DEFAULT_CONTENT_TYPE)
        {
            //HttpClientHandler handler = HttpClientHelper.GetHttpClientHandler(cookieContainer, SenparcHttpClientWebProxy, DecompressionMethods.GZip);

            //if (checkValidationResult)
            //{
            //    handler.ServerCertificateCustomValidationCallback = new Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>(CheckValidationResult);
            //}

            //if (cer != null)
            //{
            //    handler.ClientCertificates.Add(cer);
            //}

            //TODO:此处 handler并没有被使用到，因此 cer 实际无法传递（这个也是 .net core 目前针对多 cer 场景的一个问题）

            var senparcHttpClient = SenparcHttpClient.GetInstanceByName(certName);
            senparcHttpClient.SetCookie(new Uri(url), cookieContainer);//设置Cookie

            HttpClient client = senparcHttpClient.Client;
            HttpClientHeader(client, refererUrl, useAjax, headerAddition, timeOut);

            #region 处理Form表单文件上传

            var formUploadFile = fileDictionary != null && fileDictionary.Count > 0;//是否用Form上传文件
            if (formUploadFile)
            {
                contentType = "multipart/form-data";

                //通过表单上传文件
                string boundary = "----" + SystemTime.Now.Ticks.ToString("x");

                var multipartFormDataContent = new MultipartFormDataContent(boundary);
                hc = multipartFormDataContent;

                foreach (var file in fileDictionary)
                {
                    try
                    {
                        var fileName = file.Value;
                        //准备文件流
                        using (var fileStream = FileHelper.GetFileStream(fileName))
                        {
                            if (fileStream != null)
                            {
                                //存在文件
                                var memoryStream = new MemoryStream();
                                fileStream.CopyTo(memoryStream);
                                memoryStream.Seek(0, SeekOrigin.Begin);

                                //multipartFormDataContent.Add(new StreamContent(memoryStream), file.Key, Path.GetFileName(fileName)); //报流已关闭的异常

                                multipartFormDataContent.Add(CreateFileContent(memoryStream, file.Key, Path.GetFileName(fileName)), file.Key, Path.GetFileName(fileName));
                                fileStream.Dispose();
                            }
                            else
                            {
                                //不存在文件或只是注释
                                multipartFormDataContent.Add(new StringContent(file.Value), "\"" + file.Key + "\"");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                hc.Headers.ContentType = MediaTypeHeaderValue.Parse(string.Format("multipart/form-data; boundary={0}", boundary));
            }
            else
            {
                if (postStream.Length > 0)
                {
                    if (contentType == HttpClientHelper.DEFAULT_CONTENT_TYPE)
                    {
                        //如果ContentType是默认值，则设置成为二进制流
                        contentType = "application/octet-stream";
                    }

                    //contentType = "application/x-www-form-urlencoded";
                }

                hc = new StreamContent(postStream);

                hc.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                //使用Url格式Form表单Post提交的时候才使用application/x-www-form-urlencoded
                //去掉注释以测试Request.Body为空的情况
                //hc.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            }

            //HttpContentHeader(hc, timeOut);
            #endregion

            if (!string.IsNullOrEmpty(refererUrl))
            {
                client.DefaultRequestHeaders.Referrer = new Uri(refererUrl);
            }

            return client;
        }


        #endregion

        #region 同步方法

        /// <summary>
        /// 使用Post方法获取字符串结果，常规提交
        /// </summary>
        /// <returns></returns>
        public static string HttpPost(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null,
            Encoding encoding = null,
string certName = null,
            bool useAjax = false, Dictionary<string, string> headerAddition = null, int timeOut = OFoodConfig.TIME_OUT,
            bool checkValidationResult = false)
        {
            MemoryStream ms = new MemoryStream();
            formData.FillFormDataStream(ms);//填充formData

            string contentType = HttpClientHelper.GetContentType(formData);

            return HttpPost(url, cookieContainer, ms, null, null, encoding,
certName,
                useAjax, headerAddition, timeOut, checkValidationResult, contentType);
        }

        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="postStream"></param>
        /// <param name="fileDictionary">需要上传的文件，Key：对应要上传的Name，Value：本地文件名</param>
        /// <param name="encoding"></param>
        /// <param name="certName">证书唯一名称，如果不需要则保留null</param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="useAjax"></param>
        /// <param name="headerAddition">header 附加信息</param>
        /// <param name="timeOut"></param>
        /// <param name="checkValidationResult">验证服务器证书回调自动验证</param>
        /// <param name="contentType"></param>
        /// <param name="refererUrl"></param>
        /// <returns></returns>
        public static string HttpPost(string url, CookieContainer cookieContainer = null, Stream postStream = null,
            Dictionary<string, string> fileDictionary = null, string refererUrl = null, Encoding encoding = null,
string certName = null,
            bool useAjax = false, Dictionary<string, string> headerAddition = null, int timeOut = OFoodConfig.TIME_OUT, bool checkValidationResult = false,
            string contentType = HttpClientHelper.DEFAULT_CONTENT_TYPE)
        {
            if (cookieContainer == null)
            {
                cookieContainer = new CookieContainer();
            }

            var senparcResponse = HttpResponsePost(url, cookieContainer, postStream, fileDictionary, refererUrl, encoding,
certName,
                useAjax, headerAddition, timeOut, checkValidationResult, contentType);

            var response = senparcResponse.Result;//获取响应信息


            HttpClientHelper.SetResponseCookieContainer(cookieContainer, response);//设置 Cookie

            //var response = senparcResponse.Result;

            if (response.Content.Headers.ContentType.CharSet != null &&
                response.Content.Headers.ContentType.CharSet.ToLower().Contains("utf8"))
            {
                response.Content.Headers.ContentType.CharSet = "utf-8";
            }

            var retString = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            return retString;
        }


        /// <summary>
        /// 使用Post方法获取HttpWebResponse或HttpResponseMessage对象，本方法独立使用时通常用于测试）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="postStream"></param>
        /// <param name="fileDictionary">需要上传的文件，Key：对应要上传的Name，Value：本地文件名</param>
        /// <param name="encoding"></param>
        /// <param name="certName">证书唯一名称，如果不需要则保留null</param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="useAjax"></param>
        /// <param name="headerAddition">header附加信息</param>
        /// <param name="timeOut"></param>
        /// <param name="checkValidationResult">验证服务器证书回调自动验证</param>
        /// <param name="contentType"></param>
        /// <param name="refererUrl"></param>
        /// <returns></returns>
        public static SenparcHttpResponse HttpResponsePost(string url, CookieContainer cookieContainer = null, Stream postStream = null,
            Dictionary<string, string> fileDictionary = null, string refererUrl = null, Encoding encoding = null,
string certName = null,
            bool useAjax = false, Dictionary<string, string> headerAddition = null, int timeOut = OFoodConfig.TIME_OUT,
            bool checkValidationResult = false, string contentType = HttpClientHelper.DEFAULT_CONTENT_TYPE)
        {
            if (cookieContainer == null)
            {
                cookieContainer = new CookieContainer();
            }

            var postStreamIsDefaultNull = postStream == null;
            if (postStreamIsDefaultNull)
            {
                postStream = new MemoryStream();
            }

            HttpContent hc;
            var client = HttpPost_Common_NetCore(url, out hc, cookieContainer, postStream, fileDictionary, refererUrl, encoding, certName, useAjax, headerAddition, timeOut, checkValidationResult, contentType);

            var response = client.PostAsync(url, hc).ConfigureAwait(false).GetAwaiter().GetResult();//获取响应信息

            HttpClientHelper.SetResponseCookieContainer(cookieContainer, response);//设置 Cookie

            try
            {
                if (postStreamIsDefaultNull && postStream.Length > 0)
                {
                    postStream.Close();
                }

                hc.Dispose();//关闭HttpContent（StreamContent）
            }
            catch (BaseException ex)
            {
            }

            return new SenparcHttpResponse(response);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 使用Post方法获取字符串结果，常规提交
        /// </summary>
        /// <returns></returns>
        public static async Task<string> HttpPostAsync(string url, CookieContainer cookieContainer = null,
            Dictionary<string, string> formData = null, Encoding encoding = null,
string certName = null,
            bool useAjax = false, Dictionary<string, string> headerAddition = null, int timeOut = OFoodConfig.TIME_OUT,
            bool checkValidationResult = false)
        {
            MemoryStream ms = new MemoryStream();
            await formData.FillFormDataStreamAsync(ms).ConfigureAwait(false);//填充formData

            string contentType = HttpClientHelper.GetContentType(formData);

            return await HttpPostAsync(url, cookieContainer, ms, null, null, encoding,
certName,
                useAjax, headerAddition, timeOut, checkValidationResult, contentType).ConfigureAwait(false); ;
        }


        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="postStream"></param>
        /// <param name="fileDictionary">需要上传的文件，Key：对应要上传的Name，Value：本地文件名</param>
        /// <param name="certName">证书唯一名称，如果不需要则保留null</param>
        /// <param name="cer"></param>
        /// <param name="useAjax"></param>
        /// <param name="headerAddition">header附加信息</param>
        /// <param name="timeOut"></param>
        /// <param name="checkValidationResult">验证服务器证书回调自动验证</param>
        /// <param name="contentType"></param>
        /// <param name="refererUrl"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> HttpPostAsync(string url, CookieContainer cookieContainer = null, Stream postStream = null,
            Dictionary<string, string> fileDictionary = null, string refererUrl = null, Encoding encoding = null,
string certName = null,
            bool useAjax = false, Dictionary<string, string> headerAddition = null, int timeOut = OFoodConfig.TIME_OUT, bool checkValidationResult = false,
            string contentType = HttpClientHelper.DEFAULT_CONTENT_TYPE)
        {
            if (cookieContainer == null)
            {
                cookieContainer = new CookieContainer();
            }

            var postStreamIsDefaultNull = postStream == null;
            if (postStreamIsDefaultNull)
            {
                postStream = new MemoryStream();
            }


            //var dt1 = SystemTime.Now;
            //Console.WriteLine($"{System.Threading.Thread.CurrentThread.Name} - START - {dt1:HH:mm:ss.ffff}");

            var senparcResponse = await HttpResponsePostAsync(url, cookieContainer, postStream, fileDictionary, refererUrl, encoding,
certName,
                useAjax, headerAddition, timeOut, checkValidationResult, contentType).ConfigureAwait(false); ;

            var response = senparcResponse.Result;//获取响应信息



            //Console.WriteLine($"{System.Threading.Thread.CurrentThread.Name} - FINISH- {(SystemTime.Now - dt1).TotalMilliseconds:###,###} ms");


            HttpClientHelper.SetResponseCookieContainer(cookieContainer, response);//设置 Cookie


            var retString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return retString;
        }

        /// <summary>
        /// 使用Post方法获取HttpWebResponse或HttpResponseMessage对象，本方法独立使用时通常用于测试）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="postStream"></param>
        /// <param name="fileDictionary">需要上传的文件，Key：对应要上传的Name，Value：本地文件名</param>
        /// <param name="encoding"></param>
        /// <param name="certName">证书唯一名称，如果不需要则保留null</param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="useAjax"></param>
        /// <param name="headerAddition">header附加信息</param>
        /// <param name="timeOut"></param>
        /// <param name="checkValidationResult">验证服务器证书回调自动验证</param>
        /// <param name="contentType"></param>
        /// <param name="refererUrl"></param>
        /// <returns></returns>
        public static async Task<SenparcHttpResponse> HttpResponsePostAsync(string url, CookieContainer cookieContainer = null, Stream postStream = null,
            Dictionary<string, string> fileDictionary = null, string refererUrl = null, Encoding encoding = null,
string certName = null,
            bool useAjax = false, Dictionary<string, string> headerAddition = null, int timeOut = OFoodConfig.TIME_OUT,
            bool checkValidationResult = false, string contentType = HttpClientHelper.DEFAULT_CONTENT_TYPE)
        {
            if (cookieContainer == null)
            {
                cookieContainer = new CookieContainer();
            }

            var postStreamIsDefaultNull = postStream == null;
            if (postStreamIsDefaultNull)
            {
                postStream = new MemoryStream();
            }

            HttpContent hc;
            var client = HttpPost_Common_NetCore(url, out hc, cookieContainer, postStream, fileDictionary, refererUrl, encoding, certName, useAjax, headerAddition, timeOut, checkValidationResult, contentType);

            var response = await client.PostAsync(url, hc).ConfigureAwait(false);//获取响应信息

            HttpClientHelper.SetResponseCookieContainer(cookieContainer, response);//设置 Cookie

            try
            {
                if (postStreamIsDefaultNull && postStream.Length > 0)
                {
                    postStream.Close();
                }

                hc.Dispose();//关闭HttpContent（StreamContent）
            }
            catch (BaseException ex)
            {
            }

            return new SenparcHttpResponse(response);
        }


        #endregion

    }
}
