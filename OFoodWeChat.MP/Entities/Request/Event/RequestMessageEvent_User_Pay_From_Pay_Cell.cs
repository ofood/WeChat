/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_User_Pay_From_Pay_Cell.cs
    文件功能描述：事件之微信买单完成
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.Entities
{
    public class RequestMessageEvent_User_Pay_From_Pay_Cell : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 删除卡券
        /// </summary>
        public override Event Event
        {
            get { return Event.user_pay_from_pay_cell; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// code 序列号。自定义code 及非自定义code的卡券被领取后都支持事件推送。
        /// </summary>
        public string UserCardCode { get; set; }
        /// <summary>
        /// 微信支付交易订单号（只有使用买单功能核销的卡券才会出现）
        /// </summary>
        public string TransId { get; set; }
        /// <summary>
        ///门店ID，当前卡券核销的门店ID（只有通过卡券商户助手和买单核销时才会出现）
        /// </summary>
        public string LocationId { get; set; }
        /// <summary>
        ///实付金额，单位为分 
        /// </summary>
        public int Fee { get; set; }
        /// <summary>
        /// 应付金额，单位为分
        /// </summary>
        public int OriginalFee { get; set; }
    }
}
