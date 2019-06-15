using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OFoodWeChat.Infrastructure.Cache;
using OFoodWeChat.Core.Containers;

namespace OFoodWeChat.Core.Cache
{
    public class ContainerCacheStrategyFactory
    {
        //internal static Func<IContainerCacheStrategy> ContainerCacheStrateFunc;

        //internal static Func<IContainerCacheStrategy> ObjectCacheStrateFunc = () => LocalContainerCacheStrategy.Instance;//默认为 WeixinLocalObjectCacheStrategy
        //internal static IBaseCacheStrategy<TKey, TValue> GetContainerCacheStrategy<TKey, TValue>()
        //    where TKey : class
        //    where TValue : class
        //{
        //    return;
        //}

        ///// <summary>
        ///// 注册当前全局环境下的缓存策略
        ///// </summary>
        ///// <param name="func">如果为null，将使用默认的本地缓存策略（LocalObjectCacheStrategy.Instance）</param>
        //public static void RegisterContainerCacheStrategy(Func<IContainerCacheStrategy> func)
        //{
        //    ObjectCacheStrateFunc = func;

        //    //TODO:如果不考虑效率，此方法可以使用反射，自动注册所有的扩展缓存
        //}

        /// <summary>
        /// 如果
        /// </summary>
        /// <returns></returns>
        public static IContainerCacheStrategy GetContainerCacheStrategyInstance()
        {
            //从底层进行判断
            var containerCacheStrategy = CacheStrategyFactory.GetExtensionCacheStrategyInstance(ContainerCacheStrategyDomain.Instance)
                                            as IContainerCacheStrategy;
            return containerCacheStrategy;


            //if (ObjectCacheStrateFunc == null)
            //{
            //    //默认状态
            //    return LocalContainerCacheStrategy.Instance /*as IContainerCacheStrategy*/;
            //}
            //else
            //{
            //    //自定义类型
            //    var instance = ObjectCacheStrateFunc();// ?? LocalObjectCacheStrategy.Instance;
            //    return instance;
            //}
        }
    }
}
