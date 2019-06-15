/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：Semantic_WebsiteResult.cs
    文件功能描述：语意理解接口网址服务（website）返回信息
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 网址服务（website）
    /// </summary>
    public class Semantic_WebsiteResult : BaseSemanticResultJson
    {
        public Semantic_Website semantic { get; set; }
    }

    public class Semantic_Website : BaseSemanticIntent
    {
        public Semantic_Details_Website details { get; set; }
    }

    public class Semantic_Details_Website
    {
        /// <summary>
        /// 网址名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// url
        /// </summary>
        public string url { get; set; }
    }
}