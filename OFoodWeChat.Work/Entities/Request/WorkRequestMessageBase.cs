/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageBase.cs
    文件功能描述：接收到请求的消息基类
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Entities;
using OFoodWeChat.Infrastructure.Enums;

namespace OFoodWeChat.Work.Entities
{
    public interface IWorkRequestMessageBase : IRequestMessageBase
    {
        int AgentID { get; set; }
    }

    /// <summary>
    /// 接收到请求的消息
    /// </summary>
    public class WorkRequestMessageBase : RequestMessageBase, IWorkRequestMessageBase
    {
        public WorkRequestMessageBase()
        {

        }

        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Unknown; }
        }

        /// <summary>
        /// 企业应用的id，整型。可在应用的设置页面查看
        /// </summary>
        public int AgentID { get; set; }
    }
}
