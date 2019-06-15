
/*----------------------------------------------------------------   
    文件名：OrderApi.cs
    文件功能描述：微小店图片接口

----------------------------------------------------------------*/

/* 
   微小店接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
*/

using System.Threading.Tasks;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Infrastructure.Helpers;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Http;

namespace OFoodWeChat.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 微小店图片接口
    /// </summary>
    public static class PictureApi
    {
        #region 同步方法
        [ApiBind(PlatformType.WeChat_OfficialAccount, "PictureApi.UploadImg", true)]
        public static PictureResult UploadImg(string accessToken, string fileName)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/common/upload_img?access_token={0}&filename={1}";
            var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken.AsUrlData(), fileName.AsUrlData());

            var json = new PictureResult();

            using (var fs = FileHelper.GetFileStream(fileName))
            {
                json = Post.PostGetJson<PictureResult>(url, null, fs);
            }
            return json;
        }
        #endregion

        #region 异步方法
        [ApiBind(PlatformType.WeChat_OfficialAccount, "PictureApi.UploadImgAsync", true)]
        public static async Task<PictureResult> UploadImgAsync(string accessToken, string fileName)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/common/upload_img?access_token={0}&filename={1}";
            var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken.AsUrlData(), fileName.AsUrlData());

            var json = new PictureResult();

            using (var fs = FileHelper.GetFileStream(fileName))
            {
                json = await Post.PostGetJsonAsync<PictureResult>(url, null, fs);
            }
            return json;
        }
        #endregion
    }
}
