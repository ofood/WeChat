/*----------------------------------------------------------------
    文件名：RequestMessageShortVideo.cs
    文件功能描述：接收小视频消息
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Entities;

namespace OFoodWeChat.MP.Entities
{
    public class RequestMessageShortVideo : RequestMessageBase, IRequestMessageShortVideo
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.ShortVideo; }
        }

        /// <summary>
        /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string ThumbMediaId { get; set; }
    }
}
