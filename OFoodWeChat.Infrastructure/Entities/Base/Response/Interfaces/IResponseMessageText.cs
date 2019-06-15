using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OFoodWeChat.Infrastructure.Entities
{
    /// <summary>
    /// 所有ResponseMessageText的接口
    /// </summary>
    public interface IResponseMessageText : IResponseMessageBase
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        string Content { get; set; }
    }
}
