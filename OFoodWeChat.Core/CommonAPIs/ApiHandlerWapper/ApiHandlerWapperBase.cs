/*----------------------------------------------------------------    
    文件名：ApiHandlerWapperBase.cs
    文件功能描述：提供ApiHandlerWapper的公共基础方法
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.Core.Exceptions;
using OFoodWeChat.Core.Utilities;
using OFoodWeChat.Infrastructure.Data.JsonResult;
using OFoodWeChat.Infrastructure.Enums;

namespace OFoodWeChat.Core.CommonAPIs.ApiHandlerWapper
{
    /// <summary>
    /// 所有子模块ApiHandlerWapper方法调用的基础方法
    /// </summary>
    public class ApiHandlerWapperBase
    {
        #region 同步方法

        /// <summary>
        /// TryCommonApi 方法的基类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="platformType">平台类型，PlatformType枚举</param>
        /// <param name="accessTokenContainer_GetFirstOrDefaultAppIdFunc">AccessTokenContainer中的GetFirstOrDefaultAppId()方法</param>
        /// <param name="accessTokenContainer_CheckRegisteredFunc">AccessTokenContainer中的bool CheckRegistered(appId,getNew)方法</param>
        /// <param name="accessTokenContainer_GetAccessTokenResultFunc">AccessTokenContainer中的AccessTokenResult GetAccessTokenResult(appId)方法</param>
        /// <param name="invalidCredentialValue">"ReturnCode.获取access_token时AppSecret错误或者access_token无效"枚举的值</param>
        /// <param name="fun"></param>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="retryIfFaild"></param>
        /// <returns></returns>
        public static T TryCommonApiBase<T>(
            PlatformType platformType,
            Func<string> accessTokenContainer_GetFirstOrDefaultAppIdFunc,
            Func<string, bool> accessTokenContainer_CheckRegisteredFunc,
            Func<string, bool, IAccessTokenResult> accessTokenContainer_GetAccessTokenResultFunc,
            int invalidCredentialValue,
            Func<string, T> fun, string accessTokenOrAppId = null, bool retryIfFaild = true) where T : BaseJsonResult
        {

            //ApiHandlerWapperFactory.ApiHandlerWapperFactoryCollection["s"] = ()=> new Senparc.Weixin.MP.AdvancedAPIs.User.UserInfoJson();

            //var platform = ApiHandlerWapperFactory.CurrentPlatform;//当前平台

            /*
             * 对于企业微信来说，AppId = key = CorpId+CorpSecret
             */

            string appId = null;
            string accessToken = null;

            if (accessTokenOrAppId == null)
            {
                appId = accessTokenContainer_GetFirstOrDefaultAppIdFunc != null ? accessTokenContainer_GetFirstOrDefaultAppIdFunc() : null;// AccessTokenContainer.GetFirstOrDefaultAppId();
                if (appId == null)
                {
                    throw new UnRegisterAppIdException(null,
                        "尚无已经注册的AppId，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！模块：" + platformType);
                }
            }
            else if (WxApiUtility.IsAppId(accessTokenOrAppId, platformType))
            {
                //if (!AccessTokenContainer.CheckRegistered(accessTokenOrAppId))
                if (!accessTokenContainer_CheckRegisteredFunc(accessTokenOrAppId))
                {
                    throw new UnRegisterAppIdException(accessTokenOrAppId, string.Format("此appId（{0}）尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！模块：" + platformType, accessTokenOrAppId));
                }

                appId = accessTokenOrAppId;
            }
            else
            {
                accessToken = accessTokenOrAppId;//accessToken
            }

            T result = null;

            try
            {
                if (accessToken == null)
                {
                    var accessTokenResult = accessTokenContainer_GetAccessTokenResultFunc(appId, false); //AccessTokenContainer.GetAccessTokenResult(appId, false);
                    accessToken = accessTokenResult.access_token;
                }
                result = fun(accessToken);
            }
            catch (ErrorJsonResultException ex)
            {
                if (retryIfFaild
                    && appId != null    //如果 appId 为 null，已经没有重试的意义（直接提供的 AccessToken 是错误的）
                                        //&& ex.JsonResult.errcode == ReturnCode.获取access_token时AppSecret错误或者access_token无效)
                    && (int)ex.JsonResult.ErrorCodeValue == invalidCredentialValue)
                {
                    //尝试重新验证
                    var accessTokenResult = accessTokenContainer_GetAccessTokenResultFunc(appId, true);//AccessTokenContainer.GetAccessTokenResult(appId, true);
                    //强制获取并刷新最新的AccessToken
                    accessToken = accessTokenResult.access_token;
                    result = TryCommonApiBase(platformType,
                                accessTokenContainer_GetFirstOrDefaultAppIdFunc,
                                accessTokenContainer_CheckRegisteredFunc,
                                accessTokenContainer_GetAccessTokenResultFunc,
                                invalidCredentialValue,
                                fun, accessToken, false);
                }
                else
                {
                    ex.AccessTokenOrAppId = accessTokenOrAppId;
                    throw;
                }
            }
            catch (WeixinException ex)
            {
                ex.AccessTokenOrAppId = accessTokenOrAppId;
                throw;
            }

            return result;
        }


        #endregion


        #region 异步方法


        /// <summary>
        /// TryCommonApi 方法的基类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="platformType">平台类型，PlatformType枚举</param>
        /// <param name="accessTokenContainer_GetFirstOrDefaultAppIdFunc">AccessTokenContainer中的GetFirstOrDefaultAppId()方法</param>
        /// <param name="accessTokenContainer_CheckRegisteredFunc">AccessTokenContainer中的bool CheckRegistered(appId,getNew)方法</param>
        /// <param name="accessTokenContainer_GetAccessTokenResultAsyncFunc">AccessTokenContainer中的AccessTokenResult GetAccessTokenResultAsync(appId)方法（异步方法）</param>
        /// <param name="invalidCredentialValue">"ReturnCode.获取access_token时AppSecret错误或者access_token无效"枚举的值</param>
        /// <param name="fun"></param>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="retryIfFaild"></param>
        /// <returns></returns>
        public static async Task<T> TryCommonApiBaseAsync<T>(
            PlatformType platformType,
            Func<string> accessTokenContainer_GetFirstOrDefaultAppIdFunc,
            Func<string, bool> accessTokenContainer_CheckRegisteredFunc,
            Func<string, bool, Task<IAccessTokenResult>> accessTokenContainer_GetAccessTokenResultAsyncFunc,
            int invalidCredentialValue,
            Func<string, Task<T>> fun, string accessTokenOrAppId = null, bool retryIfFaild = true) where T : BaseJsonResult
        {

            //ApiHandlerWapperFactory.ApiHandlerWapperFactoryCollection["s"] = ()=> new Senparc.Weixin.MP.AdvancedAPIs.User.UserInfoJson();

            //var platform = ApiHandlerWapperFactory.CurrentPlatform;//当前平台

            string appId = null;
            string accessToken = null;

            if (accessTokenOrAppId == null)
            {
                appId = accessTokenContainer_GetFirstOrDefaultAppIdFunc();// AccessTokenContainer.GetFirstOrDefaultAppId();
                if (appId == null)
                {
                    throw new UnRegisterAppIdException(null,
                        "尚无已经注册的AppId，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！");
                }
            }
            else if (WxApiUtility.IsAppId(accessTokenOrAppId, platformType))
            {
                //if (!AccessTokenContainer.CheckRegistered(accessTokenOrAppId))
                if (!accessTokenContainer_CheckRegisteredFunc(accessTokenOrAppId))
                {
                    throw new UnRegisterAppIdException(accessTokenOrAppId,
                        string.Format("此appId（{0}）尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！",
                            accessTokenOrAppId));
                }

                appId = accessTokenOrAppId;
            }
            else
            {
                accessToken = accessTokenOrAppId; //accessToken
            }


            T result = null;

            try
            {
                if (accessToken == null)
                {
                    var accessTokenResult = await accessTokenContainer_GetAccessTokenResultAsyncFunc(appId, false);//AccessTokenContainer.GetAccessTokenResultAsync(appId, false);
                    accessToken = accessTokenResult.access_token;
                }
                result = await fun(accessToken);
            }
            catch (ErrorJsonResultException ex)
            {
                if (retryIfFaild
                    && appId != null    //如果 appId 为 null，已经没有重试的意义（直接提供的 AccessToken 是错误的）
                                        //&& ex.JsonResult.errcode == ReturnCode.获取access_token时AppSecret错误或者access_token无效)
                    && (int)ex.JsonResult.ErrorCodeValue == invalidCredentialValue)
                {
                    //尝试重新验证（如果是低版本VS，此处不能使用await关键字，可以直接使用xx.Result输出。VS2013不支持：无法在 catch 字句体中等待）
                    var accessTokenResult = await accessTokenContainer_GetAccessTokenResultAsyncFunc(appId, true);//AccessTokenContainer.GetAccessTokenResultAsync(appId, true);
                    //强制获取并刷新最新的AccessToken
                    accessToken = accessTokenResult.access_token;

                    result = await TryCommonApiBaseAsync(platformType,
                                accessTokenContainer_GetFirstOrDefaultAppIdFunc,
                                accessTokenContainer_CheckRegisteredFunc,
                                accessTokenContainer_GetAccessTokenResultAsyncFunc,
                                invalidCredentialValue,
                                fun, appId, false);
                    //result = TryCommonApiAsync(fun, appId, false);
                }
                else
                {
                    throw;
                }
            }
            return result;
        }


        #endregion


    }
}
