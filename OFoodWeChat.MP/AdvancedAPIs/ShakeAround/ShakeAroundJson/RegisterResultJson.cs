/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RegisterResultJson.cs
    文件功能描述：返回空的数据
    
    创建标识：Senparc - 20160520
----------------------------------------------------------------*/
using OFoodWeChat.MP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 申请开通功能的返回结果
    /// </summary>
    public class RegisterResultJson : WxJsonResult
    {
        public object data { get; set; }
    }
}
