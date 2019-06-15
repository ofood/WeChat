/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：CodeApi.cs
    文件功能描述：小程序代码模版库管理
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Open.Entities;

namespace OFoodWeChat.Open.WxaAPIs
{
    /// <summary>
    /// 小程序代码模版库管理
    /// </summary>
    public class CodeTemplateApi
    {
        #region 同步方法
        /// <summary>
        /// 获取草稿箱内的所有临时代码草稿
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeTemplateApi.GetTemplateDraftList", true)]
        public static GetTemplateDraftListResultJson GetTemplateDraftList(string accessToken, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/gettemplatedraftlist?access_token={0}", accessToken.AsUrlData());
            
            return CommonJsonSend.Send<GetTemplateDraftListResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取代码模版库中的所有小程序代码模版
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeTemplateApi.GetTemplateList", true)]
        public static GetTemplateListResultJson GetTemplateList(string accessToken, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/gettemplatelist?access_token={0}", accessToken.AsUrlData());

            return CommonJsonSend.Send<GetTemplateListResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 将草稿箱的草稿选为小程序代码模版
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="draft_id">草稿ID，本字段可通过“获取草稿箱内的所有临时代码草稿”接口获得</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeTemplateApi.AddToTemplate", true)]
        public static OpenJsonResult AddToTemplate(string accessToken, int draft_id, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/addtotemplate?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                draft_id = draft_id
            };

            return CommonJsonSend.Send<OpenJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 删除指定小程序代码模版
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="template_id">要删除的模版ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeTemplateApi.DeleteTemplate", true)]
        public static OpenJsonResult DeleteTemplate(string accessToken, int template_id, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/deletetemplate?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                template_id = template_id
            };

            return CommonJsonSend.Send<OpenJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        /// 获取草稿箱内的所有临时代码草稿
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeTemplateApi.GetTemplateDraftListAsync", true)]
        public static async Task<GetTemplateDraftListResultJson> GetTemplateDraftListAsync(string accessToken, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/gettemplatedraftlist?access_token={0}", accessToken.AsUrlData());

            return await CommonJsonSend.SendAsync<GetTemplateDraftListResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取代码模版库中的所有小程序代码模版
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeTemplateApi.GetTemplateListAsync", true)]
        public static async Task<GetTemplateListResultJson> GetTemplateListAsync(string accessToken, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/gettemplatelist?access_token={0}", accessToken.AsUrlData());

            return await CommonJsonSend.SendAsync<GetTemplateListResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 将草稿箱的草稿选为小程序代码模版
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="draft_id">草稿ID，本字段可通过“获取草稿箱内的所有临时代码草稿”接口获得</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeTemplateApi.AddToTemplateAsync", true)]
        public static async Task<OpenJsonResult> AddToTemplateAsync(string accessToken, int draft_id, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/addtotemplate?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                draft_id = draft_id
            };

            return await CommonJsonSend.SendAsync<OpenJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 删除指定小程序代码模版
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="template_id">要删除的模版ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeTemplateApi.DeleteTemplateAsync", true)]
        public static async Task<OpenJsonResult> DeleteTemplateAsync(string accessToken, int template_id, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/deletetemplate?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                template_id = template_id
            };

            return await CommonJsonSend.SendAsync<OpenJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        #endregion
#endif
    }
}
