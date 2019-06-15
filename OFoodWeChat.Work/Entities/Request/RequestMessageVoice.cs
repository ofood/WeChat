/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageVoice.cs
    文件功能描述：接收普通语音消息
----------------------------------------------------------------*/
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Entities;

namespace OFoodWeChat.Work.Entities
{
    public class RequestMessageVoice : WorkRequestMessageBase, IRequestMessageVoice
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Voice; }
        }

        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 语音格式：amr
        /// </summary>
        public string Format { get; set; }
    }
}
