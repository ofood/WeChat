/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：RequestMessageBase.cs
    文件功能描述：接收请求消息基类
----------------------------------------------------------------*/



using OFoodWeChat.Infrastructure.Enums;

namespace OFoodWeChat.Infrastructure.Entities
{
    /// <summary>
    /// 请求消息基础接口
    /// </summary>
    public interface IRequestMessageBase : IMessageBase
    {
        //删除MsgType因为企业号和公众号的MsgType为两个独立的枚举类型
        //RequestMessageType MsgType { get; }
        long MsgId { get; set; }

        RequestMsgType MsgType { get; set; }
        string Encrypt { get; set; }

    }

    /// <summary>
    /// 接收到请求的消息基类
    /// </summary>
    public class RequestMessageBase : MessageBase, IRequestMessageBase
    {
        public RequestMessageBase()
        {

        }

        //public virtual RequestMessageType MsgType
        //{
        //    get { return RequestMessageType.Text; }
        //}

        public long MsgId { get; set; }
        public virtual RequestMsgType MsgType { get; set; }
        public string Encrypt { get; set; }

    }
}
