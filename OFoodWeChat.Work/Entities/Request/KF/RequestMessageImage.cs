using OFoodWeChat.Infrastructure.Enums;
namespace OFoodWeChat.Work.Entities.Request.KF
{
    public class RequestMessageImage : RequestMessageFile
    {
        public RequestMessageImage()
        {
            this.MsgType = RequestMsgType.Image;
        }
        public string PicUrl { get; set; }
    }
}