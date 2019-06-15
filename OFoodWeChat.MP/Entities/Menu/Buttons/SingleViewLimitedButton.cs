
/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SingleViewLimitedButton.cs
    文件功能描述：跳转图文消息URL按钮
    
    
    创建标识：Senparc - 20170824

    修改标识：Senparc - 20181005
    修改描述：菜单按钮类型（ButtonType）改为使用 Senparc.NeuChar.MenuButtonType
    
----------------------------------------------------------------*/
using OFoodWeChat.Infrastructure.Enums;
namespace OFoodWeChat.MP.Entities.Menu
{
    /// <summary>
    /// 下发消息（除文本消息）按钮
    /// </summary>
    public class SingleViewLimitedButton : SingleButton
    {
        /// <summary>
        /// 用户点击view_limited类型按钮后，微信客户端将打开开发者在按钮中填写的永久素材id对应的图文消息URL，永久素材类型只支持图文消息。请注意：永久素材id必须是在“素材管理/新增永久素材”接口上传后获得的合法id。
        /// </summary>
        public string media_id { get; set; }

        public SingleViewLimitedButton()
            : base(MenuButtonType.view_limited.ToString())
        {
        }
    }
}
