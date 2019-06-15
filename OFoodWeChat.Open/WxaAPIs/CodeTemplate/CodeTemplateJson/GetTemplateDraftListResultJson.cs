/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：CodeResultJson.cs
    文件功能描述：代码模板草稿列表返回结果
----------------------------------------------------------------*/


using OFoodWeChat.Open.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.Open.WxaAPIs
{
    [Serializable]
    public class GetTemplateDraftListResultJson : OpenJsonResult
    {
        /// <summary>
        /// 草稿列表
        /// </summary>
        public List<DraftInfo> draft_list { get; set; }
    }

    [Serializable]
    public class DraftInfo
    {
        /// <summary>
        /// 开发者上传草稿的时间
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 模板版本号，开发者自定义字段
        /// </summary>
        public string user_version { get; set; }

        /// <summary>
        /// 模板描述，开发者自定义字段
        /// </summary>
        public string user_desc { get; set; }

        /// <summary>
        /// 草稿ID
        /// </summary>
        public int draft_id { get; set; }
    }
}
