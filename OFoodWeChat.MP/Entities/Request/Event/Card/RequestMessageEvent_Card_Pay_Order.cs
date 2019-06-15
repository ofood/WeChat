/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_Card_Pay_Order.cs
    文件功能描述：券点流水详情事件：当商户朋友的券券点发生变动时
    
    
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.Entities
{
    public class RequestMessageEvent_Card_Pay_Order : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 进入会员卡
        /// </summary>
        public override Event Event
        {
            get { return Event.card_pay_order; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 商户自定义code值。非自定code推送为空串。
        /// </summary>
        public string UserCardCode { get; set; }
        /// <summary>
        /// 商户自定义二维码渠道参数，用于标识本次扫码打开会员卡来源来自于某个渠道值的二维码
        /// </summary>
        public string OuterStr { get; set; }
    }
}
