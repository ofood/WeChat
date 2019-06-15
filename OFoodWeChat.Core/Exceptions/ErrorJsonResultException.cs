    /*----------------------------------------------------------------
    Copyright(C) 2017 Senparc
    
    文件名：ErrorJsonResultException.cs
    文件功能描述：JSON返回错误代码（比如token_access相关操作中使用）。
----------------------------------------------------------------*/
using System;
using OFoodWeChat.Core.Entities;
using OFoodWeChat.Infrastructure.Data.JsonResult;

namespace OFoodWeChat.Core.Exceptions
{
    /// <summary>
    /// JSON返回错误代码异常（比如access_token相关操作中使用）
    /// </summary>
    public class ErrorJsonResultException : WeixinException
    {
        /// <summary>
        /// JsonResult
        /// </summary>
        public BaseJsonResult JsonResult { get; set; }
        /// <summary>
        /// 接口 URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// ErrorJsonResultException
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="inner">内部异常</param>
        /// <param name="jsonResult">WxJsonResult</param>
        /// <param name="url">API地址</param>
        public ErrorJsonResultException(string message, Exception inner, BaseJsonResult jsonResult, string url = null)
            : base(message, inner, true)
        {
            JsonResult = jsonResult;
            Url = url;

            WeixinTrace.ErrorJsonResultExceptionLog(this);
        }
    }
}
