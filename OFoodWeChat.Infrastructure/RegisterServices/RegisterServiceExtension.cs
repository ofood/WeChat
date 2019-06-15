﻿/*----------------------------------------------------------------
    文件名：RegisterService.cs
    文件功能描述：Senparc.Weixin SDK 快捷注册流程


    创建标识：Senparc - 20180222

    修改标识：Senparc - 20180531
    修改描述：v4.22.2 修改 AddSenparcWeixinGlobalServices() 方法命名
    
    ----  CO2NET   ----

    修改标识：Senparc - 20180704
    修改描述：v0.1.5 RegisterServiceExtension.AddSenparcGlobalServices() 方法可自动获取 SenparcSetting 全局设置

    修改标识：Senparc - 20190429
    修改描述：v0.7.0 优化 HttpClient，重构 RequestUtility（包括 Post 和 Get），引入 HttpClientFactory 机制

    修改标识：Senparc - 20190521
    修改描述：v0.7.3 .NET Core 提供多证书注册功能

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;

using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OFoodWeChat.Infrastructure.Trace;
using OFoodWeChat.Infrastructure.Http;

namespace OFoodWeChat.Infrastructure.RegisterServices
{
    /// <summary>
    /// 快捷注册类，RegisterService 扩展类
    /// </summary>
    public static class RegisterServiceExtension
    {
        /// <summary>
        /// 注册 IServiceCollection，并返回 RegisterService，开始注册流程（必须）
        /// </summary>
        /// <param name="serviceCollection">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns></returns>
        public static IServiceCollection AddSenparcGlobalServices(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            OFoodDI.GlobalServiceCollection = serviceCollection;
            serviceCollection.Configure<OFoodSetting>(configuration.GetSection("SenparcSetting"));

            //配置 HttpClient，可使用 Head 自定义 Cookie
            
            serviceCollection.AddHttpClient<SenparcHttpClient>()
            //.ConfigureHttpMessageHandlerBuilder((c) =>
            .ConfigurePrimaryHttpMessageHandler((c) =>
            {
                var httpClientHandler = HttpClientHelper.GetHttpClientHandler(null, RequestUtility.SenparcHttpClientWebProxy, System.Net.DecompressionMethods.GZip);
                return httpClientHandler;
            });

            /*
             * appsettings.json 中添加节点：
 //CO2NET 设置
  "OFoodSetting": {
    "IsDebug": true,
    "DefaultCacheNamespace": "DefaultCache"
  },
             */

            return serviceCollection;
        }

        /// <summary>
        /// 注册 IServiceCollection，并返回 RegisterService，开始注册流程（必须）
        /// </summary>
        /// <param name="serviceCollection">IServiceCollection</param>
        /// <param name="certName">证书名称，必须全局唯一，并且确保在全局 HttpClientFactory 内唯一</param>
        /// <param name="certSecret">证书密码</param>
        /// <param name="certPath">证书路径（物理路径）</param>
        /// <param name="checkValidationResult">设置</param>
        /// <returns></returns>
        public static IServiceCollection AddSenparcHttpClientWithCertificate(this IServiceCollection serviceCollection,
            string certName, string certSecret, string certPath, bool checkValidationResult = false)
        {
            //添加注册
            if (!string.IsNullOrEmpty(certPath))
            {
                if (File.Exists(certPath))
                {
                    try
                    {
                        var cert = new X509Certificate2(certPath, certSecret, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                        return AddSenparcHttpClientWithCertificate(serviceCollection, certName, cert, checkValidationResult);
                    }
                    catch (Exception ex)
                    {
                        LogTrace.SendCustomLog($"添加微信支付证书发生异常", $"certName:{certName},certPath:{certPath}");
                        LogTrace.BaseExceptionLog(ex);
                        return serviceCollection;
                    }
                }
                else
                {
                    LogTrace.SendCustomLog($"已设置微信支付证书，但无法找到文件", $"certName:{certName},certPath:{certPath}");
                    return serviceCollection;
                }
            }
            return serviceCollection;
        }

        /// <summary>
        /// 注册 IServiceCollection，并返回 RegisterService，开始注册流程（必须）
        /// </summary>
        /// <param name="serviceCollection">IServiceCollection</param>
        /// <param name="certName">证书名称，必须全局唯一，并且确保在全局 HttpClientFactory 内唯一</param>
        /// <param name="cert">证书对象，也可以是 X509Certificate2</param>
        /// <param name="checkValidationResult">设置</param>
        /// <returns></returns>
        public static IServiceCollection AddSenparcHttpClientWithCertificate(this IServiceCollection serviceCollection,
            string certName, X509Certificate cert, bool checkValidationResult = false)
        {
            serviceCollection.AddHttpClient<SenparcHttpClient>(certName)
                         .ConfigurePrimaryHttpMessageHandler(() =>
                         {
                             var httpClientHandler = HttpClientHelper.GetHttpClientHandler(null, RequestUtility.SenparcHttpClientWebProxy, System.Net.DecompressionMethods.GZip);

                             httpClientHandler.ClientCertificates.Add(cert);

                             if (checkValidationResult)
                             {
                                 httpClientHandler.ServerCertificateCustomValidationCallback = new Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>(RequestUtility.CheckValidationResult);
                             }

                             return httpClientHandler;
                         });
             
            OFoodDI.ResetGlobalIServiceProvider();//重置 GlobalIServiceProvider
            return serviceCollection;
        }

        /// <summary>
        /// 添加作用于 SenparcHttpClient 的 WebProxy（需要在 AddSenparcGlobalServices 之前定义）
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static IServiceCollection AddSenparcHttpClientProxy(this IServiceCollection serviceCollection, string host, string port, string username, string password)
        {
            RequestUtility.SetHttpProxy(host, port, username, password);

            return serviceCollection;
        }

    }
}
