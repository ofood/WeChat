﻿/*----------------------------------------------------------------
    文件名：Register.cs
    文件功能描述：OFoodWeChat.Work 快捷注册流程
    修改标识：Senparc - 20180802
    修改描述：添加自动 根据 SenparcWeixinSetting 注册 RegisterWorkAccount() 方法

----------------------------------------------------------------*/

using OFoodWeChat.Work.Containers;
using OFoodWeChat.Infrastructure.RegisterServices;
using OFoodWeChat.Infrastructure.Settings;

namespace OFoodWeChat.Work
{
    /// <summary>
    /// 注册企业微信
    /// </summary>
    public static class Register
    {
        /// <summary>
        /// 注册公众号（或小程序）信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="weixinCorpId">weixinCorpId</param>
        /// <param name="weixinCorpSecret">weixinCorpSecret</param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别</param>
        /// <returns></returns>
        public static IRegisterService RegisterWorkAccount(this IRegisterService registerService, string weixinCorpId, string weixinCorpSecret, string name = null)
        {
            ProviderTokenContainer.Register(weixinCorpId, weixinCorpSecret, name);
            return registerService;
        }

        /// <summary>
        /// 根据 SenparcWeixinSetting 自动注册第三方平台信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="weixinSettingForWork">SenparcWeixinSetting</param>
        /// <param name="name">统一标识，如果为null，则使用 SenparcWeixinSetting.ItemKey </param>
        /// <returns></returns>
        public static IRegisterService RegisterWorkAccount(this IRegisterService registerService, IWeixinSettingForWork weixinSettingForWork, string name = null)
        {
            return RegisterWorkAccount(registerService, weixinSettingForWork.WeixinCorpId, weixinSettingForWork.WeixinCorpSecret, name ?? weixinSettingForWork.ItemKey);
        }
    }
}
