/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_User_Del_Card.cs
    文件功能描述：事件之删除卡券
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.Entities
{
    public class RequestMessageEvent_User_Del_Card : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 删除卡券
        /// </summary>
        public override Event Event
        {
            get { return Event.user_del_card; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// code 序列号。自定义code 及非自定义code的卡券被领取后都支持事件推送。
        /// </summary>
        public string UserCardCode { get; set; }
    }
}
