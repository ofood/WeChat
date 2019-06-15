/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：EventHelper.cs
    文件功能描述：xml中的Event字段转换为Event枚举
----------------------------------------------------------------*/

using System;
using System.Xml.Linq;

namespace OFoodWeChat.MP.Helpers
{
    /// <summary>
    /// 事件帮助类
    /// </summary>
    public class EventHelper
    {
        public static Event GetEventType(XDocument doc)
        {
            return GetEventType(doc.Root.Element("Event").Value);
        }

        public static Event GetEventType(string str)
        {
            return (Event)Enum.Parse(typeof(Event), str, true);
        }
    }
}
