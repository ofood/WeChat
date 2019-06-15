/*----------------------------------------------------------------
   
    文件名：RequestMessageImage.cs
    文件功能描述：接收普通图片消息
    
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Entities;
using OFoodWeChat.Infrastructure.Enums;

namespace OFoodWeChat.WxOpen.Entities
{
    public class RequestMessageImage : RequestMessageBase, IRequestMessageImage
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Image; }
        }

        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }
    }
}
