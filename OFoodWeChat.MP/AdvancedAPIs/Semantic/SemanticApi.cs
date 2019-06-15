/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SemanticApi.cs
    文件功能描述：语意理解接口
----------------------------------------------------------------*/

/*
    API：http://mp.weixin.qq.com/wiki/0/0ce78b3c9524811fee34aba3e33f3448.html
    文档下载：http://mp.weixin.qq.com/wiki/static/assets/f48efdb46b4bca35caed4f01ca92e7da.zip
 */

using System.Threading.Tasks;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.MP.AdvancedAPIs.Semantic;
using OFoodWeChat.MP.CommonAPIs;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Core;
using OFoodWeChat.MP.Entities;

namespace OFoodWeChat.MP.AdvancedAPIs
{
    /// <summary>
    /// 语意理解接口
    /// </summary>
    public static class SemanticApi
    {
        #region 同步方法
 
        /// <summary>
        /// 发送语义理解请求
        /// </summary>
        /// <typeparam name="T">语意理解返回的结果类型，在 AdvancedAPIs/Semantic/SemanticResult </typeparam>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="semanticPostData">语义理解请求需要post的数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "SemanticApi.SemanticSend", true)]
        public static T SemanticSend<T>(string accessTokenOrAppId, SemanticPostData semanticPostData, int timeOut = WxConfig.TIME_OUT) where T : WxJsonResult
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = WxConfig.ApiMpHost + "/semantic/semproxy/search?access_token={0}";

                //switch (semanticPostData.category)
                //{
                //    case "restaurant":
                //        BaseSemanticResultJson as Semantic_RestaurantResult;
                //}

                return CommonJsonSend.Send<T>(accessToken, urlFormat, semanticPostData, timeOut: timeOut);

             }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】发送语义理解请求
        /// </summary>
        /// <typeparam name="T">语意理解返回的结果类型，在 AdvancedAPIs/Semantic/SemanticResult </typeparam>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="semanticPostData">语义理解请求需要post的数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "SemanticApi.SemanticSendAsync", true)]
        public static async Task<T> SemanticSendAsync<T>(string accessTokenOrAppId, SemanticPostData semanticPostData, int timeOut = WxConfig.TIME_OUT) where T : WxJsonResult
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = WxConfig.ApiMpHost + "/semantic/semproxy/search?access_token={0}";

                //switch (semanticPostData.category)
                //{
                //    case "restaurant":
                //        BaseSemanticResultJson as Semantic_RestaurantResult;
                //}

                return await CommonJsonSend.SendAsync<T>(accessToken, urlFormat, semanticPostData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion
    }
}