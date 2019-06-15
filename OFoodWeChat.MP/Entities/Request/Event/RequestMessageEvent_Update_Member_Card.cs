/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_Update_Member_Card.cs
    文件功能描述：事件之会员卡内容更新事件：会员卡积分余额发生变动时
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.Entities
{
    public class RequestMessageEvent_Update_Member_Card : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 卡券未通过审核
        /// </summary>
        public override Event Event
        {
            get { return Event.update_member_card; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// code序列号。
        /// </summary>
        public string UserCardCode { get; set; }
        /// <summary>
        /// 变动的积分值。
        /// </summary>
        public int ModifyBonus { get; set; }
        /// <summary>
        /// 变动的余额值。
        /// </summary>
        public int ModifyBalance { get; set; }
    }
}
