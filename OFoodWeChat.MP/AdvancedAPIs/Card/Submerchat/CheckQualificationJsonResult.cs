﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：CheckQualificationJsonResult.cs
    文件功能描述：母商户资质审核查询返回结果
----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.MP.Entities;

namespace OFoodWeChat.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 母商户资质审核查询返回结果
    /// </summary>
    public class CheckQualificationJsonResult : WxJsonResult 
    {
        /// <summary>
        /// 查询结果1.RESULT_PASS：审核通过 2.RESULT_NOT_PASS：审核驳回 3.RESULT_CHECKING：待审核 4.RESULT_NOTHING_TO_CHECK：无提审记录
        /// </summary>
        public int result { get; set; }
    }
}
