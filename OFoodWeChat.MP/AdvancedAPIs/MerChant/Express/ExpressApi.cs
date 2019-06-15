/*----------------------------------------------------------------
修改标识：Senparc - 20160719
修改描述：增加其接口的异步方法
----------------------------------------------------------------*/

using System.Threading.Tasks;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Core;
using OFoodWeChat.MP.Entities;

namespace OFoodWeChat.MP.AdvancedAPIs
{
    /// <summary>
    /// 微小店接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
    /// </summary>
    public static class ExpressApi
    {
        #region 同步方法
        
      
        /// <summary>
        /// 增加邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="addExpressData">增加邮费模板需要Post的数据</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ExpressApi.AddExpress", true)]
        public static AddExpressResult AddExpress(string accessToken, AddExpressData addExpressData)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/express/add?access_token={0}";

            return CommonJsonSend.Send<AddExpressResult>(accessToken, urlFormat, addExpressData);
        }

        /// <summary>
        /// 删除邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="templateId">邮费模板Id</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ExpressApi.DeleteExpress", true)]
        public static WxJsonResult DeleteExpress(string accessToken, int templateId)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/express/del?access_token={0}";

            var data = new
            {
                template_id = templateId
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 修改邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="upDateExpressData">修改邮费模板需要Post的数据</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ExpressApi.UpDateExpress", true)]
        public static WxJsonResult UpDateExpress(string accessToken, UpDateExpressData upDateExpressData)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/express/update?access_token={0}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, upDateExpressData);
        }

        /// <summary>
        /// 获取指定ID的邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="templateId">邮费模板Id</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ExpressApi.GetByIdExpress", true)]
        public static GetByIdExpressResult GetByIdExpress(string accessToken, int templateId)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/express/getbyid?access_token={0}";

            var data = new
            {
                template_id = templateId
            };

            return CommonJsonSend.Send<GetByIdExpressResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取所有邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ExpressApi.GetAllExpress", true)]
        public static GetAllExpressResult GetAllExpress(string accessToken)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/express/getall?access_token={0}";

            return CommonJsonSend.Send<GetAllExpressResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】增加邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="addExpressData">增加邮费模板需要Post的数据</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ExpressApi.AddExpressAsync", true)]
        public static async Task<AddExpressResult> AddExpressAsync(string accessToken, AddExpressData addExpressData)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/express/add?access_token={0}";

            return await CommonJsonSend.SendAsync<AddExpressResult>(accessToken, urlFormat, addExpressData);
        }

        /// <summary>
        /// 【异步方法】删除邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="templateId">邮费模板Id</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ExpressApi.DeleteExpressAsync", true)]
        public static async Task<WxJsonResult> DeleteExpressAsync(string accessToken, int templateId)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/express/del?access_token={0}";

            var data = new
            {
                template_id = templateId
            };

            return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 【异步方法】修改邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="upDateExpressData">修改邮费模板需要Post的数据</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ExpressApi.UpDateExpressAsync", true)]
        public static async Task<WxJsonResult> UpDateExpressAsync(string accessToken, UpDateExpressData upDateExpressData)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/express/update?access_token={0}";

            return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, upDateExpressData);
        }

        /// <summary>
        /// 【异步方法】获取指定ID的邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="templateId">邮费模板Id</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ExpressApi.GetByIdExpressAsync", true)]
        public static async Task<GetByIdExpressResult> GetByIdExpressAsync(string accessToken, int templateId)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/express/getbyid?access_token={0}";

            var data = new
            {
                template_id = templateId
            };

            return await CommonJsonSend.SendAsync<GetByIdExpressResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 【异步方法】获取所有邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ExpressApi.GetAllExpressAsync", true)]
        public static async Task<GetAllExpressResult> GetAllExpressAsync(string accessToken)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/express/getall?access_token={0}";

            return await CommonJsonSend.SendAsync<GetAllExpressResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }
        #endregion
    }
}
