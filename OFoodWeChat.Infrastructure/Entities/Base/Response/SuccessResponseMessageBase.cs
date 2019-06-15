using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OFoodWeChat.Infrastructure.Entities
{
    /// <summary>
    /// 只返回"success"等指定字符串的响应信息基类
    /// </summary>
    public class SuccessResponseMessageBase : ResponseMessageBase
    {
        /// <summary>
        /// 返回字符串内容，默认为"success"
        /// </summary>
        public string ReturnText { get; set; }

        /// <summary>
        /// SuccessResponseMessage构造函数
        /// </summary>
        public SuccessResponseMessageBase()
        {
            ReturnText = "success";
        }
    }
}
