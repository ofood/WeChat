/*----------------------------------------------------------------
    文件名：Register.cs
    文件功能描述：注册小程序信息
    修改标识：Senparc - 20180802
    修改描述：添加自动 根据 SenparcWeixinSetting 注册 RegisterWxOpenAccount() 方法

----------------------------------------------------------------*/
using System;

using OFoodWeChat.Core.Exceptions;
using OFoodWeChat.Infrastructure.RegisterServices;
using OFoodWeChat.MP.Containers;
using OFoodWeChat.Infrastructure.Settings;

namespace OFoodWeChat.WxOpen
{
    /// <summary>
    /// 注册小程序
    /// </summary>
    public static class WxOpenRegister
    {
        /// <summary>
        /// 注册小程序信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="appId">微信公众号后台的【开发】>【基本配置】中的“AppID(应用ID)”</param>
        /// <param name="appSecret">微信公众号后台的【开发】>【基本配置】中的“AppSecret(应用密钥)”</param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别</param>
        /// <returns></returns>
        [Obsolete("请统一使用OFoodWeChat.MP.Register.RegisterMpAccount()方法进行注册！")]
        public static IRegisterService RegisterWxOpenAccount(this IRegisterService registerService, string appId, string appSercet, string name = null)
        {
            throw new WeixinException("请统一使用OFoodWeChat.MP.Register.RegisterMpAccount()方法进行注册！");
        }

        /// <summary>
        /// 根据 SenparcWeixinSetting 自动注册小程序信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="weixinSettingForWxOpen">SenparcWeixinSetting</param>
        /// <param name="name">统一标识，如果为null，则使用 SenparcWeixinSetting.ItemKey </param>
        /// <returns></returns>
        public static IRegisterService RegisterWxOpenAccount(this IRegisterService registerService, IWeixinSettingForWxOpen weixinSettingForWxOpen, string name = null)
        {
            AccessTokenContainer.Register(weixinSettingForWxOpen.WxOpenAppId, weixinSettingForWxOpen.WxOpenAppSecret, name ?? weixinSettingForWxOpen.ItemKey);
            return registerService;
        }

    }
}
