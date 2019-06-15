/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：JsCode2JsonResult.cs
    文件功能描述：JsCode2Json接口结果
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.Open.Entities;

namespace OFoodWeChat.Open.WxaAPIs.Sns
{
    /// <summary>
    /// JsCode2Json接口结果
    /// </summary>
    public class JsCode2JsonResult : OpenJsonResult
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 会话密钥
        /// </summary>
        public string session_key { get; set; }
    }
}
