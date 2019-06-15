
/*----------------------------------------------------------------    
    文件名：MessageHandlerException.cs
    文件功能描述：微信消息异常处理类

----------------------------------------------------------------*/
using System;

namespace OFoodWeChat.Infrastructure.Exceptions
{
    /// <summary>
    /// MessageHandler异常
    /// </summary>
    public class MessageHandlerException : BaseException
    {
          public MessageHandlerException(string message)
            : this(message, null)
        {
        }

          public MessageHandlerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
