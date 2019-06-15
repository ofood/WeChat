/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetMenuResultFull.cs
    文件功能描述：获取菜单时候的完整结构，用于接收微信服务器返回的Json信息
----------------------------------------------------------------*/

using System.Collections.Generic;
using OFoodWeChat.MP.AdvancedAPIs.AutoReply;
using OFoodWeChat.MP.Entities.Menu;
using OFoodWeChat.MP.Entities;

namespace OFoodWeChat.MP
{
    #region GetMenuResultFull 相关
    /// <summary>
    /// 获取菜单时候的完整结构，用于接收微信服务器返回的Json信息
    /// 注：menu为默认菜单，conditionalmenu为个性化菜单列表。字段说明请见个性化菜单接口页的说明。
    /// </summary>
    public class GetMenuResultFull : WxJsonResult
    {
        public MenuFull_ButtonGroup menu { get; set; }

        /// <summary>
        /// 有个性化菜单时显示。最新的在最前。
        /// </summary>
        public List<MenuFull_ConditionalButtonGroup> conditionalmenu { get; set; }
    }

    public class MenuFull_ButtonGroup
    {
        public List<MenuFull_RootButton> button { get; set; }
    }

    public class MenuFull_RootButton
    {
        public string type { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        public NewsInfo news_info { get; set; }

        #region 小程序

        public string appid { get; set; }
        public string pagepath { get; set; }

        #endregion

        public string media_id { get; set; }

        public List<MenuFull_RootButton> sub_button { get; set; }
    }
    #endregion

    #region Conditional（个性化菜单）相关
    public class MenuTryMatchResult : WxJsonResult
    {
        public List<MenuFull_RootButton> button { get; set; }
    }

    /// <summary>
    /// 自定义菜单配置
    /// </summary>
    public class SelfMenuConfigResult : WxJsonResult
    {
        /// <summary>
        /// 菜单是否开启，0代表未开启，1代表开启
        /// </summary>
        public bool is_menu_open { get; set; }

        /// <summary>
        /// 菜单信息
        /// </summary>
        public MenuFull_ButtonGroup selfmenu_info { get; set; }

    }

    /// <summary>
    /// 接收菜单信息时用的“最大可能性”类型
    /// </summary>
    public class MenuFull_ConditionalButtonGroup: MenuFull_ButtonGroup
    {
        public MenuMatchRule matchrule { get; set; }
        /// <summary>
        /// 菜单Id，只在获取的时候自动填充，提交“菜单创建”请求时不需要设置
        /// </summary>
        public long menuid { get; set; }
    }


    #endregion

}
