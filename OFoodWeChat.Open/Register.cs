/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
  
    文件名：Register.cs
    文件功能描述：注册小程序信息
    
    
    创建标识：Senparc - 20180302

    修改标识：Senparc - 20180802
    修改描述：添加自动注册 RegisterOpenComponent() 方法

    修改标识：Senparc - 20180802
    修改描述：添加自动 根据 SenparcWeixinSetting 注册 RegisterOpenComponent() 方法

----------------------------------------------------------------*/


using OFoodWeChat.Open.ComponentAPIs;
using OFoodWeChat.Open.Containers;
using OFoodWeChat.Infrastructure.RegisterServices;
using System;
using OFoodWeChat.Infrastructure.Settings;

namespace OFoodWeChat.Open
{
    /// <summary>
    /// 注册第三方平台
    /// </summary>
    public static class Register
    {
        /// <summary>
        /// 注册第三方平台信息
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="getComponentVerifyTicketFunc">获取ComponentVerifyTicket的方法</param>
        /// <param name="getAuthorizerRefreshTokenFunc">从数据库中获取已存的AuthorizerAccessToken的方法</param>
        /// <param name="authorizerTokenRefreshedFunc">AuthorizerAccessToken更新后的回调</param>
        /// <param name="name">标记名称（如开放平台名称），帮助管理员识别</param>
        /// <returns></returns>
        public static IRegisterService RegisterOpenComponent(this IRegisterService registerService,
            string componentAppId, string componentAppSecret,
            Func<string, string> getComponentVerifyTicketFunc,
            Func<string, string, string> getAuthorizerRefreshTokenFunc,
            Action<string, string, RefreshAuthorizerTokenResult> authorizerTokenRefreshedFunc,
            string name = null)
        {
            ComponentContainer.Register(
                            componentAppId, componentAppSecret,
                            getComponentVerifyTicketFunc,
                            getAuthorizerRefreshTokenFunc,
                            authorizerTokenRefreshedFunc,
                            name);
            return registerService;
        }

        /// <summary>
        /// 根据 SenparcWeixinSetting 自动注册第三方平台信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="ISenparcWeixinSettingForOpen">SenparcWeixinSetting</param>
        /// <param name="name">统一标识，如果为null，则使用 SenparcWeixinSetting.ItemKey </param>
        /// <returns></returns>
        public static IRegisterService RegisterOpenComponent(this IRegisterService registerService, IWeixinSettingForOpen weixinSettingForOpen, Func<string, string> getComponentVerifyTicketFunc,
            Func<string, string, string> getAuthorizerRefreshTokenFunc,
            Action<string, string, RefreshAuthorizerTokenResult> authorizerTokenRefreshedFunc,
            string name = null)
        {
            return RegisterOpenComponent(registerService, weixinSettingForOpen.Component_Appid, weixinSettingForOpen.Component_Secret,
                          getComponentVerifyTicketFunc,
                          getAuthorizerRefreshTokenFunc,
                          authorizerTokenRefreshedFunc,
                          name ?? weixinSettingForOpen.ItemKey);
        }

    }
}
