/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ThirdPartyAuthApi.cs
    文件功能描述：第三方应用授权接口
   

    -----------------------------------
    
    修改标识：Senparc - 20170617
    修改描述：从QY移植，同步Work接口

----------------------------------------------------------------*/

/*
    官方文档：http://work.weixin.qq.com/api/doc#10975
 */

using System.Threading.Tasks;

using OFoodWeChat.Work.AdvancedAPIs.ThirdPartyAuth;
using OFoodWeChat.Work.Entities;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Infrastructure.Helpers.Serializers;

namespace OFoodWeChat.Work.AdvancedAPIs
{
    public static class ThirdPartyAuthApi
    {
        #region 同步方法

        /// <summary>
        /// 获取应用套件令牌【QY移植修改】
        /// </summary>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="suiteSecret">应用套件secret</param>
        /// <param name="suiteTicket">微信后台推送的ticket</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetSuiteToken", true)]
        public static GetSuiteTokenResult GetSuiteToken(string suiteId, string suiteSecret, string suiteTicket, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = WxConfig.ApiWorkHost + "/cgi-bin/service/get_suite_token";

                var data = new
                {
                    suite_id = suiteId,
                    suite_secret = suiteSecret,
                    suite_ticket = suiteTicket
                };

                return CommonJsonSend.Send<GetSuiteTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteId);


        }

        ///// <summary>
        ///// 获取预授权码
        ///// </summary>
        ///// <param name="suiteAccessToken"></param>
        ///// <param name="suiteId">应用套件id</param>
        ///// <param name="appId">应用id，本参数选填，表示用户能对本套件内的哪些应用授权，不填时默认用户有全部授权权限</param>
        ///// <param name="timeOut">代理请求超时时间（毫秒）</param>
        ///// <returns></returns>
        //public static GetPreAuthCodeResult GetPreAuthCode(string suiteAccessToken, string suiteId, int[] appId, int timeOut = WxConfig.TIME_OUT)
        //{
        //    var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_pre_auth_code?suite_access_token={0}", suiteAccessToken.AsUrlData());

        //    var data = new
        //        {
        //            suite_id = suiteId,
        //            appid = appId
        //        };

        //    return CommonJsonSend.Send<GetPreAuthCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        //}

