/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：WeixinObsoleteException.cs
    文件功能描述：v4.18.11 接口或方法过期异常
    
    
    创建标识：Senparc - 20180107
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OFoodWeChat.Core.Exceptions
{
    /// <summary>
    /// 接口或方法过期异常
    /// </summary>
    public class WeixinObsoleteException : WeixinException
    {
        public WeixinObsoleteException(string message, bool logged = false) : base(message, logged)
        {
        }

        public WeixinObsoleteException(string message, Exception inner, bool logged = false) : base(message, inner, logged)
        {
        }
    }
}
