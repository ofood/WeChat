/**
*┌──────────────────────────────────────────────────────────────┐
*│<copyright file="MpCommonJsonSend" company="Meten">
*│     Copyright (c) 2019-2050 Meten. All rights reserved.
*│</copyright>
*│  描   述：                                                    
*│　作   者：peter                                              
*│　版   本：1.0                                                 
*│　创建时间：2019/6/13 15:51:52                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: OFoodWeChat.MP.CommonAPIs                                   
*│　类   名：MpCommonJsonSend                                      
*└──────────────────────────────────────────────────────────────┘
*/

using OFoodWeChat.Core;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Core.Exceptions;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Helpers;
using OFoodWeChat.Infrastructure.Helpers.Serializers;
using OFoodWeChat.MP.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.MP.CommonAPIs
{
    public static class MpCommonJsonSend
    {
        /// <summary>
        /// 设定条件，当API结果没有返回成功信息时抛出异常
        /// </summary>
        static Action<string, string> failAction = (apiUrl, returnText) =>
        {
            WeixinTrace.SendApiLog(apiUrl, returnText);

            if (returnText.Contains("errcode"))
            {
                //可能发生错误
                WxJsonResult errorResult = SerializerHelper.GetObject<WxJsonResult>(returnText);
                if (errorResult.errcode != ReturnCode.请求成功)
                {
                    //发生错误
                    throw new ErrorJsonResultException(
                         string.Format("微信请求发生错误！错误代码：{0}，说明：{1}", (int)errorResult.errcode, errorResult.errmsg), null, errorResult, apiUrl);
                }
            }
        };
        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat"></param>
        /// <param name="data">如果是Get方式，可以为null</param>
        /// <param name="sendType"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="checkValidationResult"></param>
        /// <param name="jsonSetting"></param>
        /// <returns></returns>
        public static WxJsonResult Send(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = WxConfig.TIME_OUT, bool checkValidationResult = false, JsonSetting jsonSetting = null)
        {
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, sendType, timeOut, checkValidationResult, jsonSetting);
        }
        public static async Task<WxJsonResult> SendAsync(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = WxConfig.TIME_OUT, bool checkValidationResult = false, JsonSetting jsonSetting = null)
        {
            return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, sendType, timeOut, checkValidationResult, jsonSetting);
        }
    }
}
