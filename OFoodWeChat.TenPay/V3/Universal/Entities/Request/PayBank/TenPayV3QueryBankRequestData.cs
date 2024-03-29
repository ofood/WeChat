﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
  
    文件名：TenPayV3QueryBankRequestData.cs
    文件功能描述：查询企业付款银行卡接口 请求参数
    
    创建标识：Senparc - 20180409

    修改标识：Senparc - 20180409
    修改描述：v14.11.0 添加“付款到银行卡”接口

----------------------------------------------------------------*/

namespace OFoodWeChat.TenPay.V3
{
    /// <summary>
    /// 查询企业付款银行卡接口 请求参数
    /// </summary>
    public class TenPayV3QueryBankRequestData
    {
        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 商户订单号，需保持唯一（只允许数字[0~9]或字母[A~Z]和[a~z]，最短8位，最长32位）
        /// </summary>
        public string PartnerTradeNumber { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        public readonly RequestHandler PackageRequestHandler;

        /// <summary>
        /// 通过MD5签名算法计算得出的签名值，详见MD5签名生成算法
        /// </summary>
        public readonly string Sign;

        public TenPayV3QueryBankRequestData(string mchId, string nonceStr, string key, string partnerTradeNo)
        {
            this.MchId = mchId;
            this.NonceStr = nonceStr;
            this.Key = key;
            this.PartnerTradeNumber = partnerTradeNo;

            #region 设置RequestHandler

            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();
            //设置package订单参数
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr); //随机字符串
            PackageRequestHandler.SetParameter("partner_trade_no", this.PartnerTradeNumber); //商户订单号
            PackageRequestHandler.SetParameter("mch_id", this.MchId); //商户号
            Sign = PackageRequestHandler.CreateMd5Sign("key", this.Key);
            PackageRequestHandler.SetParameter("sign", Sign); //签名

            #endregion

        }
    }
}
