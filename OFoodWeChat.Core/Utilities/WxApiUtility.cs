/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：StreamUtility.cs
    文件功能描述：微信对象公共类
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Enums;
using System;

namespace OFoodWeChat.Core.Utilities
{
    /// <summary>
    /// 微信 API 工具类
    /// </summary>
    public static class WxApiUtility
    {
        /// <summary>
        /// 判断accessTokenOrAppId参数是否是AppId（或对应企业微信的AppKey）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        public static bool IsAppId(string accessTokenOrAppId, PlatformType platFormType)
        {
            return WxConfig.DefaultAppIdCheckFunc(accessTokenOrAppId, platFormType);
        }
    }
}
