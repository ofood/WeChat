﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：UnRegisterAppIdException.cs
    文件功能描述：未注册AppId异常
    
    
    创建标识：Senparc - 20160206

----------------------------------------------------------------*/



using System;

namespace OFoodWeChat.Core.Exceptions
{
    /// <summary>
    /// 未注册 AppId 异常
    /// </summary>
    public class UnRegisterAppIdException : WeixinException
    {
        public string AppId { get; set; }
        public UnRegisterAppIdException(string appId, string message, Exception inner = null)
            : base(message, inner)
        {
            AppId = appId;
        }
    }
}
