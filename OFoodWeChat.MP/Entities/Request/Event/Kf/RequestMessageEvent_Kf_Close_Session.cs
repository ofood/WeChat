﻿
/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_Kf_Close_Session.cs
    文件功能描述：事件之多客服关闭会话(kf_close_session)
    
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 事件之多客服关闭会话(kf_close_session)
    /// </summary>
    public class RequestMessageEvent_Kf_Close_Session : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.kf_close_session; }
        }

        /// <summary>
        /// 完整客服账号，格式为：账号前缀@公众号微信号
        /// </summary>
        public string KfAccount { get; set; }
    }
}
