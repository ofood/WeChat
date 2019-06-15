/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageText.cs
    文件功能描述：接收普通文本消息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20170422
    修改描述：添加IRequestMessageText接口
    
    修改标识：Senparc - 20180901
    修改描述：支持 NeuChar

    修改标识：Senparc - 20190307
    修改描述：v16.6.13 添加 SendMenu 相关接口，并打通消息回复响应，添加 bizmsgmenuid 属性

----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Entities;

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 文本类型消息
    /// </summary>
    public class RequestMessageText : RequestMessageBase, IRequestMessageText, IRequestMessageSelectMenu
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Text; }
        }

        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 点击的菜单ID
        /// <para>收到XML推送之后，开发者可以根据提取出来的bizmsgmenuid和Content识别出微信用户点击的是哪个菜单。</para>
        /// </summary>
        public string bizmsgmenuid { get; set; }
    }
}
