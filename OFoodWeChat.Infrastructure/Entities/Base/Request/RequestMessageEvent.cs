/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：RequestMessageEvent.cs
    文件功能描述：所有事件消息的基类
    
    TODO：此类暂时没有用到，预留方便今后扩展时重构
    
----------------------------------------------------------------*/
namespace OFoodWeChat.Infrastructure.Entities
{
    /// <summary>
    /// 所有事件消息的基类
    /// </summary>
    public abstract class RequestMessageEvent : RequestMessageBase, IRequestMessageEvent
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public abstract object EventType { get; }
        /// <summary>
        /// 获取事件类型的字符串
        /// </summary>
        public virtual string EventName
        {
            get
            {
                return EventType?.ToString();
            }
        }

        /// <summary>
        /// RequestMessageEvent 构造函数
        /// </summary>
        public RequestMessageEvent()
        {

        }

    }
}
