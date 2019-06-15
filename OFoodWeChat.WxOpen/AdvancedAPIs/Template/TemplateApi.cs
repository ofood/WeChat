/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：TemplateAPI.cs
    文件功能描述：小程序的模板消息接口

    修改标识：Senparc - 20181009
    修改描述：添加下发小程序和公众号统一的服务消息接口

----------------------------------------------------------------*/

/*
    API：https://mp.weixin.qq.com/debug/wxadoc/dev/api/notice.html#接口说明
 */

using System.Threading.Tasks;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.WxOpen.Entities;

namespace OFoodWeChat.WxOpen.AdvancedAPIs.Template
{
    /// <summary>
    /// 模板消息接口
    /// </summary>
    public static class TemplateApi
    {
        #region 同步方法

        /// <summary>
        /// 小程序模板消息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="templateId"></param>
        /// <param name="data"></param>
        /// <param name="emphasisKeyword">模板需要放大的关键词，不填则默认无放大</param>
        /// <param name="color">模板内容字体的颜色，不填默认黑色（非必填）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="formId">表单提交场景下，为 submit 事件带上的 formId；支付场景下，为本次支付的 prepay_id</param>
        /// <param name="page">点击模板查看详情跳转页面，不填则模板无跳转（非必填）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "TemplateApi.SendTemplateMessage", true)]
        public static WxOpenJsonResult SendTemplateMessage(string accessTokenOrAppId, string openId, string templateId,
            object data, string formId, string page = null, string emphasisKeyword = null, string color = null, int timeOut = WxConfig.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/message/wxopen/template/send?access_token={0}";
                var msgData = new TempleteModel()
                {
                    touser = openId,
                    template_id = templateId,
                    color = color,
                    page = page,
                    form_id = formId,
                    data = data,
                    emphasis_keyword = emphasisKeyword,
                };

                return CommonJsonSend.Send<WxOpenJsonResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);

            /*
            示例：
            {
              "touser": "OPENID",  
              "template_id": "TEMPLATE_ID", 
              "page": "index",          
              "form_id": "FORMID",         
              "data": {
                  "keyword1": {
                      "value": "339208499", 
                      "color": "#173177"
                  }, 
                  "keyword2": {
                      "value": "2015年01月05日 12:30", 
                      "color": "#173177"
                  }, 
                  "keyword3": {
                      "value": "粤海喜来登酒店", 
                      "color": "#173177"
                  } , 
                  "keyword4": {
                      "value": "广州市天河区天河路208号", 
                      "color": "#173177"
                  } 
              },
              "emphasis_keyword": "keyword1.DATA" 
            }
          */
        }

        /// <summary>
        /// 下发小程序和公众号统一的服务消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="msgData"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "TemplateApi.UniformSend", true)]
        public static WxOpenJsonResult UniformSend(string accessTokenOrAppId, UniformSendData msgData, int timeOut = WxConfig.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/message/wxopen/template/uniform_send?access_token={0}";

                return CommonJsonSend.Send<WxOpenJsonResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #region 模板快速设置

        /// <summary>
        /// 获取小程序模板库标题列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="offset">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="count">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "TemplateApi.LibraryList", true)]
        public static LibraryListJsonResult LibraryList(string accessToken, int offset, int count, int timeOut = WxConfig.TIME_OUT)
        {
            string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/wxopen/template/library/list?access_token={0}";
            var data = new
            {
                offset = offset,
                count = count
            };
            return CommonJsonSend.Send<LibraryListJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        /// <summary>
        /// 获取模板库某个模板标题下关键词库
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "TemplateApi.LibraryGet", true)]
        public static LibraryGetJsonResult LibraryGet(string accessToken, string id, int timeOut = WxConfig.TIME_OUT)
        {
            string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/wxopen/template/library/get?access_token={0}";
            var data = new
            {
                id = id
            };
            return CommonJsonSend.Send<LibraryGetJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }


