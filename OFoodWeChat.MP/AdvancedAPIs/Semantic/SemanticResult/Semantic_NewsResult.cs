/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：Semantic_NewsResult.cs
    文件功能描述：语意理解接口资讯服务（news）返回信息
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 资讯服务（news）
    /// </summary>
    public class Semantic_NewsResult : BaseSemanticResultJson
    {
        public Semantic_News semantic { get; set; }
    }

    public class Semantic_News : BaseSemanticIntent
    {
        public Semantic_Details_News details { get; set; }
    }

    public class Semantic_Details_News
    {
        /// <summary>
        /// 关键词
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 新闻类别
        /// </summary>
        public string category { get; set; }
    }
}
