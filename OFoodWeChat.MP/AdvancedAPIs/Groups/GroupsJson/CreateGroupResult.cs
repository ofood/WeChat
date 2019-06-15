/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：CreateGroupResult.cs
    文件功能描述：创建分组返回结果
----------------------------------------------------------------*/

using OFoodWeChat.MP.Entities;

namespace OFoodWeChat.MP.AdvancedAPIs.Groups
{
    /// <summary>
    /// 创建分组返回结果
    /// </summary>
    public class CreateGroupResult : WxJsonResult
    {
        public GroupsJson_Group group { get; set; }
    }
}
