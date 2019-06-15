/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ResponseMessageVoice.cs
    文件功能描述：响应回复语音消息
----------------------------------------------------------------*/
using OFoodWeChat.Infrastructure.Entities;
using OFoodWeChat.Infrastructure.Enums;

namespace OFoodWeChat.Work.Entities
{
    /// <summary>
    /// 需要预先上传多媒体文件到微信服务器，只支持认证服务号。
    /// </summary>
    public class ResponseMessageVoice : WorkResponseMessageBase, IResponseMessageVoice
    {
        public new virtual ResponseMsgType MsgType
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
