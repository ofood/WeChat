﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GroupsAPI.cs
    文件功能描述：用户组接口
    

----------------------------------------------------------------*/

/* 
   API地址：http://mp.weixin.qq.com/wiki/0/56d992c605a97245eb7e617854b169fc.html
*/

using System.Threading.Tasks;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.MP.AdvancedAPIs.Groups;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Core;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.MP.Entities;
using OFoodWeChat.MP.CommonAPIs;

namespace OFoodWeChat.MP.AdvancedAPIs
{
    /// <summary>
    /// 用户组接口
    /// </summary>
    public static class GroupsApi
    {
        #region 同步方法
        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "GroupsApi.Create", true)]
        public static CreateGroupResult Create(string accessTokenOrAppId, string name, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/groups/create?access_token={0}";
                var data = new
                {
                    group = new
                    {
                        name = name
                    }
                };
                return CommonJsonSend.Send<CreateGroupResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取所有分组
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "GroupsApi.Get", true)]
        public static GroupsJson Get(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/groups/get?access_token={0}";
                var url = string.Format(urlFormat, accessToken.AsUrlData());
                return CommonJsonSend.Send< GroupsJson >(null, url, null, CommonJsonSendType.GET);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取用户分组
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "GroupsApi.GetId", true)]
        public static GetGroupIdResult GetId(string accessTokenOrAppId, string openId, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/groups/getid?access_token={0}";
                var data = new { openid = openId };
                return CommonJsonSend.Send<GetGroupIdResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 修改分组名
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="id"></param>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "GroupsApi.Update", true)]
        public static WxJsonResult Update(string accessTokenOrAppId, int id, string name, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/groups/update?access_token={0}";
                var data = new
                {
                    group = new
                    {
                        id = id,
                        name = name
                    }
                };
                return MpCommonJsonSend.Send(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="toGroupId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "GroupsApi.MemberUpdate", true)]
        public static WxJsonResult MemberUpdate(string accessTokenOrAppId, string openId, int toGroupId, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/groups/members/update?access_token={0}";
                var data = new
                {
                    openid = openId,
                    to_groupid = toGroupId
                };
                return MpCommonJsonSend.Send(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 批量移动用户分组
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="toGroupId">分组id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="openIds">用户唯一标识符openid的列表（size不能超过50）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "GroupsApi.BatchUpdate", true)]
        public static WxJsonResult BatchUpdate(string accessTokenOrAppId, int toGroupId, int timeOut = WxConfig.TIME_OUT, params string[] openIds)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/groups/members/batchupdate?access_token={0}";

                var data = new
                {
                    openid_list = openIds,
                    to_groupid = toGroupId
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="groupId">分组id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "GroupsApi.Delete", true)]
        public static WxJsonResult Delete(string accessTokenOrAppId, int groupId, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/groups/delete?access_token={0}";

                var data = new
                {
                    group = new
                    {
                        id = groupId
                    }
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】创建分组
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "GroupsApi.CreateAsync", true)]
        public static async Task<CreateGroupResult> CreateAsync(string accessTokenOrAppId, string name, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/groups/create?access_token={0}";
               var data = new
               {
                   group = new
                   {
                       name = name
                   }
               };
               return await CommonJsonSend.SendAsync<CreateGroupResult>(accessToken, urlFormat, data, timeOut: timeOut);

           }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取所有分组
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "GroupsApi.GetAsync", true)]
        public static async Task<GroupsJson> GetAsync(string accessTokenOrAppId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/groups/get?access_token={0}";
               var url = string.Format(urlFormat, accessToken.AsUrlData());
               return await CommonJsonSend.SendAsync<GroupsJson>(null, url, null, CommonJsonSendType.GET);

           }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取用户分组
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "GroupsApi.GetIdAsync", true)]
        public static async Task<GetGroupIdResult> GetIdAsync(string accessTokenOrAppId, string openId, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/groups/getid?access_token={0}";
               var data = new { openid = openId };
               return await CommonJsonSend.SendAsync<GetGroupIdResult>(accessToken, urlFormat, data, timeOut: timeOut);

           }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】修改分组名
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="id"></param>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "GroupsApi.UpdateAsync", true)]
        public static async Task<WxJsonResult> UpdateAsync(string accessTokenOrAppId, int id, string name, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/groups/update?access_token={0}";
               var data = new
               {
                   group = new
                   {
                       id = id,
                       name = name
                   }
               };
               return await MpCommonJsonSend.SendAsync(accessToken, urlFormat, data, timeOut: timeOut);

           }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】移动用户分组
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="toGroupId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "GroupsApi.MemberUpdateAsync", true)]
        public static async Task<WxJsonResult> MemberUpdateAsync(string accessTokenOrAppId, string openId, int toGroupId, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/groups/members/update?access_token={0}";
               var data = new
               {
                   openid = openId,
                   to_groupid = toGroupId
               };
               return await MpCommonJsonSend.SendAsync(accessToken, urlFormat, data, timeOut: timeOut);

           }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】批量移动用户分组
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="toGroupId">分组id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="openIds">用户唯一标识符openid的列表（size不能超过50）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "GroupsApi.BatchUpdateAsync", true)]
        public static async Task<WxJsonResult> BatchUpdateAsync(string accessTokenOrAppId, int toGroupId, int timeOut = WxConfig.TIME_OUT, params string[] openIds)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/groups/members/batchupdate?access_token={0}";

               var data = new
               {
                   openid_list = openIds,
                   to_groupid = toGroupId
               };

               return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);

           }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="groupId">分组id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "GroupsApi.DeleteAsync", true)]
        public static async Task<WxJsonResult> DeleteAsync(string accessTokenOrAppId, int groupId, int timeOut = WxConfig.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/groups/delete?access_token={0}";

               var data = new
               {
                   group = new
                   {
                       id = groupId
                   }
               };

               return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);

           }, accessTokenOrAppId);
        }
        #endregion
    }
}
