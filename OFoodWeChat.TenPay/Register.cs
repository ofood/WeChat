using OFoodWeChat.Infrastructure.RegisterServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OFoodWeChat.TenPay.V2;
using OFoodWeChat.TenPay.V3;
using OFoodWeChat.Infrastructure.Settings;

namespace OFoodWeChat.TenPay
{
    public static class Register
    {

        /// <summary>
        /// 注册微信支付Tenpay（注意：新注册账号请使用RegisterTenpayV3！
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="tenPayInfo">微信支付（旧版本）参数</param>
        /// <param name="name">公众号唯一标识名称</param>
        /// <returns></returns>
        public static IRegisterService RegisterTenpayOld(this IRegisterService registerService, Func<TenPayInfo> tenPayInfo, string name)
        {
            TenPayInfoCollection.Register(tenPayInfo(), name);
            return registerService;
        }


        /// <summary>
        /// 根据 SenparcWeixinSetting 自动注册微信支付Tenpay（注意：新注册账号请使用RegisterTenpayV3！）
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="weixinSettingForOldTepay">ISenparcWeixinSetting</param>
        /// <param name="name">统一标识，如果为null，则使用 weixinSettingForOldTepay.ItemKey </param>
        /// <returns></returns>
        public static IRegisterService RegisterTenpayOld(this IRegisterService registerService, IWeixinSettingForOldTenpay weixinSettingForOldTepay, string name)
        {
            Func<TenPayInfo> func = () => new TenPayInfo(weixinSettingForOldTepay);
            return RegisterTenpayOld(registerService, func, name ?? weixinSettingForOldTepay.ItemKey);
        }

        /// <summary>
        /// 注册微信支付TenpayV3
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="tenPayV3Info">微信支付（新版本 V3）参数</param>
        /// <param name="name">公众号唯一标识名称</param>
        /// <returns></returns>
        public static IRegisterService RegisterTenpayV3(this IRegisterService registerService, Func<TenPayV3Info> tenPayV3Info, string name)
        {
            TenPayV3InfoCollection.Register(tenPayV3Info(), name);
            return registerService;
        }

        /// <summary>
        /// 根据 SenparcWeixinSetting 自动注册微信支付Tenpay（注意：新注册账号请使用RegisterTenpayV3！
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="weixinSettingForTenpayV3">ISenparcWeixinSetting</param>
        /// <param name="name">统一标识，如果为null，则使用 SenparcWeixinSetting.ItemKey </param>
        /// <returns></returns>
        public static IRegisterService RegisterTenpayV3(this IRegisterService registerService, IWeixinSettingForTenpayV3 weixinSettingForTenpayV3, string name)
        {
            Func<TenPayV3Info> func = () => new TenPayV3Info(weixinSettingForTenpayV3);
            return RegisterTenpayV3(registerService, func, name ?? weixinSettingForTenpayV3.ItemKey);
        }

    }
}
