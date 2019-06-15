/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：MemberCreateRequest.cs
    文件功能描述：更新成员接口 请求包

----------------------------------------------------------------*/

namespace OFoodWeChat.Work.AdvancedAPIs.MailList.Member
{
    /// <summary>
    /// 更新成员接口 请求包
    /// <para>文档：<see cref="http://work.weixin.qq.com/api/doc#10020"/></para>
    /// </summary>
    public class MemberUpdateRequest : MemberBase
    {
        /// <summary>
        /// 非必填，特别地，如果userid由系统自动生成，则仅允许修改一次。新值可由new_userid字段指定。
        /// </summary>
        public string new_userid { get; set; }
    }
}
