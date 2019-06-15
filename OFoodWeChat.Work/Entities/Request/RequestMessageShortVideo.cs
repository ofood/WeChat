/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageShortVideo.cs
    文件功能描述：接收小视频消息
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Entities;

namespace OFoodWeChat.Work.Entities
{
    public class RequestMessageShortVideo : WorkRequestMessageBase, IRequestMessageShortVideo
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.ShortVideo; }
        }

        public string MediaId { get; set;}
        public string ThumbMediaId { get; set; }
    }
}
