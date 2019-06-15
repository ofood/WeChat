/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_UserEnterTempSession.cs
    文件功能描述：事件之地点审核 
----------------------------------------------------------------*/

namespace OFoodWeChat.WxOpen.Entities
{
    /// <summary>
    /// 事件之地点审核
    /// </summary>
    public class RequestMessageEvent_AddNearbyPoiAuditInfo : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.add_nearby_poi_audit_info; }
        }

        /// <summary>
        /// 审核单id
        /// </summary>
        public string audit_id { get; set; }

        /// <summary>
        /// 审核状态（3：审核通过，2：审核失败）
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 如果status为3或者4，会返回审核失败的原因
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// poi_id
        /// </summary>
        public string poi_id { get; set; }

    }
}