        /// <summary>
        /// 组合模板并添加至帐号下的个人模板库
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="keywordIdList">开发者自行组合好的模板关键词列表，关键词顺序可以自由搭配（例如[3,5,4]或[4,5,3]），最多支持10个关键词组合</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "TemplateApi.Add", true)]
        public static AddJsonResult Add(string accessToken, string id, int[] keywordIdList, int timeOut = WxConfig.TIME_OUT)
        {
            string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/wxopen/template/add?access_token={0}";
            var data = new
            {
                id = id,
                keyword_id_list = keywordIdList
            };
            return CommonJsonSend.Send<AddJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        #endregion


        #region 对已存在模板进行操作

        /// <summary>
        /// 获取帐号下已存在的模板列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="offset">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。最后一页的list长度可能小于请求的count</param>
        /// <param name="count">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。最后一页的list长度可能小于请求的count</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "TemplateApi.List", true)]
        public static ListJsonResult List(string accessToken, int offset, int count, int timeOut = WxConfig.TIME_OUT)
        {
            string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/wxopen/template/list?access_token={0}";
            var data = new
            {
                offset = offset,
                count = count
            };
            return CommonJsonSend.Send<ListJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        /// <summary>
        /// 删除帐号下的某个模板
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="templateId">要删除的模板id</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "TemplateApi.Del", true)]
        public static WxOpenJsonResult Del(string accessToken, string templateId, int timeOut = WxConfig.TIME_OUT)
        {
            string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/wxopen/template/del?access_token={0}";
            var data = new
            {
                template_id = templateId
            };
            return CommonJsonSend.Send<WxOpenJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }


        #endregion


        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】小程序模板消息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="templateId"></param>
        /// <param name="data"></param>
        /// <param name="emphasisKeyword">模板需要放大的关键词，不填则默认无放大</param>
        /// <param name="color">模板内容字体的颜色，不填默认黑色（非必填）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="formId">表单提交场景下，为 submit 事件带上的 formId；支付场景下，为本次支付的 prepay_id</param>
        /// <param name="page">点击模板查看详情跳转页面，不填则模板无跳转（非必填）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "TemplateApi.SendTemplateMessageAsync", true)]
        public static async Task<WxOpenJsonResult> SendTemplateMessageAsync(string accessTokenOrAppId, string openId, string templateId, object data, string formId, string page = null, string emphasisKeyword = null, string color = null, int timeOut = WxConfig.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/message/wxopen/template/send?access_token={0}";
                var msgData = new TempleteModel()
                {
                    touser = openId,
                    template_id = templateId,
                    color = color,
                    page = page,
                    form_id = formId,
                    data = data,
                    emphasis_keyword = emphasisKeyword,
                };

                return await CommonJsonSend.SendAsync<WxOpenJsonResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】下发小程序和公众号统一的服务消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="msgData"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "TemplateApi.UniformSendAsync", true)]
        public static async Task<WxOpenJsonResult> UniformSendAsync(string accessTokenOrAppId, UniformSendData msgData, int timeOut = WxConfig.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/message/wxopen/template/uniform_send?access_token={0}";

                return await CommonJsonSend.SendAsync<WxOpenJsonResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #region 模板快速设置

        /// <summary>
        /// 【异步方法】获取小程序模板库标题列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="offset">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="count">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "TemplateApi.LibraryListAsync", true)]
        public static async Task<LibraryListJsonResult> LibraryListAsync(string accessToken, int offset, int count, int timeOut = WxConfig.TIME_OUT)
        {
            string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/wxopen/template/library/list?access_token={0}";
            var data = new
            {
                offset = offset,
                count = count
            };
            return await CommonJsonSend.SendAsync<LibraryListJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        /// <summary>
        /// 【异步方法】获取模板库某个模板标题下关键词库
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "TemplateApi.LibraryGetAsync", true)]
        public static async Task<LibraryGetJsonResult> LibraryGetAsync(string accessToken, string id, int timeOut = WxConfig.TIME_OUT)
        {
            string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/wxopen/template/library/get?access_token={0}";
            var data = new
            {
                id = id
            };
            return await CommonJsonSend.SendAsync<LibraryGetJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }


        /// <summary>
        /// 【异步方法】组合模板并添加至帐号下的个人模板库
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="keywordIdList">开发者自行组合好的模板关键词列表，关键词顺序可以自由搭配（例如[3,5,4]或[4,5,3]），最多支持10个关键词组合</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "TemplateApi.AddAsync", true)]
        public static async Task<AddJsonResult> AddAsync(string accessToken, string id, int[] keywordIdList, int timeOut = WxConfig.TIME_OUT)
        {
            string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/wxopen/template/add?access_token={0}";
            var data = new
            {
                id = id,
                keyword_id_list = keywordIdList
            };
            return await CommonJsonSend.SendAsync<AddJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        #endregion


        #region 对已存在模板进行操作

        /// <summary>
        /// 【异步方法】获取帐号下已存在的模板列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="offset">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。最后一页的list长度可能小于请求的count</param>
        /// <param name="count">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。最后一页的list长度可能小于请求的count</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "TemplateApi.ListAsync", true)]
        public static async Task<ListJsonResult> ListAsync(string accessToken, int offset, int count, int timeOut = WxConfig.TIME_OUT)
        {
            string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/wxopen/template/list?access_token={0}";
            var data = new
            {
                offset = offset,
                count = count
            };
            return await CommonJsonSend.SendAsync<ListJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        /// <summary>
        /// 【异步方法】删除帐号下的某个模板
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="templateId">要删除的模板id</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_MiniProgram, "TemplateApi.DelAsync", true)]
        public static async Task<WxOpenJsonResult> DelAsync(string accessToken, string templateId, int timeOut = WxConfig.TIME_OUT)
        {
            string urlFormat = WxConfig.ApiMpHost + "/cgi-bin/wxopen/template/del?access_token={0}";
            var data = new
            {
                template_id = templateId
            };
            return await CommonJsonSend.SendAsync<WxOpenJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }


        #endregion


        #endregion
    }
}
