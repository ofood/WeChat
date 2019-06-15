/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：ModifyDomainResultJson.cs
    文件功能描述：修改域名接口返回类型
    
----------------------------------------------------------------*/

using OFoodWeChat.Open.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.Open.WxaAPIs.ModifyDomain
{
    /// <summary>
    /// 修改域名接口返回类型
    /// </summary>
    public class ModifyDomainResultJson : OpenJsonResult
    {
        //以下字段仅在get时返回

        public List<string> requestdomain { get; set; }
        public List<string> wsrequestdomain { get; set; }
        public List<string> uploaddomain { get; set; }
        public List<string> downloaddomain { get; set; }
    }
}
