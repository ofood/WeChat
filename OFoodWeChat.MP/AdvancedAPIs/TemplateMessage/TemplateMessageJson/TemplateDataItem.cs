/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：TemplateDataItem.cs
    文件功能描述：模板消息的数据项类型
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.AdvancedAPIs.TemplateMessage
{
    /// <summary>
    /// 模板消息的数据项类型
    /// </summary>
    public class TemplateDataItem
    {
        /// <summary>
        /// 项目值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 16进制颜色代码，如：#FF0000
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// TemplateDataItem 构造函数
        /// </summary>
        /// <param name="v">value</param>
        /// <param name="c">color</param>
        public TemplateDataItem(string v, string c = "#173177")
        {
            value = v;
            color = c;
        }
    }
}
