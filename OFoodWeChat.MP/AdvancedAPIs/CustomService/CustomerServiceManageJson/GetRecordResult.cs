/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetRecordResult.cs
    文件功能描述：聊天记录结果
----------------------------------------------------------------*/

using System.Collections.Generic;
using OFoodWeChat.MP.Entities;

namespace OFoodWeChat.MP.AdvancedAPIs.CustomService
{
    /// <summary>
    /// 聊天记录结果
    /// </summary>
    public class GetRecordResult : WxJsonResult
    {
        /// <summary>
        /// 官方文档暂没有说明
        /// </summary>
        public int retcode { get; set; }
        public List<RecordJson> recordlist { get; set; }
    }
}
