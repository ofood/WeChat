/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：Music.cs
    文件功能描述：响应回复消息 音乐类
----------------------------------------------------------------*/

namespace OFoodWeChat.Infrastructure.Entities
{
    /// <summary>
    /// Music
    /// </summary>
    public class Music
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string MusicUrl { get; set; }
        public string HQMusicUrl { get; set; }
        ///// <summary>
        ///// 缩略图的媒体id，通过上传多媒体文件，得到的id
        ///// 官方API上有，但是加入的话会出错
        ///// </summary>
        public string ThumbMediaId { get; set; }
    }
}
