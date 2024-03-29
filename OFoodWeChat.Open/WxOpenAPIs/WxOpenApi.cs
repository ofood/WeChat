﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Open.AccountAPIs;
using OFoodWeChat.Open.WxOpenAPIs.CategoryListJson;
using OFoodWeChat.Open.WxOpenAPIs.GetCategoryJson;
using OFoodWeChat.Open.WxOpenAPIs.AddCategoryJson;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Open.Entities;
using OFoodWeChat.Core;

namespace OFoodWeChat.Open.WxOpenAPIs
{
    public class WxOpenApi
    {
        #region 同步方法

        #region 换绑小程序管理员接口

        /*
         *  流程
         *  步骤一：从第三方平台页面发起，并跳转至微信公众平台指定换绑页面。
         *  步骤二：小程序原管理员扫码，并填写原管理员身份证信息确认。
         *  步骤三：填写新管理员信息(姓名、身份证、手机号)，使用新管理员的微信确认。
         *  步骤四：点击提交后跳转至第三方平台页面，第三方平台回调对应 api 完成换绑流程。
         */

        /// <summary>
        /// 从第三方平台跳转至微信公众平台授权注册页面
        /// </summary>
        /// <param name="component_appid">第三方平台的appid</param>
        /// <param name="appid">公众号的 appid</param>
        /// <param name="redirect_uri">新管理员信息填写完成点击提交后，将跳转到该地址
        /// (注：Host需和第三方平台在微信开放平台上面填写的登录授权的发起页域名一致)
        /// <para>点击页面提交按钮。 跳转回第三方平台，会在上述 redirect_uri 后拼接 taskid=*</para>
        /// <para><see cref="AccountApi.ComponentRebindAdmin"/>方法</para>
        /// </param>
        [ApiBind(PlatformType.WeChat_Open, "WxOpenApi.ComponentRebindAdmin", true)]
        public static string ComponentRebindAdmin(string component_appid, string appid, string redirect_uri)
        {
            var url =
                $"https://mp.weixin.qq.com/wxopen/componentrebindadmin?appid={appid}&component_appid={component_appid}&redirect_uri={redirect_uri.AsUrlData()}";
            return url;
        }

        #endregion

        #region 类目相关接口

        /// <summary>
        /// 获取账号可以设置的所有类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "WxOpenApi.GetAllCategories", true)]
        public static CategoryListJsonResult GetAllCategories(string accessToken)
        {
            var url = $"{WxConfig.ApiMpHost}/cgi-bin/wxopen/getallcategories?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.Send<CategoryListJsonResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 添加类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="addCategoryData">添加类目参数</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "WxOpenApi.AddCategory", true)]
        public static OpenJsonResult AddCategory(string accessToken, IList<AddCategoryData> addCategoryData)
        {
            var url = $"{WxConfig.ApiMpHost}/cgi-bin/wxopen/addcategory?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.Send<OpenJsonResult>(null, url, addCategoryData);
        }

        /// <summary>
        /// 删除类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="first">一级类目ID</param>
        /// <param name="second">二级类目ID</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "WxOpenApi.DeleteCategory", true)]
        public static OpenJsonResult DeleteCategory(string accessToken, int first, int second)
        {
            var url = $"{WxConfig.ApiMpHost}/cgi-bin/wxopen/deletecategory?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                first = first,
                second = second
            };
            return CommonJsonSend.Send<OpenJsonResult>(null, url, data);
        }

        /// <summary>
        /// 获取账号已经设置的所有类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "WxOpenApi.GetCategory", true)]
        public static GetCategoryJsonResult GetCategory(string accessToken)
        {
            var url = $"{WxConfig.ApiMpHost}/cgi-bin/wxopen/getcategory?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.Send<GetCategoryJsonResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 添加类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="first">一级类目ID</param>
        /// <param name="second">二级类目ID</param>
        /// <param name="certicates">资质名称,资质图片</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "WxOpenApi.ModifyCategory", true)]
        public static OpenJsonResult ModifyCategory(string accessToken, int first, int second,
            IList<KeyValuePair<string, string>> certicates)
        {
            var url = $"{WxConfig.ApiMpHost}/cgi-bin/wxopen/modifycategory?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                first = first,
                second = second,
                certicates = certicates
            };
            return CommonJsonSend.Send<OpenJsonResult>(null, url, data);
        }

        #endregion

        #endregion


#if !NET35 && !NET40
        #region 异步方法

        #region 类目相关接口

        /// <summary>
        /// 获取账号可以设置的所有类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "WxOpenApi.GetAllCategoriesAsync", true)]
        public static async Task<CategoryListJsonResult> GetAllCategoriesAsync(string accessToken)
        {
            var url = $"{WxConfig.ApiMpHost}/cgi-bin/wxopen/getallcategories?access_token={accessToken.AsUrlData()}";
            return await CommonJsonSend.SendAsync<CategoryListJsonResult>(null, url, null,
                CommonJsonSendType.GET);
        }

        /// <summary>
        /// 添加类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="addCategoryData">添加类目参数</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "WxOpenApi.AddCategoryAsync", true)]
        public static async Task<OpenJsonResult> AddCategoryAsync(string accessToken, IList<AddCategoryData> addCategoryData)
        {
            var url = $"{WxConfig.ApiMpHost}/cgi-bin/wxopen/addcategory?access_token={accessToken.AsUrlData()}";
            return await CommonJsonSend.SendAsync<OpenJsonResult>(null, url, addCategoryData);
        }

        /// <summary>
        /// 删除类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="first">一级类目ID</param>
        /// <param name="second">二级类目ID</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "WxOpenApi.DeleteCategoryAsync", true)]
        public static async Task<OpenJsonResult> DeleteCategoryAsync(string accessToken, int first, int second)
        {
            var url = $"{WxConfig.ApiMpHost}/cgi-bin/wxopen/deletecategory?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                first = first,
                second = second
            };
            return await CommonJsonSend.SendAsync<OpenJsonResult>(null, url, data);
        }

        /// <summary>
        /// 获取账号已经设置的所有类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "WxOpenApi.GetCategoryAsync", true)]
        public static async Task<GetCategoryJsonResult> GetCategoryAsync(string accessToken)
        {
            var url = $"{WxConfig.ApiMpHost}/cgi-bin/wxopen/getcategory?access_token={accessToken.AsUrlData()}";
            return await CommonJsonSend.SendAsync<GetCategoryJsonResult>(null, url, null,
                CommonJsonSendType.GET);
        }

        /// <summary>
        /// 添加类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="first">一级类目ID</param>
        /// <param name="second">二级类目ID</param>
        /// <param name="certicates">资质名称,资质图片</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_Open, "WxOpenApi.ModifyCategoryAsync", true)]
        public static async Task<OpenJsonResult> ModifyCategoryAsync(string accessToken, int first, int second,
            IList<KeyValuePair<string, string>> certicates)
        {
            var url = $"{WxConfig.ApiMpHost}/cgi-bin/wxopen/modifycategory?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                first = first,
                second = second,
                certicates = certicates
            };
            return await CommonJsonSend.SendAsync<OpenJsonResult>(null, url, data);
        }

        #endregion

        #endregion
#endif
    }
}