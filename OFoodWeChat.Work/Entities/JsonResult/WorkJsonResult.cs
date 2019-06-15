/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：WorkJsonResult.cs
    文件功能描述：企业微信 JSON 返回结果


    创建标识：Senparc - 20170617

    修改标识：Senparc - 20170702
    修改描述：v4.13.0 添加 ErrorCodeValue 属性。使用 BaseJsonResult 基类。

----------------------------------------------------------------*/
using OFoodWeChat.Infrastructure.Data.JsonResult;
using OFoodWeChat.Infrastructure.Enums;
using System;

namespace OFoodWeChat.Work.Entities
{
    /// <summary>
    /// 企业微信 JSON 返回结果
    /// </summary>
    [Serializable]
    public class WorkJsonResult : BaseJsonResult
    {
        /// <summary>
        /// 返回代码
        /// </summary>
        public ReturnCode_Work errcode { get; set; }

        /// <summary>
        /// 返回消息代码数字（同errcode枚举值）
        /// </summary>
        public override int ErrorCodeValue { get { return (int)errcode; } }
    }
}
