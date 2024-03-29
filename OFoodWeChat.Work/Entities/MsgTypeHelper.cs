﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：MsgTypeHelper.cs
    文件功能描述：根据xml信息返回MsgType、ThirdPartyInfo、RequestInfoType
----------------------------------------------------------------*/

using System;
using System.Xml.Linq;

namespace OFoodWeChat.Work.Helpers
{
    public static class MsgTypeHelper
    {
        #region ThirdPartyInfo
        /// <summary>
        /// 根据xml信息，返回ThirdPartyInfo
        /// </summary>
        /// <returns></returns>
        public static ThirdPartyInfo GetThirdPartyInfo(XDocument doc)
        {
            return GetThirdPartyInfo(doc.Root.Element("InfoType").Value);
        }
        /// <summary>
        /// 根据xml信息，返回RequestInfoType
        /// </summary>
        /// <returns></returns>
        public static ThirdPartyInfo GetThirdPartyInfo(string str)
        {
            return (ThirdPartyInfo)Enum.Parse(typeof(ThirdPartyInfo), str, true);
        }

        #endregion
    }
}
