/*----------------------------------------------------------------
    文件名：BaseJsonResult.cs
    文件功能描述：所有xxJsonResult（基类）的基类
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Enums;
using System;

namespace OFoodWeChat.Infrastructure.Data.JsonResult
{
    [Serializable]
    public abstract class BaseJsonResult : IJsonResult
    {
        public ReturnCode errcode { get; set; }
        /// <summary>
        /// 返回结果信息
        /// </summary>
        public virtual string errmsg { get; set; }

        /// <summary>
        /// errcode的
        /// </summary>
        public abstract int ErrorCodeValue { get; }
        public virtual object P2PData { get; set; }
    }
}
