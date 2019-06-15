/*----------------------------------------------------------------

    文件名：CacheException.cs
    文件功能描述：缓存异常
----------------------------------------------------------------*/

using System;

namespace OFoodWeChat.Infrastructure.Exceptions
{
    /// <summary>
    /// 缓存异常
    /// </summary>
    public class CacheException : BaseException
    {
        /// <summary>
        /// 缓存异常构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        /// <param name="logged"></param>
        public CacheException(string message, Exception inner = null, bool logged = false) : base(message, inner, logged)
        {
        }
    }
}
