/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ProductApi.cs
    文件功能描述：微小店商品接口
----------------------------------------------------------------*/

/* 
   微小店接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
*/

using System.Threading.Tasks;
using OFoodWeChat.Infrastructure;
using OFoodWeChat.Core.CommonAPIs;
using OFoodWeChat.Core;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.MP.Entities;

namespace OFoodWeChat.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 微小店库存接口
    /// </summary>
    public static class StockApi
    {
        #region 同步方法
        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="addStockData">增加库存需要Post的数据</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "StockApi.AddStock", true)]
        public static WxJsonResult AddStock(string accessToken, AddStockData addStockData)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/stock/add?access_token={0}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, addStockData);
        }

        /// <summary>
        /// 减少库存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="reduceStockData">减少库存需要Post的数据</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "StockApi.ReduceStock", true)]
        public static WxJsonResult ReduceStock(string accessToken, ReduceStockData reduceStockData)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/stock/reduce?access_token={0}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, reduceStockData);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】增加库存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="addStockData">增加库存需要Post的数据</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "StockApi.AddStockAsync", true)]
        public static async Task<WxJsonResult> AddStockAsync(string accessToken, AddStockData addStockData)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/stock/add?access_token={0}";

            return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, addStockData);
        }

        /// <summary>
        /// 【异步方法】减少库存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="reduceStockData">减少库存需要Post的数据</param>
        /// <returns></returns>
        [ApiBind(PlatformType.WeChat_OfficialAccount, "StockApi.ReduceStockAsync", true)]
        public static async Task<WxJsonResult> ReduceStockAsync(string accessToken, ReduceStockData reduceStockData)
        {
            var urlFormat = WxConfig.ApiMpHost + "/merchant/stock/reduce?access_token={0}";

            return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, reduceStockData);
        }
        #endregion
    }
}
