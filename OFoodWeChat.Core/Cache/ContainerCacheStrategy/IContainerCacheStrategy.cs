/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：IContainerCacheStrategy.cs
    文件功能描述：容器缓存策略基类。


    创建标识：Senparc - 20160308

    修改标识：Senparc - 20160812
    修改描述：v4.7.4  解决Container无法注册的问题

 ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using OFoodWeChat.Core.Containers;
using OFoodWeChat.Infrastructure.Cache;
namespace OFoodWeChat.Core.Cache
{
    /// <summary>
    /// 容器缓存策略接口（属于扩展领域缓存）
    /// </summary>
    public interface IContainerCacheStrategy : IDomainExtensionCacheStrategy /*: IBaseCacheStrategy<string, IBaseContainerBag>*/
    {
        /// <summary>
        /// 获取所有ContainerBag
        /// </summary>
        /// <typeparam name="TBag"></typeparam>
        /// <returns></returns>
        IDictionary<string, TBag> GetAll<TBag>() where TBag : IBaseContainerBag;

        /// <summary>
        /// 获取单个ContainerBag
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="isFullKey">是否已经是完整的Key，如果不是，则会调用一次GetFinalKey()方法</param>
        /// <returns></returns>
        TBag GetContainerBag<TBag>(string key, bool isFullKey = false) where TBag : IBaseContainerBag;

        /// <summary>
        /// 更新ContainerBag
        /// </summary>
        /// <param name="key"></param>
        /// <param name="containerBag"></param>
        /// <param name="expiry">超时时间</param>
        /// <param name="isFullKey">是否已经是完整的Key，如果不是，则会调用一次GetFinalKey()方法</param>
        void UpdateContainerBag(string key, IBaseContainerBag containerBag, TimeSpan? expiry = null, bool isFullKey = false);
    }
}
