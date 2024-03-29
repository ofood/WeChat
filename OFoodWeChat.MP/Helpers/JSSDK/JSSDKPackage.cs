﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.MP.Helpers
{
    /// <summary>
    /// 为 UI 输出准备的 JSSDK 信息包
    /// </summary>
    public class JsSdkUiPackage
    {
        /// <summary>
        /// 微信AppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }
        /// <summary>
        /// 随机码
        /// </summary>
        public string NonceStr { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }

        public JsSdkUiPackage(string appId, string timestamp, string nonceStr,string signature)
        {
            AppId = appId;
            Timestamp = timestamp;
            NonceStr = nonceStr;
            Signature = signature;
        }
    }
}
