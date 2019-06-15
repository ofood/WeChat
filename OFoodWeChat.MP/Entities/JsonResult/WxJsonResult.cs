/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：WxJsonResult.cs
    文件功能描述：同于公众号的JSON返回结果基类（用于菜单接口等）
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Data.JsonResult;
using OFoodWeChat.Infrastructure.Enums;
using System;

namespace OFoodWeChat.MP.Entities
{

    /// <summary>
    /// 公众号 JSON 返回结果（用于菜单接口等）
    /// </summary>
    [Serializable]
    public class WxJsonResult : BaseJsonResult
    {
      
        /// <summary>
        /// 返回消息代码数字（同errcode枚举值）
        /// </summary>
        public override int ErrorCodeValue { get { return (int)errcode; } }


        public override string ToString()
        {
            return string.Format("WxJsonResult：{{errcode:'{0}',errcode_name:'{1}',errmsg:'{2}'}}",
                (int)errcode, errcode.ToString(), errmsg);
        }

    }
}
