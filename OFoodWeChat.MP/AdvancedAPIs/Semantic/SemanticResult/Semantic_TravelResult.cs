﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：Semantic_TravelResult.cs
    文件功能描述：语意理解接口旅游服务（travel）返回信息
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.AdvancedAPIs.Semantic
{
    /// <summary>
    /// 旅游服务（travel）
    /// </summary>
    public class Semantic_TravelResult : BaseSemanticResultJson
    {
        public Semantic_Travel semantic { get; set; }
    }

    public class Semantic_Travel : BaseSemanticIntent
    {
        public Semantic_Details_Travel details { get; set; }
        /// <summary>
        /// SEARCH 普通查询
        /// PRICE 价格查询
        /// GUIDE 攻略查询
        /// </summary>
        public string intent { get; set; }
    }

    public class Semantic_Details_Travel
    {
        /// <summary>
        /// 旅游目的地
        /// </summary>
        public Semantic_Location location { get; set; }
        /// <summary>
        /// 景点名称
        /// </summary>
        public string spot { get; set; }
        /// <summary>
        /// 旅游日期
        /// </summary>
        public Semantic_DateTime datetime { get; set; }
        /// <summary>
        /// 旅游类型词
        /// </summary>
        public string tag { get; set; }
        /// <summary>
        /// 0默认，1自由行，2跟团游
        /// </summary>
        public int category { get; set; }
    }
}
