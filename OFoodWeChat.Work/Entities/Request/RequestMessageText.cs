/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageText.cs
    文件功能描述：接收普通文本消息
----------------------------------------------------------------*/
using OFoodWeChat.Infrastructure.Entities;
using OFoodWeChat.Infrastructure.Enums;

namespace OFoodWeChat.Work.Entities
{
    public class RequestMessageText : WorkRequestMessageBase, IRequestMessageText
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Text; }
        }
        public string Content { get; set; }
    }
}
