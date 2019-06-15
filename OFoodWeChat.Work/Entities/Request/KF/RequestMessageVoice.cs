using OFoodWeChat.Infrastructure.Enums;
namespace OFoodWeChat.Work.Entities.Request.KF
{
    public class RequestMessageVoice : RequestMessageFile
    {
        public RequestMessageVoice()
        {
            this.MsgType = RequestMsgType.Voice;
        }
    }
}