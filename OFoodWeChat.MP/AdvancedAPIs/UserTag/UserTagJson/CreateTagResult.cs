﻿using OFoodWeChat.MP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.MP.AdvancedAPIs.UserTag
{
    public class CreateTagResult : WxJsonResult
    {
        public TagJson_Tag tag { get; set; }
    }
}
