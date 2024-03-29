﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_PicWeixin.cs
    文件功能描述：事件之弹出微信相册发图器(pic_weixin)
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Entities;

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 事件之弹出微信相册发图器(pic_weixin)
    /// </summary>
    public class RequestMessageEvent_Pic_Weixin : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.pic_weixin; }
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 发送的图片信息
        /// </summary>
        public SendPicsInfo SendPicsInfo { get; set; }
    }
}
