/**
*┌──────────────────────────────────────────────────────────────┐
*│<copyright file="AccessTokenBag" company="Meten">
*│     Copyright (c) 2019-2050 Meten. All rights reserved.
*│</copyright>
*│  描   述：                                                    
*│　作   者：peter                                              
*│　版   本：1.0                                                 
*│　创建时间：2019/6/13 14:26:16                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: OFoodWeChat.Core.Containers                                   
*│　类   名：AccessTokenBag                                      
*└──────────────────────────────────────────────────────────────┘
*/

using OFoodWeChat.Core.Containers;
using OFoodWeChat.MP.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFoodWeChat.MP.Containers
{
    /// <summary>
    /// AccessToken包
    /// </summary>
    [Serializable]
    public class AccessTokenBag : BaseContainerBag, IBaseContainerBag_AppId
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }


        public DateTimeOffset AccessTokenExpireTime { get; set; }


        public AccessTokenResult AccessTokenResult { get; set; }
    }
}
