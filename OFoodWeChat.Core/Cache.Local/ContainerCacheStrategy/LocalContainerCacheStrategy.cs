/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：LocalContainerCacheStrategy.cs
    文件功能描述：本地容器缓存。


    创建标识：Senparc - 20160308

    修改标识：Senparc - 20160812
    修改描述：v4.7.4  解决Container无法注册的问题

    修改标识：Senparc - 20161024
    修改描述：v4.8.3  废除LocalCacheHelper类，将LocalObjectCacheStrategy做为基类

 ----------------------------------------------------------------*/

using System.Collections.Generic;
using OFoodWeChat.Core.Containers;
using System;
using OFoodWeChat.Infrastructure.Cache;

namespace OFoodWeChat.Core.Cache
{
    
    /// <summary>
    /// 本地容器缓存策略
    /// </summary>
    public sealed class LocalContainerCacheStrategy : BaseContainerCacheStrategy
    {
        #region IDomainExtensionCacheStrategy 成员
        public override ICacheStrategyDomain CacheStrategyDomain { get { return ContainerCacheStrategyDomain.Instance; } }

        #endregion

        #region 单例

        /// <summary>
        /// LocalCacheStrategy的构造函数
        /// </summary>
        private LocalContainerCacheStrategy() /*: base()*/
        {
            //使用底层缓存策略
            BaseCacheStrategy = () => LocalObjectCacheStrategy.Instance;

            //向底层缓存注册当前缓存策略
            base.RegisterCacheStrategyDomain(this);
        }

        //静态LocalCacheStrategy
        public static IContainerCacheStrategy Instance
        {
            get
            {
                return Nested.instance;//返回Nested类中的静态成员instance
            }
        }

        class Nested
        {
            static Nested()
            {
            }
            //将instance设为一个初始化的LocalCacheStrategy新实例
            internal static readonly LocalContainerCacheStrategy instance = new LocalContainerCacheStrategy();
        }

        #endregion

        #region BaseContainerCacheStrategy 成员


        public override IDictionary<string, TBag> GetAll<TBag>()
        {
            var dic = new Dictionary<string, TBag>();
            var baseCacheStrategy = BaseCacheStrategy();
            var cacheList = baseCacheStrategy.GetAll();
            foreach (var baseContainerBag in cacheList)
            {
                if (baseContainerBag.Value is TBag)
                {
                    dic[baseContainerBag.Key] = (TBag)baseContainerBag.Value;
                }
            }
            return dic;
        }


        #endregion


        
    }
}
