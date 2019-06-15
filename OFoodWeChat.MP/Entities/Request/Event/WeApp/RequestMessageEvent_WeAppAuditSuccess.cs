/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_WeAppAuditSuccess.cs
    文件功能描述：事件之小程序审核成功
    
    
    创建标识：Senparc - 2010828
    
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 事件之小程序审核成功
    /// </summary>
    public class RequestMessageEvent_WeAppAuditSuccess : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.weapp_audit_success; }
        }

        /// <summary>
        /// 审核成功时的时间（整型），时间戳
        /// </summary>
        public string SuccTime { get; set; }
    }
}
