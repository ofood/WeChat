/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetWeAnalysisAppidDailyVisitTrendResultJson.cs
    文件功能描述：小程序“数据分析”接口 - 访问趋势：日趋势 返回结果
   
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OFoodWeChat.WxOpen.Entities;

namespace OFoodWeChat.WxOpen.AdvancedAPIs.DataCube
{
    /// <summary>
    /// 小程序“数据分析”接口 - 访问趋势：日趋势 返回结果
    /// </summary>
    public class GetWeAnalysisAppidDailyVisitTrendResultJson : WxOpenJsonResult
    {
        public List<GetWeAnalysisAppidDailyVisitTrendResultJson_list> list { get; set; }
    }

    /// <summary>
    /// 小程序“数据分析”接口 - 访问趋势：日趋势 返回结果 - list
    /// </summary>
    public class GetWeAnalysisAppidDailyVisitTrendResultJson_list
    {
        /// <summary>
        /// 日期，如：20170313
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 打开次数
        /// </summary>
        public int session_cnt { get; set; }
        /// <summary>
        /// 访问次数
        /// </summary>
        public int visit_pv { get; set; }
        /// <summary>
        /// 访问人数
        /// </summary>
        public int visit_uv { get; set; }
        /// <summary>
        /// 新用户数
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
