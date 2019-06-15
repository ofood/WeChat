/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SpeedResult.cs
    文件功能描述：群发速度
----------------------------------------------------------------*/

using OFoodWeChat.MP.Entities;
namespace OFoodWeChat.MP.AdvancedAPIs.GroupMessage
{
    /// <summary>
    /// 获取群发速度
    /// </summary>
    public class GetSpeedResult:WxJsonResult
    {
        /// <summary>
        /// 群发速度的级别
        /// </summary>
        public int speed { get; set; }

        /// <summary>
        /// 群发速度的真实值 单位：万/分钟
        /// </summary>
        public int realspeed { get; set; }
    }
}
