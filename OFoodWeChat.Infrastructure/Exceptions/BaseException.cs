/*----------------------------------------------------------------
    文件功能描述：异常基类

----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Trace;
using System;


namespace OFoodWeChat.Infrastructure.Exceptions
{
    /// <summary>
    /// 异常基类
    /// </summary>
    public class BaseException : Exception
    {
        /// <summary>
        /// BaseException 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logged"></param>
        public BaseException(string message, bool logged = false)
            : this(message, null, logged)
        {
        }

        /// <summary>
        /// BaseException
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="inner">内部异常信息</param>
        /// <param name="logged">是否已经使用WeixinTrace记录日志，如果没有，BaseException会进行概要记录</param>
        public BaseException(string message, Exception inner, bool logged = false)
            : base(message, inner)
        {
            if (!logged)
            {
                //LogTrace.Log(string.Format("BaseException（{0}）：{1}", this.GetType().Name, message));
                LogTrace.BaseExceptionLog(this);
            }
        }
    }
}
