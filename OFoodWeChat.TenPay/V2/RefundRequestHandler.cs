﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
 
    文件名：RefundRequestHandler.cs
    文件功能描述：微信支付退款 请求处理
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using Microsoft.AspNetCore.Http;
using System;

namespace OFoodWeChat.TenPay.V2
{
    /// <summary>
    /// RefundRequestHandler
    /// </summary>
    public class RefundRequestHandler : ClientRequestHandler
    {
        /// <summary>
        /// 退款接口
        /// </summary>
        /// <param name="httpContext"></param>
        public RefundRequestHandler(HttpContext httpContext)
            : base(httpContext)
        {
            this.SetGateUrl("https://mch.tenpay.com/refundapi/gateway/refund.xml");
        }

        public void SetParameter(string v, object p)
        {
            throw new NotImplementedException();
        }
    }
}
