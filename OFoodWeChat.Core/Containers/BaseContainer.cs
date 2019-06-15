/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：WeixinContainer.cs
    文件功能描述：微信容器（如Ticket、AccessToken）
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using OFoodWeChat.Infrastructure.Cache;
using OFoodWeChat.Core.Cache;
using OFoodWeChat.Core.Exceptions;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Core.Utilities;

namespace OFoodWeChat.Core.Containers
{
    /// <summary>
    /// IBaseContainer
    /// </summary>
    public interface IBaseContainer
    {
    }

    /// <summary>
    /// 带IBaseContainerBag泛型的IBaseContainer
    /// </summary>
    /// <typeparam name="TBag"></typeparam>
    public interface IBaseContainer<TBag> : IBaseContainer where TBag : IBaseContainerBag, new()
    {
    }

    /// <summary>
    /// 微信容器接口（如Ticket、AccessToken）
    /// </summary>
    /// <typeparam name="TBag"></typeparam>
    [Serializable]
    public abstract class BaseContainer<TBag> : IBaseContainer<TBag> where TBag : class, IBaseContainerBag, new()
    {
        private static IBaseObjectCacheStrategy _baseCache = null;
        private static IContainerCacheStrategy _containerCache = null;

        /// <summary>
        /// 获取符合当前缓存策略配置的缓存的操作对象实例
        /// </summary>
        protected static IBaseObjectCacheStrategy /*IBaseCacheStrategy<string,Dictionary<string, TBag>>*/ Cache
        {
            get
            {
                //使用工厂模式或者配置进行动态加载
                //return CacheStrategyFactory.GetContainerCacheStrategyInstance();

                //以下代码可以实现缓存“热切换”，损失的效率有限。如果需要追求极致效率，可以禁用type的判断
                var containerCacheStrategy = ContainerCacheStrategyFactory.GetContainerCacheStrategyInstance()/*.ContainerCacheStrategy*/;
                if (_containerCache == null || _containerCache.GetType() != containerCacheStrategy.GetType())
                {
                    _containerCache = containerCacheStrategy;
                }

                if (_baseCache == null)
                {
                    _baseCache = _baseCache ?? containerCacheStrategy.BaseCacheStrategy();
                }
                return _baseCache;
            }
        }

        /// <summary>
        /// 进行注册过程的委托
        /// </summary>
        protected static Func<TBag> RegisterFunc { get; set; }

        /// <summary>
        /// 如果注册不成功，测尝试重新注册（前提是已经进行过注册），这种情况适用于分布式缓存被清空（重启）的情况。
        /// </summary>
        private static TBag TryReRegister()
        {
            return RegisterFunc();
            //TODO:如果需要校验ContainerBag的正确性，可以从返回值进行判断
        }

        /// <summary>
        /// 返回已经注册的第一个AppId
        /// </summary>
        /// <returns></returns>
        public static string GetFirstOrDefaultAppId(PlatformType platformType)
        {
            string appId = null;
            switch (platformType)
            {
                case PlatformType.WeChat_OfficialAccount:
                    appId = WxConfig.WeixinSetting.WeixinAppId;
                    break;
                case PlatformType.WeChat_Open:
                    appId = WxConfig.WeixinSetting.WeixinAppId;
                    break;
                case PlatformType.WeChat_MiniProgram:
                    appId = WxConfig.WeixinSetting.WxOpenAppId;
                    break;
                case PlatformType.WeChat_Work:
                    break;
                default:
                    break;
            }

            if (appId == null)
            {
                var firstBag = GetAllItems().FirstOrDefault() as IBaseContainerBag_AppId;
                appId = firstBag == null ? null : firstBag.AppId;
            }

            return appId;
        }

        /// <summary>
        /// 获取ItemCollection缓存Key
        /// </summary>
        /// <param name="shortKey">最简短的Key，比如AppId，不需要考虑容器前缀</param>
        /// <returns></returns>
        public static string GetBagCacheKey(string shortKey)
        {
            return ContainerHelper.GetItemCacheKey(typeof(TBag), shortKey);
        }
        /// <summary>
        /// 获取所有容器内已经注册的项目
        /// （此方法将会遍历Dictionary，当数据项很多的时候效率会明显降低）
        /// </summary>
        /// <returns></returns>
        public static List<TBag> GetAllItems()
        {
            //return Cache.GetAll<TBag>().Values
            return _containerCache.GetAll<TBag>().Values
                //如果需要做进一步的筛选，则使用Select或Where，但需要注意效率问题
                //.Select(z => z)
                .ToList();
        }

