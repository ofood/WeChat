namespace OFoodWeChat.Infrastructure.Settings
{
    /// <summary>
    /// WeixinSetting基础接口
    /// </summary>
    public interface IWeixinSettingBase
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        string ItemKey { get; set; }
    }
}
