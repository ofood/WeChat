/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：DecodedPhoneNumber.cs
    文件功能描述：用户绑定手机号解密类
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.WxOpen.Entities
{
    /*
    {
        "phoneNumber": "13580006666",  
        "purePhoneNumber": "13580006666", 
        "countryCode": "86",
        "watermark":
        {
            "appid":"APPID",
            "timestamp":TIMESTAMP
        }
    }
    */

    /// <summary>
    /// 用户绑定手机号解密类
    /// </summary>
    public class DecodedPhoneNumber : DecodeEntityBase
    {
        /// <summary>
        /// 用户绑定的手机号（国外手机号会有区号）
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// 没有区号的手机号
        /// </summary>
        public string purePhoneNumber { get; set; }
        /// <summary>
        /// 区号（Senparc注：国别号）
        /// </summary>
        public string countryCode { get; set; }
    }
}
