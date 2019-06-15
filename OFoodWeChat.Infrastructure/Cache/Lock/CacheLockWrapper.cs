﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.Core.Cache
{
    public class CacheLockWrapper : IDisposable
    {
        private string _resourceName;
        private IBaseCacheStrategy _containerCacheStragegy;
        private int retryCount;
        private TimeSpan _retryDelay;

        public bool LockSuccessful { get; set; }

        public CacheLockWrapper(IBaseCacheStrategy containerCacheStragegy, string resourceName, string key)
            : this(containerCacheStragegy, resourceName, key, 0, new TimeSpan())
        {

        }

        public CacheLockWrapper(IBaseCacheStrategy containerCacheStragegy, string resourceName, string key, int retryCount, TimeSpan retryDelay)
        {
            _containerCacheStragegy = containerCacheStragegy;
            _resourceName = resourceName + key;/*加上Key可以针对某个AppId加锁*/

            if (retryCount != 0 && retryDelay.Ticks != 0)
            {
                LockSuccessful = _containerCacheStragegy.Lock(_resourceName, retryCount, retryDelay);
            }
            else
            {
                LockSuccessful = _containerCacheStragegy.Lock(_resourceName);
            }
        }

        public void Dispose()
        {
            _containerCacheStragegy.UnLock(_resourceName);
        }
    }
}
