/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GroupMessageByTagId.cs
    文件功能描述：根据 TagId 群发所需的数据
    
    
    创建标识：Senparc - 20171217

----------------------------------------------------------------*/

using System;

namespace OFoodWeChat.MP.AdvancedAPIs.GroupMessage
{
    /// <summary>
    /// 根据 TagId 群发筛选
    /// </summary>
    public class GroupMessageByTagId : BaseGroupMessageByFilter
    {
        public string tag_id { get; set; }
    }
}
