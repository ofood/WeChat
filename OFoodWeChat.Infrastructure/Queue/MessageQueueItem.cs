﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SenparcMessageQueueItem.cs
    文件功能描述：SenparcMessageQueue消息队列项
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OFoodWeChat.Infrastructure.Queue
{
    /// <summary>
    /// SenparcMessageQueue消息队列项
    /// </summary>
    public class MessageQueueItem
    {
        /// <summary>
        /// 队列项唯一标识
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 队列项目命中触发时执行的委托
        /// </summary>
        public Action Action { get; set; }
        /// <summary>
        /// 此实例对象的创建时间
        /// </summary>
        public DateTimeOffset AddTime { get; set; }
        /// <summary>
        /// 项目说明（主要用于调试）
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 初始化SenparcMessageQueue消息队列项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        public MessageQueueItem(string key, Action action, string description = null)
        {
            Key = key;
            Action = action;
            Description = description;
            AddTime = SystemTime.Now;
        }
    }
}
