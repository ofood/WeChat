/*----------------------------------------------------------------
    文件名：NeuCharException.cs
    文件功能描述：NeuChar异常处理
    
----------------------------------------------------------------*/
using System;

namespace OFoodWeChat.Infrastructure.Exceptions
{
    /// <summary>
    /// MessageHandler异常
    /// </summary>
    public class NeuCharException : BaseException
    {
          public NeuCharException(string message)
            : this(message, null)
        {
        }

          public NeuCharException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
