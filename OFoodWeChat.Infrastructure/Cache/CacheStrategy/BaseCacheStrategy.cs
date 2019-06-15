﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：BaseCacheStrategy.cs
    文件功能描述：泛型缓存策略基类。


    创建标识：Senparc - 20160813 v4.7.7 


    --CO2NET--

    修改标识：Senparc - 20180606
    修改描述：GetFinalKey() 方法添加虚方法关键字

 ----------------------------------------------------------------*/


using System;
using System.Threading.Tasks;

namespace OFoodWeChat.Infrastructure.Cache
{
    /// <summary>
    /// 泛型缓存策略基类
    /// </summary>
    public abstract class BaseCacheStrategy : IBaseCacheStrategy
    {
        ///// <summary>
        ///// 默认下级命名空间
        ///// </summary>
        //public virtual string ChildNamespace { get; set; }

        /// <summary>
        /// 获取拼装后的FinalKey
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="isFullKey">是否已经是经过拼接的FullKey</param>
        /// <returns></returns>
        public virtual string GetFinalKey(string key, bool isFullKey = false)
        {
            return isFullKey ? key : $"Senparc:{OFoodConfig.DefaultCacheNamespace}:{key}";
        }

        /// <summary>
        /// 获取一个同步锁
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="key"></param>
        /// <param name="retryCount"></param>
        /// <param name="retryDelay"></param>
        /// <returns></returns>
        public abstract ICacheLock BeginCacheLock(string resourceName, string key, int retryCount = 0, TimeSpan retryDelay = new TimeSpan());



        /// <summary>
        /// 【异步方法】创建一个（分布）锁
        /// </summary>
        /// <param name="resourceName">资源名称</param>
        /// <param name="key">Key标识</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="retryDelay">重试延时</param>
        /// <returns></returns>
        public abstract Task<ICacheLock> BeginCacheLockAsync(string resourceName, string key, int retryCount = 0, TimeSpan retryDelay = new TimeSpan());

    }
}
