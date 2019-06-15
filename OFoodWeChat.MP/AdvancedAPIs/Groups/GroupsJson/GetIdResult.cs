/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetIdResult.cs
    文件功能描述：获取用户分组ID返回结果
----------------------------------------------------------------*/

using OFoodWeChat.MP.Entities;

namespace OFoodWeChat.MP.AdvancedAPIs.Groups
{
    /// <summary>
    /// 获取用户分组ID返回结果
    /// </summary>
    public class GetGroupIdResult : WxJsonResult
    {
        public int groupid { get; set; }
    }
}
