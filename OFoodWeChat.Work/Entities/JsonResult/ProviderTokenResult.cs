/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ProviderTokenResult.cs
    文件功能描述：获取应用提供商凭证返回格式

----------------------------------------------------------------*/

using System;
using OFoodWeChat.Infrastructure.Data.JsonResult;

namespace OFoodWeChat.Work.Entities
{
    /// <summary>
    /// 获取应用提供商凭证返回格式
    /// </summary>
    [Serializable]
    public class ProviderTokenResult : WorkJsonResult
    {
        /// <summary>
        /// 服务提供商的accesstoken，可用于用户授权登录信息查询接口
        /// </summary>
        public string provider_access_token { get; set; }
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }
    }
}
