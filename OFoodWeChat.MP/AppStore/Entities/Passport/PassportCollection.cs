/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
  
    文件名：PassportCollection.cs
    文件功能描述：同时管理多个应用的Passport的容器

----------------------------------------------------------------*/

using System.Collections.Generic;

namespace OFoodWeChat.MP.AppStore
{
    /// <summary>
    /// 同时管理多个应用的Passport的容器
    /// </summary>
    public class PassportCollection : Dictionary<string, PassportBag>
    {
        /// <summary>
        /// 统一URL前缀，如http://api.weiweihi.com:8080/App/Api
        /// </summary>
        public string BasicUrl { get; set; }
        public string MarketingToolUrl { get; set; }
        public PassportCollection()
        {
        }
    }
}
