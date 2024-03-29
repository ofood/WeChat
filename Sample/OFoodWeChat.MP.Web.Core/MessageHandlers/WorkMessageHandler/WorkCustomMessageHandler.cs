﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：WorkCustomMessageHandler.cs
    文件功能描述：自定义WorkMessageHandler
----------------------------------------------------------------*/

//DPBMARK_FILE Work
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OFoodWeChat.MP.Web.Core.WorkMessageHandler;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.MessageHandlers;

namespace OFoodWeChat.MP.Web.Core.WorkMessageHandlers
{
    public class WorkCustomMessageHandler : WorkMessageHandler<WorkCustomMessageContext>
    {
        public WorkCustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {
        }

        public override IWorkResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您发送了消息：" + requestMessage.Content;
            return responseMessage;
        }

        public override IWorkResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageImage>();
            responseMessage.Image.MediaId = requestMessage.MediaId;
            return responseMessage;
        }

        public override IWorkResponseMessageBase OnEvent_PicPhotoOrAlbumRequest(RequestMessageEvent_Pic_Photo_Or_Album requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您刚发送的图片如下：";
            return responseMessage;
        }

        public override IWorkResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = string.Format("位置坐标 {0} - {1}", requestMessage.Latitude, requestMessage.Longitude);
            return responseMessage;
        }

        public override IWorkResponseMessageBase OnEvent_EnterAgentRequest(RequestMessageEvent_Enter_Agent requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "欢迎进入应用！现在时间是：" + SystemTime.Now.DateTime.ToString();
            return responseMessage;
        }

        public override Work.Entities.IWorkResponseMessageBase DefaultResponseMessage(Work.Entities.IWorkRequestMessageBase requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这是一条没有找到合适回复信息的默认消息。";
            return responseMessage;
        }
    }
}
