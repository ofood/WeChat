﻿/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
 
    文件名：TenPayV3.PayBank.cs
    文件功能描述：微信支付V3接口：付款到银行卡
    
    
    创建标识：Senparc - 20171129

    修改标识：Mc7246 - 20180725
    修改描述：请求携带证书

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.TenPay.V3
{
    /// <summary>
    /// 付款到银行卡，文档：https://pay.weixin.qq.com/wiki/doc/api/tools/mch_pay.php?chapter=24_2
    /// </summary>
    public static partial class TenPayV3
    {
        #region 同步方法

        /// <summary>
        /// <para>企业付款到银行卡</para>
        /// <para>用于企业向微信用户银行卡付款,目前支持接口API的方式向指定微信用户的银行卡付款。</para>
        /// <para>注意：请求需要双向证书</para>
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="cert">证书路径</param>
        /// <param name="certPassword">证书密码</param>
        /// <returns></returns>
        public static PayBankResult PayBank(TenPayV3PayBankRequestData dataInfo, string cert, string certPassword)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}mmpaysptrans/pay_bank");

            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            var resultXml = CertPost(cert,certPassword, data, urlFormat);
            return new PayBankResult(resultXml);
        }


        /// <summary>
        /// <para>查询企业付款银行卡</para>
        /// <para>注意：请求需要双向证书</para>
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="cert">证书路径</param>
        /// <param name="certPassword">证书密码</param>
        /// <returns></returns>
        public static QueryBankResult QueryBank(TenPayV3QueryBankRequestData dataInfo, string cert, string certPassword)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}mmpaysptrans/query_bank");

            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            var resultXml = CertPost(cert, certPassword, data, urlFormat);
            return new QueryBankResult(resultXml);
        }

        /// <summary>
        /// <para>获取 RSA 加密公钥接口</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/api/tools/mch_pay.php?chapter=24_7&index=4</para>
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="cert">证书路径</param>
        /// <param name="certPassword">证书密码</param>
        /// <returns></returns>
        public static GetPublicKeyResult GetPublicKey(TenPayV3GetPublicKeyRequestData dataInfo, string cert, string certPassword)
        {
            //TODO：官方文档没有明确此接口是否支持沙箱
            var urlFormat = ReurnPayApiUrl("https://fraud.mch.weixin.qq.com/{0}risk/getpublickey");

            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            var resultXml = CertPost(cert, certPassword, data, urlFormat);
            return new GetPublicKeyResult(resultXml);
        }

        #endregion


        #region 异步方法

        /// <summary>
        /// <para>企业付款到银行卡</para>
        /// <para>用于企业向微信用户银行卡付款,目前支持接口API的方式向指定微信用户的银行卡付款。</para>
        /// <para>注意：请求需要双向证书</para>
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="cert">证书路径</param>
        /// <param name="certPassword">证书密码</param>
        /// <returns></returns>
        public static async Task<PayBankResult> PayBankAsync(TenPayV3PayBankRequestData dataInfo, string cert, string certPassword)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}mmpaysptrans/pay_bank");

            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            var resultXml = await CertPostAsync(cert, certPassword, data, urlFormat);
            return new PayBankResult(resultXml);
        }


        /// <summary>
        /// <para>查询企业付款银行卡</para>
        /// <para>注意：请求需要双向证书</para>
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="cert">证书路径</param>
        /// <param name="certPassword">证书密码</param>
        /// <returns></returns>
        public static async Task<QueryBankResult> QueryBankAsync(TenPayV3QueryBankRequestData dataInfo, string cert, string certPassword)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}mmpaysptrans/query_bank");

            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            var resultXml = await CertPostAsync(cert, certPassword, data, urlFormat);
            return new QueryBankResult(resultXml);
        }

        /// <summary>
        /// <para>获取 RSA 加密公钥接口</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/api/tools/mch_pay.php?chapter=24_7&index=4</para>
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <param name="cert">证书路径</param>
        /// <param name="certPassword">证书密码</param>
        /// <returns></returns>
        public static async Task<GetPublicKeyResult> GetPublicKeyAsync(TenPayV3QueryBankRequestData dataInfo, string cert, string certPassword)
        {
            //TODO：官方文档没有明确此接口是否支持沙箱
            var urlFormat = ReurnPayApiUrl("https://fraud.mch.weixin.qq.com/{0}risk/getpublickey");

            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            var resultXml = await CertPostAsync(cert, certPassword, data, urlFormat);
            return new GetPublicKeyResult(resultXml);
        }

        #endregion
    }
}
