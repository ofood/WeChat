/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SingleMediaIdButton.cs
    文件功能描述：下发消息（除文本消息）按钮
    
    
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
    public class SingleMediaIdButton : SingleButton
    {
        /// <summary>
        /// 下发消息（除文本消息）用户点击media_id类型按钮后，微信服务器会将开发者填写的永久素材id对应的素材下发给用户，永久素材类型可以是图片、音频、视频、图文消息。请注意：永久素材id必须是在“素材管理/新增永久素材”接口上传后获得的合法id。
        /// </summary>
        public string media_id { get; set; }

        public SingleMediaIdButton()
            : base(MenuButtonType.media_id.ToString())
        {
        }
    }
}
