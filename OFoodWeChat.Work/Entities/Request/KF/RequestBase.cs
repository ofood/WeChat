/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：RequestBase.cs
    文件功能描述：所有客服请求消息的基类
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Enums;
using System;

namespace OFoodWeChat.Work.Entities.Request.KF
{
    public class RequestBase
    {
        public RequestMsgType MsgType { get; protected set; }
        public string FromUserName { get; set; }
        public DateTimeOffset CreateTime { get; set; }
    }
}
