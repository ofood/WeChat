/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：ScanCodeInfo.cs
    文件功能描述：扫码事件中的ScanCodeInfo

----------------------------------------------------------------*/

namespace OFoodWeChat.Infrastructure.Entities
{
    /// <summary>
    /// 扫码事件中的ScanCodeInfo
    /// </summary>
    public class ScanCodeInfo
    {
        public string ScanType { get; set; }
        public string ScanResult { get; set; }
    }
}
