﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：Semantic_NovelResult.cs
    文件功能描述：语意理解接口小说服务（novel）返回信息
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 小说服务（novel）
    /// </summary>
    public class Semantic_NovelResult : BaseSemanticResultJson
    {
        public Semantic_Novel semantic { get; set; }
    }

    public class Semantic_Novel : BaseSemanticIntent
    {
        public Semantic_Details_Novel details { get; set; }
    }

    public class Semantic_Details_Novel
    {
        /// <summary>
        /// 小说名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 小说作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 小说类型
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 小说章节
        /// </summary>
        public Semantic_Number chapter { get; set; }
        /// <summary>
        /// 排序类型：0排序无要求（默认），1热度高优先级，2时间升序，3时间降序
        /// </summary>
        public int sort { get; set; }
    }
}
