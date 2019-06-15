
/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
  
    文件名：Passport.cs
    文件功能描述：P2P通行证

----------------------------------------------------------------*/

using System;

namespace OFoodWeChat.MP.AppStore
{
    /// <summary>
    /// P2P通行证
    /// </summary>
    public class Passport : IAppResultData
    {
        public string Token { get; set; }
        public string AppKey { get; set; }
        public string Secret { get; set; }
        /// <summary>
        /// API常规URL
        /// </summary>
        public string ApiUrl { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        /// <summary>
        /// 供微微嗨服务器记录唯一开发人员ID
        /// </summary>
        public int DeveloperId { get; set; }
    }
}
