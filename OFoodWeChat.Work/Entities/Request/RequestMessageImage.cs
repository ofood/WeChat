/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageImage.cs
    文件功能描述：接收普通图片消息
----------------------------------------------------------------*/
using OFoodWeChat.Infrastructure.Entities;
using OFoodWeChat.Infrastructure.Enums;

namespace OFoodWeChat.Work.Entities
{
    public class RequestMessageImage : WorkRequestMessageBase, IRequestMessageImage
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Image; }
        }

        public string MediaId { get; set; }
        public string PicUrl { get; set; }
    }
}
