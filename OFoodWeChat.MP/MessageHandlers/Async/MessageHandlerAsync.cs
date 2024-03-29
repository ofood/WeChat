﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：MessageHandlerAsync.cs
    文件功能描述：微信请求【异步方法】的集中处理方法
   
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Xml.Linq;

using OFoodWeChat.MP.AppStore;
using OFoodWeChat.MP.Entities;
using OFoodWeChat.MP.Entities.Request;
using OFoodWeChat.MP.Helpers;
using System.Threading.Tasks;
using System.Threading;
using OFoodWeChat.Infrastructure.Entities;
using OFoodWeChat.Infrastructure.MessageHandlers;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Trace;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure.Context;

namespace OFoodWeChat.MP.MessageHandlers
{


    /// <summary>
    /// 微信请求的集中处理方法
    /// 此方法中所有过程，都基于OFoodWeChat.MP的基础功能，只为简化代码而设。
    /// </summary>
    public abstract partial class MessageHandler<TC> :
        MessageHandler<TC, IRequestMessageBase, IResponseMessageBase>, IMessageHandler
        where TC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
    {
        /// <summary>
        /// 自动判断默认异步方法调用（在没有override的情况下调用的默认方法）
        /// </summary>
        /// <param name="requestMessage">requestMessage</param>
        /// <param name="syncMethod">同名的同步方法(DefaultMessageHandlerAsyncEvent值为SelfSynicMethod时调用)</param>
        /// <returns></returns>
        private async Task<IResponseMessageBase> DefaultAsyncMethod(IRequestMessageBase requestMessage, Func<IResponseMessageBase> syncMethod)
        {
            return (base.DefaultMessageHandlerAsyncEvent == DefaultMessageHandlerAsyncEvent.DefaultResponseMessageAsync
                            ? await DefaultResponseMessageAsync(requestMessage)
                            : await Task.Run(syncMethod));
        }

        /// <summary>
        /// 【异步方法】执行微信请求
        /// </summary>
        public override async Task BuildResponseMessageAsync(CancellationToken cancellationToken)
        {
            #region NeuChar 执行过程

            var weixinAppId = this._postModel == null ? "" : this._postModel.AppId;

            switch (RequestMessage.MsgType)
            {
                case RequestMsgType.Text:
                    {
                        try
                        {
                            var requestMessage = RequestMessage as RequestMessageText;
                            ResponseMessage = await CurrentMessageHandlerNode.ExecuteAsync(requestMessage, this, weixinAppId)
                                ?? ((await (OnTextOrEventRequestAsync(requestMessage))
                                ?? (await OnTextRequestAsync(requestMessage))));
                        }
                        catch (Exception ex)
                        {
                            LogTrace.SendCustomLog("mp-response error", ex.Message + "\r\n|||\r\n" + (ex.InnerException != null ? ex.InnerException.ToString() : ""));

                        }

                    }
                    break;
                case RequestMsgType.Location:
                    ResponseMessage = await OnLocationRequestAsync(RequestMessage as RequestMessageLocation);
                    break;
                case RequestMsgType.Image:

                    WeixinTrace.SendCustomLog("NeuChar Image", $"appid:{weixinAppId}");

                    ResponseMessage = await CurrentMessageHandlerNode.ExecuteAsync(RequestMessage, this, weixinAppId) ?? await OnImageRequestAsync(RequestMessage as RequestMessageImage);
                    break;
                case RequestMsgType.Voice:
                    ResponseMessage = await OnVoiceRequestAsync(RequestMessage as RequestMessageVoice);
                    break;
                case RequestMsgType.Video:
                    ResponseMessage = await OnVideoRequestAsync(RequestMessage as RequestMessageVideo);
                    break;
                case RequestMsgType.Link:
                    ResponseMessage = await OnLinkRequestAsync(RequestMessage as RequestMessageLink);
                    break;
                case RequestMsgType.ShortVideo:
                    ResponseMessage = await OnShortVideoRequestAsync(RequestMessage as RequestMessageShortVideo);
                    break;
                case RequestMsgType.File:
                    ResponseMessage = await OnFileRequestAsync(RequestMessage as RequestMessageFile);
                    break;
                case RequestMsgType.NeuChar:
                    ResponseMessage = await OnNeuCharRequestAsync(RequestMessage as RequestMessageNeuChar);
                    break;
                case RequestMsgType.Unknown:
                    ResponseMessage = await OnUnknownTypeRequestAsync(RequestMessage as RequestMessageUnknownType);
                    break;
                case RequestMsgType.Event:
                    {
                        var requestMessageText = (RequestMessage as IRequestMessageEventBase).ConvertToRequestMessageText();
                        ResponseMessage = await CurrentMessageHandlerNode.ExecuteAsync(RequestMessage, this, weixinAppId) ??
                                            await OnTextOrEventRequestAsync(requestMessageText) ??
                                                (await OnEventRequestAsync(RequestMessage as IRequestMessageEventBase));
                    }
                    break;

                default:
                    WeixinTrace.SendCustomLog("NeuChar", "未知的MsgType请求类型" + RequestMessage.MsgType);
                    //throw new UnknownRequestMsgTypeException("未知的MsgType请求类型", null);
                    break;
            }

            #endregion
        }

        /// <summary>
        /// 【异步方法】OnExecutingAsync()
        /// </summary>
        /// <returns></returns>
        public override async Task OnExecutingAsync(CancellationToken cancellationToken)
        {
            //已放入Init()方法中
            //#region 消息去重

            //if ((OmitRepeatedMessageFunc == null || OmitRepeatedMessageFunc(RequestMessage) == true)
            //&& OmitRepeatedMessage && CurrentMessageContext.RequestMessages.Count > 1
            ////&& !(RequestMessage is RequestMessageEvent_Merchant_Order)批量订单的MsgId可能会相同
            //)
            //{
            //    var currentIndex = CurrentMessageContext.RequestMessages.FindLastIndex(z=>z.)


            //    var lastMessage = CurrentMessageContext.RequestMessages[CurrentMessageContext.RequestMessages.Count - 2];

            //    if (
            //        //使用MsgId去重
            //        (lastMessage.MsgId != 0 && lastMessage.MsgId == RequestMessage.MsgId)
            //        //使用CreateTime去重（OpenId对象已经是同一个）
            //        || (lastMessage.MsgId == RequestMessage.MsgId
            //            && lastMessage.CreateTime == RequestMessage.CreateTime
            //            && lastMessage.MsgType == RequestMessage.MsgType)
            //        )
            //    {
            //        CancelExcute = true;//重复消息，取消执行
            //        MessageIsRepeated = true;
            //        return;
            //    }
            //}

            //#endregion

            await base.OnExecutingAsync(cancellationToken);

            //判断是否已经接入开发者信息
            if (DeveloperInfo != null || CurrentMessageContext.AppStoreState == AppStoreState.Enter)
            {
                //优先请求云端应用商店资源
            }
        }

        /// <summary>
        /// 【异步方法】OnExecutedAsync()
        /// </summary>
        /// <returns></returns>
        public override async Task OnExecutedAsync(CancellationToken cancellationToken)
        {
            await base.OnExecutedAsync(cancellationToken);
        }

    }
}
