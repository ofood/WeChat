﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：CodeApi.cs
    文件功能描述：代码管理
    
    
    创建标识：Senparc - 20170726


----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.Core.CommonAPIs;
using System.IO;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure.Http;

namespace OFoodWeChat.Open.WxaAPIs
{
    public class CodeApi
    {
        #region 同步方法
        /// <summary>
        /// 为授权的小程序帐号上传小程序代码
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="template_id">代码库中的代码模版ID</param>
        /// <param name="ext_json">第三方自定义的配置</param>
        /// <param name="user_version">代码版本号</param>
        /// <param name="user_desc">代码描述</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.Commit", true)]
        public static CodeResultJson Commit(string accessToken, int template_id, string ext_json, string user_version, string user_desc, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/commit?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                template_id = template_id,
                ext_json = ext_json,
                user_version = user_version,
                user_desc = user_desc
            };

            return CommonJsonSend.Send<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取小程序的体验二维码
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.GetQRCode", true)]
        public static CodeResultJson GetQRCode(string accessToken, Stream stream, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/get_qrcode?access_token={0}", accessToken.AsUrlData());

            Get.Download(url, stream);
            return new CodeResultJson()
            {
                errcode = ReturnCode.请求成功
            };
            //CommonJsonSend.Send<CodeResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取授权小程序帐号的可选类目
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.GetCategory", true)]
        public static GetCategoryResultJson GetCategory(string accessToken, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/get_category?access_token={0}", accessToken.AsUrlData());

            return CommonJsonSend.Send<GetCategoryResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取小程序的第三方提交代码的页面配置
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.GetPage", true)]
        public static GetPageResultJson GetPage(string accessToken, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/get_page?access_token={0}", accessToken.AsUrlData());


            return CommonJsonSend.Send<GetPageResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 将第三方提交的代码包提交审核
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.SubmitAudit", true)]
        public static GetAuditStatusResultJson SubmitAudit(string accessToken, List<SubmitAuditPageInfo>  item_list, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/submit_audit?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                item_list = item_list
            };

            return CommonJsonSend.Send<GetAuditStatusResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询某个指定版本的审核状态
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="auditid">提交审核时获得的审核id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.GetAuditStatus", true)]
        public static GetAuditStatusResultJson GetAuditStatus(string accessToken, int auditid, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/get_auditstatus?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                auditid = auditid
            };

            return CommonJsonSend.Send<GetAuditStatusResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询最新一次提交的审核状态
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.GetLatestAuditStatus", true)]
        public static GetAuditStatusResultJson GetLatestAuditStatus(string accessToken, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/get_latest_auditstatus?access_token={0}", accessToken.AsUrlData());

            return CommonJsonSend.Send<GetAuditStatusResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }
        /// <summary>
        /// 发布已通过审核的小程序
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.Release", true)]
        public static CodeResultJson Release(string accessToken, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/release?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
            };

            return CommonJsonSend.Send<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        /// <summary>
        /// 修改小程序线上代码的可见状态
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="action">设置可访问状态，发布后默认可访问，close为不可见，open为可见</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.ChangeVisitStatus", true)]
        public static CodeResultJson ChangeVisitStatus(string accessToken, ChangVisitStatusAction action, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/change_visitstatus?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                action = action.ToString()
            };

            return CommonJsonSend.Send<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        #endregion

#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        /// 为授权的小程序帐号上传小程序代码
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="template_id">代码库中的代码模版ID</param>
        /// <param name="ext_json">第三方自定义的配置</param>
        /// <param name="user_version">代码版本号</param>
        /// <param name="user_desc">代码描述</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.CommitAsync", true)]
        public static async Task<CodeResultJson> CommitAsync(string accessToken, int template_id, string ext_json, string user_version, string user_desc, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/commit?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                template_id = template_id,
                ext_json = ext_json,
                user_version = user_version,
                user_desc = user_desc
            };

            return await CommonJsonSend.SendAsync<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取小程序的体验二维码
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.GetQRCodeAsync", true)]
        public static async Task<CodeResultJson> GetQRCodeAsync(string accessToken, Stream stream, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/get_qrcode?access_token={0}", accessToken.AsUrlData());

            await Get.DownloadAsync(url, stream);
            return new CodeResultJson()
            {
                errcode = ReturnCode.请求成功
            };
            //CommonJsonSend.Send<CodeResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取授权小程序帐号的可选类目
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.GetCategoryAsync", true)]
        public static async Task<GetCategoryResultJson> GetCategoryAsync(string accessToken, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/get_category?access_token={0}", accessToken.AsUrlData());


            return await CommonJsonSend.SendAsync<GetCategoryResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取小程序的第三方提交代码的页面配置
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.GetPageAsync", true)]
        public static async Task<GetPageResultJson> GetPageAsync(string accessToken, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/get_page?access_token={0}", accessToken.AsUrlData());


            return await CommonJsonSend.SendAsync<GetPageResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 将第三方提交的代码包提交审核
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.SubmitAuditAsync", true)]
        public static async Task<GetAuditStatusResultJson> SubmitAuditAsync(string accessToken, List<SubmitAuditPageInfo> item_list, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/submit_audit?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                item_list = item_list
            };

            return await CommonJsonSend.SendAsync<GetAuditStatusResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询某个指定版本的审核状态
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="auditid">提交审核时获得的审核id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.GetAuditStatusAsync", true)]
        public static async Task<GetAuditStatusResultJson> GetAuditStatusAsync(string accessToken, int auditid, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/get_auditstatus?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                auditid = auditid
            };

            return await CommonJsonSend.SendAsync<GetAuditStatusResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询最新一次提交的审核状态
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.GetLatestAuditStatusAsync", true)]
        public static async Task<GetAuditStatusResultJson> GetLatestAuditStatusAsync(string accessToken, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/get_latest_auditstatus?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {

            };

            return await CommonJsonSend.SendAsync<GetAuditStatusResultJson>(null, url, data, CommonJsonSendType.GET, timeOut);
        }
        /// <summary>
        /// 发布已通过审核的小程序
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.ReleaseAsync", true)]
        public static async Task<CodeResultJson> ReleaseAsync(string accessToken, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/release?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
            };

            return await CommonJsonSend.SendAsync<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        /// <summary>
        /// 修改小程序线上代码的可见状态
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="action">设置可访问状态，发布后默认可访问，close为不可见，open为可见</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "CodeApi.ChangeVisitStatusAsync", true)]
        public static async Task<CodeResultJson> ChangeVisitStatusAsync(string accessToken, ChangVisitStatusAction action, int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/change_visitstatus?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                action = action.ToString()
            };

            return await CommonJsonSend.SendAsync<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        #endregion
#endif
    }
}
