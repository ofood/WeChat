﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_ScancodeWaitmsg.cs
    文件功能描述：事件之扫码推事件且弹出“消息接收中”提示框(scancode_waitmsg)
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Entities;
namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 事件之扫码推事件且弹出“消息接收中”提示框(scancode_waitmsg)
    /// </summary>
    public class RequestMessageEvent_Scancode_Waitmsg : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.scancode_waitmsg; }
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 扫描信息
        /// </summary>
        public ScanCodeInfo ScanCodeInfo { get; set; }
    }
}
