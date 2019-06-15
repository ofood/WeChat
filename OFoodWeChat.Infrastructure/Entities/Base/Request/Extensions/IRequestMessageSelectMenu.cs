/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：IRequestMessageSelectMenu.cs
    文件功能描述：选择菜单点击后的请求接口  
----------------------------------------------------------------*/

namespace OFoodWeChat.Infrastructure.Entities
{
    /// <summary>
    /// 选择菜单点击后的请求接口
    /// </summary>
    public interface IRequestMessageSelectMenu
    {
        /// <summary>
        /// 选择菜单Id（对应微信 SendMenu 接口）
        /// </summary>
        string bizmsgmenuid { get; set; }
    }
}
