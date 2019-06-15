/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：WeixinException.cs
    文件功能描述：微信菜单异常处理类
----------------------------------------------------------------*/



using System;

namespace OFoodWeChat.Core.Exceptions
{
    /// <summary>
    /// 菜单异常
    /// </summary>
    public class WeixinMenuException : WeixinException
    {
        public WeixinMenuException(string message)
            : this(message, null)
        {
        }

        public WeixinMenuException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
