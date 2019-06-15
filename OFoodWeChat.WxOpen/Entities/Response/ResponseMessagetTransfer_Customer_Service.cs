/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ResponseMessagetTransfer_Customer_Service.cs
    文件功能描述：响应回复多客服消息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20180924
    修改描述：从 Senparc.Weixi.MP 移植并修改

----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Entities;
using OFoodWeChat.Infrastructure.Enums;

namespace OFoodWeChat.WxOpen.Entities
{
    /// <summary>
    /// 响应回复多客服消息
    /// </summary>
	public class ResponseMessageTransfer_Customer_Service : ResponseMessageBase, IResponseMessageTransfer_Customer_Service
    {
		public ResponseMessageTransfer_Customer_Service()
		{
		}

		public override ResponseMsgType MsgType
		{
			get { return ResponseMsgType.Transfer_Customer_Service; }
		}

	}
 }