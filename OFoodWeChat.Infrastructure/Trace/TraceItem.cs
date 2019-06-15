/*----------------------------------------------------------------
    文件名：SenparcTraceItem.cs
    文件功能描述：每一次跟踪日志的对象信息
 ----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OFoodWeChat.Infrastructure.Trace
{
    /// <summary>
    /// 每一次跟踪日志的对象信息
    /// </summary>
    public class TraceItem : IDisposable
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTimeOffset DateTime { get; set; }

        public int ThreadId { get; set; } = Thread.CurrentThread.GetHashCode();

        private Action<TraceItem> _logEndAction;

        public TraceItem(Action<TraceItem> logEndAction, string title = null, string content = null)
        {
            _logEndAction = logEndAction;
            Title = title;
            Content = content;
            DateTime = SystemTime.Now;
        }

        public void Log(string messageFormat, params object[] param)
        {
            Log(messageFormat.FormatWith(param));
        }

        public void Log(string message)
        {
            if (Content != null)
            {
                Content += System.Environment.NewLine;
            }
            Content += $"\t{message}";
        }

        /// <summary>
        /// 获取完整单条日志的字符串信息
        /// </summary>
        public string GetFullLog()
        {
            string logStr = $@"[[[{Title}]]]
[{DateTime.ToString("yyyy/MM/dd HH:mm:ss.ffff")}]
[线程：{ThreadId}]
{Content}

";
            return logStr;
        }

        public void Dispose()
        {
            _logEndAction?.Invoke(this);
        }
    }
}