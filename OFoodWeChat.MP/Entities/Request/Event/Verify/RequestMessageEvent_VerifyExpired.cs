/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_VerifyExpired.cs
    文件功能描述：事件之认证过期失效通知
    
    
    创建标识：Senparc - 20170826

    修改标识：Senparc - 20170522
    修改描述：v16.6.2 修改 DateTime 为 DateTimeOffset

----------------------------------------------------------------*/

using System;

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 事件之认证过期失效通知
    /// </summary>
    public class RequestMessageEvent_VerifyExpired : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.verify_expired; }
        }

        /// <summary>
        /// 有效期 (整形)，指的是时间戳，表示已于该时间戳认证过期，需要重新发起微信认证
        /// </summary>
        public DateTimeOffset ExpiredTime { get; set; }
    }
}
