/*----------------------------------------------------------------
    文件名：WebSocketRouteConfig.cs
    文件功能描述：自动配置WebSocket路由
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Routing;

namespace OFoodWeChat.WebSocket
{
    /// <summary>
    /// WebSocket 配置
    /// </summary>
    public class WebSocketConfig
    {
        internal static Func<WebSocketMessageHandler> WebSocketMessageHandlerFunc { get; set; }


        /// <summary>
        /// 注册WebSocketMessageHandler
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void RegisterMessageHandler<T>() where T : WebSocketMessageHandler, new()
        {
            WebSocketMessageHandlerFunc = () => new T();
        }

        /// <summary>
        /// 注册WebSocketMessageHandler，自定义对象的实例化方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void RegisterMessageHandler<T>(Func<T> func) where T : WebSocketMessageHandler, new()
        {
            WebSocketMessageHandlerFunc = func;
        }
    }
}
