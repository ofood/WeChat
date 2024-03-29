﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：Semantic_VideoResult.cs
    文件功能描述：语意理解接口视频服务（video）返回信息
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 视频服务（video）
    /// </summary>
    public class Semantic_VideoResult : BaseSemanticResultJson
    {
        public Semantic_Video semantic { get; set; }
    }

    public class Semantic_Video : BaseSemanticIntent
    {
        public Semantic_Details_Video details { get; set; }
    }

    public class Semantic_Details_Video
    {
        /// <summary>
        /// 视频名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 主演/嘉宾
        /// </summary>
        public string actor { get; set; }
        /// <summary>
        /// 导演/主持人
        /// </summary>
        public string director { get; set; }
        /// <summary>
        /// 视频类型：MOVIE（电影），TV（电视剧），COMIC（动漫），SHOW（综艺节目），OTHER（其他）
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 类型：动作片，剧情片，…
        /// </summary>
        public string tag { get; set; }
        /// <summary>
        /// 地区：美国，大陆，香港，…
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 季，部等
        /// </summary>
        public Semantic_Number season { get; set; }
        /// <summary>
        /// 集
        /// </summary>
        public Semantic_Number episode { get; set; }
        /// <summary>
        /// 排序类型：0好评（默认），1热门，2经典
        /// </summary>
        public int sort { get; set; }
    }
}
