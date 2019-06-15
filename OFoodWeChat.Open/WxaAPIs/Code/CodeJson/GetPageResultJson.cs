/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetPageResultJson.cs
    文件功能描述：首页和每页信息返回结果
    
    
    创建标识：Senparc - 20170726


----------------------------------------------------------------*/


using OFoodWeChat.Open.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.Open.WxaAPIs
{
    public class GetPageResultJson : OpenJsonResult
    {
        public List<string> page_list { get; set; }
    }
}
