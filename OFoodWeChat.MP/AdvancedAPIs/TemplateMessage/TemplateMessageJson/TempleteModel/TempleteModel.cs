/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：TempleteModel.cs
    文件功能描述：模板消息接口需要的数据
    修改标识：Senparc - 20170328
    修改描述：添加对小程序的支持

----------------------------------------------------------------*/

namespace OFoodWeChat.MP.AdvancedAPIs.TemplateMessage
{
    /// <summary>
    /// 普通模板消息参数
    /// </summary>
    public class TempleteModel
    {
        /// <summary>
        /// 目标用户OpenId
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 模板消息顶部颜色（16进制），默认为#FF0000
        /// </summary>
        public string topcolor { get; set; }

        /// <summary>
        /// 模板跳转链接
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 跳小程序所需数据，不需跳小程序可不用传该数据
        /// </summary>
        public TempleteModel_MiniProgram miniprogram { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object data { get; set; }


        public TempleteModel()
        {
            topcolor = "#FF0000";
        }
    }

    /// <summary>
    /// 小程序定义
    /// </summary>
    //[Senparc.Weixin.Helpers.JsonSetting.IgnoreValue(false)]
    public class TempleteModel_MiniProgram
    {
        /// <summary>
        /// 小程序AppId
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 路径，如：index?foo=bar
        /// </summary>
        public string pagepath { get; set; }
    }
}
