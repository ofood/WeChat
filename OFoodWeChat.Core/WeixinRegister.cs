﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：WeixinRegister.cs
    文件功能描述：Senparc.Weixin 快捷注册流程（包括Thread、TraceLog等）
    

    修改标识：Senparc - 20180707
    修改描述：v5.0.8.4 优化 WeixinRegister.UseSenparcWeixin() 提供 autoScanExtensionCacheStrategies 参数，
              可设置是否全局扫描扩展缓存（扫描会增加系统启动时间）
    修改描述：v5.0.10 UseSenparcWeixin() 添加 SenparcSetting 参数

----------------------------------------------------------------*/

using Microsoft.Extensions.Options;

using OFoodWeChat.Infrastructure;
using OFoodWeChat.Infrastructure.Cache;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Infrastructure.Helpers;
using OFoodWeChat.Infrastructure.RegisterServices;
using OFoodWeChat.Core.Cache;
using OFoodWeChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using OFoodWeChat.Infrastructure.Settings;

namespace OFoodWeChat.Core
{
    /// <summary>
    /// 微信注册
    /// </summary>
    public static class WeixinRegister
    {
        /// <summary>
        /// 开始 Senparc.Weixin SDK 初始化参数流程
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="senparcWeixinSetting">微信全局设置参数，必填</param>
        /// <param name="senparcSetting">用于提供 SenparcSetting.Cache_Redis_Configuration 和 Cache_Memcached_Configuration 两个参数，如果不使用这两种分布式缓存可传入null</param>
        /// <returns></returns>
        public static IRegisterService UseSenparcWeixin(this IRegisterService registerService, WeixinSetting senparcWeixinSetting, OFoodSetting senparcSetting = null)
        {
            senparcWeixinSetting = senparcWeixinSetting ?? new WeixinSetting();
            senparcSetting = senparcSetting ?? new OFoodSetting();

            //Senparc.Weixin SDK 配置
            WxConfig.WeixinSetting = senparcWeixinSetting;

            /* 扩展缓存注册开始 */
            var cacheTypes = "";//所有注册的扩展缓存

            // 微信的 本地 缓存
            var cache = LocalContainerCacheStrategy.Instance;//只要引用就可以被激活
            cacheTypes += typeof(LocalContainerCacheStrategy);

            var dt1 = SystemTime.Now;

            //官方扩展缓存注册

            //var officialTypes = new List<Type>() { typeof(LocalContainerCacheStrategy) };//官方提供的扩展缓存策略

            //自动注册 Redis 和 Memcached
            //Redis
            var redisConfiguration = senparcSetting.Cache_Redis_Configuration;
            if (!string.IsNullOrEmpty(redisConfiguration) &&
                /*缓存配置默认值，不启用*/
                redisConfiguration != "Redis配置" &&
                redisConfiguration != "#{Cache_Redis_Configuration}#")
            {
                try
                {
                    var redisInstance = ReflectionHelper.GetStaticMember("Senparc.Weixin.Cache.Redis", "Senparc.Weixin.Cache.Redis", "RedisContainerCacheStrategy", "Instance");
                    if (redisInstance != null)
                    {
                        //officialTypes.Add(redisInstance.GetType());
                        cacheTypes += "\r\n" + redisInstance.GetType();
                    }
                }
                catch (Exception ex)
                {
                    WeixinTrace.WeixinExceptionLog(new Exceptions.WeixinException(ex.Message, ex));
                }
            }

            //Memcached
            var memcachedConfiguration = senparcSetting.Cache_Memcached_Configuration;
            if (!string.IsNullOrEmpty(memcachedConfiguration) &&
                /*缓存配置默认值，不启用*/
                memcachedConfiguration != "Memcached配置" &&
                memcachedConfiguration != "#{Cache_Memcached_Configuration}#")
            {
                try
                {
                    var memcachedInstance = ReflectionHelper.GetStaticMember("Senparc.Weixin.Cache.Memcached", "Senparc.Weixin.Cache.Memcached", "MemcachedContainerCacheStrategy", "Instance");
                    if (memcachedInstance != null)
                    {
                        //officialTypes.Add(memcachedInstance.GetType());
                        cacheTypes += "\r\n" + memcachedInstance.GetType();
                    }
                }
                catch (Exception ex)
                {
                    WeixinTrace.WeixinExceptionLog(new Exceptions.WeixinException(ex.Message, ex));
                }
            }

            var dt2 = SystemTime.Now;
            var exCacheLog = "微信扩展缓存注册总用时：{0}ms\r\n扩展缓存：{1}".FormatWith((dt2 - dt1).TotalMilliseconds, cacheTypes);
            WeixinTrace.SendCustomLog("微信扩展缓存注册完成", exCacheLog);

            /* 扩展缓存注册结束 */

            //ApiBind 自动扫描
            Register.RegisterApiBind(false);

            return registerService;
        }
    }
}
