/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：Video.cs
    文件功能描述：响应回复消息 视频类
----------------------------------------------------------------*/

namespace OFoodWeChat.Infrastructure.Entities
{
    /// <summary>
    /// Video
    /// </summary>
    public class Video
    {
        public string MediaId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
