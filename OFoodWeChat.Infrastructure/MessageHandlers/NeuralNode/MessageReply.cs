﻿/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：MessageReply.cs
    文件功能描述：MessageReply 相关类
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.Infrastructure.NeuralSystems
{
    /// <summary>
    /// 消息回复设置
    /// </summary>
    public class MessageReply : NeuralNodeConfig
    {
        /// <summary>
        /// 请求-响应 配置列表
        /// </summary>
        public List<MessagePair> MessagePair { get; set; }

        /// <summary>
        /// MessageReply 构造函数
        /// </summary>
        public MessageReply()
        {
            MessagePair = new List<MessagePair>();
        }
    }

    /// <summary>
    /// 请求-响应 配置
    /// </summary>
    public class MessagePair : IConfigItem
    {
        #region IConfigItem
        /// <summary>
        /// 备注名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 唯一编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 版本号，如："2018.9.27.1"
        /// </summary>
        public string Version { get; set; }
        #endregion

        /// <summary>
        /// 请求条件设置
        /// </summary>
        public Request Request { get; set; }
        /// <summary>
        /// 优先响应设置（至少一项）
        /// </summary>
        public List<Response> Responses { get; set; }

        /// <summary>
        /// MessagePair 构造函数
        /// </summary>
        public MessagePair()
        {
            Request = new Request();
            Responses = new List<Response>();
        }
    }

    /// <summary>
    /// 请求配置
    /// </summary>
    public class Request
    {
        /// <summary>
        /// 说明：目前只支持Text和Image
        /// </summary>
        public RequestMsgType Type { get; set; }
        /// <summary>
        /// 文本、事件的关键字
        /// </summary>
        public List<string> Keywords { get; set; }

        public Request()
        {
            Type = RequestMsgType.Unknown;
            Keywords = new List<string>();
        }
    }

    /// <summary>
    /// 响应配置
    /// </summary>
    public class Response
    {
        /// <summary>
        /// 响应类型
        /// </summary>
        public ResponseMsgType Type { get; set; }

        /// <summary>
        /// 响应内容
        /// </summary>
        public string MaterialId { get; set; }

        public Response()
        {
            Type = ResponseMsgType.Unknown;
        }
    }
}
