/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SubscribeMsgTempleteModel.cs
    文件功能描述：模板消息接口需要的数据
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.AdvancedAPIs.TemplateMessage
{
    /// <summary>
    /// 一次性订阅消息模板
    /// </summary>
    public class SubscribeMsgTempleteModel: TempleteModel
    {
        /// <summary>
        /// 消息标题，15字以内
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// （必填）订阅场景值
        /// </summary>
        public string scene { get; set; }

        public SubscribeMsgTempleteModel() : base()
        {
        }
    }
}
