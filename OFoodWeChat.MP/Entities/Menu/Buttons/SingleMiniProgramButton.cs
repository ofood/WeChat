/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SingleMiniProgramButton.cs
    文件功能描述：小程序按钮
    
    
    创建标识：Senparc - 20170327

    修改标识：Senparc - 20181005
    修改描述：菜单按钮类型（ButtonType）改为使用 Senparc.NeuChar.MenuButtonType

----------------------------------------------------------------*/
using OFoodWeChat.Infrastructure.Enums;
namespace OFoodWeChat.MP.Entities.Menu
{
    /// <summary>
    /// 小程序按钮
    /// </summary>
    public class SingleMiniProgramButton : SingleButton
    {
        /// <summary>
        /// 类型为miniprogram时必须
        /// 小程序Url，用户点击按钮可打开小程序，不超过1024字节（不支持小程序的老版本客户端将打开本url）
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 小程序的appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 小程序的页面路径
        /// </summary>
        public string pagepath { get; set; }

        public SingleMiniProgramButton()
            : base(MenuButtonType.miniprogram.ToString())
        {
        }
    }
}
