﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：ModifyDomainApi.cs
    文件功能描述：成员管理接口


    创建标识：Senparc - 20170629

    修改标识：Senparc - 20170726
    修改描述：完成接口开放平台-代码管理及小程序码获取
    
    修改标识：Senparc - 20160707
    修改描述：完善微信开放平台帐号管理
----------------------------------------------------------------*/

using OFoodWeChat.Open.WxaAPIs.Tester;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Core;

namespace OFoodWeChat.Open.WxaAPIs
{
    public class TesterApi
    {
        #region 同步方法

        /// <summary>
        /// 创建开放平台帐号并绑定公众号/小程序。
        /// 该API用于创建一个开放平台帐号，并将一个尚未绑定开放平台帐号的公众号/小程序绑定至该开放平台帐号上。新创建的开放平台帐号的主体信息将设置为与之绑定的公众号或小程序的主体。
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="appId">授权公众号或小程序的appid</param>
        /// <returns></returns>
        [Obsolete("此方法已过期，请使用OFoodWeChat.Open.MpAPIs.Open.Create()方法")]
        [ApiBind(PlatformType.WeChat_Open, "TesterApi.CreateTester", true)]
        public static CreateJsonResult CreateTester(string accessToken, string appId)
        {
            var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/open/create?access_token={0}";
            var data = new { appid = appId };
            return CommonJsonSend.Send<CreateJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 【同步接口】绑定小程序的体验者 接口
        /// </summary>
        /// <param name="accessToken">authorizer_access_token</param>
        /// <param name="wechatid">微信号</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "TesterApi.BindTester", true)]
        public static TesterResultJson BindTester(string accessToken, string wechatid, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/bind_tester?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                wechatid = wechatid.ToString()
            };

            return CommonJsonSend.Send<TesterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 【同步接口】解除绑定小程序的体验者 接口
        /// </summary>
        /// <param name="accessToken">authorizer_access_token</param>
        /// <param name="wechatid">微信号</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "TesterApi.UnBindTester", true)]
        public static TesterResultJson UnBindTester(string accessToken, string wechatid, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/unbind_tester?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                wechatid = wechatid.ToString()
            };

            return CommonJsonSend.Send<TesterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取公众号/小程序所绑定的开放平台帐号
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="appId">授权公众号或小程序的appid</param>
        /// <returns></returns>
        [Obsolete("此方法已过期，请使用OFoodWeChat.Open.MpAPIs.Open.Get()方法")]
        [ApiBind(PlatformType.WeChat_Open, "TesterApi.GetTester", true)]
        public static GetJsonResult GetTester(string accessToken, string appId)
        {
            var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/open/get?access_token={0}";
            var data = new { appid = appId };
            return CommonJsonSend.Send<GetJsonResult>(accessToken, urlFormat, data);
        }

        #endregion


#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        /// 【异步方法】创建开放平台帐号并绑定公众号/小程序。
        /// 该API用于创建一个开放平台帐号，并将一个尚未绑定开放平台帐号的公众号/小程序绑定至该开放平台帐号上。新创建的开放平台帐号的主体信息将设置为与之绑定的公众号或小程序的主体。
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="appId">授权公众号或小程序的appid</param>
        /// <returns></returns>
        [Obsolete("此方法已过期，请使用OFoodWeChat.Open.MpAPIs.Open.CreateAsync()方法")]
        [ApiBind(PlatformType.WeChat_Open, "TesterApi.CreateTesterAsync", true)]
        public static async Task<CreateJsonResult> CreateTesterAsync(string accessToken, string appId)
        {
            var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/open/create?access_token={0}";
            var data = new { appid = appId };
            return await CommonJsonSend.SendAsync<CreateJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 【异步方法】绑定小程序的体验者 接口
        /// </summary>
        /// <param name="accessToken">authorizer_access_token</param>
        /// <param name="wechatid">微信号</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "TesterApi.BindTesterSync", true)]
        public static async Task<TesterResultJson> BindTesterSync(string accessToken, string wechatid, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/bind_tester?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                wechatid = wechatid.ToString()
            };

            return await CommonJsonSend.SendAsync<TesterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        /// <summary>
        /// 【异步方法】解除绑定小程序的体验者 接口
        /// </summary>
        /// <param name="accessToken">authorizer_access_token</param>
        /// <param name="wechatid">微信号</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "TesterApi.UnBindTesterSync", true)]
        public static async Task<TesterResultJson> UnBindTesterSync(string accessToken, string wechatid, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/unbind_tester?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                wechatid = wechatid.ToString()
            };

            return await CommonJsonSend.SendAsync<TesterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取公众号/小程序所绑定的开放平台帐号
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="appId">授权公众号或小程序的appid</param>
        /// <returns></returns>
        [Obsolete("此方法已过期，请使用OFoodWeChat.Open.MpAPIs.Open.GetAsync()方法")]
        [ApiBind(PlatformType.WeChat_Open, "TesterApi.GetTesterAsync", true)]
        public static async Task<GetJsonResult> GetTesterAsync(string accessToken, string appId)
        {
            var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/open/get?access_token={0}";
            var data = new { appid = appId };
            return await CommonJsonSend.SendAsync<GetJsonResult>(accessToken, urlFormat, data);
        }

        #endregion
#endif
    }
}
