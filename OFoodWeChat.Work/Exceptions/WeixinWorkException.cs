/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：WeixinOpenException.cs
    文件功能描述：微信开放平台异常处理类
----------------------------------------------------------------*/

using System;
using OFoodWeChat.Work.CommonAPIs;
using OFoodWeChat.Work.Containers;
using OFoodWeChat.Core.Exceptions;
using OFoodWeChat.Core.Containers;

namespace OFoodWeChat.Work.Exceptions
{
    /// <summary>
    /// 企业微信异常
    /// </summary>
    public class WeixinWorkException : WeixinException
    {
        public AccessTokenBag AccessTokenBag { get; set; }

        public WeixinWorkException(string message, AccessTokenBag accessTokenBag = null, Exception inner=null)
            : base(message, inner)
        {
            AccessTokenBag = accessTokenBag;
        }
    }
}
