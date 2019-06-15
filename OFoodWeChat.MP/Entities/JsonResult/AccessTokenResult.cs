/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：AccessTokenResult.cs
    文件功能描述：access_token请求后的JSON返回格式
----------------------------------------------------------------*/

using System;
using OFoodWeChat.Infrastructure.Data.JsonResult;
namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// access_token请求后的JSON返回格式
    /// </summary>
    [Serializable]
    public class AccessTokenResult : WxJsonResult, IAccessTokenResult
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }
    }
}
