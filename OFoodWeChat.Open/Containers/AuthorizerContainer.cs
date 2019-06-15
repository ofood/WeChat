﻿/*----------------------------------------------------------------
Copyright(C) 2018 Senparc

    文件名：AuthorizerContainer.cs
    文件功能描述：通用接口JsApiTicket容器，用于OPEN第三方JSSDK自动管理JsApiTicket，如果过期会重新获取
    
----------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using OFoodWeChat.Open.ComponentAPIs;

using OFoodWeChat.Core.Containers;
using OFoodWeChat.Open.Exceptions;
using OFoodWeChat.Infrastructure.Utilities;
using OFoodWeChat.Open.Entities;

namespace OFoodWeChat.Open.Containers
{
    /// <summary>
    /// 之前的JsApiTicketBag
    /// </summary>
    [Serializable]
    public class AuthorizerBag : BaseContainerBag
    {
        /// <summary>
        /// 授权方AppId，缓存中实际的Key
        /// </summary>
        public string AuthorizerAppId { get; set; }
        

        /// <summary>
        /// 第三方平台AppId
        /// </summary>
        public string ComponentAppId { get; set; }
        /// <summary>
        /// 授权信息
        /// </summary>
        public GetAuthorizerInfoResult FullAuthorizerInfoResult
        {
            get
            {
                var result = new GetAuthorizerInfoResult()
                {
                    authorizer_info = AuthorizerInfo,
                    authorization_info = AuthorizationInfo
                };
                return result;
            }
        }


        public JsApiTicketResult JsApiTicketResult { get; set; }


        public DateTimeOffset JsApiTicketExpireTime { get; set; }


        /// <summary>
        /// 授权信息（请使用TryUpdateAuthorizationInfo()方法进行更新）
        /// </summary>
        public AuthorizationInfo AuthorizationInfo { get; set; }


        public DateTimeOffset AuthorizationInfoExpireTime { get; set; }


        /// <summary>
        /// 授权方资料信息
        /// </summary>
        public AuthorizerInfo AuthorizerInfo { get; set; }

        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        internal object Lock = new object();
    }

    /// <summary>
    /// 授权方信息（用户的微信公众号）
    /// 包括通用接口JsApiTicket容器，用于自动管理JsApiTicket，如果过期会重新获取
    /// </summary>
    public class AuthorizerContainer : BaseContainer<AuthorizerBag>
    {
        const string LockResourceName = "Open.AuthorizerContainer";

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket，
        /// </summary>
        /// <param name="authorizerAppId"></param>
        /// <param name="componentAppId"></param>
        /// <param name="name">标记Authorizer名称（如微信公众号名称），帮助管理员识别</param>
        private static void Register(string componentAppId, string authorizerAppId, string name = null)
        {
            var componentBag = ComponentContainer.TryGetItem(componentAppId);
            if (componentBag == null)
            {
                throw new WeixinOpenException(string.Format("注册AuthorizerContainer之前，必须先注册对应的ComponentContainer！ComponentAppId：{0},AuthorizerAppId:{1}", componentAppId, authorizerAppId));
            }

            RegisterFunc = () =>
            {
                //using (FlushCache.CreateInstance())
                //{
                var bag = new AuthorizerBag()
                {
                    Name = name,

                    AuthorizerAppId = authorizerAppId,
                    ComponentAppId = componentAppId,

                    AuthorizationInfo = new AuthorizationInfo(),
                    AuthorizationInfoExpireTime = DateTimeOffset.MinValue,

                    AuthorizerInfo = new AuthorizerInfo(),
                    //AuthorizerInfoExpireTime = DateTimeOffset.MinValue,

                    JsApiTicketResult = new JsApiTicketResult(),
                    JsApiTicketExpireTime = DateTimeOffset.MinValue,
                };
                Update(authorizerAppId, bag, null);
                return bag;
                //}
            };
            RegisterFunc();

            //TODO：这里也可以考虑尝试进行授权（会影响速度）
        }


        #region 同步方法


        /// <summary>
        /// 尝试注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <returns></returns>
        private static void TryRegister(string componentAppId, string authorizerAppid)
        {
            if (!CheckRegistered(authorizerAppid))
            {
                Register(componentAppId, authorizerAppid);
            }
        }

        #region 授权信息

        /// <summary>
        /// 获取或更新AuthorizationInfo。
        /// 如果读取refreshToken失败，则返回null。
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static AuthorizationInfo GetAuthorizationInfo(string componentAppId, string authorizerAppid,
            bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var authorizerBag = TryGetItem(authorizerAppid);
            using (Cache.BeginCacheLock(LockResourceName + ".GetAuthorizationInfo", authorizerAppid))//同步锁
            {
                //更新Authorization
                if (getNewTicket || authorizerBag.AuthorizationInfoExpireTime <= SystemTime.Now)
                {
                    var componentVerifyTicket = ComponentContainer.TryGetComponentVerifyTicket(componentAppId);
                    var componentAccessToken = ComponentContainer.GetComponentAccessToken(componentAppId, componentVerifyTicket);

                    //获取新的AuthorizerAccessToken
                    var refreshToken = ComponentContainer.GetAuthorizerRefreshTokenFunc(componentAppId, authorizerAppid);

                    if (refreshToken == null)
                    {
                        return null;
                    }

                    var refreshResult = RefreshAuthorizerToken(componentAccessToken, componentAppId, authorizerAppid,
                        refreshToken);

                    //更新数据
                    TryUpdateAuthorizationInfo(componentAppId, authorizerAppid,
                        refreshResult.authorizer_access_token, refreshResult.authorizer_refresh_token, refreshResult.expires_in);

                    authorizerBag = TryGetItem(authorizerAppid);//外部缓存需要重新获取新数据
                }
            }
            return authorizerBag.AuthorizationInfo;
        }


        /// <summary>
        /// 获取可用AuthorizerAccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetAuthorizerAccessToken(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var authorizationInfo = GetAuthorizationInfo(componentAppId, authorizerAppid, getNewTicket);
            return authorizationInfo.authorizer_access_token;

            //v2.3.4 改用以上方法，避免authorization_info.authorizer_access_token值为空
            //return GetAuthorizerInfoResult(componentAppId, authorizerAppid, getNewTicket).authorization_info.authorizer_access_token;
        }

        /// <summary>
        /// 获取可用的GetAuthorizerInfoResult
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        ///// <exception cref="WeixinOpenException">此公众号没有高级权限</exception>
        public static GetAuthorizerInfoResult GetAuthorizerInfoResult(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var authorizerBag = TryGetItem(authorizerAppid);
            using (Cache.BeginCacheLock(LockResourceName + ".GetAuthorizerInfoResult", authorizerAppid))//同步锁
            {
                //更新AuthorizerInfo
                if (getNewTicket || authorizerBag.AuthorizerInfo.user_name == null)
                {
                    var componentVerifyTicket = ComponentContainer.TryGetComponentVerifyTicket(componentAppId);
                    var componentAccessToken = ComponentContainer.GetComponentAccessToken(componentAppId, componentVerifyTicket);

                    //已过期，重新获取
                    var getAuthorizerInfoResult = ComponentApi.GetAuthorizerInfo(componentAccessToken, componentAppId, authorizerAppid);//TODO:如果是过期，可以通过刷新的方式重新获取

                    //AuthorizerInfo
                    authorizerBag.AuthorizerInfo = getAuthorizerInfoResult.authorizer_info;

                    //AuthorizationInfo
                    var getAuthorizationInfoResult = GetAuthorizationInfo(componentAppId, authorizerAppid, getNewTicket);
                    authorizerBag.AuthorizationInfo = getAuthorizationInfoResult;

                    Update(authorizerBag, null);//更新到缓存

                    //var componentBag = ComponentContainer.TryGetItem(componentAppId);
                    //if (string.IsNullOrEmpty(authorizerBag.AuthorizerInfoResult.authorization_info.authorizer_access_token))
                    //{
                    //    //账号没有此权限
                    //    throw new WeixinOpenException("此公众号没有高级权限", componentBag);
                    //}
                }
            }
            return authorizerBag.FullAuthorizerInfoResult;
        }

        /// <summary>
        /// 尝试更新AuthorizationInfo（如果没有AccessToken则不更新）
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="authorizationInfo"></param>
        public static void TryUpdateAuthorizationInfo(string componentAppId, string authorizerAppid, AuthorizationInfo authorizationInfo)
        {
            TryRegister(componentAppId, authorizerAppid);

            if (authorizationInfo.expires_in > 0 && authorizationInfo.authorizer_access_token != null)
            {
                var authorizerBag = TryGetItem(authorizerAppid);

                var refreshTokenChanged = authorizerBag.AuthorizationInfo.authorizer_access_token !=
                                         authorizationInfo.authorizer_access_token
                                           || authorizerBag.AuthorizationInfo.authorizer_refresh_token !=
                                              authorizationInfo.authorizer_refresh_token;

                authorizerBag.AuthorizationInfo = authorizationInfo;
                authorizerBag.AuthorizationInfoExpireTime = ApiUtility.GetExpireTime(authorizationInfo.expires_in);

                Update(authorizerBag, null);//立即更新

                //通知变更
                if (refreshTokenChanged)
                {
                    ComponentContainer.AuthorizerTokenRefreshedFunc(componentAppId, authorizerAppid,
                        new RefreshAuthorizerTokenResult(authorizationInfo.authorizer_access_token,
                            authorizationInfo.authorizer_refresh_token, authorizationInfo.expires_in));
                }
            }
        }

        /// <summary>
        /// 尝试更新AuthorizationInfo（如果没有AccessToken则不更新）。
        /// 如果AuthorizerBag更新则返回最新的对象，否则返回null
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="authorizerAccessToken"></param>
        /// <param name="authorizerRefreshToken"></param>
        /// <param name="expiresIn"></param>
        public static void TryUpdateAuthorizationInfo(string componentAppId, string authorizerAppid, string authorizerAccessToken, string authorizerRefreshToken, int expiresIn)
        {
            TryRegister(componentAppId, authorizerAppid);

            if (expiresIn > 0 && authorizerAccessToken != null)
            {
                using (FlushCache.CreateInstance())
                {
                    var authorizerBag = TryGetItem(authorizerAppid);

                    var refreshTokenChanged = authorizerBag.AuthorizationInfo.authorizer_access_token !=
                                              authorizerAccessToken
                                              || authorizerBag.AuthorizationInfo.authorizer_refresh_token !=
                                              authorizerRefreshToken;

                    authorizerBag.AuthorizationInfo.authorizer_access_token = authorizerAccessToken;
                    authorizerBag.AuthorizationInfo.authorizer_refresh_token = authorizerRefreshToken;
                    authorizerBag.AuthorizationInfo.expires_in = expiresIn;
                    authorizerBag.AuthorizationInfoExpireTime = ApiUtility.GetExpireTime(expiresIn);

                    Update(authorizerBag, null);//立即更新

                    //通知变更
                    if (refreshTokenChanged)
                    {
                        ComponentContainer.AuthorizerTokenRefreshedFunc(componentAppId, authorizerAppid,
                            new RefreshAuthorizerTokenResult(authorizerAccessToken, authorizerRefreshToken, expiresIn));
                    }
                }
            }
        }

        /// <summary>
        /// 刷新AuthorizerToken
        /// </summary>
        /// <param name="componentAccessToken"></param>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public static RefreshAuthorizerTokenResult RefreshAuthorizerToken(string componentAccessToken, string componentAppId, string authorizerAppid,
                      string refreshToken)
        {
            var refreshResult = ComponentApi.ApiAuthorizerToken(componentAccessToken, componentAppId, authorizerAppid,
                         refreshToken);
            //更新到存储
            ComponentContainer.AuthorizerTokenRefreshedFunc(componentAppId, authorizerAppid, refreshResult);
            return refreshResult;
        }

        #endregion

        #region JSTicket


        /// <summary>
        /// 使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetJsApiTicket(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            return GetJsApiTicket(componentAppId, authorizerAppid, getNewTicket);
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetJsApiTicket(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            return GetJsApiTicketResult(componentAppId, authorizerAppid, getNewTicket).ticket;
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static JsApiTicketResult GetJsApiTicketResult(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var accessTicketBag = TryGetItem(authorizerAppid);
            using (Cache.BeginCacheLock(LockResourceName + ".GetJsApiTicketResult", authorizerAppid))//同步锁
            {
                if (getNewTicket || accessTicketBag.JsApiTicketExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    var authorizerAccessToken = TryGetAuthorizerAccessToken(componentAppId, authorizerAppid);

                    accessTicketBag.JsApiTicketResult = ComponentApi.GetJsApiTicket(authorizerAccessToken);

                    accessTicketBag.JsApiTicketExpireTime = ApiUtility.GetExpireTime(accessTicketBag.JsApiTicketResult.expires_in);

                    Update(accessTicketBag, null);//更新到缓存
                }
            }
            return accessTicketBag.JsApiTicketResult;
        }

        #endregion

        #endregion

#if !NET35 && !NET40
        #region 异步方法

        #region 授权信息

        /// <summary>
        /// 【异步方法】获取或更新AuthorizationInfo。
        /// 如果读取refreshToken失败，则返回null。
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static async Task<AuthorizationInfo> GetAuthorizationInfoAsync(string componentAppId, string authorizerAppid,
    bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var authorizerBag = TryGetItem(authorizerAppid);
            using (Cache.BeginCacheLock(LockResourceName + ".GetAuthorizationInfo", authorizerAppid))//同步锁
            {
                //更新Authorization
                if (getNewTicket || authorizerBag.AuthorizationInfoExpireTime <= SystemTime.Now)
                {
                    var componentVerifyTicket = ComponentContainer.TryGetComponentVerifyTicket(componentAppId);
                    var componentAccessToken = await ComponentContainer.GetComponentAccessTokenAsync(componentAppId, componentVerifyTicket);

                    //获取新的AuthorizerAccessToken
                    var refreshToken = ComponentContainer.GetAuthorizerRefreshTokenFunc(componentAppId, authorizerAppid);

                    if (refreshToken == null)
                    {
                        return null;
                    }

                    var refreshResult = await RefreshAuthorizerTokenAsync(componentAccessToken, componentAppId, authorizerAppid,
                        refreshToken);

                    //更新数据
                    TryUpdateAuthorizationInfo(componentAppId, authorizerAppid,
                        refreshResult.authorizer_access_token, refreshResult.authorizer_refresh_token, refreshResult.expires_in);

                    authorizerBag = TryGetItem(authorizerAppid);//外部缓存需要重新获取新数据
                }
            }
            return authorizerBag.AuthorizationInfo;
        }

        /// <summary>
        /// 【异步方法】获取可用AuthorizerAccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static async Task<string> TryGetAuthorizerAccessTokenAsync(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var authorizationInfo = await GetAuthorizationInfoAsync(componentAppId, authorizerAppid, getNewTicket);
            return authorizationInfo.authorizer_access_token;
        }

        /// <summary>
        /// 【异步方法】获取可用的GetAuthorizerInfoResult
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        ///// <exception cref="WeixinOpenException">此公众号没有高级权限</exception>
        public static async Task<GetAuthorizerInfoResult> GetAuthorizerInfoResultAsync(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var authorizerBag = TryGetItem(authorizerAppid);
            using (Cache.BeginCacheLock(LockResourceName + ".GetAuthorizerInfoResult", authorizerAppid))//同步锁
            {

                //更新AuthorizerInfo
                if (getNewTicket || authorizerBag.AuthorizerInfo.user_name == null)
                {
                    var componentVerifyTicket = ComponentContainer.TryGetComponentVerifyTicket(componentAppId);
                    var componentAccessToken = ComponentContainer.GetComponentAccessToken(componentAppId, componentVerifyTicket);

                    //已过期，重新获取
                    var getAuthorizerInfoResult = await ComponentApi.GetAuthorizerInfoAsync(componentAccessToken, componentAppId, authorizerAppid);//TODO:如果是过期，可以通过刷新的方式重新获取

                    //AuthorizerInfo
                    authorizerBag.AuthorizerInfo = getAuthorizerInfoResult.authorizer_info;

                    Update(authorizerBag, null);//更新到缓存

                    //var componentBag = ComponentContainer.TryGetItem(componentAppId);
                    //if (string.IsNullOrEmpty(authorizerBag.AuthorizerInfoResult.authorization_info.authorizer_access_token))
                    //{
                    //    //账号没有此权限
                    //    throw new WeixinOpenException("此公众号没有高级权限", componentBag);
                    //}
                }
            }
            return authorizerBag.FullAuthorizerInfoResult;
        }



        /// <summary>
        /// 【异步方法】刷新AuthorizerToken
        /// </summary>
        /// <param name="componentAccessToken"></param>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public static async Task<RefreshAuthorizerTokenResult> RefreshAuthorizerTokenAsync(string componentAccessToken, string componentAppId, string authorizerAppid,
                      string refreshToken)
        {
            var refreshResult = await ComponentApi.ApiAuthorizerTokenAsync(componentAccessToken, componentAppId, authorizerAppid,
                         refreshToken);
            //更新到存储
            ComponentContainer.AuthorizerTokenRefreshedFunc(componentAppId, authorizerAppid, refreshResult);
            return refreshResult;
        }

        #endregion

        #region JSTicket

        /// <summary>
        /// 【异步方法】使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static async Task<string> TryGetJsApiTicketAsync(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            return await GetJsApiTicketAsync(componentAppId, authorizerAppid, getNewTicket);
        }

        /// <summary>
        /// 【异步方法】获取可用Ticket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<string> GetJsApiTicketAsync(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            var result = await GetJsApiTicketResultAsync(componentAppId, authorizerAppid, getNewTicket);
            return result.ticket;
        }

        /// <summary>
        /// 【异步方法】获取可用Ticket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static async Task<JsApiTicketResult> GetJsApiTicketResultAsync(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var accessTicketBag = TryGetItem(authorizerAppid);
            using (Cache.BeginCacheLock(LockResourceName + ".GetJsApiTicketResult", authorizerAppid))//同步锁
            {
                if (getNewTicket || accessTicketBag.JsApiTicketExpireTime <= SystemTime.Now)
                {
                    //已过期，重新获取
                    var authorizerAccessToken = await TryGetAuthorizerAccessTokenAsync(componentAppId, authorizerAppid);

                    accessTicketBag.JsApiTicketResult = await ComponentApi.GetJsApiTicketAsync(authorizerAccessToken);

                    accessTicketBag.JsApiTicketExpireTime = ApiUtility.GetExpireTime(accessTicketBag.JsApiTicketResult.expires_in);

                    Update(accessTicketBag, null);//更新到缓存
                }
            }
            return accessTicketBag.JsApiTicketResult;
        }

        #endregion

        #endregion
#endif
    }
}
