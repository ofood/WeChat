/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：CustomAPI.cs
    文件功能描述：小程序客服接口
----------------------------------------------------------------*/

/* 
   API地址：https://developers.weixin.qq.com/miniprogram/dev/api/custommsg/conversation.html
*/

using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure.Enums;
using System.Threading.Tasks;
using OFoodWeChat.WxOpen.Entities;



namespace OFoodWeChat.WxOpen.AdvancedAPIs
{

    /// <summary>
    /// 小程序客服接口
    /// </summary>
    public class CustomApi
    {
        /// <summary>
        /// 客服消息统一请求地址格式
        /// </summary>
        public static readonly string UrlFormat = WxConfig.ApiMpHost + "/cgi-bin/message/custom/send?access_token={0}";
        #region 同步方法

        /// <summary>
        /// 发送文本信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="content">文本消息内容</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "CustomApi.SendText", true)]
        public static WxOpenJsonResult SendText(string accessTokenOrAppId, string openId, string content,
            int timeOut = WxConfig.TIME_OUT)
        {
            object data = null;
            data = new
            {
                touser = openId,
                msgtype = "text",
                text = new
                {
                    content = content
                }
            };
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return CommonJsonSend.Send<WxOpenJsonResult>(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="mediaId">发送的图片的媒体ID，通过新增素材接口上传图片文件获得。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "CustomApi.SendImage", true)]
        public static WxOpenJsonResult SendImage(string accessTokenOrAppId, string openId, string mediaId, int timeOut = WxConfig.TIME_OUT)
        {
            object data = null;
            data = new
            {
                touser = openId,
                msgtype = "image",
                image = new
                {
                    media_id = mediaId
                }

            };
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return CommonJsonSend.Send<WxOpenJsonResult>(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送图文链接
        /// <para>每次可以发送一个图文链接</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="title">消息标题</param>
        /// <param name="description">图文链接消息</param>
        /// <param name="url">图文链接消息被点击后跳转的链接</param>
        /// <param name="thumbUrl">[官方文档未给说明]</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "CustomApi.SendLink", true)]
        public static WxOpenJsonResult SendLink(string accessTokenOrAppId, string openId, string title, string description, string url, string thumbUrl, int timeOut = WxConfig.TIME_OUT)
        {
            object data = new
            {
                touser = openId,
                msgtype = "link",
                link = new
                {
                    title = title,
                    description = description,
                    url = url,
                    thumb_url = thumbUrl
                }
            };

            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return CommonJsonSend.Send<WxOpenJsonResult>(accessToken, OFoodWeChat.MP.AdvancedAPIs.CustomApi.UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);

        }


        /// <summary>
        /// 发送小程序卡片
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="title">消息标题</param>
        /// <param name="pagePath">小程序的页面路径，跟app.json对齐，支持参数，比如pages/index/index?foo=bar</param>
        /// <param name="thumbMediaId">小程序消息卡片的封面， image类型的media_id，通过新增素材接口上传图片文件获得，建议大小为520*416</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "CustomApi.SendMiniProgramPage", true)]
        public static WxOpenJsonResult SendMiniProgramPage(string accessTokenOrAppId, string openId, string title, string pagePath, string thumbMediaId, int timeOut = WxConfig.TIME_OUT)
        {
            object data = new
            {
                touser = openId,
                msgtype = "miniprogrampage",
                miniprogrampage = new
                {
                    title = title,
                    pagepath = pagePath,
                    url = thumbMediaId,
                    thumb_media_id = thumbMediaId
                }
            };

            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return CommonJsonSend.Send<WxOpenJsonResult>(accessToken, OFoodWeChat.MP.AdvancedAPIs.CustomApi.UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】发送文本信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="content">文本消息内容</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "CustomApi.SendTextAsync", true)]
        public static async Task<WxOpenJsonResult> SendTextAsync(string accessTokenOrAppId, string openId, string content,
            int timeOut = WxConfig.TIME_OUT)
        {
            object data = null;
            data = new
            {
                touser = openId,
                msgtype = "text",
                text = new
                {
                    content = content
                }

            };
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                return await CommonJsonSend.SendAsync<WxOpenJsonResult>(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 【异步方法】发送图片消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="mediaId">发送的图片的媒体ID，通过新增素材接口上传图片文件获得。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "CustomApi.SendImageAsync", true)]
        public static async Task<WxOpenJsonResult> SendImageAsync(string accessTokenOrAppId, string openId, string mediaId, int timeOut = WxConfig.TIME_OUT)
        {
            object data = null;
            data = new
            {
                touser = openId,
                msgtype = "image",
                image = new
                {
                    media_id = mediaId
                }

            };
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                return await CommonJsonSend.SendAsync<WxOpenJsonResult>(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】发送图文链接
        /// <para>每次可以发送一个图文链接</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="title">消息标题</param>
        /// <param name="description">图文链接消息</param>
        /// <param name="url">图文链接消息被点击后跳转的链接</param>
        /// <param name="thumbUrl">[官方文档未给说明]</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "CustomApi.SendLinkAsnyc", true)]
        public static async Task<WxOpenJsonResult> SendLinkAsnyc(string accessTokenOrAppId, string openId, string title, string description, string url, string thumbUrl, int timeOut = WxConfig.TIME_OUT)
        {
            object data = new
            {
                touser = openId,
                msgtype = "link",
                link = new
                {
                    title = title,
                    description = description,
                    url = url,
                    thumb_url = thumbUrl
                }
            };

            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                return await CommonJsonSend.SendAsync<WxOpenJsonResult>(accessToken, OFoodWeChat.MP.AdvancedAPIs.CustomApi.UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);

        }


        /// <summary>
        /// 【异步方法】发送小程序卡片
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">普通用户(openid)</param>
        /// <param name="title">消息标题</param>
        /// <param name="pagePath">小程序的页面路径，跟app.json对齐，支持参数，比如pages/index/index?foo=bar</param>
        /// <param name="thumbMediaId">小程序消息卡片的封面， image类型的media_id，通过新增素材接口上传图片文件获得，建议大小为520*416</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "CustomApi.SendMiniProgramPageAsync", true)]
        public static async Task<WxOpenJsonResult> SendMiniProgramPageAsync(string accessTokenOrAppId, string openId, string title, string pagePath, string thumbMediaId, int timeOut = WxConfig.TIME_OUT)
        {
            object data = new
            {
                touser = openId,
                msgtype = "miniprogrampage",
                miniprogrampage = new
                {
                    title = title,
                    pagepath = pagePath,
                    url = thumbMediaId,
                    thumb_media_id = thumbMediaId
                }
            };

            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                return await CommonJsonSend.SendAsync<WxOpenJsonResult>(accessToken, OFoodWeChat.MP.AdvancedAPIs.CustomApi.UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion
    }
}
