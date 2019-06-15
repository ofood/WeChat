/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：LibraryListJsonResult.cs
    文件功能描述：“获取模板库某个模板标题下关键词库”接口：LibraryGet 结果
    
    
    创建标识：Senparc - 20170827

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.WxOpen.Entities;
namespace OFoodWeChat.WxOpen.AdvancedAPIs.Template
{
    /// <summary>
    /// “获取模板库某个模板标题下关键词库”接口：LibraryGet 结果
    /// </summary>
    public class LibraryGetJsonResult : WxOpenJsonResult
    {
        public string id { get; set; }
        public string title { get; set; }
        public List<LibraryGetJsonResult_KeywordList> keyword_list { get; set; }
    }

    public class LibraryGetJsonResult_KeywordList
    {
        /// <summary>
        /// 关键词id，添加模板时需要
        /// </summary>
        public int keyword_id { get; set; }
        /// <summary>
        /// 关键词内容
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 关键词内容对应的示例
        /// </summary>
        public string example { get; set; }
    }
}
