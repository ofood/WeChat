/*----------------------------------------------------------------

    文件名：LocalContainerCacheStrategy.cs
    文件功能描述：本地容器缓存。

 ----------------------------------------------------------------*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using OFoodWeChat.Infrastructure.Cache;
using OFoodWeChat.Infrastructure.Exceptions;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;


namespace OFoodWeChat.Infrastructure.Cache
{
    /// <summary>
    /// 全局静态数据源帮助类
    /// </summary>
    public static class LocalObjectCacheHelper
    {
        private static IMemoryCache _localObjectCache;

        /// <summary>
        /// 所有数据集合的列表
        /// </summary>
        public static IMemoryCache LocalObjectCache
        {
            get
            {
                if (_localObjectCache == null)
                {
                    _localObjectCache = OFoodDI.GetService<IMemoryCache>();

                    if (_localObjectCache == null)
                    {
                        throw new CacheException("IMemoryCache 依赖注入未设置！请在 Startup.cs 中使用 serviceCollection.AddMemoryCache() 进行设置！");
                    }
                }
                return _localObjectCache;
            }
        }

        /// <summary>
        /// .NET Core 的 MemoryCache 不提供遍历所有项目的方法，因此这里做一个储存Key的地方
        /// </summary>
        public static Dictionary<string, DateTimeOffset> Keys { get; set; } = new Dictionary<string, DateTimeOffset>();

        static LocalObjectCacheHelper()
        {

        }

        /// <summary>
        /// 获取储存Keys信息的缓存键
        /// </summary>
        /// <param name="cacheStrategy"></param>
        /// <returns></returns>
        public static string GetKeyStoreKey(BaseCacheStrategy cacheStrategy)
        {
            var keyStoreFinalKey = cacheStrategy.GetFinalKey("CO2NET_KEY_STORE");
            return keyStoreFinalKey;
        }
    }

    /// <summary>
    /// 本地容器缓存策略
    /// </summary>
    public class LocalObjectCacheStrategy : BaseCacheStrategy, IBaseObjectCacheStrategy
    {
        #region 数据源

        private IMemoryCache _cache = LocalObjectCacheHelper.LocalObjectCache;

        #endregion

        #region 单例

        ///<summary>
        /// LocalCacheStrategy的构造函数
        ///</summary>
        LocalObjectCacheStrategy()
        {
        }

        //静态LocalCacheStrategy
        public static LocalObjectCacheStrategy Instance
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
            internal static readonly LocalObjectCacheStrategy instance = new LocalObjectCacheStrategy();
        }


        #endregion

        #region IObjectCacheStrategy 成员

        //public IContainerCacheStrategy ContainerCacheStrategy
        //{
        //    get { return LocalContainerCacheStrategy.Instance; }
        //}

        #region 同步方法

        public void Set(string key, object value, TimeSpan? expiry = null, bool isFullKey = false)
        {
            if (key == null || value == null)
            {
                return;
            }

            var finalKey = base.GetFinalKey(key, isFullKey);

            var newKey = !CheckExisted(finalKey, true);

            if (expiry.HasValue)
            {
                _cache.Set(finalKey, value, expiry.Value);
            }
            else
            {
                _cache.Set(finalKey, value);
            }

            //由于MemoryCache不支持遍历Keys，所以需要单独储存
            if (newKey)
            {
                var keyStoreFinalKey = LocalObjectCacheHelper.GetKeyStoreKey(this);
                List<string> keys;
                if (!CheckExisted(keyStoreFinalKey, true))
                {
                    keys = new List<string>();
                }
                else
                {
                    keys = _cache.Get<List<string>>(keyStoreFinalKey);
                }
                keys.Add(finalKey);
                _cache.Set(keyStoreFinalKey, keys);
            }
        }

        public void RemoveFromCache(string key, bool isFullKey = false)
        {
            var cacheKey = GetFinalKey(key, isFullKey);
            _cache.Remove(cacheKey);

            //移除key
            var keyStoreFinalKey = LocalObjectCacheHelper.GetKeyStoreKey(this);
            if (CheckExisted(keyStoreFinalKey, true))
            {
                var keys = _cache.Get<List<string>>(keyStoreFinalKey);
                keys.Remove(cacheKey);
                _cache.Set(keyStoreFinalKey, keys);
            }

        }

        public object Get(string key, bool isFullKey = false)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            if (!CheckExisted(key, isFullKey))
            {
                return null;
                //InsertToCache(key, new ContainerItemCollection());
            }

            var cacheKey = GetFinalKey(key, isFullKey);

            return _cache.Get(cacheKey);
        }

        /// <summary>
        /// 返回指定缓存键的对象，并强制指定类型
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="isFullKey">是否已经是完整的Key，如果不是，则会调用一次GetFinalKey()方法</param>
        /// <returns></returns>
        public T Get<T>(string key, bool isFullKey = false)
        {
            if (string.IsNullOrEmpty(key))
            {
                return default(T);
            }

            if (!CheckExisted(key, isFullKey))
            {
                return default(T);
                //InsertToCache(key, new ContainerItemCollection());
            }

            var cacheKey = GetFinalKey(key, isFullKey);

            return _cache.Get<T>(cacheKey);
        }

        public IDictionary<string, object> GetAll()
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            //获取所有Key
            var keyStoreFinalKey = LocalObjectCacheHelper.GetKeyStoreKey(this);
            if (CheckExisted(keyStoreFinalKey, true))
            {
                var keys = _cache.Get<List<string>>(keyStoreFinalKey);
                foreach (var key in keys)
                {
                    data[key] = Get(key, true);
                }
            }
            return data;

        }

        public bool CheckExisted(string key, bool isFullKey = false)
        {
            var cacheKey = GetFinalKey(key, isFullKey);

            return _cache.Get(cacheKey) != null;
        }

        public long GetCount()
        {
            var keyStoreFinalKey = LocalObjectCacheHelper.GetKeyStoreKey(this);
            if (CheckExisted(keyStoreFinalKey, true))
            {
                var keys = _cache.Get<List<string>>(keyStoreFinalKey);
                return keys.Count;
            }
            else
            {
                return 0;
            }
        }

        public void Update(string key, object value, TimeSpan? expiry = null, bool isFullKey = false)
        {
            Set(key, value, expiry, isFullKey);
        }

        #endregion

        #region 异步方法
        public async Task SetAsync(string key, object value, TimeSpan? expiry = null, bool isFullKey = false)
        {
            await Task.Factory.StartNew(() => Set(key, value, expiry, isFullKey)).ConfigureAwait(false);
        }

        public async Task RemoveFromCacheAsync(string key, bool isFullKey = false)
        {
            await Task.Factory.StartNew(() => RemoveFromCache(key, isFullKey)).ConfigureAwait(false);
        }

        public async Task<object> GetAsync(string key, bool isFullKey = false)
        {
            return await Task.Factory.StartNew(() => Get(key, isFullKey)).ConfigureAwait(false);
        }

        /// <summary>
        /// 返回指定缓存键的对象，并强制指定类型
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="isFullKey">是否已经是完整的Key，如果不是，则会调用一次GetFinalKey()方法</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key, bool isFullKey = false)
        {
            return await Task.Factory.StartNew(() => Get<T>(key, isFullKey)).ConfigureAwait(false);
        }

        public async Task<IDictionary<string, object>> GetAllAsync()
        {
            return await Task.Factory.StartNew(() => GetAll()).ConfigureAwait(false);
        }


        public async Task<bool> CheckExistedAsync(string key, bool isFullKey = false)
        {
            return await Task.Factory.StartNew(() => CheckExisted(key, isFullKey)).ConfigureAwait(false);

        }

        public async Task<long> GetCountAsync()
        {
            return await Task.Factory.StartNew(() => GetCount()).ConfigureAwait(false);
        }


        public async Task UpdateAsync(string key, object value, TimeSpan? expiry = null, bool isFullKey = false)
        {
            await Task.Factory.StartNew(() => Update(key, value, expiry, isFullKey)).ConfigureAwait(false);
        }
        #endregion

        #endregion

        #region ICacheLock

        public override ICacheLock BeginCacheLock(string resourceName, string key, int retryCount = 0, TimeSpan retryDelay = new TimeSpan())
        {
            return LocalCacheLock.CreateAndLock(this, resourceName, key, retryCount, retryDelay);
        }

        public override async Task<ICacheLock> BeginCacheLockAsync(string resourceName, string key, int retryCount = 0, TimeSpan retryDelay = new TimeSpan())
        {
            return await LocalCacheLock.CreateAndLockAsync(this, resourceName, key, retryCount, retryDelay).ConfigureAwait(false);
        }
        #endregion

    }
}
