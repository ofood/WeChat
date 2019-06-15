/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：Register.cs
    文件功能描述：Senparc.CO2NET.Cache.Redis 快捷注册流程


    创建标识：Senparc - 20180222

    修改标识：Senparc - 20180606
    修改描述：缓存工厂重命名为 ContainerCacheStrategyFactory

    修改标识：Senparc - 20180802
    修改描述：v3.1.0 1、Register.RegisterCacheRedis 标记为过期
                     2、新增 Register.SetConfigurationOption() 方法
                     3、新增 Register.UseKeyValueRedisNow() 方法
                     4、新增 Register.UseHashRedisNow() 方法

----------------------------------------------------------------*/
using OFoodWeChat.Infrastructure.Cache;
using OFoodWeChat.Infrastructure.RegisterServices;
using System;

namespace OFoodWeChat.Cache.Redis
{
    /// <summary>
    /// Redis 注册
    /// </summary>
    public static class Register
    {
        /// <summary>
        /// 设置连接字符串（不立即启用）
        /// </summary>
        /// <param name="redisConfigurationString"></param>
        public static void SetConfigurationOption(string redisConfigurationString)
        {
            RedisManager.ConfigurationOption = redisConfigurationString;
        }

        /// <summary>
        /// 立即使用键值对方式储存的 Redis（推荐）
        /// </summary>
        public static void UseKeyValueRedisNow()
        {
            CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);//键值Redis
        }

        /// <summary>
        /// 立即使用 HashSet 方式储存的 Redis 缓存策略
        /// </summary>
        public static void UseHashRedisNow()
        {
            CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisHashSetObjectCacheStrategy.Instance);//Hash格式储存的Redis
        }
    }
}
