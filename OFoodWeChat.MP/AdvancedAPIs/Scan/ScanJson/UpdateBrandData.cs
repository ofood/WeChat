/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：UpdateBrandData.cs
    文件功能描述：更新商品信息数据
----------------------------------------------------------------*/

using OFoodWeChat.MP.Entities;

namespace OFoodWeChat.MP.AdvancedAPIs
{
    /// <summary>
    /// 更新商品信息数据
    /// </summary>
    public class UpdateBrandData
    {
        public string keystandard { get; set; }
        public string keystr { get; set; }
        public Brand_Info brand_info { get; set; }
    }

    public class Brand_Info
    {
        public Action_Info action_info { get; set; }
    }

    public class Action_Info
    {
        public Action_List[] action_list { get; set; }
    }

    public class Action_List
    {
        public string type { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string image { get; set; }
        public string showtype { get; set; }
        public string appid { get; set; }
        public string text { get; set; }
    }

    /// <summary>
    /// 更新商品信息返回结果
    /// </summary>
    public class UpdateBrandResultJson : WxJsonResult
    {
        public string pid { get; set; }
    }
}
