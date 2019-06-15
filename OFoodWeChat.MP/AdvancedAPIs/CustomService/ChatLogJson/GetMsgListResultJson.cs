/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：GetMsgListResultJson.cs
    文件功能描述：GetMsgListResultJson

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.MP.Entities;

namespace OFoodWeChat.MP.AdvancedAPIs.CustomService
{
    public class GetMsgListResultJson : WxJsonResult
    {
        public List<GetMsgList> recordList { get; set; }
        public int number { get; set; }
        public long msgid { get; set; }
    }

    public class GetMsgList
    {
        public string openid { get; set; }
        public string opercode { get; set; }
        public string text { get; set; }
        public DateTimeOffset time { get; set; }
        public string worker { get; set; }
    }
}
