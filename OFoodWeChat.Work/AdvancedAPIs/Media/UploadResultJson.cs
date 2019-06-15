/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：UploadResultJson.cs
    文件功能描述：上传媒体文件返回结果

----------------------------------------------------------------*/

using OFoodWeChat.Work.Entities;

namespace OFoodWeChat.Work.AdvancedAPIs.Media
{
    /// <summary>
    /// 上传临时媒体文件返回结果
    /// </summary>
    public class UploadTemporaryResultJson : WorkJsonResult
    {
        public UploadMediaFileType type { get; set; }
        public string media_id { get; set; }
        public long created_at { get; set; }
    }

    /// <summary>
    /// 上传永久素材返回结果
    /// </summary>
    public class UploadForeverResultJson : WorkJsonResult
    {
        public string media_id { get; set; }
    }
}
