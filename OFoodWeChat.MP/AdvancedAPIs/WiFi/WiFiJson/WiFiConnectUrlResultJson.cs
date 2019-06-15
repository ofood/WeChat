/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：WiFiConnectUrlResultJson.cs
    文件功能描述：获取公众号连网URL返回结果
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.MP.Entities;
namespace OFoodWeChat.MP.AdvancedAPIs.WiFi
{
    /// <summary>
    /// WiFiConnectUrlResultJson
    /// </summary>
    public class WiFiConnectUrlResultJson : WxJsonResult
    {
        /// <summary>
        /// data
        /// </summary>
        public WiFiConnectUrl_Data data { get; set; }
    }

    /// <summary>
    /// WiFiConnectUrl_Data
    /// </summary>
    public class WiFiConnectUrl_Data
    {
        public string connect_url { get; set; }
    }
}
