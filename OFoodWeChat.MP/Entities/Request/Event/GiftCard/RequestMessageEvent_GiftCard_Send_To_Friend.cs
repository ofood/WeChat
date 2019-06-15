/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_GiftCard_Send_To_Friend.cs
    文件功能描述：用户购买后赠送

----------------------------------------------------------------*/

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 用户购买后赠送
    /// </summary>
    public class RequestMessageEvent_GiftCard_Send_To_Friend : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件：用户购买后赠送
        /// </summary>
        public override Event Event
        {
            get { return Event.giftcard_send_to_friend; }
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
        /// <summary>
        /// 标识礼品卡是否因超过24小时未被领取，退回卡包。True时表明超时退回卡包
        /// </summary>
        public string IsReturnBack { get; set; }
    }
}
