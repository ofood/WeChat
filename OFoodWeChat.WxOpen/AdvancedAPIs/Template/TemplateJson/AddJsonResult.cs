/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：AddJsonResult.cs
    文件功能描述：“获取模板库某个模板标题下关键词库”接口：Add 结果
    
    
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
    /// “获取模板库某个模板标题下关键词库”接口：Add 结果
    /// </summary>
    public class AddJsonResult : WxOpenJsonResult
    {
        /// <summary>
        /// 添加至帐号下的模板id，发送小程序模板消息时所需
        /// </summary>
        public string template_id { get; set; }
    }
}
