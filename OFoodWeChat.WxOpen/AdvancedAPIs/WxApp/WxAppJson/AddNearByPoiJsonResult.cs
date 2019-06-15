using OFoodWeChat.WxOpen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.WxOpen.AdvancedAPIs.WxApp.WxAppJson
{
    /// <summary>
    /// 添加地点返回结果
    /// </summary>
    public class AddNearbyPoiJsonResult : WxOpenJsonResult
    {
        /// <summary>
        /// 审核单ID
        /// </summary>
        public string audit_id { get; set; }


        /// <summary>
        /// 附近地点ID
        /// </summary>
        public string poi_id { get; set; }

        /// <summary>
        /// 经营资质证件号
        /// </summary>
        public string related_credential { get; set; }
    }
}
