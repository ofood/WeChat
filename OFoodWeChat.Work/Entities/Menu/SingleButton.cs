/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SingleButton.cs
    文件功能描述：所有单击按钮的基类
----------------------------------------------------------------*/

namespace OFoodWeChat.Work.Entities.Menu
{
    /// <summary>
    /// 所有单击按钮的基类（view，click等）
    /// </summary>
    public abstract class SingleButton : BaseButton, IBaseButton
    {
        /// <summary>
        /// 按钮类型（click或view）
        /// </summary>
        public string type { get; set; }

        public SingleButton(string theType)
        {
            type = theType;
        }
    }
}
