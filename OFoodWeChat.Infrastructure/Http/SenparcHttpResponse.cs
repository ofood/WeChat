/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：SenparcHttpResponse.cs
    文件功能描述：统一封装HttpResonse请求，提供Http请求过程中的调试、跟踪等扩展能力


    创建标识：Senparc - 20171104

    修改描述：统一封装HttpResonse请求

    修改标识：Senparc - 20190429
    修改描述：v0.7.0 优化 HttpClient，重构 RequestUtility（包括 Post 和 Get），引入 HttpClientFactory 机制

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using OFoodWeChat.Infrastructure.Extensions;
using System.Net.Http;
using System.Net.Http.Headers;

namespace OFoodWeChat.Infrastructure.Http
{
    /// <summary>
    /// 统一封装HttpResonse请求，提供Http请求过程中的调试、跟踪等扩展能力
    /// </summary>
    public class SenparcHttpResponse
    {
        public HttpResponseMessage Result { get; set; }
        public SenparcHttpResponse(HttpResponseMessage httpWebResponse)
        {
            Result = httpWebResponse;
        }

        
    }
}