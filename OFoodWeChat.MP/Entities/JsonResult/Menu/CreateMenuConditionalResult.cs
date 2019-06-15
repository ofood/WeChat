/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：CreateMenuConditionalResult.cs.cs
    文件功能描述：创建个性化菜单结果
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// CreateMenuConditional返回的Json结果
    /// </summary>
    public class CreateMenuConditionalResult : WxJsonResult
    {
        /* JSON:
        {"menuid":401654628}
        */
        /// <summary>
        /// menuid
        /// </summary>
        public long menuid { get; set; }
    }
}
