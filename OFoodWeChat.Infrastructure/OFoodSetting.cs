/*----------------------------------------------------------------
    文件名：OFoodSetting.cs
    文件功能描述：全局设置

----------------------------------------------------------------*/
namespace OFoodWeChat.Infrastructure
{
    /// <summary>
    /// 全局设置
    /// </summary>
    public class OFoodSetting
    {
        /// <summary>
        /// 是否出于Debug状态
        /// </summary>
        public bool IsDebug { get; set; }

        /// <summary>
        /// 默认缓存键的第一级命名空间，默认值：DefaultCache
        /// </summary>
        public string DefaultCacheNamespace { get; set; }

        /// <summary>
        /// Senparc 统一代理标识
        /// </summary>
        public string SenparcUnionAgentKey { get; set; }


        #region 分布式缓存

        /// <summary>
        /// Redis连接字符串
        /// </summary>
        public string Cache_Redis_Configuration { get; set; }

        /// <summary>
        /// Memcached连接字符串
        /// </summary>
        public string Cache_Memcached_Configuration { get; set; }


        #endregion


        /// <summary>
        /// SenparcSetting 构造函数
        /// </summary>
        public OFoodSetting() : this(false)
        {

        }

        /// <summary>
        /// OFoodSetting 构造函数
        /// </summary>
        public OFoodSetting(bool isDebug)
        {
            IsDebug = isDebug;
        }
    }
}
