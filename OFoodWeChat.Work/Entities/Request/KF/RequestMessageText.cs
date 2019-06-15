using OFoodWeChat.Infrastructure.Enums;
namespace OFoodWeChat.Work.Entities.Request.KF
{
    public class RequestMessageText : RequestMessage
    {
        public RequestMessageText()
        {
            this.MsgType = RequestMsgType.Text;
        }
        public string Content { get; set; }
    }
}