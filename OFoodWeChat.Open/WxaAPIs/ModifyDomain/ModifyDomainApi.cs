/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：ModifyDomainApi.cs
    文件功能描述：修改域名接口


    创建标识：Senparc - 20170601

    修改标识：Senparc - 20171201
    修改描述：v1.7.3 修复ModifyDomainApi.ModifyDomain()方法判断问题
        
----------------------------------------------------------------*/

using OFoodWeChat.Open.WxaAPIs.ModifyDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Core.CommonAPIs;

namespace OFoodWeChat.Open.WxaAPIs
{
    public class ModifyDomainApi
    {
        #region 同步方法

        /// <summary>
        /// 修改服务器地址 接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="action">操作类型</param>
        /// <param name="requestdomain">request合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="wsrequestdomain">socket合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="uploaddomain">uploadFile合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="downloaddomain">downloadFile合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "ModifyDomainApi.ModifyDomain", true)]
        public static ModifyDomainResultJson ModifyDomain(string accessToken, ModifyDomainAction action,
            List<string> requestdomain,
            List<string> wsrequestdomain,
            List<string> uploaddomain,
            List<string> downloaddomain,
            int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/modify_domain?access_token={0}", accessToken.AsUrlData());

            object data;

            if (action == ModifyDomainAction.get)
            {
                data = new
                {
                    action = action.ToString()
                };
            }
            else
            {
                data = new
                {
                    action = action.ToString(),
                    requestdomain = requestdomain,
                    wsrequestdomain = wsrequestdomain,
                    uploaddomain = uploaddomain,
                    downloaddomain = downloaddomain
                };
            }

            return CommonJsonSend.Send<ModifyDomainResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        #endregion


#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】修改服务器地址 接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="action">操作类型</param>
        /// <param name="requestdomain">request合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="wsrequestdomain">socket合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="uploaddomain">uploadFile合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="downloaddomain">downloadFile合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "ModifyDomainApi.ModifyDomainAsync", true)]
        public static async Task<ModifyDomainResultJson> ModifyDomainAsync(string accessToken, ModifyDomainAction action,
            List<string> requestdomain,
            List<string> wsrequestdomain,
            List<string> uploaddomain,
            List<string> downloaddomain,
            int timeOut = WxConfig.TIME_OUT)
        {
            var url = string.Format(WxConfig.ApiMpHost + "/wxa/modify_domain?access_token={0}", accessToken.AsUrlData());

            object data;

            if (action == ModifyDomainAction.get)
            {
                data = new
                {
                    action = action.ToString()
                };
            }
            else
            {
                data = new
                {
                    action = action.ToString(),
                    requestdomain = requestdomain,
                    wsrequestdomain = wsrequestdomain,
                    uploaddomain = uploaddomain,
                    downloaddomain = downloaddomain
                };
            }

            return await CommonJsonSend.SendAsync<ModifyDomainResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }


        #endregion
#endif
    }
}
