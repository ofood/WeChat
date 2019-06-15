/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：TenPayInfoCollection.cs
    文件功能描述：微信支付信息集合，Key为商户号（PartnerId）


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20180707
    修改描述：TenPayInfoCollection 的 Register() 的微信参数自动添加到 Config.SenparcWeixinSetting.Items 下

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Core.Exceptions;
using OFoodWeChat.Core;

namespace OFoodWeChat.TenPay.V2
{
    /// <summary>
    /// 微信支付信息集合，Key为商户号（PartnerId）
    /// </summary>
    public class TenPayInfoCollection : Dictionary<string, TenPayInfo>
    {
        /// <summary>
        /// 微信支付信息集合，Key为商户号（PartnerId）
        /// </summary>
        public static TenPayInfoCollection Data = new TenPayInfoCollection();

        /// <summary>
        /// 注册WeixinPayInfo信息
        /// </summary>
        /// <param name="weixinPayInfo"></param>
        /// <param name="name">公众号唯一标识（或名称）</param>
        public static void Register(TenPayInfo weixinPayInfo, string name)
        {
            Data[weixinPayInfo.PartnerId] = weixinPayInfo;

            //添加到全局变量
            if (!name.IsNullOrEmpty())
            {
                WxConfig.WeixinSetting.Items[name].WeixinPay_PartnerId = weixinPayInfo.PartnerId;
                WxConfig.WeixinSetting.Items[name].WeixinPay_Key = weixinPayInfo.Key;
                WxConfig.WeixinSetting.Items[name].WeixinPay_AppId = weixinPayInfo.AppId;
                WxConfig.WeixinSetting.Items[name].WeixinPay_AppKey = weixinPayInfo.AppKey;
                WxConfig.WeixinSetting.Items[name].WeixinPay_TenpayNotify = weixinPayInfo.TenPayNotify;
            }
        }

        public new TenPayInfo this[string key]
        {
            get
            {
                if (!base.ContainsKey(key))
                {
                    throw new WeixinException(string.Format("WeixinPayInfoCollection尚未注册Partner：{0}", key));
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

        public TenPayInfoCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        {

        }
    }
}
