/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：LibraryListJsonResult.cs
    文件功能描述：“获取小程序模板库标题列表”接口：LibraryList 结果
    
    
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
    /// “获取小程序模板库标题列表”接口：LibraryList 结果
    /// </summary>
    public class LibraryListJsonResult : WxOpenJsonResult
    {
        public List<LibraryListJsonResult_List> list { get; set; }
        /// <summary>
        /// 模板库标题总数
        /// </summary>
        public int total_count { get; set; }
    }

    public class LibraryListJsonResult_List
    {
        /// <summary>
        /// 模板标题id（获取模板标题下的关键词库时需要）
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 模板标题内容
        /// </summary>
        public string title { get; set; }
    }
}
