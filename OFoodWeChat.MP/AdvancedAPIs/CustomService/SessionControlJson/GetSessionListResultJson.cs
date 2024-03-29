﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetSessionListResultJson.cs
    文件功能描述：获取客服的会话列表返回结果
   
----------------------------------------------------------------*/

using OFoodWeChat.MP.Entities;
using System.Collections.Generic;

namespace OFoodWeChat.MP.AdvancedAPIs.CustomService
{
    /// <summary>
    /// 获取客服的会话列表返回结果
    /// </summary>
    public class GetSessionListResultJson : WxJsonResult
	{
        /// <summary>
        /// 会话列表
        /// </summary>
        public List<SingleSessionList> sessionlist { get; set; }
	}

    public class SingleSessionList
    {
        /// <summary>
        /// 客户openid
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 会话创建时间，UNIX时间戳
        /// </summary>
        public string createtime { get; set; }
    }
}