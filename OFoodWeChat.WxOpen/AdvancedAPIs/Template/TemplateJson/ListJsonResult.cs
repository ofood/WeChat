/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ListJsonResult.cs
    文件功能描述：“获取帐号下已存在的模板列表”接口：List 结果
    
    
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
    /// “获取帐号下已存在的模板列表”接口：List 结果
    /// </summary>
    public class ListJsonResult : WxOpenJsonResult
    {
        /// <summary>
        /// 帐号下的模板列表
        /// </summary>
        public List<ListJsonResult_List> list { get; set; }
    }

    public class ListJsonResult_List
    {
        /// <summary>
        /// 模板id，发送小程序模板消息时所需
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 模板标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 模板内容示例
        /// </summary>
        public string example { get; set; }
    }
}
