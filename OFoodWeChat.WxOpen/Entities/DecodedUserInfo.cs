/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：DecodedUserInfo.cs
    文件功能描述：用户信息解密类

----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.WxOpen.Entities
{
        /// <summary>
    /// 解码后的用户信息
    /// </summary>
    [Serializable]
    public class DecodedUserInfo : DecodeEntityBase
    {
        public string openId { get; set; }
        public string nickName { get; set; }
        public int gender { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string avatarUrl { get; set; }
        public string unionId { get; set; }
    }


}
