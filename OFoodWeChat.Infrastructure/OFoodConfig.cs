/*----------------------------------------------------------------
    文件名：Config.cs
    文件功能描述：全局配置文件
   
----------------------------------------------------------------*/
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace OFoodWeChat.Infrastructure
{
    /// <summary>
    /// 全局设置
    /// </summary>
    public class OFoodConfig
    {
        /// <summary>
        /// APM 信息自动过期时间
        /// </summary>
        public static TimeSpan DataExpire = TimeSpan.FromMinutes(20);//20分钟

        /// <summary>
        /// 启用 APM
        /// </summary>
        public static bool EnableAPM = true;
        /// <summary>
        /// <para>全局配置</para>
        /// <para>在 startup.cs 中运行 RegisterServiceExtension.AddSenparcGlobalServices() 即可自动注入</para>
        /// </summary>
        public static OFoodSetting Setting { get; set; } = new OFoodSetting();//TODO:需要考虑分布式的情况，后期需要储存在缓存中

        /// <summary>
        /// 指定是否是Debug状态，如果是，系统会自动输出日志
        /// </summary>
        public static bool IsDebug
        {
            get
            {
                return Setting.IsDebug;
            }
            set
            {
                Setting.IsDebug = value;

                //if (_isDebug)
                //{
                //    SenparcTrace.Open();
                //}
                //else
                //{
                //    SenparcTrace.Close();
                //}
            }
        }

        /// <summary>
        /// 请求超时设置（以毫秒为单位），默认为10秒。
        /// 说明：此处常量专为提供给方法的参数的默认值，不是方法内所有请求的默认超时时间。
        /// </summary>
        public const int TIME_OUT = 10000;

        /// <summary>
        /// JavaScriptSerializer 类接受的 JSON 字符串的最大长度
        /// </summary>
        public static int MaxJsonLength = int.MaxValue;//TODO:需要考虑分布式的情况，后期需要储存在缓存中


        /// <summary>
        /// 默认缓存键的第一级命名空间，默认值：DefaultCache
        /// </summary>
        public static string DefaultCacheNamespace
        {
            get
            {
                return Setting.DefaultCacheNamespace ?? "DefaultCache";
            }
            set
            {
                Setting.DefaultCacheNamespace = value;
            }
        }

        private static string _rootDictionaryPath = null;

        /// <summary>
        /// 网站根目录绝对路径
        /// </summary>
        public static string RootDictionaryPath
        {
            get
            {
                if (_rootDictionaryPath==null)
                {
                    _rootDictionaryPath = AppContext.BaseDirectory;
                }

                return _rootDictionaryPath;
            }
            set
            {
                _rootDictionaryPath = value;
            }
        }
    }
}
