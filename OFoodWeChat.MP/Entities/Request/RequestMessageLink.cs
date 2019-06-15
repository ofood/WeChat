/*----------------------------------------------------------------
    文件名：RequestMessageLink.cs
    文件功能描述：接收普通链接消息
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Entities;

namespace OFoodWeChat.MP.Entities
{
    public class RequestMessageLink : RequestMessageBase, IRequestMessageLink
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Link; }
        }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }
    }
}
