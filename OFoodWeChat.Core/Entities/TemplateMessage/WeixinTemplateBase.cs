/*----------------------------------------------------------------
    创建描述：v4.10.0 添加TemplateMessageBase作为所有模板消息数据实体基类

----------------------------------------------------------------*/

namespace OFoodWeChat.Core.Entities.TemplateMessage
{
    /// <summary>
    /// 模板消息数据基础类接口
    /// </summary>
    public interface ITemplateMessageBase
    {
        /// <summary>
        /// Url，为null时会自动忽略
        /// </summary>
        string TemplateId { get; set; }
        /// <summary>
        /// Url，为null时会自动忽略
        /// </summary>
        string Url { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        string TemplateName { get; set; }
    }

    /// <summary>
    /// 模板消息数据基础类
    /// </summary>
    public abstract class TemplateMessageBase : ITemplateMessageBase
    {
        /// <summary>
        /// 每个公众号都不同的templateId
        /// </summary>
        public string TemplateId { get; set; }
        /// <summary>
        /// Url，为null时会自动忽略
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }

        public TemplateMessageBase(string templateId, string url, string templateName)
        {
            TemplateId = templateId;
            Url = url;
            TemplateName = templateName;
        }
    }
}
