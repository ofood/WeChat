/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_View.cs
    文件功能描述：事件之URL跳转视图（View）
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Entities;
namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 事件之URL跳转视图（View）
    /// </summary>
    public class RequestMessageEvent_View : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.VIEW; }
        }

        /// <summary>
        /// 事件KEY值，设置的跳转URL
        /// </summary>
        public string EventKey { get; set; }
    }
}
