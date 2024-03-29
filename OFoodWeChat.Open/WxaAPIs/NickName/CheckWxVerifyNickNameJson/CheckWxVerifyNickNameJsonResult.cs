﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OFoodWeChat.Open.Entities;

namespace OFoodWeChat.Open.WxaAPIs.NickName.CheckWxVerifyNickNameJson
{
    [Serializable]
    public class CheckWxVerifyNickNameJsonResult : OpenJsonResult
    {
        /// <summary>
        ///  是否命中关键字策略。若命中，可以选填关键字材料
        /// </summary>
        public bool hit_condition { get; set; }

        /// <summary>
        /// 命中关键字的说明描述（给用户看的）
        /// </summary>
        public string wording { get; set; }
    }
}