/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：CustomAPI.cs
    文件功能描述：客服接口
    
----------------------------------------------------------------*/

/* 
   API地址：http://mp.weixin.qq.com/wiki/1/70a29afed17f56d537c833f89be979c9.html
   新地址（2019年3月）：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140547
*/

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.MP.Entities;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Infrastructure.Entities;
using OFoodWeChat.Infrastructure.Helpers.Serializers;
using OFoodWeChat.MP.CommonAPIs;

namespace OFoodWeChat.MP.AdvancedAPIs
{
    /// <summary>
    /// 客服接口
    /// </summary>
    public static class CustomApi
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
        /// <param name="openId"></param>
        /// <param name="content"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendText", true)]
        public static WxJsonResult SendText(string accessTokenOrAppId, string openId, string content,
            int timeOut = WxConfig.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "text",
                    text = new
                    {
                        content = content
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "text",
                    text = new
                    {
                        content = content
                    },
                    customservice = new
                    {
                        kf_account = kfAccount
                    }

                };
            }

            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return MpCommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendImage", true)]
        public static WxJsonResult SendImage(string accessTokenOrAppId, string openId, string mediaId, int timeOut = WxConfig.TIME_OUT, string kfAccount = "")
        {

            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "image",
                    image = new
                    {
                        media_id = mediaId
                    }

                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "image",
                    image = new
                    {
                        media_id = mediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return MpCommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendVoice", true)]
        public static WxJsonResult SendVoice(string accessTokenOrAppId, string openId, string mediaId, int timeOut = WxConfig.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {

                data = new
                {
                    touser = openId,
                    msgtype = "voice",
                    voice = new
                    {
                        media_id = mediaId
                    }

                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "voice",
                    voice = new
                    {
                        media_id = mediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {

                return MpCommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <param name="thumb_media_id"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendVideo", true)]
        public static WxJsonResult SendVideo(string accessTokenOrAppId, string openId, string mediaId, string title, string description, int timeOut = WxConfig.TIME_OUT, string kfAccount = "", string thumb_media_id = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "video",
                    video = new
                    {
                        media_id = mediaId,
                        thumb_media_id = thumb_media_id,
                        title = title,
                        description = description
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "video",
                    video = new
                    {
                        media_id = mediaId,
                        thumb_media_id = thumb_media_id,
                        title = title,
                        description = description
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {

                return MpCommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 发送音乐消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="title">音乐标题（非必须）</param>
        /// <param name="description">音乐描述（非必须）</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="thumbMediaId">视频缩略图的媒体ID</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendMusic", true)]
        public static WxJsonResult SendMusic(string accessTokenOrAppId, string openId, string title, string description,
                                    string musicUrl, string hqMusicUrl, string thumbMediaId, int timeOut = WxConfig.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "music",
                    music = new
                    {
                        title = title,
                        description = description,
                        musicurl = musicUrl,
                        hqmusicurl = hqMusicUrl,
                        thumb_media_id = thumbMediaId
                    }

                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "music",
                    music = new
                    {
                        title = title,
                        description = description,
                        musicurl = musicUrl,
                        hqmusicurl = hqMusicUrl,
                        thumb_media_id = thumbMediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }

                };
            }
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return MpCommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送图文消息（点击跳转到外链）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="articles"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendNews", true)]
        public static WxJsonResult SendNews(string accessTokenOrAppId, string openId, List<Article> articles, int timeOut = WxConfig.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "news",
                    news = new
                    {
                        articles = articles.Select(z => new
                        {
                            title = z.Title,
                            description = z.Description,
                            url = z.Url,
                            picurl = z.PicUrl //图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
                        }).ToList()
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "news",
                    news = new
                    {
                        articles = articles.Select(z => new
                        {
                            title = z.Title,
                            description = z.Description,
                            url = z.Url,
                            picurl = z.PicUrl//图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
                        }).ToList()
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }

                };
            }
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return MpCommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送图文消息（点击跳转到图文消息页面）
        /// 图文消息条数限制在8条以内，注意，如果图文数超过8，则将会无响应。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut"></param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendMpNews", true)]
        public static WxJsonResult SendMpNews(string accessTokenOrAppId, string openId, string mediaId, int timeOut = WxConfig.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "mpnews",
                    mpnews = new
                    {
                        media_id = mediaId
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "mpnews",
                    mpnews = new
                    {
                        media_id = mediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {

                return MpCommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发送卡券
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="cardId"></param>
        /// <param name="cardExt"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendCard", true)]
        public static WxJsonResult SendCard(string accessTokenOrAppId, string openId, string cardId, CardExt cardExt, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    touser = openId,
                    msgtype = "wxcard",
                    wxcard = new
                    {
                        card_id = cardId,
                        card_ext = cardExt
                    }
                };
                JsonSetting jsonSetting = new JsonSetting()
                {
                    TypesToIgnoreNull = new List<System.Type>() { typeof(CardExt) }
                };

                return MpCommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 客服输入状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="typingStatus"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.GetTypingStatus", true)]
        public static WxJsonResult GetTypingStatus(string accessTokenOrAppId, string cardId, string typingStatus, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(WxConfig.ApiMpHost + "/cgi-bin/message/custom/typing?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    touser = cardId,
                    command = typingStatus
                };

                return MpCommonJsonSend.Send(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 发送客户菜单消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId">接受人员OPenid</param>
        /// <param name="head">标题</param>
        /// <param name="menuList">内容</param>
        /// <param name="tail">结尾内容</param>
        /// <param name="timeOut">超时时间</param>     
        /// <returns></returns>
        public static WxJsonResult SendMenu(string accessTokenOrAppId, string openId,
        string head, List<SendMenuContent> menuList, string tail,
         int timeOut = WxConfig.TIME_OUT)
        {
            var data = new
            {
                touser = openId,
                msgtype = "msgmenu",
                msgmenu = new
                {
                    head_content = head,
                    list = menuList,
                    tail_content = tail
                }
            };
            return ApiHandlerWapper.TryCommonApi(accessToken =>
             {
                 return MpCommonJsonSend.Send(accessToken, UrlFormat, data, timeOut: timeOut);
             }, accessTokenOrAppId);
        }
        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】发送文本信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="content"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendTextAsync", true)]
        public static async Task<WxJsonResult> SendTextAsync(string accessTokenOrAppId, string openId, string content, int timeOut = WxConfig.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (string.IsNullOrEmpty(kfAccount))
            {
                data = new
                {
                    touser = openId,
                    msgtype = "text",
                    text = new
                    {
                        content = content
                    }

                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "text",
                    text = new
                    {
                        content = content
                    },
                    customservice = new
                    {
                        kf_account = kfAccount
                    }

                };
            }

            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                return await MpCommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 【异步方法】发送图片消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendImageAsync", true)]
        public static async Task<WxJsonResult> SendImageAsync(string accessTokenOrAppId, string openId, string mediaId, int timeOut = WxConfig.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "image",
                    image = new
                    {
                        media_id = mediaId
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "image",
                    image = new
                    {
                        media_id = mediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                return await MpCommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】发送语音消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendVoiceAsync", true)]
        public static async Task<WxJsonResult> SendVoiceAsync(string accessTokenOrAppId, string openId, string mediaId, int timeOut = WxConfig.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "voice",
                    voice = new
                    {
                        media_id = mediaId
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "voice",
                    voice = new
                    {
                        media_id = mediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                return await MpCommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】发送视频消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <param name="thumb_media_id"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendVideoAsync", true)]
        public static async Task<WxJsonResult> SendVideoAsync(string accessTokenOrAppId, string openId, string mediaId, string title, string description, int timeOut = WxConfig.TIME_OUT, string kfAccount = "", string thumb_media_id = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "video",
                    video = new
                    {
                        media_id = mediaId,
                        thumb_media_id = thumb_media_id,
                        title = title,
                        description = description
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "video",
                    video = new
                    {
                        media_id = mediaId,
                        thumb_media_id = thumb_media_id,
                        title = title,
                        description = description
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                return await MpCommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】发送音乐消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="title">音乐标题（非必须）</param>
        /// <param name="description">音乐描述（非必须）</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="thumbMediaId">视频缩略图的媒体ID</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendMusicAsync", true)]
        public static async Task<WxJsonResult> SendMusicAsync(string accessTokenOrAppId, string openId, string title, string description,
                                    string musicUrl, string hqMusicUrl, string thumbMediaId, int timeOut = WxConfig.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "music",
                    music = new
                    {
                        title = title,
                        description = description,
                        musicurl = musicUrl,
                        hqmusicurl = hqMusicUrl,
                        thumb_media_id = thumbMediaId
                    }

                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "music",
                    music = new
                    {
                        title = title,
                        description = description,
                        musicurl = musicUrl,
                        hqmusicurl = hqMusicUrl,
                        thumb_media_id = thumbMediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }

                };
            }
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                return await MpCommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】发送图文消息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="articles"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendNewsAsync", true)]
        public static async Task<WxJsonResult> SendNewsAsync(string accessTokenOrAppId, string openId, List<Article> articles, int timeOut = WxConfig.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "news",
                    news = new
                    {
                        articles = articles.Select(z => new
                        {
                            title = z.Title,
                            description = z.Description,
                            url = z.Url,
                            picurl = z.PicUrl //图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
                        }).ToList()
                    }

                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "news",
                    news = new
                    {
                        articles = articles.Select(z => new
                        {
                            title = z.Title,
                            description = z.Description,
                            url = z.Url,
                            picurl = z.PicUrl//图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
                        }).ToList()
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }

                };
            }
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                return await MpCommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 【异步方法】发送图文消息（点击跳转到图文消息页面）
        /// 图文消息条数限制在8条以内，注意，如果图文数超过8，则将会无响应。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut"></param>
        /// <param name="kfAccount">客服</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendMpNewsAsync", true)]
        public static async Task<WxJsonResult> SendMpNewsAsync(string accessTokenOrAppId, string openId, string mediaId, int timeOut = WxConfig.TIME_OUT, string kfAccount = "")
        {
            object data = null;
            if (kfAccount.IsNullOrWhiteSpace())
            {
                data = new
                {
                    touser = openId,
                    msgtype = "mpnews",
                    mpnews = new
                    {
                        media_id = mediaId
                    }
                };
            }
            else
            {
                data = new
                {
                    touser = openId,
                    msgtype = "mpnews",
                    mpnews = new
                    {
                        media_id = mediaId
                    },
                    CustomService = new
                    {
                        kf_account = kfAccount
                    }
                };
            }
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {

                return await MpCommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】发送卡券
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="cardId"></param>
        /// <param name="cardExt"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.SendCardAsync", true)]
        public static async Task<WxJsonResult> SendCardAsync(string accessTokenOrAppId, string openId, string cardId, CardExt cardExt, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    touser = openId,
                    msgtype = "wxcard",
                    wxcard = new
                    {
                        card_id = cardId,
                        card_ext = cardExt
                    }
                };
                JsonSetting jsonSetting = new JsonSetting()
                {
                    TypesToIgnoreNull = new List<System.Type>() { typeof(CardExt) }
                };

                return await MpCommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】客服输入状态
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="typingStatus"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CustomApi.TypingAsync", true)]
        public static async Task<WxJsonResult> GetTypingStatusAsync(string accessTokenOrAppId, string cardId, string typingStatus, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(WxConfig.ApiMpHost + "/cgi-bin/message/custom/typing?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    command = typingStatus
                };

                return await MpCommonJsonSend.SendAsync(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】发送客户菜单消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId">接受人员OPenid</param>
        /// <param name="head">标题</param>
        /// <param name="menuList">内容</param>
        /// <param name="tail">结尾内容</param>
        /// <param name="timeOut">超时时间</param>     
        /// <returns></returns>
        public static async Task<WxJsonResult> SendMenuAsync(string accessTokenOrAppId, string openId,
       string head, List<SendMenuContent> menuList, string tail,
        int timeOut = WxConfig.TIME_OUT)
        {
            var data = new
            {
                touser = openId,
                msgtype = "msgmenu",
                msgmenu = new
                {
                    head_content = head,
                    list = menuList,
                    tail_content = tail
                }
            };
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
              {
                  return await MpCommonJsonSend.SendAsync(accessToken, UrlFormat, data, timeOut: timeOut);

              }, accessTokenOrAppId);

        }

        #endregion


        /////
        ///// 发送卡券 查看card_ext字段详情及签名规则，特别注意客服消息接口投放卡券仅支持非自定义Code码的卡券。 
        /////

    }


}