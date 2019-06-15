/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：IMessageHandler.cs
    文件功能描述：MessageHandler接口    
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Entities;
using OFoodWeChat.Infrastructure.MessageHandlers;

namespace OFoodWeChat.MP.MessageHandlers
{

    public interface IMessageHandler : IMessageHandler<IRequestMessageBase, IResponseMessageBase>
    {
        new IRequestMessageBase RequestMessage { get; set; }
        new IResponseMessageBase ResponseMessage { get; set; }
    }
}
