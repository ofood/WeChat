/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetCallBackIpResult.cs
    文件功能描述：获取微信服务器的ip段的JSON返回格式
    
----------------------------------------------------------------*/

namespace OFoodWeChat.Work.Entities
{
    /// <summary>
    /// 获取微信服务器的 IP 段后的 JSON 返回格式
    /// </summary>
    public class GetCallBackIpResult : WorkJsonResult
    {
        public string[] ip_list { get; set; }
    }
}
