/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：MessageHandler.cs
    文件功能描述：微信请求【异步方法】的集中处理方法
----------------------------------------------------------------*/


using System;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;

using System.Threading;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Exceptions;
using OFoodWeChat.Infrastructure.Context;
using OFoodWeChat.Infrastructure.Entities;
using OFoodWeChat.Infrastructure.Data;

namespace OFoodWeChat.Infrastructure.MessageHandlers
{
    /// <summary>
    /// 微信请求的集中处理方法
    /// 此方法中所有过程，都基于Senparc.NeuChar.基础功能，只为简化代码而设。
    /// </summary>
    public abstract partial class MessageHandler<TC, TRequest, TResponse> : IMessageHandler<TRequest, TResponse>
        where TC : class, IMessageContext<TRequest, TResponse>, new()
        where TRequest : IRequestMessageBase
        where TResponse : IResponseMessageBase
    {
        #region 异步方法

        /// <summary>
        /// 默认参数设置为 DefaultResponseMessageAsync
        /// </summary>
        private DefaultMessageHandlerAsyncEvent _defaultMessageHandlerAsyncEvent = DefaultMessageHandlerAsyncEvent.DefaultResponseMessageAsync;

        /// <summary>
        /// <para>MessageHandler事件异步方法的默认调用方法（在没有override的情况下）。默认值：DefaultDefaultResponseMessageAsync。</para>
        /// <para>默认参数设置为 DefaultResponseMessageAsync，目的是为了确保默认状态下不会执行意料以外的代码，
        /// 因此，如果需要在异步方法中调用同名的同步方法，请手动将此参数设置为SelfSynicMethod。</para>
        /// </summary>
        public DefaultMessageHandlerAsyncEvent DefaultMessageHandlerAsyncEvent
        {
            get { return _defaultMessageHandlerAsyncEvent; }
            set { _defaultMessageHandlerAsyncEvent = value; }
        }

        public virtual async Task OnExecutingAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() => this.OnExecuting(), cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            //进行 APM 记录
            ExecuteStatTime = SystemTime.Now;

            DataOperation apm = new DataOperation(PostModel?.DomainId);

            await apm.SetAsync(NeuCharApmKind.Message_Request.ToString(), 1, tempStorage: OpenId).ConfigureAwait(false);

            if (CancelExcute)
            {
                return;
            }

            await OnExecutingAsync(cancellationToken).ConfigureAwait(false);

            if (CancelExcute)
            {
                return;
            }

            try
            {
                if (RequestMessage == null)
                {
                    return;
                }

                await BuildResponseMessageAsync(cancellationToken).ConfigureAwait(false);

                //记录上下文
                //此处修改
                if (MessageContextGlobalConfig.UseMessageContext && ResponseMessage != null && !string.IsNullOrEmpty(ResponseMessage.FromUserName))
                {
                    GlobalMessageContext.InsertMessage(ResponseMessage);
                }
                await apm.SetAsync(NeuCharApmKind.Message_SuccessResponse.ToString(), 1, tempStorage: OpenId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                apm.SetAsync(NeuCharApmKind.Message_Exception.ToString(), 1, tempStorage: OpenId).ConfigureAwait(false).GetAwaiter().GetResult();
                throw new MessageHandlerException("MessageHandler中Execute()过程发生错误：" + ex.Message, ex);
            }
            finally
            {
                await OnExecutedAsync(cancellationToken).ConfigureAwait(false);
                await apm.SetAsync(NeuCharApmKind.Message_ResponseMillisecond.ToString(), (SystemTime.Now - this.ExecuteStatTime).TotalMilliseconds, tempStorage: OpenId).ConfigureAwait(false);
            }

            //await Task.Run(() => this.Execute()).ConfigureAwait(false);
        }

        public virtual async Task BuildResponseMessageAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() => this.BuildResponseMessage(), cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task OnExecutedAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() => this.OnExecuted(), cancellationToken).ConfigureAwait(false);
        }

        #endregion

    }
}
