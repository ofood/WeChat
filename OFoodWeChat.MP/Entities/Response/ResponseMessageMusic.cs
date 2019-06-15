/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ResponseMessageMusic.cs
    文件功能描述：响应回复音乐消息
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Entities;

namespace OFoodWeChat.MP.Entities
{
    public class ResponseMessageMusic : ResponseMessageBase, IResponseMessageMusic
    {
        public override ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Music; }
        }

        public Music Music { get; set; }
        //public string ThumbMediaId { get; set; } //把该参数移动到Music对象内部
        public ResponseMessageMusic()
        {
            Music = new Music();
        }
    }
}
