/*----------------------------------------------------------------
    文件名：XmlDocument_XxeFixed.cs
    文件功能描述：解决 XXE 漏洞，继承自 XmlDocument 对象，自动将 XmlResolver 设为 null


----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OFoodWeChat.Infrastructure.ExtensionEntities
{
    /// <summary>
    /// 解决 XXE 漏洞，自动将 XmlResolver 设为 null
    /// </summary>
    public class XmlDocument_XxeFixed : XmlDocument
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public XmlDocument_XxeFixed(XmlResolver xmlResolver = null)
        {
            XmlResolver = null;
        }
    }
}
