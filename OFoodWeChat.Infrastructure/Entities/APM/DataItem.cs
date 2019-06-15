﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：DataItem.cs
    文件功能描述：日志记录的最小单位

 ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace OFoodWeChat.Infrastructure.Entities.APM
{
    /// <summary>
    /// 日志记录的最小单位
    /// </summary>
    public class DataItem
    {
        /// <summary>
        /// 统计类别名称
        /// </summary>
        public string KindName { get; set; }
        /// <summary>
        /// 统计时间
        /// </summary>
        public DateTimeOffset DateTime { get; set; }
        /// <summary>
        /// 统计值
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// 复杂类型数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 临时储存（不会对外传递）
        /// </summary>
        public object TempStorage { get; set; }
    }
}
