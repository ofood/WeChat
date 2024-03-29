﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：AuthorizerOptionResult.cs
    文件功能描述：获取授权方的选项设置信息返回结果
----------------------------------------------------------------*/

using OFoodWeChat.Open.Entities;

namespace OFoodWeChat.Open.ComponentAPIs
{
    /// <summary>
    /// 获取授权方的选项设置信息返回结果
    /// </summary>
    public class AuthorizerOptionResult : OpenJsonResult
    {
        /// <summary>
        /// 第三方平台appid
        /// </summary>
        public string authorizer_appid { get; set; }
        /// <summary>
        /// 授权公众号appid
        /// </summary>
        public string option_name { get; set; }
        /// <summary>
        /// 选项名称
        /// </summary>
        public string option_value { get; set; }
    }
}