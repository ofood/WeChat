/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：RequestMessageUnknownType.cs
    文件功能描述：未知请求类型    
----------------------------------------------------------------*/


using OFoodWeChat.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace OFoodWeChat.Infrastructure.Entities
{
    public class RequestMessageUnknownType : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Unknown; }
        }

        /// <summary>
        /// 请求消息的XML对象（明文）
        /// </summary>
        public XDocument RequestDocument { get; set; }

    }
}
