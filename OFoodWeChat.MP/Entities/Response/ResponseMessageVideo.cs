/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ResponseMessageVideo.cs
    文件功能描述：响应回复视频消息
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Entities;

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 需要预先上传多媒体文件到微信服务器，只支持认证服务号。
    /// </summary>
    public class ResponseMessageVideo : ResponseMessageBase, IResponseMessageVideo
    {
        public override ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Video; }
        }

        public Video Video { get; set; }

        public ResponseMessageVideo()
        {
            Video = new Video();
        }
    }
}
