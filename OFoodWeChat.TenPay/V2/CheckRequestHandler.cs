/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
 
    文件名：CheckRequestHandler.cs
    文件功能描述：对账单下载接口
----------------------------------------------------------------*/

using System.Collections;
using System.Text;
using OFoodWeChat.Infrastructure.Helpers;

using Microsoft.AspNetCore.Http;


namespace OFoodWeChat.TenPay.V2
{
    public class CheckRequestHandler : ClientRequestHandler
    {
        /// <summary>
        /// 对账单下载接口
        /// </summary>
        /// <param name="httpContext"></param>
        public CheckRequestHandler(HttpContext httpContext)
            : base(httpContext)
        {

            this.SetGateUrl("http://mch.tenpay.com/cgi-bin/mchdown_real_new.cgi");
        }



        protected override void CreateSign()
        {
            StringBuilder sb = new StringBuilder();

            ArrayList akeys = new ArrayList();
            akeys.Add("spid");
            akeys.Add("trans_time");
            akeys.Add("stamp");
            akeys.Add("cft_signtype");
            akeys.Add("mchtype");


            foreach (string k in akeys)
            {
                string v = (string)Parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + this.GetKey());
            string sign = EncryptHelper.GetMD5(sb.ToString(), GetCharset()).ToLower();

            this.SetParameter("sign", sign);

            //debug信息
            this.SetDebugInfo(sb.ToString() + " => sign:" + sign);
        }
    }
}
