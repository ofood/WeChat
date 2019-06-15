/*----------------------------------------------------------------  
    文件名：IAccessTokenResult.cs
    文件功能描述：AccessTokenResult的公共接口
----------------------------------------------------------------*/

namespace OFoodWeChat.Infrastructure.Data.JsonResult
{
    /// <summary>
    /// access_token请求后的JSON返回格式
    /// </summary>
    public interface IAccessTokenResult
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        string access_token { get; set; }
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        int expires_in { get; set; }
    }
}
