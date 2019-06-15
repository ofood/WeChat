/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：BaseGroupMessageDataByOpenId.cs
    文件功能描述：根据OpenId群发所需的数据
    
    修改标识：Senparc - 2011224
    修改描述：v14.8.12 完成群发接口添加clientmsgid属性

----------------------------------------------------------------*/

namespace OFoodWeChat.MP.AdvancedAPIs.GroupMessage
{
    public class BaseGroupMessageDataByOpenId
    {
        public string[] touser { get; set; }
        public string msgtype { get; set; }
        /// <summary>
        /// （非必填）开发者侧群发msgid，长度限制64字节，如不填，则后台默认以群发范围和群发内容的摘要值做为clientmsgid
        /// </summary>
        public string clientmsgid { get; set; }
    }

    public class GroupMessageByOpenId_MediaId
    {
        public string media_id { get; set; }
    }

    public class GroupMessageByOpenId_Content
    {
        public string content { get; set; }
    }

    public class GroupMessageByOpenId_Video
    {
        public string title { get; set; }
        public string media_id { get; set; }
        public string description { get; set; }
    }

    public class GroupMessageByOpenId_WxCard
    {
        public string card_id { get; set; }
    }

    public class GroupMessageByOpenId_VoiceData : BaseGroupMessageDataByOpenId
    {
        public GroupMessageByOpenId_MediaId voice { get; set; }  
    }

    public class GroupMessageByOpenId_ImageData : BaseGroupMessageDataByOpenId
    {
        public GroupMessageByOpenId_MediaId image { get; set; }
    }

    public class GroupMessageByOpenId_TextData : BaseGroupMessageDataByOpenId
    {
        public GroupMessageByOpenId_Content text { get; set; }
    }

    public class GroupMessageByOpenId_MpNewsData : BaseGroupMessageDataByOpenId
    {
        public GroupMessageByOpenId_MediaId mpnews { get; set; }
    }

    public class GroupMessageByOpenId_MpVideoData : BaseGroupMessageDataByOpenId
    {
        public GroupMessageByOpenId_Video video { get; set; }
    }

    public class GroupMessageByOpenId_WxCardData : BaseGroupMessageDataByOpenId
    {
        public GroupMessageByOpenId_WxCard wxcard { get; set; }
    }
}
