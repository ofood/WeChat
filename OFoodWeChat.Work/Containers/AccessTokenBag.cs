using OFoodWeChat.Core.Containers;
using OFoodWeChat.Work.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFoodWeChat.Work.Containers
{
    [Serializable]
    public class AccessTokenBag : BaseContainerBag
    {
        /// <summary>
        /// CorpId
        /// </summary>
        public string CorpId { get; set; }
        /// <summary>
        /// CorpSecret
        /// </summary>
        public string CorpSecret { get; set; }


        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTimeOffset ExpireTime { get; set; }


        /// <summary>
        /// AccessTokenResult
        /// </summary>
        public AccessTokenResult AccessTokenResult { get; set; }


        /// <summary>
        /// 只针对这个CorpId的锁
        /// </summary>
        internal object Lock = new object();
    }
}
