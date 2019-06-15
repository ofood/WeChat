/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageInfo_Change_Contact.cs
    文件功能描述：通讯录变更事件通知 请求消息
    
    
    创建标识：Senparc - 20161204

    修改标识：Senparc - 20180909
    修改描述：v3.1.2 RequestMessageInfo_Contact_Sync 改名为 RequestMessageInfo_Change_Contact；
                     枚举 ThirdPartyInfo.CONTACT_SYNC 改名为 ThirdPartyInfo.CHANGE_CONTACT
    
----------------------------------------------------------------*/

namespace OFoodWeChat.Work.Entities
{
    /// <summary>
    /// 变更授权的通知
    /// </summary>
    public class RequestMessageInfo_Change_Contact : ThirdPartyInfoBase, IThirdPartyInfoBase
    {
        public override ThirdPartyInfo InfoType
        {
            get { return ThirdPartyInfo.CHANGE_CONTACT; }
        }

        /// <summary>
        /// 授权方企业号的corpid
        /// </summary>
        public string AuthCorpId { get; set; }

        /// <summary>
        /// 当前序号
        /// </summary>
        public int Seq { get; set; }
    }
}
