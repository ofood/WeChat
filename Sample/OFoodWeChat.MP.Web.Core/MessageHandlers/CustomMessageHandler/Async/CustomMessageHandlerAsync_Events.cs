/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：CustomMessageHandler_Events.cs
    文件功能描述：自定义MessageHandler
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

//DPBMARK_FILE MP
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using OFoodWeChat.Infrastructure.Context;
using OFoodWeChat.Core.Exceptions;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.MP.AdvancedAPIs;
using OFoodWeChat.MP.Entities;
using OFoodWeChat.MP.Helpers;
using OFoodWeChat.MP.MessageHandlers;
using OFoodWeChat.MP.Web.Core.Download;
using OFoodWeChat.MP.Web.Core.Utilities;
using OFoodWeChat.Infrastructure.Entities;

using Microsoft.AspNetCore.Http;

namespace OFoodWeChat.MP.Web.Core.CustomMessageHandler
{
    /// <summary>
    /// 自定义MessageHandler
    /// </summary>
    public partial class CustomMessageHandler
    {
        public override Task<IResponseMessageBase> OnEvent_ClickRequestAsync(RequestMessageEvent_Click requestMessage)
        {
            return Task.Factory.StartNew<IResponseMessageBase>(() =>
            {
                var syncResponseMessage = OnEvent_ClickRequest(requestMessage);//这里为了保持Demo的连贯性，结果先从同步方法获取，实际使用过程中可以全部直接定义异步方法
                //常识获取Click事件的同步方法
                if (syncResponseMessage is ResponseMessageText)
                {
                    var textResponseMessage = syncResponseMessage as ResponseMessageText;
                    textResponseMessage.Content += "\r\n\r\n  -- 来自【异步MessageHandler】的回复";
                }

                return syncResponseMessage;
            });
        }
    }
}