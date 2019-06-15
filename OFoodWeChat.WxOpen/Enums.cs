/*----------------------------------------------------------------
    文件名：Enums.cs
    文件功能描述：枚举类型
----------------------------------------------------------------*/

using System.ComponentModel;

namespace OFoodWeChat.WxOpen
{
    /// <summary>
    /// 当RequestMsgType类型为Event时，Event属性的类型
    /// </summary>
    public enum Event
    {
        /// <summary>
        /// 进入会话事件
        /// </summary>
        user_enter_tempsession,
        add_nearby_poi_audit_info
    }
    
}
