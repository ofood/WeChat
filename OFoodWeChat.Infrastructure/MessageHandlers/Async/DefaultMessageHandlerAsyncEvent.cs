/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：DefaultMessageHandlerAsyncEvent.cs
    文件功能描述：MessageHandler事件异步方法的默认调用方法（在没有override的情况下）
   
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.Infrastructure.MessageHandlers
{
    /// <summary>
    /// MessageHandler事件异步方法的默认调用方法（在没有override的情况下）
    /// </summary>
    public enum DefaultMessageHandlerAsyncEvent
    {
        /// <summary>
        /// 调用DefaultResponseMessageAsync()方法
        /// </summary>
        DefaultResponseMessageAsync,
        /// <summary>
        /// 调用同名的同步方法（可能会导致执行会阻塞的过程）
        /// </summary>
        SelfSynicMethod
    }
}
