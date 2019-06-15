/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：MessageBase.cs
    文件功能描述：所有Request和Response消息的基类

----------------------------------------------------------------*/

using System;

namespace OFoodWeChat.Infrastructure.Entities
{
    /// <summary>
    /// 所有Request和Response消息的接口
    /// </summary>
    public interface IMessageBase : IEntityBase
    {
        /// <summary>
        /// 接收人（在 Request 中为公众号的微信号，在 Response 中为 OpenId）
        /// </summary>
        string ToUserName { get; set; }
        /// <summary>
        /// 发送人（在 Request 中为OpenId，在 Response 中为公众号的微信号）
        /// </summary>
        string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间
        /// </summary>
        DateTimeOffset CreateTime { get; set; }
    }

    /// <summary>
    /// 所有Request和Response消息的基类
    /// </summary>
    public abstract class MessageBase : /*EntityBase, */IMessageBase
    {
        /// <summary>
        /// 接收人（在 Request 中为公众号的微信号，在 Response 中为 OpenId）
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送人（在 Request 中为OpenId，在 Response 中为公众号的微信号）
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间
        /// </summary>
        public DateTimeOffset CreateTime { get; set; }

        /// <summary>
        /// ToString() 方法重写
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //TODO:直接输出XML


            return base.ToString();
        }
    }
}
