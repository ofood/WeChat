/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：IMessageHandlerWithContext.cs
    文件功能描述：具有上下文的 MessageHandler 接口
----------------------------------------------------------------*/


using OFoodWeChat.Infrastructure.Context;
using OFoodWeChat.Infrastructure.Entities;
using System;

namespace OFoodWeChat.Infrastructure.MessageHandlers
{
    /// <summary>
    /// 具有上下文的 MessageHandler 接口
    /// </summary>
    /// <typeparam name="TC"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IMessageHandlerWithContext<TC, TRequest, TResponse> : IMessageHandler<TRequest, TResponse>
        where TC : class, IMessageContext<TRequest, TResponse>, new()
        where TRequest : IRequestMessageBase
        where TResponse : IResponseMessageBase
    {
        /// <summary>
        /// 全局消息上下文
        /// </summary>
        GlobalMessageContext<TC, TRequest, TResponse> GlobalMessageContext { get; }
        /// <summary>
        /// 当前用户消息上下文
        /// </summary>
        TC CurrentMessageContext { get; }
        /// <summary>
        /// 忽略重复发送的同一条消息（通常因为微信服务器没有收到及时的响应）
        /// </summary>
         bool OmitRepeatedMessage { get; set; }
        /// <summary>
        /// 消息是否已经被去重
        /// </summary>
         bool MessageIsRepeated { get; set; }
    }
}
