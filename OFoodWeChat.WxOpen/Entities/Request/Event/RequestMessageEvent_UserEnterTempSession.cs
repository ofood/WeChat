/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_UserEnterTempSession.cs
    文件功能描述：事件之用户进入客服 
----------------------------------------------------------------*/

namespace OFoodWeChat.WxOpen.Entities
{
    /// <summary>
    /// 事件之用户进入客服
    /// </summary>
    public class RequestMessageEvent_UserEnterTempSession : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.user_enter_tempsession; }
        }

        /// <summary>
        /// 开发者在客服会话按钮设置的sessionFrom参数
        /// </summary>
        public string SessionFrom { get; set; }

    }
}
