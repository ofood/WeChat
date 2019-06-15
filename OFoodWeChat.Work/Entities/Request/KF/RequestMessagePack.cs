/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestPack.cs
    文件功能描述：客服请求消息
    
    
    创建标识：Senparc - 2017616

    修改标识：Senparc - 20180901
    修改描述：支持 NeuChar
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.Infrastructure.Entities;

namespace OFoodWeChat.Work.Entities.Request.KF
{
    public class RequestPack : IEntityBase
    {
        public AgentType AgentType { get; set; }
        public string ToUserName { get; set; }
        public int ItemCount { get; set; }
        public List<RequestBase> Items { get; set; }
        public long PackageId { get; set; }
    }

    public enum AgentType
    {
        /// <summary>
        /// 内部客服
        /// </summary>
        kf_internal,
        /// <summary>
        /// 外部客服
        /// </summary>
        kf_external
    }
}
