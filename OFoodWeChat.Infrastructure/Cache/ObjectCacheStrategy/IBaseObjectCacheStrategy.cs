using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OFoodWeChat.Infrastructure.Cache
{
    /// <summary>
    /// 所有以 string 类型为 key ，object 为 value 的缓存策略接口
    /// </summary>
    public interface IBaseObjectCacheStrategy : IBaseCacheStrategy<string, object>, IBaseCacheStrategy
    {
        //IContainerCacheStrategy ContainerCacheStrategy { get; }

        /// <summary>
        /// 注册的扩展缓存策略
        /// </summary>
        //Dictionary<IExtensionCacheStrategy, IBaseObjectCacheStrategy> ExtensionCacheStratety { get; set; }
    }
}
