/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：SessionContainer.cs
    文件功能描述：小程序 Session 容器

----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.Core.Containers;

using OFoodWeChat.WxOpen.Helpers;
using OFoodWeChat.Infrastructure.Helpers;

namespace OFoodWeChat.WxOpen.Containers
{
    /// <summary>
    /// 第三方APP信息包
    /// </summary>
    [Serializable]
    public class SessionBag : BaseContainerBag
    {
        /// <summary>
        /// Session的Key（3rd_session / sessionId）
        /// </summary>
        new public string Key { get; set; }
        

        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId { get; set; }
        

        public string UnionId { get; set; }

        /// <summary>
        /// SessionKey
        /// </summary>
        public string SessionKey { get; set; }
        
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTimeOffset ExpireTime { get; set; }
        
        /// <summary>
        /// ComponentBag
        /// </summary>
        public SessionBag()
        {
        }
    }


    /// <summary>
    /// 3rdSession容器
    /// </summary>
    public class SessionContainer : BaseContainer<SessionBag>
    {
        /// <summary>
        /// 获取最新的过期时间
        /// </summary>
        /// <returns></returns>
        private static TimeSpan GetExpireTime()
        {
            return TimeSpan.FromDays(2);//有效期2天
        }

        #region 同步方法

        /// <summary>
        /// 获取Session
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static SessionBag GetSession(string key)
        {
            var bag = TryGetItem(key);
            if (bag == null)
            {
                return null;
            }

            if (bag.ExpireTime < SystemTime.Now)
            {
                //已经过期
                Cache.RemoveFromCache(key);
                return null;
            }

            //using (FlushCache.CreateInstance())
            //{
            bag.ExpireTime = SystemTime.Now.Add(GetExpireTime());//滚动过期时间
            Update(key, bag, GetExpireTime());
            //}
            return bag;
        }

        /// <summary>
        /// 更新或插入SessionBag
        /// </summary>
        /// <param name="key">如果留空，则新建一条记录</param>
        /// <param name="openId">OpenId</param>
        /// <param name="sessionKey">SessionKey</param>
        /// <param name="uniondId">UnionId</param>
        /// <returns></returns>
        public static SessionBag UpdateSession(string key, string openId, string sessionKey, string uniondId)
        {
            key = key ?? SessionHelper.GetNewThirdSessionName();

            //using (FlushCache.CreateInstance())
            //{
            var sessionBag = new SessionBag()
            {
                Key = key,
                OpenId = openId,
                UnionId = uniondId,
                SessionKey = sessionKey,
                ExpireTime = SystemTime.Now.Add(GetExpireTime())
            };
            Update(key, sessionBag, GetExpireTime());
            return sessionBag;
            //}
        }

        #endregion

    }
}
