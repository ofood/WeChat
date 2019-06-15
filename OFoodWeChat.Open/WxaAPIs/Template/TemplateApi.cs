/*----------------------------------------------------------------   
    文件名：TemplateApi.cs
    文件功能描述：小程序模板消息
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Threading.Tasks;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Core.Exceptions;
using OFoodWeChat.Open.Entities;
using OFoodWeChat.Core;

namespace OFoodWeChat.Open.WxaAPIs.Template
{
    /// <summary>
    /// 小程序模板消息接口
    /// </summary>
    public static class TemplateApi
    {
        #region 同步方法


        #region 模板快速设置

        /// <summary>
        /// 获取小程序模板库标题列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="offset">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="count">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [Obsolete("请在小程序模块Senparc.Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！")]
        [ApiBind(PlatformType.WeChat_Open, "TemplateApi.LibraryList", true)]
        public static OpenJsonResult LibraryList(string accessToken, int offset, int count, int timeOut = WxConfig.TIME_OUT)
        {
          throw new WeixinObsoleteException("Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！");
        }

        /// <summary>
        /// 获取模板库某个模板标题下关键词库
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [Obsolete("请在小程序模块Senparc.Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！")]
        [ApiBind(PlatformType.WeChat_Open, "TemplateApi.LibraryGet", true)]
        public static OpenJsonResult LibraryGet(string accessToken, string id, int timeOut = WxConfig.TIME_OUT)
        {
            throw new WeixinObsoleteException("Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！");
        }


        /// <summary>
        /// 组合模板并添加至帐号下的个人模板库
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="keywordIdList">开发者自行组合好的模板关键词列表，关键词顺序可以自由搭配（例如[3,5,4]或[4,5,3]），最多支持10个关键词组合</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [Obsolete("请在小程序模块Senparc.Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！")]
        [ApiBind(PlatformType.WeChat_Open, "TemplateApi.Add", true)]
        public static OpenJsonResult Add(string accessToken, string id, int[] keywordIdList, int timeOut = WxConfig.TIME_OUT)
        {
            throw new WeixinObsoleteException("Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！");
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
        [Obsolete("请在小程序模块Senparc.Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！")]
        [ApiBind(PlatformType.WeChat_Open, "TemplateApi.List", true)]
        public static OpenJsonResult List(string accessToken, int offset, int count, int timeOut = WxConfig.TIME_OUT)
        {
            throw new WeixinObsoleteException("Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！");
        }

        /// <summary>
        /// 删除帐号下的某个模板
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="templateId">要删除的模板id</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [Obsolete("请在小程序模块Senparc.Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！")]
        [ApiBind(PlatformType.WeChat_Open, "TemplateApi.Del", true)]
        public static OpenJsonResult Del(string accessToken, string templateId, int timeOut = WxConfig.TIME_OUT)
        {
            throw new WeixinObsoleteException("Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！");
        }


        #endregion

        #endregion

#if !NET35 && !NET40
        #region 异步方法


        #region 模板快速设置

        /// <summary>
        /// 【异步方法】获取小程序模板库标题列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="offset">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="count">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [Obsolete("请在小程序模块Senparc.Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！")]
        [ApiBind(PlatformType.WeChat_Open, "TemplateApi.LibraryListAsync", true)]
        public static Task<OpenJsonResult> LibraryListAsync(string accessToken, int offset, int count, int timeOut = WxConfig.TIME_OUT)
        {
            throw new WeixinObsoleteException("Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！");
        }

        /// <summary>
        /// 【异步方法】获取模板库某个模板标题下关键词库
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [Obsolete("请在小程序模块Senparc.Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！")]
        [ApiBind(PlatformType.WeChat_Open, "TemplateApi.LibraryGetAsync", true)]
        public static Task<OpenJsonResult> LibraryGetAsync(string accessToken, string id, int timeOut = WxConfig.TIME_OUT)
        {
            throw new WeixinObsoleteException("Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！");
        }


        /// <summary>
        /// 【异步方法】组合模板并添加至帐号下的个人模板库
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="id">模板标题id，可通过接口获取，也可登录小程序后台查看获取</param>
        /// <param name="keywordIdList">开发者自行组合好的模板关键词列表，关键词顺序可以自由搭配（例如[3,5,4]或[4,5,3]），最多支持10个关键词组合</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [Obsolete("请在小程序模块Senparc.Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！")]
        [ApiBind(PlatformType.WeChat_Open, "TemplateApi.AddAsync", true)]
        public static Task<OpenJsonResult> AddAsync(string accessToken, string id, int[] keywordIdList, int timeOut = WxConfig.TIME_OUT)
        {
            throw new WeixinObsoleteException("Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！");
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
        [Obsolete("请在小程序模块Senparc.Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！")]
        [ApiBind(PlatformType.WeChat_Open, "TemplateApi.ListAsync", true)]
        public static async Task<OpenJsonResult> ListAsync(string accessToken, int offset, int count, int timeOut = WxConfig.TIME_OUT)
        {
            throw new WeixinObsoleteException("Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！");
        }

        /// <summary>
        /// 【异步方法】删除帐号下的某个模板
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="templateId">要删除的模板id</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [Obsolete("请在小程序模块Senparc.Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！")]
        [ApiBind(PlatformType.WeChat_Open, "TemplateApi.DelAsync", true)]
        public static async Task<OpenJsonResult> DelAsync(string accessToken, string templateId, int timeOut = WxConfig.TIME_OUT)
        {
            throw new WeixinObsoleteException("Weixin.WxOpen.AdvancedAPIs.TemplateApi下调用同名接口！");
        }

        #endregion


        #endregion
#endif
    }
}