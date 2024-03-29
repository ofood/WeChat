﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：LinkerCorpApi.cs
    文件功能描述：互联企业消息推送接口
    
----------------------------------------------------------------*/

/*
    官方文档：https://work.weixin.qq.com/api/doc#14689
 */
using OFoodWeChat.Core;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Helpers.Serializers;
using OFoodWeChat.Work.AdvancedAPIs.Mass;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OFoodWeChat.Work.AdvancedAPIs
{
    /// <summary>
    /// 发送消息
    /// </summary>
    public static class LinkerCorpApi
    {
        private static string _urlFormat = WxConfig.ApiWorkHost + "/cgi-bin/linkedcorp/message/send?access_token={0}";

        #region 同步方法


        /// <summary>
        /// 发送文本信息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendText", true)]
        public static LinkedCorpMassResult SendText(string accessTokenOrAppKey, SendTextData data, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return CommonJsonSend.Send<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendImage", true)]
        public static LinkedCorpMassResult SendImage(string accessTokenOrAppKey, SendImageData data, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return CommonJsonSend.Send<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendVoice", true)]
        public static LinkedCorpMassResult SendVoice(string accessTokenOrAppKey, SendVoiceData data, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return CommonJsonSend.Send<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendVideo", true)]
        public static LinkedCorpMassResult SendVideo(string accessTokenOrAppKey, SendVideoData data, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return CommonJsonSend.Send<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 发送文件消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendFile", true)]
        public static LinkedCorpMassResult SendFile(string accessTokenOrAppKey, SendFileData data, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return CommonJsonSend.Send<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendNews", true)]
        public static LinkedCorpMassResult SendNews(string accessTokenOrAppKey, SendNewsData data, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return CommonJsonSend.Send<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 发送mpnews消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendMpNews", true)]
        public static LinkedCorpMassResult SendMpNews(string accessTokenOrAppKey, SendMpNewsData data, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return CommonJsonSend.Send<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 发送文本卡片消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendTextCard", true)]
        public static LinkedCorpMassResult SendTextCard(string accessTokenOrAppKey, SendTextCardData data, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return CommonJsonSend.Send<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 发送小程序通知消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendMiniNoticeCard", true)]
        public static LinkedCorpMassResult SendMiniNoticeCard(string accessTokenOrAppKey, SendMiniNoticeData data, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return CommonJsonSend.Send<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);
        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】发送文本信息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendTextAsync", true)]
        public static async Task<LinkedCorpMassResult> SendTextAsync(string accessTokenOrAppKey, SendTextData data, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return await CommonJsonSend.SendAsync<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);

        }

        /// <summary>
        /// 【异步方法】发送图片消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendImageAsync", true)]
        public static async Task<LinkedCorpMassResult> SendImageAsync(string accessTokenOrAppKey, SendImageData data, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return await CommonJsonSend.SendAsync<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】发送语音消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendVoiceAsync", true)]
        public static async Task<LinkedCorpMassResult> SendVoiceAsync(string accessTokenOrAppKey, SendVoiceData data, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return await CommonJsonSend.SendAsync<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】发送视频消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendVideoAsync", true)]
        public static async Task<LinkedCorpMassResult> SendVideoAsync(string accessTokenOrAppKey, SendVideoData data, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return await CommonJsonSend.SendAsync<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】发送文件消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendFileAsync", true)]
        public static async Task<LinkedCorpMassResult> SendFileAsync(string accessTokenOrAppKey, SendFileData data, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return await CommonJsonSend.SendAsync<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】发送图文消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendNewsAsync", true)]
        public static async Task<LinkedCorpMassResult> SendNewsAsync(string accessTokenOrAppKey, SendNewsData data, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return await CommonJsonSend.SendAsync<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】发送mpnews消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendMpNewsAsync", true)]
        public static async Task<LinkedCorpMassResult> SendMpNewsAsync(string accessTokenOrAppKey, SendMpNewsData data, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                return await CommonJsonSend.SendAsync<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppKey);

        }

        /// <summary>
        /// 【异步方法】发送textcard消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendTextCardAsync", true)]
        public static async Task<LinkedCorpMassResult> SendTextCardAsync(string accessTokenOrAppKey, SendTextCardData data,int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
               JsonSetting jsonSetting = new JsonSetting(true);

                return await CommonJsonSend.SendAsync<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 【异步方法】发送小程序通知消息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Work, "LinkerCorpApi.SendMiniNoticeCardAsync", true)]
        public static async Task<LinkedCorpMassResult> SendMiniNoticeCardAsync(string accessTokenOrAppKey, SendMiniNoticeData data, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                JsonSetting jsonSetting = new JsonSetting(true);

                return await CommonJsonSend.SendAsync<LinkedCorpMassResult>(accessToken, _urlFormat, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion
#endif
    }
}
