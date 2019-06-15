/*----------------------------------------------------------------   
    文件名：MutipleCacheTestHelper.cs
    文件功能描述：多种缓存测试帮助类

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using OFoodWeChat.Infrastructure.Cache;
using OFoodWeChat.Infrastructure.Enums;

namespace OFoodWeChat.Infrastructure.Helpers
{
    /// <summary>
    /// 多种缓存测试帮助类
    /// </summary>
    public class MutipleCacheTestHelper
    {
        /// <summary>
        /// 测试多种缓存
        /// </summary>
        public static void RunMutipleCache(Action action)
        {
            RunMutipleCache(action, CacheType.Local);
        }

        /// <summary>
        /// 遍历使用多种缓存测试同一个过程（委托），确保不同的缓存策略行为一致
        /// </summary>
        public static void RunMutipleCache(Action action, params CacheType[] cacheTypes)
        {
            List<IBaseObjectCacheStrategy> cacheStrategies = new List<IBaseObjectCacheStrategy>();

            foreach (var cacheType in cacheTypes)
            {
                var assabmleName = cacheType == CacheType.Local
                    ? "OFoodWeChat.Infrastructure"
                    : "OFoodWeChat.Infrastructure.Cache." + cacheType.ToString();

                var nameSpace = cacheType == CacheType.Local
                                    ? "OFoodWeChat.Infrastructure.Cache"
                                    : "OFoodWeChat.Infrastructure.Cache." + cacheType.ToString();

                var className = cacheType.ToString() + "ObjectCacheStrategy";


                var cacheInstance = ReflectionHelper.GetStaticMember(assabmleName, nameSpace,
                    className, "Instance"/*获取单例的属性*/) as IBaseObjectCacheStrategy;

                cacheStrategies.Add(cacheInstance);

                //switch (cacheType)
                //{
                //    case CacheType.Local:
                //        cacheStrategies.Add(LocalObjectCacheStrategy.Instance);
                //        break;
                //    case CacheType.Redis:
                //        cacheStrategies.Add(RedisObjectCacheStrategy.Instance);
                //        break;
                //    case CacheType.Memcached:
                //        cacheStrategies.Add(MemcachedObjectCacheStrategy.Instance);
                //        break;
                //}
            }

            foreach (var objectCacheStrategy in cacheStrategies)
            {
                //原始缓存策越
                var originalCache = CacheStrategyFactory.GetObjectCacheStrategyInstance();

                Console.WriteLine("== 使用缓存策略：" + objectCacheStrategy.GetType().Name + " 开始 == ");

                //使用当前缓存策略
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => objectCacheStrategy);

                try
                {
                    action();//执行
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                Console.WriteLine("== 使用缓存策略：" + objectCacheStrategy.GetType().Name + " 结束 == \r\n");

                //还原缓存策略
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => originalCache);
            }
        }
    }
}
