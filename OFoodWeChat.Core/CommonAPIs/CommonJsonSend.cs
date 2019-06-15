/*----------------------------------------------------------------
    文件名：CommonJsonSend.cs
    文件功能描述：通过CommonJsonSend中的方法调用接口
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Infrastructure.Helpers;
using OFoodWeChat.Infrastructure.Helpers.Serializers;
using OFoodWeChat.Core.Exceptions;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Http;

namespace OFoodWeChat.Core.CommonAPIs
{
    /// <summary>
    /// 所有高级接口共用的向微信服务器发送 API 请求的方法
    /// </summary>
    public  static class CommonJsonSend
    {
        #region 同步方法
        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat">用accessToken参数填充{0}</param>
        /// <param name="data">如果是Get方式，可以为null</param>
        /// <param name="sendType"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="checkValidationResult">验证服务器证书回调自动验证</param>
        /// <param name="jsonSetting"></param>
        /// <param name="postFailAction">设定条件，当API结果没有返回成功信息时抛出异常</param>
        /// <returns></returns>
        public static T Send<T>(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = WxConfig.TIME_OUT, bool checkValidationResult = false, JsonSetting jsonSetting = null,Action<string,string> failAction=null)
        {
            try
            {
                var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken.AsUrlData());

                switch (sendType)
                {
                    case CommonJsonSendType.GET:
                        return Get.GetJson<T>(url, afterReturnText: failAction);
                    case CommonJsonSendType.POST:
                        var jsonString = SerializerHelper.GetJsonString(data, jsonSetting);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            var bytes = Encoding.UTF8.GetBytes(jsonString);
                            ms.Write(bytes, 0, bytes.Length);
                            ms.Seek(0, SeekOrigin.Begin);

                            WeixinTrace.SendApiPostDataLog(url, jsonString);//记录Post的Json数据

                            //PostGetJson方法中将使用WeixinTrace记录结果
                            return  Post.PostGetJson<T>(url, null, ms,
                                timeOut: timeOut,
                                afterReturnText: failAction,
                                checkValidationResult: checkValidationResult);
                        }
                    default:
                        throw new ArgumentOutOfRangeException("sendType");
                }
            }
            catch (ErrorJsonResultException ex)
            {
                ex.Url = urlFormat;
                throw;
            }
        }
        
        #endregion

        #region 异步方法
        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat"></param>
        /// <param name="data">在POST方式中将被转为JSON字符串提交</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="checkValidationResult">验证服务器证书回调自动验证</param>
        /// <param name="jsonSetting">JSON字符串生成设置</param>
        /// <param name="failAction">设定条件，当API结果没有返回成功信息时抛出异常</param>
        /// <returns></returns>
        public static async Task<T> SendAsync<T>(string accessToken, string urlFormat, object data,
            CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = WxConfig.TIME_OUT,
            bool checkValidationResult = false, JsonSetting jsonSetting = null,Action<string,string> failAction=null)
        {
            try
            {
                var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken.AsUrlData());

                switch (sendType)
                {
                    case CommonJsonSendType.GET:
                        return await Get.GetJsonAsync<T>(url, afterReturnText: failAction);
                    case CommonJsonSendType.POST:
                        var jsonString = SerializerHelper.GetJsonString(data, jsonSetting);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            var bytes = Encoding.UTF8.GetBytes(jsonString);
                            await ms.WriteAsync(bytes, 0, bytes.Length);
                            ms.Seek(0, SeekOrigin.Begin);

                            WeixinTrace.SendApiPostDataLog(url, jsonString);//记录Post的Json数据

                            //PostGetJson方法中将使用WeixinTrace记录结果
                            return await Post.PostGetJsonAsync<T>(url, null, ms,
                                timeOut: timeOut,
                                afterReturnText: failAction,
                                checkValidationResult: checkValidationResult);
                        }
                    default:
                        throw new ArgumentOutOfRangeException("sendType");
                }
            }
            catch (ErrorJsonResultException ex)
            {
                ex.Url = urlFormat;
                throw;
            }
        }
        
        #endregion
    }
}