        /// <summary>
        /// 获取预授权码【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetPreAuthCode", true)]
        public static GetPreAuthCodeResult GetPreAuthCode(string suiteAccessToken, string suiteId, int timeOut = 10000)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_pre_auth_code?suite_access_token={0}", suiteAccessToken.AsUrlData());
                var data = new
                {
                    suite_id = suiteId,
                };
                return CommonJsonSend.Send<GetPreAuthCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 设置授权配置【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="authCode">预授权码</param>
        /// <param name="appid">允许进行授权的应用id，如1、2、3， 不填或者填空数组都表示允许授权套件内所有应用 </param>
        /// <param name="auth_type">授权类型：0 正式授权， 1 测试授权， 默认值为0 </param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.SetAuthConfig", true)]
        public static WorkJsonResult SetAuthConfig(string suiteAccessToken, string authCode, int[] appid = null, int? auth_type = null, int timeOut = 10000)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/set_session_info?suite_access_token={0}", suiteAccessToken.AsUrlData());
                var data = new
                {
                    pre_auth_code = authCode,
                    session_info = new
                    {
                        appid = appid,
                        auth_type = auth_type
                    }
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 获取企业号的永久授权码【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCode">临时授权码会在授权成功时附加在redirect_uri中跳转回应用提供商网站。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetPermanentCode", true)]
        public static GetPermanentCodeResult GetPermanentCode(string suiteAccessToken, string suiteId, string authCode, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_permanent_code?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_code = authCode
                };

                return CommonJsonSend.Send<GetPermanentCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 获取企业授权信息【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，通过get_permanent_code获取</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetAuthInfo", true)]
        public static GetAuthInfoResult GetAuthInfo(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_auth_info?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode
                };

                return CommonJsonSend.Send<GetAuthInfoResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 获取企业号应用【Work中未定义】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，从get_permanent_code接口中获取</param>
        /// <param name="agentId">授权方应用id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetAgent", true)]
        public static GetAgentResult GetAgent(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, string agentId, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_agent?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode,
                    agentid = agentId
                };

                return CommonJsonSend.Send<GetAgentResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 设置企业号应用【Work中未定义】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，从get_permanent_code接口中获取</param>
        /// <param name="agent">要设置的企业应用的信息</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.SetAgent", true)]
        public static WorkJsonResult SetAgent(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, ThirdParty_AgentData agent, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/set_agent?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode,
                    agent = agent
                };

                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 获取企业access_token【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，通过get_permanent_code获取</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetCorpToken", true)]
        public static GetCorpTokenResult GetCorpToken(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_corp_token?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode,
                };

                return CommonJsonSend.Send<GetCorpTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);

        }

        /// <summary>
        /// 获取应用的管理员列表
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="agentId">授权方安装的应用agentid</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetAdminList", true)]
        public static GetAdminListResult GetAdminList(string suiteAccessToken, string agentId, string authCorpId, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_admin_list?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    auth_corpid = authCorpId,
                    agentid = agentId,
                };

                return CommonJsonSend.Send<GetAdminListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);

        }

        /// <summary>
        /// 第三方根据code获取企业成员信息
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="code"></param>
        /// <param name="agentId"></param>
        /// <param name="authCorpId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetUserInfo", true)]
        public static GetUserInfoResult GetUserInfo(string suiteAccessToken, string code, string agentId, string authCorpId, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/getuserinfo3rd?access_token={0}&code={1}", suiteAccessToken.AsUrlData(), code);

                return CommonJsonSend.Send<GetUserInfoResult>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, suiteAccessToken);

        }

        /// <summary>
        /// 第三方使用user_ticket获取成员详情
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="userTicket"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetUserInfoByTicket", true)]
        public static GetUserInfoByTicketResult GetUserInfoByTicket(string suiteAccessToken, string userTicket, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/getuserdetail3rd?access_token={0}", suiteAccessToken.AsUrlData());
                var data = new
                {
                    user_ticket = userTicket
                };
                return CommonJsonSend.Send<GetUserInfoByTicketResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);

        }

        /// <summary>
        /// 获取注册码
        /// </summary>
        /// <param name="providerAccessToken"></param>
        /// <param name="userTicket"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetRegisterCode", true)]
        public static GetRegisterCodeResult GetRegisterCode(string providerAccessToken, GetRegisterCodeData data, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_register_code?provider_access_token={0}", providerAccessToken.AsUrlData());

                return CommonJsonSend.Send<GetRegisterCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, providerAccessToken);

        }

        /// <summary>
        /// 查询注册状态
        /// </summary>
        /// <param name="providerAccessToken"></param>
        /// <param name="userTicket"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetRegisterInfo", true)]
        public static GetRegisterInfoResult GetRegisterInfo(string providerAccessToken, string registerCode, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_register_info?provider_access_token={0}", providerAccessToken.AsUrlData());
                var data = new
                {
                    register_code = registerCode
                };
                return CommonJsonSend.Send<GetRegisterInfoResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, providerAccessToken);

        }

        /// <summary>
        /// 设置授权应用可见范围
        /// </summary>
        /// <param name="providerAccessToken"></param>
        /// <param name="userTicket"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.SetScope", true)]
        public static SetScopeResult SetScope(string AccessToken, SetScopeData data, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/agent/set_scope?access_token={0}", AccessToken.AsUrlData());

                return CommonJsonSend.Send<SetScopeResult>(null, url, data, CommonJsonSendType.GET, timeOut);
            }, AccessToken);

        }

        /// <summary>
        /// 设置通讯录同步完成
        /// </summary>
        /// <param name="providerAccessToken"></param>
        /// <param name="userTicket"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.ContactSyncSuccess", true)]
        public static WorkJsonResult ContactSyncSuccess(string AccessToken, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/sync/contact_sync_success?access_token={0}", AccessToken.AsUrlData());

                return CommonJsonSend.Send<WorkJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, AccessToken);

        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        ///【异步方法】 获取应用套件令牌【QY移植修改】
        /// </summary>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="suiteSecret">应用套件secret</param>
        /// <param name="suiteTicket">微信后台推送的ticket</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetSuiteTokenAsync", true)]
        public static async Task<GetSuiteTokenResult> GetSuiteTokenAsync(string suiteId, string suiteSecret, string suiteTicket, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = WxConfig.ApiWorkHost + "/cgi-bin/service/get_suite_token";

                var data = new
                {
                    suite_id = suiteId,
                    suite_secret = suiteSecret,
                    suite_ticket = suiteTicket
                };

                return await CommonJsonSend.SendAsync<GetSuiteTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteId);


        }

        ///// <summary>
        ///// 【异步方法】获取预授权码
        ///// </summary>
        ///// <param name="suiteAccessToken"></param>
        ///// <param name="suiteId">应用套件id</param>
        ///// <param name="appId">应用id，本参数选填，表示用户能对本套件内的哪些应用授权，不填时默认用户有全部授权权限</param>
        ///// <param name="timeOut">代理请求超时时间（毫秒）</param>
        ///// <returns></returns>
        //public static GetPreAuthCodeResult GetPreAuthCode(string suiteAccessToken, string suiteId, int[] appId, int timeOut = WxConfig.TIME_OUT)
        //{
        //    var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_pre_auth_code?suite_access_token={0}", suiteAccessToken.AsUrlData());

        //    var data = new
        //        {
        //            suite_id = suiteId,
        //            appid = appId
        //        };

        //    return CommonJsonSend.Send<GetPreAuthCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        //}

        /// <summary>
        /// 【异步方法】获取预授权码【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetPreAuthCodeAsync", true)]
        public static async Task<GetPreAuthCodeResult> GetPreAuthCodeAsync(string suiteAccessToken, string suiteId, int timeOut = 10000)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_pre_auth_code?suite_access_token={0}", suiteAccessToken.AsUrlData());
                var data = new
                {
                    suite_id = suiteId,
                };
                return await CommonJsonSend.SendAsync<GetPreAuthCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 【异步方法】设置授权配置【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="authCode">预授权码</param>
        /// <param name="appid">允许进行授权的应用id，如1、2、3， 不填或者填空数组都表示允许授权套件内所有应用 </param>
        /// <param name="auth_type">授权类型：0 正式授权， 1 测试授权， 默认值为0 </param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.SetAuthConfigAsync", true)]
        public static async Task<WorkJsonResult> SetAuthConfigAsync(string suiteAccessToken, string authCode, int[] appid = null, int? auth_type = null, int timeOut = 10000)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/set_session_info?suite_access_token={0}", suiteAccessToken.AsUrlData());
                var data = new
                {
                    pre_auth_code = authCode,
                    session_info = new
                    {
                        appid = appid,
                        auth_type = auth_type
                    }
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 【异步方法】获取企业号的永久授权码【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCode">临时授权码会在授权成功时附加在redirect_uri中跳转回应用提供商网站。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetPermanentCodeAsync", true)]
        public static async Task<GetPermanentCodeResult> GetPermanentCodeAsync(string suiteAccessToken, string suiteId, string authCode, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_permanent_code?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_code = authCode
                };

                return await CommonJsonSend.SendAsync<GetPermanentCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 【异步方法】获取企业授权信息【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，通过get_permanent_code获取</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetAuthInfoAsync", true)]
        public static async Task<GetAuthInfoResult> GetAuthInfoAsync(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_auth_info?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode
                };

                return await CommonJsonSend.SendAsync<GetAuthInfoResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 【异步方法】获取企业号应用【Work中未定义】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，从get_permanent_code接口中获取</param>
        /// <param name="agentId">授权方应用id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetAgentAsync", true)]
        public static async Task<GetAgentResult> GetAgentAsync(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, string agentId, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_agent?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode,
                    agentid = agentId
                };

                return await CommonJsonSend.SendAsync<GetAgentResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 【异步方法】设置企业号应用【Work中未定义】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，从get_permanent_code接口中获取</param>
        /// <param name="agent">要设置的企业应用的信息</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.SetAgentAsync", true)]
        public static async Task<WorkJsonResult> SetAgentAsync(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, ThirdParty_AgentData agent, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/set_agent?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode,
                    agent = agent
                };

                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 【异步方法】获取企业access_token【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，通过get_permanent_code获取</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetCorpTokenAsync", true)]
        public static async Task<GetCorpTokenResult> GetCorpTokenAsync(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_corp_token?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode,
                };

                return await CommonJsonSend.SendAsync<GetCorpTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 【异步方法】获取应用的管理员列表
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="agentId">授权方安装的应用agentid</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetAdminListAsync", true)]
        public static async Task<GetAdminListResult> GetAdminListAsync(string suiteAccessToken, string agentId, string authCorpId, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_admin_list?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    auth_corpid = authCorpId,
                    agentid = agentId,
                };

                return await CommonJsonSend.SendAsync<GetAdminListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);

        }

        /// <summary>
        /// 【异步方法】第三方根据code获取企业成员信息
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="code"></param>
        /// <param name="agentId"></param>
        /// <param name="authCorpId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetUserInfoAsync", true)]
        public static async Task<GetUserInfoResult> GetUserInfoAsync(string suiteAccessToken, string code, string agentId, string authCorpId, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/getuserinfo3rd?access_token={0}&code={1}", suiteAccessToken.AsUrlData(), code);

                return await CommonJsonSend.SendAsync<GetUserInfoResult>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, suiteAccessToken);

        }

        /// <summary>
        /// 【异步方法】第三方使用user_ticket获取成员详情
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="userTicket"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetUserInfoByTicketAsync", true)]
        public static async Task<GetUserInfoByTicketResult> GetUserInfoByTicketAsync(string suiteAccessToken, string userTicket, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/getuserdetail3rd?access_token={0}", suiteAccessToken.AsUrlData());
                var data = new
                {
                    user_ticket = userTicket
                };
                return await CommonJsonSend.SendAsync<GetUserInfoByTicketResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);

        }

        /// <summary>
        /// 【异步方法】获取注册码
        /// </summary>
        /// <param name="providerAccessToken"></param>
        /// <param name="userTicket"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetRegisterCodeAsync", true)]
        public static async Task<GetRegisterCodeResult> GetRegisterCodeAsync(string providerAccessToken, GetRegisterCodeData data, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_register_code?provider_access_token={0}", providerAccessToken.AsUrlData());

                return await CommonJsonSend.SendAsync<GetRegisterCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, providerAccessToken);

        }

        /// <summary>
        /// 【异步方法】查询注册状态
        /// </summary>
        /// <param name="providerAccessToken"></param>
        /// <param name="userTicket"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.GetRegisterInfoAsync", true)]
        public static async Task<GetRegisterInfoResult> GetRegisterInfoAsync(string providerAccessToken, string registerCode, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/service/get_register_info?provider_access_token={0}", providerAccessToken.AsUrlData());
                var data = new
                {
                    register_code = registerCode
                };
                return await CommonJsonSend.SendAsync<GetRegisterInfoResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, providerAccessToken);

        }

        /// <summary>
        /// 【异步方法】设置授权应用可见范围
        /// </summary>
        /// <param name="providerAccessToken"></param>
        /// <param name="userTicket"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.SetScopeAsync", true)]
        public static async Task<SetScopeResult> SetScopeAsync(string AccessToken, SetScopeData data, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/agent/set_scope?access_token={0}", AccessToken.AsUrlData());

                return await CommonJsonSend.SendAsync<SetScopeResult>(null, url, data, CommonJsonSendType.GET, timeOut);
            }, AccessToken);

        }

        /// <summary>
        /// 【异步方法】设置通讯录同步完成
        /// </summary>
        /// <param name="providerAccessToken"></param>
        /// <param name="userTicket"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "ThirdPartyAuthApi.ContactSyncSuccessAsync", true)]
        public static async Task<WorkJsonResult> ContactSyncSuccessAsync(string AccessToken, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(WxConfig.ApiWorkHost + "/cgi-bin/sync/contact_sync_success?access_token={0}", AccessToken.AsUrlData());

                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, AccessToken);

        }

        #endregion
#endif
    }
}
