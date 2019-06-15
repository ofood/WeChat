/*----------------------------------------------------------------   
    文件功能描述：JSON 配置
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace OFoodWeChat.Infrastructure.Settings
{
    /// <summary>
    /// <para>JSON 配置</para>
    /// </summary>
    public class WeixinSetting : WeixinSettingItem//继承 WeixinSettingItem 是为了可以得到一组默认的参数，方便访问
    {
        #region 微信全局

        /// <summary>
        /// 是否处于 Debug 状态（仅限微信范围）
        /// </summary>
        public bool IsDebug { get; set; }

        #endregion

        /// <summary>
        /// 系统中所有微信设置的参数，默认已经添加一个 Key 为“Default”的对象
        /// </summary>
        public WeixinSettingItemCollection Items { get; set; }

        /// <summary>
        /// SenparcWeixinSetting 构造函数
        /// </summary>
        public WeixinSetting() : this(false)
        { }

        /// <summary>
        /// SenparcWeixinSetting 构造函数
        /// </summary>
        /// <param name="isDebug">是否开启 Debug 模式</param>
        public WeixinSetting(bool isDebug)
        {
            IsDebug = isDebug;

            Items = new WeixinSettingItemCollection();
            Items["Default"] = this;//储存第一个默认参数
        }
    }
}
