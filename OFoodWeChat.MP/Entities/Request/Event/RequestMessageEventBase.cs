/*----------------------------------------------------------------
    文件名：RequestMessageEventBase.cs
    文件功能描述：事件基类
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Entities;
using OFoodWeChat.Infrastructure.Enums;

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// IRequestMessageEventBase
    /// </summary>
    public interface IRequestMessageEventBase : IRequestMessageEvent
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        Event Event { get; }
        ///// <summary>
        ///// 事件KEY值，与自定义菜单接口中KEY值对应
        ///// </summary>
        //string EventKey { get; set; }
    }

    /// <summary>
    /// 请求消息的事件推送消息基类
    /// </summary>
    public class RequestMessageEventBase : RequestMessageEvent, IRequestMessageEventBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Event; }
        }

        /// <summary>
        /// 事件类型
        /// </summary>
        public virtual Event Event
        {
            get { return Event.ENTER; }
        }

        /// <summary>
        /// 事件类型
        /// </summary>
        public override object EventType { get { return Event; } }
    }
}
