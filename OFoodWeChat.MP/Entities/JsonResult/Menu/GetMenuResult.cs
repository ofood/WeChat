/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetMenuResult.cs
    文件功能描述：获取菜单返回的Json结果
----------------------------------------------------------------*/

using System.Collections.Generic;
using OFoodWeChat.MP.Entities.Menu;

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// GetMenu返回的Json结果
    /// </summary>
    public class GetMenuResult : WxJsonResult
    {
        //TODO：这里如果有更加复杂的情况，可以换成ButtonGroupBase类型，并提供泛型
        public ButtonGroupBase menu { get; set; }

        /// <summary>
        /// 有个性化菜单时显示。最新的在最前。
        /// </summary>
        public List<ConditionalButtonGroup> conditionalmenu { get; set; }

        public GetMenuResult(ButtonGroupBase buttonGroupBase)
        {
            menu = buttonGroupBase;
        }
    }
}
