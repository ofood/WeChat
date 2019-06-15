using OFoodWeChat.Infrastructure.Exceptions;
using OFoodWeChat.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OFoodWeChat.Infrastructure.Exceptions
{
    /// <summary>
    /// APM 异常
    /// </summary>
    public class APMException : BaseException
    {
        public APMException(string message, string domain, string kindName, string method, Exception inner = null) :
            base(message, inner, true)
        {
            LogTrace.SendCustomLog("APM 执行出错", $@"Domain: {domain}
KindName: {kindName}
Message: {message}
Exception: {inner?.ToString()}");
        }
    }
}
