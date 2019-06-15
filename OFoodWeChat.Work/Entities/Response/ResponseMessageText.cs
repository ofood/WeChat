/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ResponseMessageText.cs
    文件功能描述：响应回复文本消息
----------------------------------------------------------------*/
using OFoodWeChat.Infrastructure.Entities;
using OFoodWeChat.Infrastructure.Enums;

namespace OFoodWeChat.Work.Entities
{
    public class ResponseMessageText : WorkResponseMessageBase, IResponseMessageText
    {
        public new virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Text; }
        }

        public string Content { get; set; }
    }
}
