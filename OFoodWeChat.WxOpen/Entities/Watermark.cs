/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：DecodeEntityBase.cs
    文件功能描述：所有解密类的基类，提供watermark属性

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.Infrastructure.Helpers;

namespace OFoodWeChat.WxOpen.Entities
{
    [Serializable]
    public class DecodeEntityBase
    {
        public Watermark watermark { get; set; }
    }

    /// <summary>
    /// 水印
    /// </summary>
    [Serializable]
    public class Watermark
    {
        public string appid { get; set; }
        public long timestamp { get; set; }

        public DateTimeOffset DateTimeStamp
        {
            get { return DateTimeHelper.GetDateTimeFromXml(timestamp); }
        }
    }
}
