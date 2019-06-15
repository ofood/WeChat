/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ShakeAroundApi.cs
    文件功能描述：摇一摇周边接口
    
    
    创建标识：Senparc - 20150921

    修改标识：Senparc - 20170712
    修改描述：v14.5.1 AccessToken HandlerWaper改造
----------------------------------------------------------------*/

/*
    官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E8%8E%B7%E5%8F%96%E8%AE%BE%E5%A4%87%E5%8F%8A%E7%94%A8%E6%88%B7%E4%BF%A1%E6%81%AF
 */

using System.Threading.Tasks;
using OFoodWeChat.Work.AdvancedAPIs.ShakeAround;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core.CommonAPIs;

namespace OFoodWeChat.Work.AdvancedAPIs
{
    public static class ShakeAroundApi
    {
        #region 同步方法

        /// <summary>
        /// 获取设备及用户信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="ticket">摇周边业务的ticket，可在摇到的URL中得到，ticket生效时间为30分钟，每一次摇都会重新生成新的ticket</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ShakeAroundApi.GetSuiteToken", true)]
        public static GetShakeInfoResult GetSuiteToken(string accessTokenOrAppKey, string ticket, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/shakearound/getshakeinfo?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    ticket = ticket
                };

                return CommonJsonSend.Send<GetShakeInfoResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }
        #endregion

#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        /// 【异步方法】获取设备及用户信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="ticket">摇周边业务的ticket，可在摇到的URL中得到，ticket生效时间为30分钟，每一次摇都会重新生成新的ticket</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ShakeAroundApi.GetSuiteTokenAsync", true)]
        public static async Task<GetShakeInfoResult> GetSuiteTokenAsync(string accessTokenOrAppKey, string ticket, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/shakearound/getshakeinfo?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    ticket = ticket
                };

                return await CommonJsonSend.SendAsync<GetShakeInfoResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }
        #endregion
#endif
    }
}
