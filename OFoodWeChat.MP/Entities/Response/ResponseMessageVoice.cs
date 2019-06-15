/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ResponseMessageVoice.cs
    文件功能描述：响应回复语音消息
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Entities;

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 需要预先上传多媒体文件到微信服务器，只支持认证服务号。
    /// </summary>
    public class ResponseMessageVoice : ResponseMessageBase, IResponseMessageVoice
    {
        public override ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Voice; }
        }

        public Voice Voice { get; set; }

        public ResponseMessageVoice()
        {
            Voice = new Voice();
        }
    }
}
