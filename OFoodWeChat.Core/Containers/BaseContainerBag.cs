/*----------------------------------------------------------------    
    文件名：BaseContainerBag.cs
    文件功能描述：微信容器接口中的封装Value（如Ticket、AccessToken等数据集合）

----------------------------------------------------------------*/

using System;
using System.Runtime.CompilerServices;
using OFoodWeChat.Infrastructure.Queue;
using OFoodWeChat.Core.Cache;
using OFoodWeChat.Core.Entities;

namespace OFoodWeChat.Core.Containers
{
    /// <summary>
    /// IBaseContainerBag，BaseContainer容器中的Value类型
    /// </summary>
    public interface IBaseContainerBag
    {
        /// <summary>
        /// 用于标记，方便后台管理
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 缓存键，形如：wx669ef95216eef885，最底层的Key，不考虑命名空间等
        /// </summary>
        string Key { get; set; }
        /// <summary>
        /// 当前对象被缓存的时间
        /// </summary>
        DateTimeOffset CacheTime { get; set; }
    }

    /// <summary>
    /// 提供给具有 AppId 的 IBaseContainerBag 使用的接口
    /// </summary>
    public interface IBaseContainerBag_AppId
    {
        /// <summary>
        /// AppId
        /// </summary>
        string AppId { get; set; }
    }

    /// <summary>
    /// BaseContainer容器中的Value类型
    /// </summary>
    [Serializable]
    public class BaseContainerBag : IBaseContainerBag
    {
 
        /// <summary>
        /// 用于标记，方便后台管理
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 通常为AppId
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 缓存时间，不使用属性变化监听
        /// </summary>
        public DateTimeOffset CacheTime { get; set; }


        
        public BaseContainerBag()
        {
            //base.PropertyChanged += BaseContainerBag_PropertyChanged;
        }
    }
}
