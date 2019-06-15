/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：MktActivityData.cs
    文件功能描述：社交立减金活动数据
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.AdvancedAPIs.Card
{
    /// <summary>
    ///  创建支付后领取立减金活动接口数据
    /// </summary>
    public class CreateActivityData
    {
        public Info info { get; set; }
    }

    public class Info
    {
        public Basic_Info basic_info { get; set; }
        public Card_Info_List[] card_info_list { get; set; }
        public Custom_Info custom_info { get; set; }
    }

    public class Basic_Info
    {
        public string activity_bg_color { get; set; }
        public string activity_tinyappid { get; set; }
        public int begin_time { get; set; }
        public int end_time { get; set; }
        public int gift_num { get; set; }
        public int max_partic_times_act { get; set; }
        public int max_partic_times_one_day { get; set; }
        public string mch_code { get; set; }
    }

    public class Custom_Info
    {
        public string type { get; set; }
    }

    public class Card_Info_List
    {
        public string card_id { get; set; }
        public int min_amt { get; set; }
        public string membership_appid { get; set; }
    }


    public class ApiConfirmAuthorizationData
    {
        public string component_appid { get; set; }
        public string authorizer_appid { get; set; }
        public int funcscope_category_id { get; set; }
        public int confirm_value { get; set; }
    }
    
}
