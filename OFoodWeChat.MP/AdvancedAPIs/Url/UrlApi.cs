﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
  
    修改标识：Senparc - 20160621
    修改描述：修改命名空间
              其改为OFoodWeChat.MP.AdvancedAPIs      
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.MP.CommonAPIs;
using OFoodWeChat.MP.AdvancedAPIs.Url;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Core;

namespace OFoodWeChat.MP.AdvancedAPIs
{
    /// <summary>
    /// 长短链接接口
    /// </summary>
    public class UrlApi
    {
        /*
        接口地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1443433600&token=&lang=zh_CN
                    将一条长链接转成短链接。
        主要使用场景： 开发者用于生成二维码的原链接（商品、支付二维码等）太长导致扫码速度和成功率下降，将原长链接通过此接口转成短链接再生成二维码将大大提升扫码速度和成功率。
        */
        #region 同步方法
        
        
        ///  <summary>
        /// 将一条长链接转成短链接。
        ///  </summary>
        ///  <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        ///  <param name="action">此处填long2short，代表长链接转短链接</param>
        ///  <param name="longUrl">需要转换的长链接，支持http://、https://、weixin://wxpay 格式的url</param>
        /// <param name="timeOut">请求超时时间</param>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "UrlApi.ShortUrl", true)]
        public static ShortUrlResult ShortUrl(string accessTokenOrAppId, string action, string longUrl, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/shorturl?access_token={0}";
                var data = new
                {
                    action = action,
                    long_url = longUrl
                };
                return CommonJsonSend.Send<ShortUrlResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
         ///  <summary>
        /// 将一条长链接转成短链接。
        ///  </summary>
        ///  <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        ///  <param name="action">此处填long2short，代表长链接转短链接</param>
        ///  <param name="longUrl">需要转换的长链接，支持http://、https://、weixin://wxpay 格式的url</param>
        /// <param name="timeOut">请求超时时间</param>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "UrlApi.ShortUrlAsync", true)]
        public static async Task<ShortUrlResult> ShortUrlAsync(string accessTokenOrAppId, string action, string longUrl, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/shorturl?access_token={0}";
                var data = new
                {
                    action = action,
                    long_url = longUrl
                };
                return await CommonJsonSend.SendAsync<ShortUrlResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion
    }
}
