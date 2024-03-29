﻿/*----------------------------------------------------------------
    文件名：WebSocketHelper.cs
    文件功能描述：WebSocket处理类
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace OFoodWeChat.WebSocket
{
    /// <summary>
    /// WebSocketHelper
    /// </summary>
    public class WebSocketHelper
    {
        //private readonly AspNetWebSocketContext _webSocketContext;
        private readonly System.Net.WebSockets.WebSocket _webSocket;
        private readonly CancellationToken _cancellationToken;


        /// <summary>
        /// WebSocketHelper
        /// </summary>
        ///// <param name="webSocketContext"></param>
        /// <param name="cancellationToken"></param>
        public WebSocketHelper(System.Net.WebSockets.WebSocket socket,/*AspNetWebSocketContext webSocketContext,*/ CancellationToken cancellationToken)
        {
            //_webSocketContext = webSocketContext;
            //_webSocket = webSocketContext.WebSocket;
            _webSocket = socket;
            _cancellationToken = cancellationToken;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">文字消息</param>
        /// <returns></returns>
        public async Task SendMessage(string message)
        {
            var data = new
            {
                content = message,
                time = DateTimeOffset.Now.DateTime.ToString()
            };

            var newString = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            //String.Format("Hello, " + receiveString + " ! Time {0}", DateTimeOffset.Now.ToString());

            Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(newString);
            await _webSocket.SendAsync(new ArraySegment<byte>(bytes),
                              WebSocketMessageType.Text, true, _cancellationToken);
        }
    }
}
