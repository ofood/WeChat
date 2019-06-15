/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_User_Gifting_Card.cs
    文件功能描述：卡券转赠事件推送
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.Entities
{
    public class RequestMessageEvent_User_Gifting_Card : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 卡券未通过审核
        /// </summary>
        public override Event Event
        {
            get { return Event.user_gifting_card; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        ///接收卡券用户的openid 
        /// </summary>
        public string FriendUserName { get; set; }
        /// <summary>
        /// code序列号。
        /// </summary>
        public string UserCardCode { get; set; }
        /// <summary>
        /// 是否转赠退回，0代表不是，1代表是。
        /// </summary>
        public string IsReturnBack { get; set; }
        /// <summary>
        /// 是否是群转赠
        /// </summary>
        public string IsChatRoom { get; set; }
    }
}
