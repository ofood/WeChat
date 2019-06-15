/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：AutoReplyApi.cs
    文件功能描述：获取自动回复规则接口
----------------------------------------------------------------*/

/*
    Api地址：http://mp.weixin.qq.com/wiki/7/7b5789bb1262fb866d01b4b40b0efecb.html
 */



using System.Threading.Tasks;
using OFoodWeChat.MP.AdvancedAPIs.AutoReply;
using OFoodWeChat.MP.CommonAPIs;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Core;

namespace OFoodWeChat.MP.AdvancedAPIs
{
    /// <summary>
    /// 获取自动回复规则
    /// </summary>
    public static class AutoReplyApi
    {
        #region 同步方法

        /// <summary>
        /// 获取自动回复规则
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "AutoReplyApi.GetCurrentAutoreplyInfo", true)]
        public static GetCurrentAutoreplyInfoResult GetCurrentAutoreplyInfo(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/get_current_autoreply_info?access_token={0}";

                return CommonJsonSend.Send<GetCurrentAutoreplyInfoResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);

            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】获取自动回复规则
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "AutoReplyApi.GetCurrentAutoreplyInfoAsync", true)]
        public static async Task<GetCurrentAutoreplyInfoResult> GetCurrentAutoreplyInfoAsync(string accessTokenOrAppId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/get_current_autoreply_info?access_token={0}";

                return await CommonJsonSend.SendAsync<GetCurrentAutoreplyInfoResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);

            }, accessTokenOrAppId);
        }
        #endregion
    }
}