﻿/*----------------------------------------------------------------
   
    文件功能描述：Weixin SDK 中单个公众号配置信息
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.Infrastructure.Settings
{
    /// <summary>
    /// Weixin SDK 中单个公众号配置信息
    /// </summary>
    public class WeixinSettingItem : IWeixinSettingForMP, IWeixinSettingForWxOpen, IWeixinSettingForWork, IWeixinSettingForOldTenpay,
                                            IWeixinSettingForTenpayV3, IWeixinSettingForOpen, IWeixinSettingForExtension
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public virtual string ItemKey { get; set; }

        #region 公众号

        /// <summary>
        /// 公众号Token
        /// </summary>
        public virtual string Token { get; set; }
        /// <summary>
        /// 公众号消息加密Key
        /// </summary>
        public virtual string EncodingAESKey { get; set; }
        /// <summary>
        /// 公众号AppId
        /// </summary>
        public virtual string WeixinAppId { get; set; }
        /// <summary>
        /// 公众号AppSecret
        /// </summary>
        public virtual string WeixinAppSecret { get; set; }

        #endregion

        #region 小程序

        /// <summary>
        /// 小程序AppId
        /// </summary>
        public virtual string WxOpenAppId { get; set; }
        /// <summary>
        /// 小程序AppSecret
        /// </summary>
        public virtual string WxOpenAppSecret { get; set; }
        /// <summary>
        /// 小程序 Token
        /// </summary>
        public virtual string WxOpenToken { get; set; }
        /// <summary>
        /// 小程序EncodingAESKey 
        /// </summary>
        public virtual string WxOpenEncodingAESKey { get; set; }

        #endregion

        #region 企业微信

        /// <summary>
        /// 企业微信CorpId
        /// </summary>
        public virtual string WeixinCorpId { get; set; }
        /// <summary>
        /// 企业微信CorpSecret
        /// </summary>
        public virtual string WeixinCorpSecret { get; set; }

        #endregion

        #region 微信支付

        #region 微信支付V2（旧版）
        /// <summary>
        /// WeixinPay_PartnerId（微信支付V2）
        /// </summary>
        public virtual string WeixinPay_PartnerId { get; set; }
        /// <summary>
        /// WeixinPay_Key（微信支付V2）
        /// </summary>
        public virtual string WeixinPay_Key { get; set; }
        /// <summary>
        /// WeixinPay_AppId（微信支付V2）
        /// </summary>
        public virtual string WeixinPay_AppId { get; set; }
        /// <summary>
        /// WeixinPay_AppKey（微信支付V2）
        /// </summary>
        public virtual string WeixinPay_AppKey { get; set; }
        /// <summary>
        /// WeixinPay_TenpayNotify（微信支付V2）
        /// </summary>
        public virtual string WeixinPay_TenpayNotify { get; set; }

        #endregion

        #region 微信支付V3（新版）

        /// <summary>
        /// MchId（商户ID）
        /// </summary>
        public virtual string TenPayV3_MchId { get; set; }
        /// <summary>
        /// 子商户 MchId，没有可留空
        /// </summary>
        public string TenPayV3_SubMchId { get; set; }
        /// <summary>
        /// MchKey
        /// </summary>
        public virtual string TenPayV3_Key { get; set; }
        /// <summary>
        /// 微信支付AppId
        /// </summary>
        public virtual string TenPayV3_AppId { get; set; }
        /// <summary>
        /// 微信支付AppKey
        /// </summary>
        public virtual string TenPayV3_AppSecret { get; set; }
        /// <summary>
        /// 微信支付TenpayNotify
        /// </summary>
        public virtual string TenPayV3_TenpayNotify { get; set; }
        /// <summary>
        /// 小程序微信支付WxOpenTenpayNotify
        /// </summary>
        public virtual string TenPayV3_WxOpenTenpayNotify { get; set; }
        /// <summary>
        /// 特约商户微信支付 子商户ID
        /// </summary>
        public virtual string TenPayV3_Sub_MchId { get; set; }
        /// <summary>
        /// 特约商户微信支付 子商户AppID
        /// </summary>
        public virtual string TenPayV3_Sub_AppId { get; set; }
        #endregion

        #endregion

        #region 开放平台

        /// <summary>
        /// Component_Appid
        /// </summary>
        public virtual string Component_Appid { get; set; }
        /// <summary>
        /// Component_Secret
        /// </summary>
        public virtual string Component_Secret { get; set; }
        /// <summary>
        /// 全局统一的 Component_Token（非必须）
        /// </summary>
        public virtual string Component_Token { get; set; }
        /// <summary>
        /// 全局统一的 Component_EncodingAESKey（非必须）
        /// </summary>
        public virtual string Component_EncodingAESKey { get; set; }

        #endregion

        #region 扩展

        public virtual string AgentUrl { get; set; }
        public virtual string AgentToken { get; set; }
        public virtual string SenparcWechatAgentKey { get; set; }

        #endregion
    }
}
