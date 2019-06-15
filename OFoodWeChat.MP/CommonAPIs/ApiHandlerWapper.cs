/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ApiHandlerWapper.cs（v12之前原AccessTokenHandlerWapper.cs）
    文件功能描述：使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150703
    修改描述：添加TryCommonApi()方法
 
    修改标识：Senparc - 20170102
    修改描述：v14.3.116 TryCommonApi抛出ErrorJsonResultException、WeixinException异常时加入了accessTokenOrAppId参数

    修改标识：Senparc - 20170123
    修改描述：v14.3.121 TryCommonApiAsync方法返回代码改为return await result;避免死锁。

    修改标识：Senparc - 20180917
    修改描述：BaseContainer.GetFirstOrDefaultAppId() 方法添加 PlatformType 属性

----------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Core.CommonAPIs.ApiHandlerWapper;
using OFoodWeChat.MP.Containers;
using OFoodWeChat.Infrastructure.Data.JsonResult;
using OFoodWeChat.MP.Entities;

namespace OFoodWeChat.MP
{
    /// <summary>
    /// 针对AccessToken无效或过期的自动处理类
    /// </summary>
    public static class ApiHandlerWapper
    {
        #region 同步方法

        /// <summary>
        /// 使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试。
        /// 使用此方法之前必须使用AccessTokenContainer.Register(_appId, _appSecret);或JsApiTicketContainer.Register(_appId, _appSecret);方法对账号信息进行过注册，否则会出错。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <param name="accessTokenOrAppId">AccessToken或AppId。如果为null，则自动取已经注册的第一个appId/appSecret来信息获取AccessToken。</param>
        /// <param name="retryIfFaild">请保留默认值true，不用输入。</param>
        /// <returns></returns>
        public static T TryCommonApi<T>(Func<string, T> fun, string accessTokenOrAppId = null, bool retryIfFaild = true) where T : WxJsonResult
        {

            Func<string> accessTokenContainer_GetFirstOrDefaultAppIdFunc =
                () => AccessTokenContainer.GetFirstOrDefaultAppId(PlatformType.WeChat_OfficialAccount);

            Func<string, bool> accessTokenContainer_CheckRegisteredFunc =
                appId => AccessTokenContainer.CheckRegistered(appId);

            Func<string, bool, IAccessTokenResult> accessTokenContainer_GetAccessTokenResultFunc =
                (appId, getNewToken) => AccessTokenContainer.GetAccessTokenResult(appId, getNewToken);

            int invalidCredentialValue = (int)ReturnCode.获取access_token时AppSecret错误或者access_token无效;

            var result = ApiHandlerWapperBase.
                TryCommonApiBase(
                    PlatformType.WeChat_OfficialAccount,
                    accessTokenContainer_GetFirstOrDefaultAppIdFunc,
                    accessTokenContainer_CheckRegisteredFunc,
                    accessTokenContainer_GetAccessTokenResultFunc,
                    invalidCredentialValue,
                    fun, accessTokenOrAppId, retryIfFaild);
            return result;
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试。
        /// 使用此方法之前必须使用AccessTokenContainer.Register(_appId, _appSecret);或JsApiTicketContainer.Register(_appId, _appSecret);方法对账号信息进行过注册，否则会出错。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <param name="accessTokenOrAppId">AccessToken或AppId。如果为null，则自动取已经注册的第一个appId/appSecret来信息获取AccessToken。</param>
        /// <param name="retryIfFaild">请保留默认值true，不用输入。</param>
        /// <returns></returns>
        public static async Task<T> TryCommonApiAsync<T>(Func<string, Task<T>> fun, string accessTokenOrAppId = null, bool retryIfFaild = true) where T : WxJsonResult
        {
            Func<string> accessTokenContainer_GetFirstOrDefaultAppIdFunc =
                () => AccessTokenContainer.GetFirstOrDefaultAppId(PlatformType.WeChat_OfficialAccount);

            Func<string, bool> accessTokenContainer_CheckRegisteredFunc =
                appId => AccessTokenContainer.CheckRegistered(appId);

            Func<string, bool, Task<IAccessTokenResult>> accessTokenContainer_GetAccessTokenResultAsyncFunc =
                (appId, getNewToken) => AccessTokenContainer.GetAccessTokenResultAsync(appId, getNewToken);

            int invalidCredentialValue = (int)ReturnCode.获取access_token时AppSecret错误或者access_token无效;

            var result = ApiHandlerWapperBase.
                TryCommonApiBaseAsync(
                    PlatformType.WeChat_OfficialAccount,
                    accessTokenContainer_GetFirstOrDefaultAppIdFunc,
                    accessTokenContainer_CheckRegisteredFunc,
                    accessTokenContainer_GetAccessTokenResultAsyncFunc,
                    invalidCredentialValue,
                    fun, accessTokenOrAppId, retryIfFaild);
            return await result;
        }
        #endregion
    }
}
