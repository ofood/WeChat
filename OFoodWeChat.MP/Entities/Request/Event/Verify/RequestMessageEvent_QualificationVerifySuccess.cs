
/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_QualificationVerifySuccess.cs
    文件功能描述：事件之资质认证成功（此时立即获得接口权限）
    
    
    创建标识：Senparc - 20170826

    修改标识：Senparc - 20170522
    修改描述：v16.6.2 修改 DateTime 为 DateTimeOffset

----------------------------------------------------------------*/

using System;

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 事件之资质认证成功（此时立即获得接口权限）
    /// </summary>
    public class RequestMessageEvent_QualificationVerifySuccess : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.qualification_verify_success; }
        }

        /// <summary>
        /// 有效期 (整形)，指的是时间戳，将于该时间戳认证过期
        /// </summary>
        public DateTimeOffset ExpiredTime { get; set; }
    }
}
