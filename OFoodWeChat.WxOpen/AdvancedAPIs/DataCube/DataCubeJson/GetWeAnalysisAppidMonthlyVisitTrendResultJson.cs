/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetWeAnalysisAppidMonthlyVisitTrendResultJson.cs
    文件功能描述：小程序“数据分析”接口 - 访问趋势：月趋势 返回结果
    
    
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
    /// 小程序“数据分析”接口 - 访问趋势：月趋势 返回结果
    /// </summary>
    public class GetWeAnalysisAppidMonthlyVisitTrendResultJson : WxOpenJsonResult
    {
        public List<GetWeAnalysisAppidMonthlyVisitTrendResultJson_list> list { get; set; }
    }

    /// <summary>
    /// 小程序“数据分析”接口 - 访问趋势：月趋势 返回结果 - list
    /// </summary>
    public class GetWeAnalysisAppidMonthlyVisitTrendResultJson_list
    {
        /// <summary>
        /// 时间，如："201702"
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 打开次数（自然月内汇总）
        /// </summary>
        public int session_cnt { get; set; }
        /// <summary>
        /// 访问次数（自然月内汇总）
        /// </summary>
        public int visit_pv { get; set; }
        /// <summary>
        /// 访问人数（自然月内去重）
        /// </summary>
        public int visit_uv { get; set; }

        /// <summary>
        /// 新用户数（自然月内去重）
        /// </summary>
        public int visit_uv_new { get; set; }
        /// <summary>
        /// 人均停留时长 (浮点型，单位：秒)
        /// </summary>
        public double stay_time_uv { get; set; }
        /// <summary>
        /// 次均停留时长 (浮点型，单位：秒)
        /// </summary>
        public double stay_time_session { get; set; }
        /// <summary>
        /// 平均访问深度 (浮点型)
        /// </summary>
        public double visit_depth { get; set; }
    }

}
