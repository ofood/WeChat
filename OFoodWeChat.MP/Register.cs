/*----------------------------------------------------------------
    文件名：Register.cs
    文件功能描述：OFoodWeChat.MP 快捷注册流程
----------------------------------------------------------------*/
using OFoodWeChat.MP.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.Infrastructure.RegisterServices;
using OFoodWeChat.Infrastructure.Settings;

namespace OFoodWeChat.MP
{
    /// <summary>
    /// 公众号账号信息注册
    /// </summary>
    public static class Register
    {
        /// <summary>
        /// 注册公众号（或小程序）信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="appId">微信公众号后台的【开发】>【基本配置】中的“AppID(应用ID)”</param>
        /// <param name="appSecret">微信公众号后台的【开发】>【基本配置】中的“AppSecret(应用密钥)”</param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别</param>
        /// <returns></returns>
        public static IRegisterService RegisterMpAccount(this IRegisterService registerService, string appId, string appSecret, string name)
        {
            AccessTokenContainer.Register(appId, appSecret, name);
            return registerService;
        }

        /// <summary>
        /// 根据 SenparcWeixinSetting 自动注册公众号信息（包括JsApi）
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="weixinSettingForMP">SenparcWeixinSetting</param>
        /// <param name="name">统一标识，如果为null，则使用 weixinSettingForMP.ItemKey </param>
        /// <returns></returns>
        public static IRegisterService RegisterMpAccount(this IRegisterService registerService, IWeixinSettingForMP weixinSettingForMP, string name = null)
        {
            return RegisterMpAccount(registerService, weixinSettingForMP.WeixinAppId, weixinSettingForMP.WeixinAppSecret, name ?? weixinSettingForMP.ItemKey);
        }

        /// <summary>
        /// 注册公众号（或小程序）的JSApi（RegisterMpAccount注册过程中已包含）
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="appId">微信公众号后台的【开发】>【基本配置】中的“AppID(应用ID)”</param>
        /// <param name="appSecret">微信公众号后台的【开发】>【基本配置】中的“AppSecret(应用密钥)”</param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别</param>
        /// <returns></returns>
        public static IRegisterService RegisterMpJsApiTicket(this IRegisterService registerService, string appId, string appSecret, string name)
        {
            JsApiTicketContainer.Register(appId, appSecret, name);
            return registerService;
        }

    }
}
