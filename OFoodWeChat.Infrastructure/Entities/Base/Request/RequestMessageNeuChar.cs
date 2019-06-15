/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：RequestMessageNeuChar.cs
    文件功能描述：接收 NeuChar 消息

----------------------------------------------------------------*/
using OFoodWeChat.Infrastructure.Enums;

namespace OFoodWeChat.Infrastructure.Entities
{
    /// <summary>
    /// NeuChar 请求消息
    /// </summary>
    public class RequestMessageNeuChar : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.NeuChar; }
        }

        /// <summary>
        /// 具体操作类型
        /// </summary>
        public NeuCharActionType NeuCharMessageType { get; set; }

        /// <summary>
        /// 设置信息（通常为JSON）
        /// </summary>
        public string ConfigRoot { get; set; }

        /// <summary>
        /// 请求数据的 JSON 字符串
        /// </summary>
        public string RequestData { get; set; } = string.Empty;
    }
}
