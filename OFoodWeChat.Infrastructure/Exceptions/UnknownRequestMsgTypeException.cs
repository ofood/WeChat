/*----------------------------------------------------------------    
    文件名：UnknownRequestMsgTypeException.cs
    文件功能描述：未知请求类型
----------------------------------------------------------------*/


using System;

namespace OFoodWeChat.Infrastructure.Exceptions
{
    /// <summary>
    /// 未知请求类型异常
    /// </summary>
    public class UnknownRequestMsgTypeException : MessageHandlerException //ArgumentOutOfRangeException
    {
        public UnknownRequestMsgTypeException(string message)
            : this(message, null)
        {
        }

        public UnknownRequestMsgTypeException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
