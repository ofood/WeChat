/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetMenuResult.cs
    文件功能描述：获取菜单返回的Json结果

----------------------------------------------------------------*/

using OFoodWeChat.Work.Entities.Menu;

namespace OFoodWeChat.Work.Entities
{
    /// <summary>
    /// GetMenu返回的Json结果
    /// </summary>
    public class GetMenuResult: WorkJsonResult
    {
        public ButtonGroup menu { get; set; }

        public GetMenuResult()
        {
            menu = new ButtonGroup();
        }
    }
}
