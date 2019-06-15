using OFoodWeChat.WxOpen.Entities;
using System.Collections.Generic;

namespace OFoodWeChat.WxOpen.AdvancedAPIs.WxApp.WxAppJson
{
    /// <summary>
    /// 获取当前所有插件使用方返回结果
    /// </summary>
    public class DevPluginResultJson : WxOpenJsonResult
    {
        public List<ApplyItem> apply_list { get; set; }
    }

    public class ApplyItem
    {
        public string appid { get; set; }
        public int status { get; set; }
        public string nickname { get; set; }
        public string headimgurl { get; set; }
    }

    public class GetPluginListResultJson : WxOpenJsonResult
    {
        public Plugin_List[] plugin_list { get; set; }
    }

    public class Plugin_List
    {
        public string appid { get; set; }
        public int status { get; set; }
        public string nickname { get; set; }
        public string headimgurl { get; set; }
    }

}
