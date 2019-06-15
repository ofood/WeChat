/*----------------------------------------------------------------
    文件功能描述：浏览器公共类
----------------------------------------------------------------*/

using System.Web;

using Microsoft.AspNetCore.Http;


namespace OFoodWeChat.Infrastructure.Utilities
{
    /// <summary>
    /// 浏览器公共类
    /// </summary>
    public static class BrowserUtility
    {
        /// <summary>
        /// 获取 Headers 中的 User-Agent 字符串
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static string GetUserAgent(HttpRequest httpRequest)
        {
            string userAgent = null;
            var userAgentHeader = httpRequest.Headers["User-Agent"];
            if (userAgentHeader.Count > 0)
            {
                userAgent = userAgentHeader[0];//.ToUpper();
            }
            return userAgent;
        }
        /// <summary>
        /// 判断是否在微信内置浏览器中
        /// </summary>
        /// <param name="httpContext">HttpContextBase对象</param>
        /// <returns>true：在微信内置浏览器内。false：不在微信内置浏览器内。</returns>

        public static bool SideInWeixinBrowser(this HttpContext httpContext)

        {
            string userAgent = GetUserAgent(httpContext.Request).ToUpper();
            //判断是否在微信浏览器内部
            var isInWeixinBrowser = userAgent != null &&
                        (userAgent.Contains("MICROMESSENGER")/*MicroMessenger*/ ||
                        userAgent.Contains("WINDOWS PHONE")/*Windows Phone*/);
            return isInWeixinBrowser;
        }

        /// <summary>
        /// 判断是否在微信小程序内发起请求（注意：此方法在Android下有效，在iOS下暂时无效！）
        /// </summary>
        /// <param name="httpContext">HttpContextBase对象</param>
        /// <returns>true：在微信内置浏览器内。false：不在微信内置浏览器内。</returns>

        public static bool SideInWeixinMiniProgram(this HttpContext httpContext)
        {
            string userAgent = GetUserAgent(httpContext.Request).ToUpper();
            //判断是否在微信小程序的 web-view 组件内部
            var isInWeixinMiniProgram = userAgent != null && userAgent.Contains("MINIPROGRAM")/*miniProgram*/;
            return isInWeixinMiniProgram;
        }
    }
}
