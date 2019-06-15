/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
 
    文件名：TenPayRights.cs
    文件功能描述：微信支付维权接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20190129
    修改描述：统一 CommonJsonSend.Send<T>() 方法请求接口

----------------------------------------------------------------*/

/*
    官方API：https://mp.weixin.qq.com/htmledition/res/bussiness-course2/wxm-payment-kf-api.pdf
 */

using System.Threading.Tasks;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Infrastructure.Data.JsonResult;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Core;

namespace OFoodWeChat.TenPay.V2
{
    /// <summary>
    /// 微信支付维权接口，官方API：https://mp.weixin.qq.com/htmledition/res/bussiness-course2/wxm-payment-kf-api.pdf
    /// </summary>
    public static class TenPayRights
    {
        #region 同步方法
        
        
        /// <summary>
        /// 标记客户的投诉处理状态
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">支付该笔订单的用户 ID</param>
        /// <param name="feedBackId">投诉单号</param>
        /// <returns></returns>
        public static BaseJsonResult UpDateFeedBack(string accessToken, string openId, string feedBackId)
        {
            var urlFormat = WxConfig.ApiMpHost + "/payfeedback/update?access_token={0}&openid={1}&feedbackid={2}";
            var url = string.Format(urlFormat, accessToken.AsUrlData(), openId.AsUrlData(), feedBackId.AsUrlData());

            return CommonJsonSend.Send<BaseJsonResult>(null, url, null, CommonJsonSendType.GET);
        }
        #endregion

        #region 异步方法
         
        /// <summary>
        /// 【异步方法】标记客户的投诉处理状态
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">支付该笔订单的用户 ID</param>
        /// <param name="feedBackId">投诉单号</param>
        /// <returns></returns>
        public static async Task<BaseJsonResult> UpDateFeedBackAsync(string accessToken, string openId, string feedBackId)
        {
            var urlFormat = WxConfig.ApiMpHost + "/payfeedback/update?access_token={0}&openid={1}&feedbackid={2}";
            var url = string.Format(urlFormat, accessToken.AsUrlData(), openId.AsUrlData(), feedBackId.AsUrlData());

            return await CommonJsonSend.SendAsync<BaseJsonResult>(null, url, null, CommonJsonSendType.GET);
        }
        #endregion
    }
}
