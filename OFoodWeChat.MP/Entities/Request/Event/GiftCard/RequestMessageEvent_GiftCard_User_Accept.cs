/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_GiftCard_User_Accept.cs
    文件功能描述：用户领取礼品卡成功
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 事件：用户领取礼品卡成功
    /// </summary>
    public class RequestMessageEvent_GiftCard_User_Accept : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件
        /// </summary>
        public override Event Event
        {
            get { return Event.giftcard_user_accept; }
        }

        /// <summary>
        /// 货架的id
        /// </summary>
        public string PageId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 礼品卡是否发送至群，true为是
        /// </summary>
        public string IsChatRoom { get; set; }
    }
}
