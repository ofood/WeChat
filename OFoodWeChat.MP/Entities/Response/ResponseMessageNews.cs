﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ResponseMessageNews.cs
    文件功能描述：响应回复图文消息
----------------------------------------------------------------*/


using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Entities;
using System.Collections.Generic;

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 图文消息
    /// </summary>
    public class ResponseMessageNews : ResponseMessageBase, IResponseMessageNews
    {
        public override ResponseMsgType MsgType
        {
            get { return ResponseMsgType.News; }
        }

        public int ArticleCount
        {
            get
            {
                return Articles == null ? 0 : Articles.Count;
            }
            set
            {
                //这里开放set只为了逆向从Response的Xml转成实体的操作一致性，没有实际意义。
            }
        }

        /// <summary>
        /// 文章列表，微信客户端只能输出前10条（可能未来数字会有变化，出于视觉效果考虑，建议控制在8条以内）
        /// </summary>
        public List<Article> Articles { get; set; }

        public ResponseMessageNews()
        {
            Articles = new List<Article>();
        }
    }
}
