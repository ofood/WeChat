﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
 
    文件名：TenPay.cs
    文件功能描述：微信支付接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法
----------------------------------------------------------------*/

/*
    官方API：https://mp.weixin.qq.com/paymch/readtemplate?t=mp/business/course2_tmpl&lang=zh_CN&token=25857919#4
 */

using System.Threading.Tasks;
using OFoodWeChat.Core;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Infrastructure.Data.JsonResult;

namespace OFoodWeChat.TenPay.V2
{
    /// <summary>
    /// 微信支付接口，官方API：https://mp.weixin.qq.com/paymch/readtemplate?t=mp/business/course2_tmpl&lang=zh_CN&token=25857919#4
    /// </summary>
    public static class TenPay
    {
        #region 同步方法
        
       /*此接口不提供异步方法*/
        /// <summary>
        /// Native
        /// </summary>
        /// <param name="sign">签名</param>
        /// <param name="appId">开放平台账户的唯一标识</param>
        /// <param name="timesTamp">时间戳</param>
        /// <param name="nonceStr">32 位内的随机串，防重发</param>
        /// <param name="productId">商品唯一id</param>
        public static string NativePay(string sign, string appId, string timesTamp, string nonceStr, string productId)
        {
            var urlFormat = "weixin://wxpay/bizpayurl?sign={0}&appid={1}&productid={2}&timestamp={3}&noncestr={4}";
            var url = string.Format(urlFormat, sign, appId, productId, timesTamp, nonceStr);

            return url;
        }

        /// <summary>
        /// 发货通知
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="accessToken">公众平台AccessToken</param>
        /// <param name="openId">购买用户的OpenId</param>
        /// <param name="transId">交易单号</param>
        /// <param name="out_Trade_No">第三方订单号</param>
        /// <param name="deliver_TimesTamp">发货时间戳</param>
        /// <param name="deliver_Status">发货状态，1 表明成功，0 表明失败，失败时需要在deliver_msg 填上失败原因</param>
        /// <param name="deliver_Msg">发货状态信息，失败时可以填上UTF8 编码的错误提示信息，比如“该商品已退款</param>
        /// <param name="app_Signature">签名</param>
        /// <param name="sign_Method">签名方法</param>
        public static BaseJsonResult Delivernotify(string appId,string accessToken, string openId, string transId, string out_Trade_No, string deliver_TimesTamp, string deliver_Status, string deliver_Msg, string app_Signature, string sign_Method = "sha1")
        {

            var urlFormat = WxConfig.ApiMpHost + "/pay/delivernotify?access_token={0}";

            //组装发送消息
            var data = new
            {
                appid = appId,
                openid = openId,
                transid = transId,
                out_trade_no = out_Trade_No,
                deliver_timestamp = deliver_TimesTamp,
                deliver_status = deliver_Status,
                deliver_msg = deliver_Msg,
                app_signature = app_Signature,
                sign_method = sign_Method
            };

            return CommonJsonSend.Send<BaseJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="accessToken">公众平台AccessToken</param>
        /// <param name="package">查询订单的关键信息数据</param>
        /// <param name="timesTamp">linux 时间戳</param>
        /// <param name="app_Signature">签名</param>
        /// <param name="sign_Method">签名方法</param>
        public static OrderqueryResult Orderquery(string appId, string accessToken,string package, string timesTamp, string app_Signature, string sign_Method)
        {

            var urlFormat = WxConfig.ApiMpHost + "/pay/orderquery?access_token={0}";

            //组装发送消息
            var data = new
            {
                appid = appId,
                package = package,
                timestamp = timesTamp,
                app_signature = app_Signature,
                sign_method = sign_Method
            };

            return CommonJsonSend.Send<OrderqueryResult>(accessToken, urlFormat, data);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】发货通知
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="accessToken">公众平台AccessToken</param>
        /// <param name="openId">购买用户的OpenId</param>
        /// <param name="transId">交易单号</param>
        /// <param name="out_Trade_No">第三方订单号</param>
        /// <param name="deliver_TimesTamp">发货时间戳</param>
        /// <param name="deliver_Status">发货状态，1 表明成功，0 表明失败，失败时需要在deliver_msg 填上失败原因</param>
        /// <param name="deliver_Msg">发货状态信息，失败时可以填上UTF8 编码的错误提示信息，比如“该商品已退款</param>
        /// <param name="app_Signature">签名</param>
        /// <param name="sign_Method">签名方法</param>
        public static async Task<BaseJsonResult> DelivernotifyAsync(string appId, string accessToken,string openId, string transId, string out_Trade_No, string deliver_TimesTamp, string deliver_Status, string deliver_Msg, string app_Signature, string sign_Method = "sha1")
        {
            var urlFormat = WxConfig.ApiMpHost + "/pay/delivernotify?access_token={0}";

            //组装发送消息
            var data = new
            {
                appid = appId,
                openid = openId,
                transid = transId,
                out_trade_no = out_Trade_No,
                deliver_timestamp = deliver_TimesTamp,
                deliver_status = deliver_Status,
                deliver_msg = deliver_Msg,
                app_signature = app_Signature,
                sign_method = sign_Method
            };

            return await CommonJsonSend.SendAsync<BaseJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 【异步方法】订单查询
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="accessToken">公众平台AccessToken</param>
        /// <param name="package">查询订单的关键信息数据</param>
        /// <param name="timesTamp">linux 时间戳</param>
        /// <param name="app_Signature">签名</param>
        /// <param name="sign_Method">签名方法</param>
        public static async Task<OrderqueryResult> OrderqueryAsync(string appId, string accessToken, string package, string timesTamp, string app_Signature, string sign_Method)
        {
            var urlFormat = WxConfig.ApiMpHost + "/pay/orderquery?access_token={0}";

            //组装发送消息
            var data = new
            {
                appid = appId,
                package = package,
                timestamp = timesTamp,
                app_signature = app_Signature,
                sign_method = sign_Method
            };

            return await CommonJsonSend.SendAsync<OrderqueryResult>(accessToken, urlFormat, data);
        }
        #endregion
    }
}
