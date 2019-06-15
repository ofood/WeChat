/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：CommonApi.Menu.Conditional
    文件功能描述：个性化自定义菜单接口


    创建标识：Senparc - 20151222

    修改标识：Senparc - 20151222
    修改描述：v13.5.1 添加个性化菜单接口

    修改标识：Senparc - 20170317
    修改描述：v14.3.133 修复CommonApi.CreateMenuConditional()方法调用出现“invalid button size”错误的问题
    
----------------------------------------------------------------*/

/*
    API：http://mp.weixin.qq.com/wiki/0/c48ccd12b69ae023159b4bfaa7c39c20.html
 */

using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Infrastructure.Helpers.Serializers;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Core;
using OFoodWeChat.MP.Entities;
using OFoodWeChat.MP.Entities.Menu;

namespace OFoodWeChat.MP.CommonAPIs
{
    /// <summary>
    /// 公众号公用接口
    /// </summary>
    public partial class CommonApi
    {
        /// <summary>
        /// 创建个性化菜单
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId。当为AppId时，如果AccessToken错误将自动获取一次。当为null时，获取当前注册的第一个AppId。</param>
        /// <param name="buttonData">菜单内容</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CommonApi.CreateMenuConditional", true)]
        public static CreateMenuConditionalResult CreateMenuConditional(string accessTokenOrAppId, ConditionalButtonGroup buttonData, int timeOut = WxConfig.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
             {
                 var urlFormat = WxConfig.ApiMpHost + "/cgi-bin/menu/addconditional?access_token={0}";
                 //var jsonSetting = new JsonSetting(true);//设置成true会导致发布失败
                 var jsonSetting = new JsonSetting(false);
                 return CommonJsonSend.Send<CreateMenuConditionalResult>(accessToken, urlFormat, buttonData, timeOut: timeOut, jsonSetting: jsonSetting);

             }, accessTokenOrAppId);
        }


        #region GetMenu

        /* 使用普通自定义菜单查询接口可以获取默认菜单和全部个性化菜单信息，请见自定义菜单查询接口的说明 */

        /// <summary>
        /// 测试个性化菜单匹配结果
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="userId">可以是粉丝的OpenID，也可以是粉丝的微信号。</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CommonApi.TryMatch", true)]
        public static MenuTryMatchResult TryMatch(string accessTokenOrAppId, string userId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/menu/trymatch?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    user_id = userId
                };

                return CommonJsonSend.Send<MenuTryMatchResult>(accessToken, url, data, CommonJsonSendType.POST);

            }, accessTokenOrAppId);
        }

        #endregion

        /// <summary>
        /// 删除个性化菜单
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="menuId">菜单Id</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "CommonApi.DeleteMenuConditional", true)]
        public static WxJsonResult DeleteMenuConditional(string accessTokenOrAppId, string menuId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(WxConfig.ApiMpHost + "/cgi-bin/menu/delconditional?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    menuid = menuId
                };

                return MpCommonJsonSend.Send(accessToken, url, data, CommonJsonSendType.POST);

            }, accessTokenOrAppId);

        }

        /* 使用普通自定义菜单删除接口可以删除所有自定义菜单（包括默认菜单和全部个性化菜单），请见自定义菜单删除接口的说明。 */
    }
}
