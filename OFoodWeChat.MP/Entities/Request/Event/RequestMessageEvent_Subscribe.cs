﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_Subscribe.cs
    文件功能描述：事件之订阅
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Entities;
namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 事件之订阅
    /// </summary>
    public class RequestMessageEvent_Subscribe : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.subscribe; }
        }

        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值（如果不是扫描场景二维码，此参数为空）
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片（如果不是扫描场景二维码，此参数为空）
        /// </summary>
        public string Ticket { get; set; }
    }
}
