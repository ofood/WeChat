/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：CommonGetWeAnalysisAppidRetainInfoResultJson.cs
    文件功能描述：小程序“数据分析”接口 - 访问留存：日/周/月留存 返回结果
    
    
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
    /// 小程序“数据分析”接口 - 访问留存：周留存 返回结果
    /// </summary>
    public class CommonGetWeAnalysisAppidRetainInfoResultJson : WxOpenJsonResult
    {
        /// <summary>
        /// 时间。
        /// 日存留如："20170313"；
        /// 周存留如："20170306-20170312"；
        /// 月存留如："201702"。
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 新增用户留存
        /// </summary>
        public List<CommonGetWeAnalysisAppidRetainInfoResultJson_visit> visit_uv_new { get; set; }
        /// <summary>
        /// 活跃用户留存
        /// </summary>
        public List<CommonGetWeAnalysisAppidRetainInfoResultJson_visit> visit_uv { get; set; }
    }

    /// <summary>
    /// 公共类：小程序“数据分析”接口 - 访问留存：日/周/月留存 返回结果 - visit_uv及相关属性
    /// </summary>
    public class CommonGetWeAnalysisAppidRetainInfoResultJson_visit
    {
        /// <summary>
        /// 标识。
        /// 日留存：0开始，0表示当天，1表示1天后，依此类推，key取值分别是：0,1,2,3,4,5,6,7,14,30；
        /// 周留存：0开始，0表示当周，1表示1周后，依此类推，key取值分别是：0,1,2,3,4；
        /// 月留存：标识，0开始，0表示当月，1表示1月后，key取值分别是：0,1。
        /// </summary>
        public int key { get; set; }
        /// <summary>
        /// key对应日期的新增用户数/活跃用户数（key=0时）或留存用户数（k>0时）
        /// </summary>
        public int value { get; set; }
    }
}
