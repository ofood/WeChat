/*----------------------------------------------------------------
    文件功能描述：日志记录
----------------------------------------------------------------*/


using OFoodWeChat.Infrastructure.Cache;
using OFoodWeChat.Infrastructure.Exceptions;
using OFoodWeChat.Infrastructure.Queue;
using System;
using System.IO;

namespace OFoodWeChat.Infrastructure.Trace
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public class LogTrace
    {
        /// <summary>
        /// 统一日志锁名称
        /// </summary>
        const string LockName = "LogTraceLock";


        /// <summary>
        /// 记录BaseException日志时需要执行的任务
        /// </summary>
        public static Action<BaseException> OnBaseExceptionFunc;

        /// <summary>
        /// 执行所有日志记录操作时执行的任务（发生在记录日志之后）
        /// </summary>
        public static Action OnLogFunc;

        /// <summary>
        /// 是否开放每次 APM 录入的记录，默认为关闭（当 Senparc.CO2ENT.APM 启用时有效）
        /// </summary>
        public static bool RecordAPMLog = false;

        #region 私有方法

        /// <summary>
        /// Senparc.Weixin全局统一的缓存策略
        /// </summary>
        private static IBaseObjectCacheStrategy Cache
        {
            get
            {
                //使用工厂模式或者配置进行动态加载
                return CacheStrategyFactory.GetObjectCacheStrategyInstance();
            }
        }

        /// <summary>
        /// 队列执行逻辑
        /// </summary>
        protected static Action<string> _queue = (logStr) =>
        {
            using (Cache.BeginCacheLock(LockName, ""))
            {
                string logDir;
                logDir = Path.Combine(OFoodConfig.RootDictionaryPath, "App_Data", "TraceLog");


                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }

                //TODO：可以进行合并写入

                string logFile = Path.Combine(logDir, string.Format("LogTrace-{0}.log", SystemTime.Now.ToString("yyyyMMdd")));
                using (var fs = new FileStream(logFile, FileMode.OpenOrCreate))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        fs.Seek(0, SeekOrigin.End);
                        sw.Write(logStr);
                        sw.Flush();
                    }
                }

                if (OnLogFunc != null)
                {
                    try
                    {
                        OnLogFunc();
                    }
                    catch
                    {
                    }
                }
            }
        };

        /// <summary>
        /// 结束日志记录
        /// </summary>
        protected static Action<TraceItem> _logEndActon = (traceItem) =>
        {
            var logStr = traceItem.GetFullLog();
            MessageQueue messageQueue = new MessageQueue();
            var key = SystemTime.Now.Ticks.ToString() + traceItem.ThreadId.ToString() + logStr.Length.ToString();//确保全局唯一
            messageQueue.Add(key, () => _queue(logStr));
        };

        #endregion

        #region 日志记录

        /// <summary>
        /// 系统日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Log(string message)
        {
            SendCustomLog("系统日志", message);
        }

        /// <summary>
        /// 自定义日志
        /// </summary>
        /// <param name="typeName">日志类型</param>
        /// <param name="content">日志内容</param>
        public static void SendCustomLog(string typeName, string content)
        {
            if (!OFoodConfig.IsDebug)
            {
                return;
            }

            using (var traceItem = new TraceItem(_logEndActon, typeName, content))
            {
                //traceItem.Log(content);
            }
        }

        /// <summary>
        /// API请求日志（接收结果）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="returnText"></param>
        public static void SendApiLog(string url, string returnText)
        {
            if (!OFoodConfig.IsDebug)
            {
                return;
            }

            using (var traceItem = new TraceItem(_logEndActon, "接口调用"))
            {
                //TODO:从源头加入AppId
                traceItem.Log("URL：{0}", url);
                traceItem.Log("Result：\r\n{0}", returnText);
            }
        }

        /// <summary>
        /// API请求日志（Post发送消息）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        public static void SendApiPostDataLog(string url, string data)
        {
            if (!OFoodConfig.IsDebug)
            {
                return;
            }

            using (var traceItem = new TraceItem(_logEndActon, "接口调用"))
            {
                traceItem.Log("URL：{0}", url);
                traceItem.Log("Post Data：\r\n{0}", data);
            }
        }


        #endregion

        #region BaseException


        /// <summary>
        /// BaseException 日志
        /// </summary>
        /// <param name="ex"></param>
        public static void BaseExceptionLog(Exception ex)
        {
            BaseExceptionLog(new BaseException(ex.Message, ex));
        }

        /// <summary>
        /// BaseException 日志
        /// </summary>
        /// <param name="ex"></param>
        public static void BaseExceptionLog(BaseException ex)
        {
            if (!OFoodConfig.IsDebug)
            {
                return;
            }


            using (var traceItem = new TraceItem(_logEndActon, "BaseException"))
            {
                traceItem.Log(ex.GetType().Name);
                traceItem.Log("Message：{0}", ex.Message);
                traceItem.Log("StackTrace：{0}", ex.StackTrace);

                if (ex.InnerException != null)
                {
                    traceItem.Log("InnerException：{0}", ex.InnerException.Message);
                    traceItem.Log("InnerException.StackTrace：{0}", ex.InnerException.StackTrace);
                }

                if (OnBaseExceptionFunc != null)
                {
                    try
                    {
                        OnBaseExceptionFunc(ex);
                    }
                    catch
                    {
                    }
                }
            }
        }

        #endregion
    }
}
