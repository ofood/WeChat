﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：TenPayV3InfoCollection.cs
    文件功能描述：微信支付V3信息集合，Key为商户号（MchId）


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20180707
    修改描述：TenPayV3InfoCollection 的 Register() 的微信参数自动添加到 Config.SenparcWeixinSetting.Items 下

    修改标识：Senparc - 20180802
    修改描述：v15.2.0 SenparcWeixinSetting 添加 TenPayV3_WxOpenTenpayNotify 属性，用于设置小程序支付回调地址


    TODO：升级为Container
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using OFoodWeChat.Core;
using OFoodWeChat.Core.Exceptions;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Infrastructure.Settings;

namespace OFoodWeChat.TenPay.V3
{
    /// <summary>
    /// 微信支付信息集合，Key为商户号（MchId）
    /// </summary>
    public class TenPayV3InfoCollection : Dictionary<string, TenPayV3Info>
    {
        /// <summary>
        /// 微信支付信息集合，Key为商户号（MchId）
        /// </summary>
        public static TenPayV3InfoCollection Data = new TenPayV3InfoCollection();

        /// <summary>
        /// 获取完整件
        /// </summary>
        /// <param name="mchId"></param>
        /// <param name="subMchId"></param>
        /// <returns></returns>
        public static string GetKey(string mchId, string subMchId)
        {
            return mchId + "_" + subMchId;
        }

        /// <summary>
        /// 获取完整件
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3">ISenparcWeixinSettingForTenpayV3，也可以直接传入 SenparcWeixinSetting</param>
        /// <returns></returns>
        public static string GetKey(IWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3)
        {
            return GetKey(senparcWeixinSettingForTenpayV3.TenPayV3_MchId, senparcWeixinSettingForTenpayV3.TenPayV3_SubMchId);
        }

        /// <summary>
        /// 注册TenPayV3Info信息
        /// </summary>
        /// <param name="tenPayV3Info"></param>
        /// <param name="name">公众号唯一标识（或名称）</param>
        public static void Register(TenPayV3Info tenPayV3Info,string name)
        {
            var key = GetKey(tenPayV3Info.MchId, tenPayV3Info.Sub_MchId);
            Data[key] = tenPayV3Info;

            //添加到全局变量
            if (!name.IsNullOrEmpty())
            {
                WxConfig.WeixinSetting.Items[name].TenPayV3_AppId = tenPayV3Info.AppId;
                WxConfig.WeixinSetting.Items[name].TenPayV3_AppSecret = tenPayV3Info.AppSecret;
                WxConfig.WeixinSetting.Items[name].TenPayV3_MchId = tenPayV3Info.MchId;
                WxConfig.WeixinSetting.Items[name].TenPayV3_Key = tenPayV3Info.Key;
                WxConfig.WeixinSetting.Items[name].TenPayV3_TenpayNotify = tenPayV3Info.TenPayV3Notify;
                WxConfig.WeixinSetting.Items[name].TenPayV3_WxOpenTenpayNotify = tenPayV3Info.TenPayV3_WxOpenNotify;
                WxConfig.WeixinSetting.Items[name].TenPayV3_Sub_MchId = tenPayV3Info.Sub_MchId;
                WxConfig.WeixinSetting.Items[name].TenPayV3_Sub_AppId = tenPayV3Info.Sub_AppId;
            }
        }

        /// <summary>
        /// 索引 TenPayV3Info
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new TenPayV3Info this[string key]
        {
            get
            {
                if (!base.ContainsKey(key))
                {
                    throw new WeixinException(string.Format("TenPayV3InfoCollection尚未注册Mch：{0}", key));
                }
                else
                {
                    return base[key];
                }
            }
            set
            {
                base[key] = value;
            }
        }

        /// <summary>
        /// TenPayV3InfoCollection 构造函数
        /// </summary>
        public TenPayV3InfoCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        {

        }
    }
}
