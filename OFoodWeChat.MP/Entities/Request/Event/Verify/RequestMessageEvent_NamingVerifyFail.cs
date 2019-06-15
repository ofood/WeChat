
/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_NamingVerifyFail.cs
    文件功能描述：事件之名称认证失败（这时虽然客户端不打勾，但仍有接口权限）
    
    
    创建标识：Senparc - 20170826

    修改标识：Senparc - 20170522
    修改描述：v16.6.2 修改 DateTime 为 DateTimeOffset

----------------------------------------------------------------*/

using System;

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 事件之名称认证失败（这时虽然客户端不打勾，但仍有接口权限）
    /// </summary>
    public class RequestMessageEvent_NamingVerifyFail : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.naming_verify_fail; }
        }

        /// <summary>
        /// 失败发生时间 (整形)，时间戳
        /// </summary>
        public DateTimeOffset FailTime { get; set; }
        /// <summary>
        /// 认证失败的原因
        /// </summary>

        public string FailReason { get; set; }
    }
}
