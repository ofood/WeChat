﻿/*----------------------------------------------------------------
    文件名：MessageContainer.cs
    文件功能描述：微信消息容器
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace OFoodWeChat.Infrastructure.Context
{
    /// <summary>
    /// 消息容器（列表）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessageContainer<T> : List<T> 
        //where T : IMessageBase
    {
        /// <summary>
        /// 最大记录条数（保留尾部），如果小于等于0则不限制
        /// </summary>
        public int MaxRecordCount { get; set; }

        public MessageContainer()
        {
        }

        public MessageContainer(int maxRecordCount)
        {
            MaxRecordCount = maxRecordCount;
        }

        public new void Add(T item)
        {
            base.Add(item);
            RemoveExpressItems();
        }

        private void RemoveExpressItems()
        {
            if (MaxRecordCount > 0 && base.Count > MaxRecordCount)
            {
                base.RemoveRange(0, base.Count - MaxRecordCount);
            }
        }

        public new void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);
            RemoveExpressItems();
        }

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            RemoveExpressItems();
        }

        public new void InsertRange(int index, IEnumerable<T> collection)
        {
            base.InsertRange(index, collection);
            RemoveExpressItems();
        }
    }
}
