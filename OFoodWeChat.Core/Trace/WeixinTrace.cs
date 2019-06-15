/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：WeixinTrace.cs
    文件功能描述：跟踪日志相关
    
    
    创建标识：Senparc - 20151012

    修改标识：Senparc - 20161225
    修改描述：v4.9.7 1、使用同步锁
                     2、修改日志储存路径，新路径为/App_Data/WeixinTraceLog/SenparcWeixinTrace-yyyyMMdd.log
                     3、添加WeixinExceptionLog方法

    修改标识：Senparc - 20161231
    修改描述：v4.9.8 将SendLog方法改名为SendApiLog，添加SendCustomLog方法

    修改标识：Senparc - 20170101
    修改描述：v4.9.9 1、优化日志记录方法（围绕OnWeixinExceptionFunc为主）
                     2、输出AccessTokenOrAppId

    修改标识：Senparc - 20170304
    修改描述：Senparc.Wexin v4.11.3 日志中添加对线程的记录

    修改标识：Senparc - 20181118
    修改描述：v16.5.0 使用 CO2NET v0.3.0 新的 SenparcTrace 记录方法

----------------------------------------------------------------*/

using System;
using System.Diagnostics;
using System.IO;
using OFoodWeChat.Core.Cache;
using OFoodWeChat.Core.Exceptions;
using System.Threading;
using OFoodWeChat.Infrastructure.Trace;
using OFoodWeChat.Infrastructure.Extensions;

namespace OFoodWeChat.Core
{
    //TODO：将WeixinTrace和SenparcTrace通过某种标记明显区分开来

    /// <summary>
    /// 微信日志跟踪
    /// </summary>
    public class WeixinTrace : LogTrace
    {
        /// <summary>
        /// 记录WeixinException日志时需要执行的任务
        /// </summary>
        public static Action<WeixinException> OnWeixinExceptionFunc;

        /// <summary>
        /// 记录系统日志
        /// </summary>
        /// <param name="messageFormat"></param>
        /// <param name="param"></param>
        public static void Log(string messageFormat, params object[] param)
        {
            LogTrace.Log(messageFormat.FormatWith(param));
        }

        #region WeixinException 相关日志

        /// <summary>
        /// WeixinException 日志
        /// </summary>
        /// <param name="ex"></param>
        public static void WeixinExceptionLog(WeixinException ex)
        {
            if (!WxConfig.IsDebug)
            {
                return;
            }
            using (var traceItem = new TraceItem(LogTrace._logEndActon, "WeixinException"))
            {
                traceItem.Log(ex.GetType().Name);
                traceItem.Log("AccessTokenOrAppId：{0}", ex.AccessTokenOrAppId);
                traceItem.Log("Message：{0}", ex.Message);
                traceItem.Log("StackTrace：{0}", ex.StackTrace);
                if (ex.InnerException != null)
                {
                    traceItem.Log("InnerException：{0}", ex.InnerException.Message);
                    traceItem.Log("InnerException.StackTrace：{0}", ex.InnerException.StackTrace);
                }
            }

            if (OnWeixinExceptionFunc != null)
            {
                try
                {
                    OnWeixinExceptionFunc(ex);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// ErrorJsonResultException 日志
        /// </summary>
        /// <param name="ex"></param>
        public static void ErrorJsonResultExceptionLog(ErrorJsonResultException ex)
        {
            if (!WxConfig.IsDebug)
            {
                return;
            }

            using (var traceItem = new TraceItem(LogTrace._logEndActon, "ErrorJsonResultException"))
            {
                traceItem.Log("ErrorJsonResultException");
                traceItem.Log("AccessTokenOrAppId：{0}", ex.AccessTokenOrAppId ?? "null");
                traceItem.Log("URL：{0}", ex.Url);
                traceItem.Log("errcode：{0}", ex.JsonResult.ErrorCodeValue);
                traceItem.Log("errmsg：{0}", ex.JsonResult.errmsg);
            }

            if (OnWeixinExceptionFunc != null)
            {
                try
                {
                    OnWeixinExceptionFunc(ex);
                }
                catch
                {
                }
            }
        }

        #endregion
    }
}
