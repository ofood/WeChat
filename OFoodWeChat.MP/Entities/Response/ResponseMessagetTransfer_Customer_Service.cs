/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ResponseMessagetTransfer_Customer_Service.cs
    文件功能描述：响应回复多客服消息
    
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Entities;
using System.Collections.Generic;

namespace OFoodWeChat.MP.Entities
{
    /// <summary>
    /// 响应回复多客服消息
    /// </summary>
	public class ResponseMessageTransfer_Customer_Service : ResponseMessageBase, IResponseMessageTransfer_Customer_Service
    {
		public ResponseMessageTransfer_Customer_Service()
		{
			TransInfo = new List<CustomerServiceAccount>();
		}

		public override ResponseMsgType MsgType
		{
			get { return ResponseMsgType.Transfer_Customer_Service; }
		}

		public List<CustomerServiceAccount> TransInfo { get; set; }
	}

	//public class CustomerServiceAccount
	//{
	//	public string KfAccount { get; set; }
	//}
 }