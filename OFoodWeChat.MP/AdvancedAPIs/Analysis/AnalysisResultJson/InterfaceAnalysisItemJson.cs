﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：InterfaceAnalysisItemJson.cs
    文件功能描述：获取接口分析数据返回结果 单条数据类
----------------------------------------------------------------*/


namespace OFoodWeChat.MP.AdvancedAPIs.Analysis
{

    public class BaseInterfaceAnalysisResult : BaseAnalysisObject
    {
        /// <summary>
        /// 数据的日期，需在begin_date和end_date之间
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 通过服务器配置地址获得消息后，被动回复用户消息的次数      
        /// </summary>
        public int callback_count { get; set; }
        /// <summary>
        /// 上述动作的失败次数
        /// </summary>
        public int fail_count { get; set; }
        /// <summary>
        /// 总耗时，除以callback_count即为平均耗时
        /// </summary>
        public int total_time_cost { get; set; }
        /// <summary>
        /// 最大耗时
        /// </summary>
        public int max_time_cost { get; set; }
    }

    /// <summary>
    /// 接口分析数据 单条数据
    /// </summary>
    public class InterfaceSummaryItem : BaseInterfaceAnalysisResult
    {

    }

    /// <summary>
    /// 接口分析分时数据 单条数据
    /// </summary>
    public class InterfaceSummaryHourItem : BaseInterfaceAnalysisResult
    {
        /// <summary>
        /// 数据的小时
        /// </summary>
        public int ref_hour { get; set; }
    }
}
