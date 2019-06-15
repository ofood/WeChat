/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：AnalysisResultJson.cs
    文件功能描述：分析数据接口返回结果
----------------------------------------------------------------*/


using System.Collections.Generic;

namespace OFoodWeChat.MP.AdvancedAPIs.Analysis
{
    /// <summary>
    /// 分析数据接口返回结果
    /// </summary>
    public class AnalysisResultJson<T> : BaseAnalysisResult
    {
        public List<T> list
        {
            get { return base.ListObj as List<T>; }
            set { base.ListObj = value; }
        }

        public AnalysisResultJson()
        {
            ListObj = new List<T>();
        }
    }

}
