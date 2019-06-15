/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_GiftCard_Pay_Done.cs
    文件功能描述：用户购买礼品卡付款成功
    
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 用户购买礼品卡付款成功
    /// </summary>
    public class RequestMessageEvent_GiftCard_Pay_Done : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件：用户购买礼品卡付款成功
        /// </summary>
        public override Event Event
        {
            get { return Event.giftcard_pay_done; }
        }

        /// <summary>
        /// 货架的id
        /// </summary>
        public string PageId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
    }
}
