/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ConcernApi.cs
    文件功能描述：二次验证接口

----------------------------------------------------------------*/

/*
    官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E5%85%B3%E6%B3%A8%E4%B8%8E%E5%8F%96%E6%B6%88%E5%85%B3%E6%B3%A8
 */

using System.Threading.Tasks;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Work.Entities;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure.Extensions;

namespace OFoodWeChat.Work.AdvancedAPIs
{
    /// <summary>
    /// 关注与取消关注
    /// </summary>
    public static class ConcernApi
    {
        #region 同步方法

        /// <summary>
        /// 二次验证
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ConcernApi.TwoVerification", true)]
        public static WorkJsonResult TwoVerification(string accessTokenOrAppKey, string userId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/user/authsucc?access_token={0}&userid={1}", accessToken.AsUrlData(), userId.AsUrlData());
                return CommonJsonSend.Send<WorkJsonResult>(null, url, null, CommonJsonSendType.GET);
            }, accessTokenOrAppKey);


        }
        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】二次验证
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ConcernApi.TwoVerificationAsync", true)]
        public static async Task<WorkJsonResult> TwoVerificationAsync(string accessTokenOrAppKey, string userId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/user/authsucc?access_token={0}&userid={1}", accessToken.AsUrlData(), userId.AsUrlData());
                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, null, CommonJsonSendType.GET);
            }, accessTokenOrAppKey);


        }
        #endregion
#endif
    }
}
