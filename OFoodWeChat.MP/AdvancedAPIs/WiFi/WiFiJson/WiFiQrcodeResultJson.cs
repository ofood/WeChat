/*----------------------------------------------------------------
    文件名：WiFiQrcodeResultJson.cs
    文件功能描述：获取物料二维码返回结果
----------------------------------------------------------------*/

using OFoodWeChat.MP.Entities;
namespace OFoodWeChat.MP.AdvancedAPIs.WiFi
{
    /// <summary>
    /// 获取物料二维码返回结果
    /// </summary>
    public class GetQrcodeResult : WxJsonResult
    {
        public GetQrcode_Data data { get; set; }
    }

    public class GetQrcode_Data
    {
        /// <summary>
        /// 二维码图片url
        /// </summary>
        public string qrcode_url { get; set; }
    }
}
