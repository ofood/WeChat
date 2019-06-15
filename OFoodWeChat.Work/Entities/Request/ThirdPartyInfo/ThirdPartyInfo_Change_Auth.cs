/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ThirdPartyInfo_Change_Auth.cs
    文件功能描述：变更授权的通知
----------------------------------------------------------------*/

namespace OFoodWeChat.Work.Entities
{
    /// <summary>
    /// 变更授权的通知
    /// </summary>
    public class RequestMessageInfo_Change_Auth : ThirdPartyInfoBase, IThirdPartyInfoBase
    {
        public override ThirdPartyInfo InfoType
        {
            get { return ThirdPartyInfo.CHANGE_AUTH; }
        }

        /// <summary>
        /// 授权方企业号的corpid
        /// </summary>
        public string AuthCorpId { get; set; }
    }
}
