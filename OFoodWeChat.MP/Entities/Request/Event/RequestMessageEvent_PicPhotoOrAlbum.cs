/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_PicPhotoOrAlbum.cs
    文件功能描述：事件之弹出拍照或者相册发图（pic_photo_or_album）
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Entities;

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 事件之弹出拍照或者相册发图（pic_photo_or_album）
    /// </summary>
    public class RequestMessageEvent_Pic_Photo_Or_Album : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.pic_photo_or_album; }
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 发送的图片信息
        /// </summary>
        public SendPicsInfo SendPicsInfo { get; set; }
    }
}
