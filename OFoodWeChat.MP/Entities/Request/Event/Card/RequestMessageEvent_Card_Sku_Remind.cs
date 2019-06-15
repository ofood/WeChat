/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_Card_Sku_Remind.cs
    文件功能描述：卡券库存报警事件：当某个card_id的初始库存数大于200且当前库存小于等于100时
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.Entities
{
    public class RequestMessageEvent_Card_Sku_Remind : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 进入会员卡
        /// </summary>
        public override Event Event
        {
            get { return Event.card_sku_remind; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 报警详细信息
        /// </summary>
        public string Detail { get; set; }

    }
}
