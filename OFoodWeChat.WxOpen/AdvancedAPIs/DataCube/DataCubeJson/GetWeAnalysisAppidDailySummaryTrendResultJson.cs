/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetWeAnalysisAppidDailySummaryTrendResultJson.cs
    文件功能描述：小程序“数据分析”接口 - 概况趋势 返回结果
    
    
    创建标识：Senparc - 20180101
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OFoodWeChat.WxOpen.Entities;

namespace OFoodWeChat.WxOpen.AdvancedAPIs.DataCube
{
    /// <summary>
    /// 小程序“数据分析”接口 - 概况趋势 返回结果
    /// </summary>
    public class GetWeAnalysisAppidDailySummaryTrendResultJson: WxOpenJsonResult
    {
        public List<GetWeAnalysisAppidDailySummaryTrendResultJson_list> list { get; set; }
    }

    /// <summary>
    /// 小程序“数据分析”接口 - 概况趋势 返回结果 - list
    /// </summary>
    public class GetWeAnalysisAppidDailySummaryTrendResultJson_list
    {
        /// <summary>
        /// 日期，如：20170313
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 累计用户数
        /// </summary>
        public int visit_total { get; set; }
        /// <summary>
        /// 转发次数
        /// </summary>
        public int share_pv { get; set; }
        /// <summary>
        /// 转发人数
        /// </summary>
        public int share_uv { get; set; }
    }

}
