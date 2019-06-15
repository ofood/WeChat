/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetCardMerchantJsonResult.cs
    文件功能描述：拉取单个子商户信息的返回结果
----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.MP.Entities;

namespace OFoodWeChat.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 拉取单个子商户信息的返回结果
    /// </summary>
    public class GetCardMerchantJsonResult : WxJsonResult 
    {
        /// <summary>
        /// 
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int primary_category_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int secondary_category_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string submit_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int result { get; set; }
        
    }
  
}
