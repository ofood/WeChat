using OFoodWeChat.Infrastructure.Enums;
namespace OFoodWeChat.Work.Entities.Request.KF
{
    public class RequestMessageFile : RequestMessage
    {
        public RequestMessageFile()
        {
            this.MsgType = RequestMsgType.File;
        }
        public string MediaId { get; set; }
    }
}