/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetAuditStatusResultJson.cs
    文件功能描述：审核ID返回结果

----------------------------------------------------------------*/


using OFoodWeChat.Open.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.Open.WxaAPIs
{
    public class GetAuditStatusResultJson : OpenJsonResult
    {

        /// <summary>
        /// 最新的审核ID，只在使用GetLatestAuditStatus接口时才有返回值
        /// </summary>
        public string auditid { get; set; }
        /// <summary>
        /// 审核状态，其中0为审核成功，1为审核失败，2为审核中
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 当status=1，审核被拒绝时，返回的拒绝原因
        /// </summary>
        public string reason { get; set; }
    }
}