        /// <summary>
        /// 尝试获取某一项Bag
        /// </summary>
        /// <param name="shortKey"></param>
        /// <returns></returns>
        public static TBag TryGetItem(string shortKey)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            if (Cache.CheckExisted(cacheKey))
            {
                return Cache.Get<TBag>(cacheKey);
            }

            return default(TBag);
        }

        /// <summary>
        /// 尝试获取某一项Bag中的具体某个属性
        /// </summary>
        /// <param name="shortKey"></param>
        /// <param name="property">具体某个属性</param>
        /// <returns></returns>
        public static TK TryGetItem<TK>(string shortKey, Func<TBag, TK> property)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            if (Cache.CheckExisted(cacheKey))
            {
                var item = Cache.Get<TBag>(cacheKey);
                return property(item);
            }
            return default(TK);
        }

        /// <summary>
        /// 更新数据项
        /// </summary>
        /// <param name="shortKey">即bag.Key</param>
        /// <param name="bag">为null时删除该项</param>
        /// <param name="expiry"></param>
        public static void Update(string shortKey, TBag bag, TimeSpan? expiry)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            if (bag == null)
            {
                Cache.RemoveFromCache(cacheKey);
            }
            else
            {
                if (string.IsNullOrEmpty(bag.Key))
                {
                    bag.Key = shortKey;//确保Key有值，形如：wx669ef95216eef885，最底层的Key
                }
                //else
                //{
                //    cacheKey = bag.Key;//统一key
                //}

                //if (string.IsNullOrEmpty(cacheKey))
                //{
                //    throw new WeixinException("key和value,Key不可以同时为null或空字符串！");
                //}

                //var c1 = ItemCollection.GetCount();
                //ItemCollection[key] = bag;
                //var c2 = ItemCollection.GetCount();
            }
            //var containerCacheKey = GetContainerCacheKey();

            bag.CacheTime = SystemTime.Now;

            Cache.Update(cacheKey, bag, expiry);//更新到缓存，TODO：有的缓存框架可一直更新Hash中的某个键值对
        }

        /// <summary>
        /// 更新已经添加过的数据项
        /// </summary>
        /// <param name="bag">为null时删除该项</param>
        /// <param name="expiry"></param>
        public static void Update(TBag bag, TimeSpan? expiry)
        {
            if (string.IsNullOrEmpty(bag.Key))
            {
                throw new WeixinException("ContainerBag 更新时，ey 不能为空！类型：" + bag.GetType());
            }

            Update(bag.Key, bag, expiry);
        }

        /// <summary>
        /// 更新数据项（本地缓存不会改变原有值的 HashCode）
        /// </summary>
        /// <param name="shortKey"></param>
        /// <param name="partialUpdate">为null时删除该项</param>
        /// <param name="expiry"></param>
        public static void Update(string shortKey, Action<TBag> partialUpdate, TimeSpan? expiry)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            if (partialUpdate == null)
            {
                Cache.RemoveFromCache(cacheKey);//移除对象
            }
            else
            {
                if (!Cache.CheckExisted(cacheKey))
                {
                    var newBag = new TBag()
                    {
                        Key = cacheKey//确保这一项Key已经被记录
                    };

                    Cache.Set(cacheKey, newBag, expiry);
                }
                partialUpdate(TryGetItem(shortKey));//更新对象
            }
        }

        /// <summary>
        /// 检查Key是否已经注册
        /// </summary>
        /// <param name="shortKey"></param>
        /// <returns></returns>
        public static bool CheckRegistered(string shortKey)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            var registered = Cache.CheckExisted(cacheKey);
            if (!registered && RegisterFunc != null)
            {
                //如果注册不成功，测尝试重新注册（前提是已经进行过注册），这种情况适用于分布式缓存被清空（重启）的情况。
                TryReRegister();
            }

            return Cache.CheckExisted(cacheKey);
        }

        /// <summary>
        /// 从缓存中删除指定项
        /// </summary>
        /// <param name="shortKey"></param>
        public static void RemoveFromCache(string shortKey)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            Cache.RemoveFromCache(cacheKey);
        }
    }
}
