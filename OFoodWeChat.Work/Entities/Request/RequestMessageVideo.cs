/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageVideo.cs
    文件功能描述：接收普通视频消息
----------------------------------------------------------------*/
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Entities;

namespace OFoodWeChat.Work.Entities
{
    public class RequestMessageVideo : WorkRequestMessageBase, IRequestMessageVideo
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Video; }
        }

        public string MediaId { get; set;}
        public string ThumbMediaId { get; set; }
    }
}
