/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：IRequestMessageEventKey.cs
    文件功能描述：具有EventKey属性的RequestMessage接口
----------------------------------------------------------------*/

namespace OFoodWeChat.Work.Entities
{
    /// <summary>
    /// 具有EventKey属性的RequestMessage接口
    /// </summary>
    public interface IRequestMessageEventKey
    {
        string EventKey { get; set; }
    }
}
