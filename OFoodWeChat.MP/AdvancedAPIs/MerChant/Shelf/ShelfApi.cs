/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ShelfApi.cs
    文件功能描述：微小店货架接口
    
----------------------------------------------------------------*/

/* 
   微小店接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
*/

using System.Threading.Tasks;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Infrastructure.Entities;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.MP.Entities;
using OFoodWeChat.Core;

namespace OFoodWeChat.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 微小店货架接口
    /// </summary>
    public static class ShelfApi
    {
        #region 同步方法
        /// <summary>
        /// 增加货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="m1">控件1数据</param>
        /// <param name="m2">控件2数据</param>
        /// <param name="m3">控件3数据</param>
        /// <param name="m4">控件4数据</param>
        /// <param name="m5">控件5数据</param>
        /// <param name="shelfBanner">货架招牌图片Url</param>
        /// <param name="shelfName">货架名称</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ShelfApi.AddShelves", true)]
        public static AddShelfResult AddShelves(string accessToken, M1 m1, M2 m2, M3 m3, M4 m4, M5 m5, string shelfBanner, string shelfName)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/shelf/add?access_token={0}";

            var data = new
            {
                shelf_data = new
                {
                    module_infos = new object[]
                                {
                                    m1,
                                    m2,
                                    m3,
                                    m4,
                                    m5
                                }
                },
                shelf_banner = shelfBanner,
                shelf_name = shelfName
            };

            return CommonJsonSend.Send<AddShelfResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 删除货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="shelfId">货架Id</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ShelfApi.DeleteShelves", true)]
        public static WxJsonResult DeleteShelves(string accessToken, int shelfId)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/shelf/del?access_token={0}";

            var data = new
            {
                shelf_id = shelfId
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 修改货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="m1">控件1数据</param>
        /// <param name="m2">控件2数据</param>
        /// <param name="m3">控件3数据</param>
        /// <param name="m4">控件4数据</param>
        /// <param name="m5">控件5数据</param>
        /// <param name="shelfId">货架Id</param>
        /// <param name="shelfBanner">货架招牌图片Url</param>
        /// <param name="shelfName">货架名称</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ShelfApi.ModShelves", true)]
        public static WxJsonResult ModShelves(string accessToken, M1 m1, M2 m2, M3 m3, M4 m4, M5 m5, int shelfId, string shelfBanner, string shelfName)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/shelf/mod?access_token={0}";

            var data = new
            {
                shelf_id = shelfId,
                shelf_data = new
                {
                    module_infos = new object[]
                                {
                                    m1,
                                    m2,
                                    m3,
                                    m4,
                                    m5
                                }
                },
                shelf_banner = shelfBanner,
                shelf_name = shelfName
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取所有货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ShelfApi.GetAllShelves", true)]
        public static GetAllShelfResult GetAllShelves(string accessToken)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/shelf/getall?access_token=ACCESS_TOKEN";

            return CommonJsonSend.Send<GetAllShelfResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 根据货架ID获取货架信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="shelfId">货架Id</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ShelfApi.GetByIdShelves", true)]
        public static GetByIdShelfResult GetByIdShelves(string accessToken, int shelfId)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/shelf/getbyid?access_token={0}";

            var data = new
            {
                shelf_id = shelfId
            };

            return CommonJsonSend.Send<GetByIdShelfResult>(accessToken, urlFormat, data);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】增加货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="m1">控件1数据</param>
        /// <param name="m2">控件2数据</param>
        /// <param name="m3">控件3数据</param>
        /// <param name="m4">控件4数据</param>
        /// <param name="m5">控件5数据</param>
        /// <param name="shelfBanner">货架招牌图片Url</param>
        /// <param name="shelfName">货架名称</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ShelfApi.AddShelvesAsync", true)]
        public static async Task<AddShelfResult> AddShelvesAsync(string accessToken, M1 m1, M2 m2, M3 m3, M4 m4, M5 m5, string shelfBanner, string shelfName)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/shelf/add?access_token={0}";

            var data = new
            {
                shelf_data = new
                {
                    module_infos = new object[]
                                {
                                    m1,
                                    m2,
                                    m3,
                                    m4,
                                    m5
                                }
                },
                shelf_banner = shelfBanner,
                shelf_name = shelfName
            };

            return await CommonJsonSend.SendAsync<AddShelfResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 【异步方法】删除货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="shelfId">货架Id</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ShelfApi.DeleteShelvesAsync", true)]
        public static async Task<WxJsonResult> DeleteShelvesAsync(string accessToken, int shelfId)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/shelf/del?access_token={0}";

            var data = new
            {
                shelf_id = shelfId
            };

            return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 【异步方法】修改货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="m1">控件1数据</param>
        /// <param name="m2">控件2数据</param>
        /// <param name="m3">控件3数据</param>
        /// <param name="m4">控件4数据</param>
        /// <param name="m5">控件5数据</param>
        /// <param name="shelfId">货架Id</param>
        /// <param name="shelfBanner">货架招牌图片Url</param>
        /// <param name="shelfName">货架名称</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ShelfApi.ModShelvesAsync", true)]
        public static async Task<WxJsonResult> ModShelvesAsync(string accessToken, M1 m1, M2 m2, M3 m3, M4 m4, M5 m5, int shelfId, string shelfBanner, string shelfName)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/shelf/mod?access_token={0}";

            var data = new
            {
                shelf_id = shelfId,
                shelf_data = new
                {
                    module_infos = new object[]
                                {
                                    m1,
                                    m2,
                                    m3,
                                    m4,
                                    m5
                                }
                },
                shelf_banner = shelfBanner,
                shelf_name = shelfName
            };

            return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 【异步方法】获取所有货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ShelfApi.GetAllShelvesAsync", true)]
        public static async Task<GetAllShelfResult> GetAllShelvesAsync(string accessToken)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/shelf/getall?access_token=ACCESS_TOKEN";

            return await CommonJsonSend.SendAsync<GetAllShelfResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 【异步方法】根据货架ID获取货架信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="shelfId">货架Id</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "ShelfApi.GetByIdShelvesAsync", true)]
        public static async Task<GetByIdShelfResult> GetByIdShelvesAsync(string accessToken, int shelfId)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/shelf/getbyid?access_token={0}";

            var data = new
            {
                shelf_id = shelfId
            };

            return await CommonJsonSend.SendAsync<GetByIdShelfResult>(accessToken, urlFormat, data);
        }
        #endregion
    }
}
