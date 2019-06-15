/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SubmitAuditResultJson.cs
    文件功能描述：审核ID
 
----------------------------------------------------------------*/


using OFoodWeChat.Open.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.Open.WxaAPIs
{
    public class SubmitAuditResultJson : OpenJsonResult
    {
        public int auditid { get; set; }
    }
}
