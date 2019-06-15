/*----------------------------------------------------------------
    文件名：Config.cs
    文件功能描述：全局设置

----------------------------------------------------------------*/

using System;
using OFoodWeChat.Core.Entities;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Infrastructure.Settings;
using OFoodWeChat.Infrastructure.Enums;

namespace OFoodWeChat.Core
{
    /// <summary>
    /// OFoodWeChat.Weixin 全局设置
    /// </summary>
    public static class WxConfig
    {
        /// <summary>
        /// <para>指定是否是Debug状态，如果是，系统会自动输出日志。</para>
        /// <para>如果 OFoodConfig.IsDebug 为 true，则此参数也会为 true，否则以此参数为准。</para>
        /// </summary>
        public static bool IsDebug
        {
            get { return OFoodConfig.IsDebug || WeixinSetting.IsDebug; }
            set { WeixinSetting.IsDebug = value; }
        }
        /// <summary>
        /// <para>微信全局配置</para>
        /// <para>注意：在程序运行过程中修改 WeixinSetting.Items 中的微信配置值，并不能修改 Container 中的对应信息（如AppSecret），</para>
        /// <para>如果需要修改微信信息（如AppSecret）应该使用 xxContainer.Register() 修改，这里的值也会随之更新。</para>
        /// </summary>
        public static WeixinSetting WeixinSetting { get; set; }

        /// <summary>
        /// 微信支付使用沙箱模式（默认为false）
        /// </summary>
        public static bool UseSandBoxPay { get; set; }

        /// <summary>
        /// 请求超时设置（以毫秒为单位），默认为10秒。
        /// 说明：此处常量专为提供给方法的参数的默认值，不是方法内所有请求的默认超时时间。
        /// </summary>
        public const int TIME_OUT = OFoodConfig.TIME_OUT;

        /// <summary>
        /// 网站根目录绝对路径
        /// </summary>
        public static string RootDictionaryPath
        {
            get { return OFoodConfig.RootDictionaryPath; }
            set { OFoodConfig.RootDictionaryPath = value; }
        }

        /// <summary>
        /// 默认缓存键的第一级命名空间，默认值：DefaultCache
        /// </summary>
        public static string DefaultCacheNamespace
        {
            get { return OFoodConfig.DefaultCacheNamespace; }
            set { OFoodConfig.DefaultCacheNamespace = value; }
        }

        #region API地址（前缀）设置

        #region  公众号（小程序）、开放平台 API 的服务器地址（默认为：https://api.weixin.qq.com）

        /// <summary>
        /// 公众号（小程序）、开放平台 API 的服务器地址（默认为：https://api.weixin.qq.com）
        /// </summary>
        private static string _apiMpHost = "https://api.weixin.qq.com";
        /// <summary>
        /// 公众号（小程序）、开放平台 API 的服务器地址（默认为：https://api.weixin.qq.com）
        /// </summary>
        public static string ApiMpHost
        {
            get { return _apiMpHost; }
            set { _apiMpHost = value; }
        }

        /// <summary>
        /// 公众号（小程序）、开放平台【文件下载】 API 的服务器地址（默认为：https://api.weixin.qq.com）
        /// </summary>
        private static string _apiMpFileHost = "http://file.api.weixin.qq.com";
        /// <summary>
        /// 公众号（小程序）、开放平台【文件下载】 API 的服务器地址（默认为：http://file.api.weixin.qq.com）
        /// </summary>
        public static string ApiMpFileHost
        {
            get { return _apiMpFileHost; }
            set { _apiMpFileHost = value; }
        }
        #endregion

        #region 企业微信API的服务器地址（默认为：https://qyapi.weixin.qq.com）

        /// <summary>
        /// 企业微信API的服务器地址（默认为：https://qyapi.weixin.qq.com）
        /// </summary>
        private static string _apiWorkHost = "https://qyapi.weixin.qq.com";
        /// <summary>
        /// 企业微信API的服务器地址（默认为：https://qyapi.weixin.qq.com）
        /// </summary>
        public static string ApiWorkHost
        {
            get { return _apiWorkHost; }
            set { _apiWorkHost = value; }
        }

        #endregion

        #endregion

        /// <summary>
        /// 默认的AppId检查规则
        /// </summary>
        public static Func<string, PlatformType, bool> DefaultAppIdCheckFunc = (accessTokenOrAppId, platFormType) =>
        {
            if (platFormType == PlatformType.WeChat_Work)
            {
                /*
                 * 企业号（企业微信）AppKey（Length=84）：wx7618c00000000222@044ZI5s6-ACxpAuOcm4md410pZ460pQUmxO9hIoMd09kRaJ1iSqhPfmg3-aBFF7q
                 * 企业号（企业微信）AccessToken（length=300）：MGelzm_P0N-41qH3PwHsNxp70rdVuB0SMEN7dE4E8eKpb0OpNQSp8jPUfgwIL_P9jcz-qGIOLbLEy3d8XQEJFfZtOLgTJqyg0rJbj6WyQJxdRVjbLnHr0-pg7oN9dD1NFI7-T7GLuJER3Pun-5cSiSmZgAegTDhXKZC8XfgjQAPPYLjZl7StBnO7dVcZStdyivZ92zq4PrDdNif9fa2p9lPSLqkur2PpDB9P7MsR8PDJWsKghEcmjB41OXohHGnqPWd5lUZaV1Y8p35BVz6PqjF-90UgAjI9IohVKVRClks
                 */

                //return accessTokenOrAppId != null && accessTokenOrAppId.Length < 256;

                /*
                 * 2017年9月26日开始，AccessToken长度有变化（长度有300、215、191等）
                 * AccessToken（Length=215）：_0evr6HbAnWCUfn1tRpbVY2uV63fDOfT-fUnpQcq6egl8bYFp3Xq45ebImXn5Aj1_nz_mFCUz9sDnoEkfy-jyXqJEc4Hty0BAo2VQTB8ogx7qkL2w1p0H2E1fKWwJrQ1285V0XhEQ0pcHMLwy9RbHuD4sHdAJ5ZkXGchNQ1eHsmseoBxucKvyAnEq9psJVLMjkU4G3ZRa0NoTBSy0g6ujg
                 */

                return accessTokenOrAppId != null && accessTokenOrAppId.Contains("@");
            }
            else
            {
                /*
                 * 公众号AppId：wxe273c3a02e09ff8c
                 * 公众号AccessToken：ga0wJ5ZmdB1Ef1gMMxmps6Uz1a9TXoutQtRqgYTbIqHfTm4Ssfoj0DjMLp1_KkG7FkaqS7m7f9rrYbqBQMBizRBQjHFG5ZIov8Wb0FBnHDq5fGpCu0S2H2j2aM8c6KDqGGEiAIAJJH
                 */
                return accessTokenOrAppId != null && accessTokenOrAppId.Length <= 32 /*wxc3c90837b0e76080*/
                ;
            }
        };

        static WxConfig()
        {
            WeixinSetting = new WeixinSetting();//提供默认实例
        }
    }
}
