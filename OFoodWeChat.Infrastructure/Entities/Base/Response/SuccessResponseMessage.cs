/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：SuccessResponseMessage.cs
    文件功能描述：只返回"success"等指定字符串的响应信息

----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Enums;

namespace OFoodWeChat.Infrastructure.Entities
{
    /// <summary>
    /// 只返回"success"等指定字符串的响应信息
    /// </summary>
    public class SuccessResponseMessage : SuccessResponseMessageBase, IResponseMessageBase
    {
        public override ResponseMsgType MsgType
        {
            get { return ResponseMsgType.SuccessResponse; }
        }
    }
}
