/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：BaseAnalysisResult.cs
    文件功能描述：分析数据接口返回结果基类
----------------------------------------------------------------*/
using OFoodWeChat.MP.Entities;

namespace OFoodWeChat.MP.AdvancedAPIs.Analysis
{
    public interface IBaseAnalysisResult
    {
        object ListObj { get; set; }
    }

    public abstract class BaseAnalysisResult : WxJsonResult, IBaseAnalysisResult
    {
        public object ListObj { get; set; }

    }
}
