/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：OpenIdResultJson.cs
    文件功能描述：获取关注者OpenId信息返回结果
----------------------------------------------------------------*/

using System.Collections.Generic;
using OFoodWeChat.MP.Entities;
namespace OFoodWeChat.MP.AdvancedAPIs.User
{
    public class OpenIdResultJson : WxJsonResult
    {
       public int total { get; set; }
       public int count { get; set; }
       public OpenIdResultJson_Data data { get; set; }
       public string next_openid { get; set; }
    }

    public class OpenIdResultJson_Data
    {
        public List<string> openid { get; set; }
    }
}
