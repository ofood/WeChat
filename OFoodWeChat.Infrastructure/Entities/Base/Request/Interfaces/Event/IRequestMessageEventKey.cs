/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：IRequestMessageEventKey.cs
    文件功能描述：具有EventKey属性的RequestMessage接口

----------------------------------------------------------------*/

namespace OFoodWeChat.Infrastructure.Entities
{
    /// <summary>
    /// 具有EventKey属性的RequestMessage接口
    /// </summary>
    public interface IRequestMessageEventKey
    {
        /// <summary>
        /// EventKey值
        /// </summary>
        string EventKey { get; set; }
    }
}
