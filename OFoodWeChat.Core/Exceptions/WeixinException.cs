﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：WeixinException.cs
    文件功能描述：微信自定义异常基类
----------------------------------------------------------------*/


using OFoodWeChat.Infrastructure.Exceptions;
using System;

namespace OFoodWeChat.Core.Exceptions
{
    /// <summary>
    /// 微信自定义异常基类
    /// </summary>
    public class WeixinException : BaseException
    {
        /// <summary>
        /// 当前正在请求的公众号AccessToken或AppId
        /// </summary>
        public string AccessTokenOrAppId { get; set; }

        /// <summary>
        /// WeixinException
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="logged">是否已经使用WeixinTrace记录日志，如果没有，WeixinException会进行概要记录</param>
        public WeixinException(string message, bool logged = false)
            : this(message, null, logged)
        {
        }

        /// <summary>
        /// WeixinException
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="inner">内部异常信息</param>
        /// <param name="logged">是否已经使用WeixinTrace记录日志，如果没有，WeixinException会进行概要记录</param>
        public WeixinException(string message, Exception inner, bool logged = false)
            : base(message, inner, true/* 标记为日志已记录 */)
        {
            if (!logged)
            {
                WeixinTrace.WeixinExceptionLog(this);
            }
        }
    }
}
