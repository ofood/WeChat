/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：ThirdFasteRegisterInfo.cs
    文件功能描述：第三方快速注册小程序的注册审核事件推送中的info
----------------------------------------------------------------*/

namespace OFoodWeChat.Infrastructure.Entities
{
    /// <summary>
    /// 第三方快速注册小程序的注册审核事件推送中的info
    /// </summary>
    public class ThirdFasteRegisterInfo
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 企业代码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 企业代码类型 
        /// </summary>
        public int code_type { get; set; }
        /// <summary>
        /// 法人微信号
        /// </summary>
        public string legal_persona_wechat { get; set; }
        /// <summary>
        /// 法人姓名
        /// </summary>
        public string legal_persona_name { get; set; }
        /// <summary>
        /// 第三方联系电话
        /// </summary>
        public string component_phone { get; set; }
    }
}
