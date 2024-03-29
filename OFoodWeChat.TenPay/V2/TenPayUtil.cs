﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
 
    文件名：TenPayUtil.cs
    文件功能描述：微信支付配置文件
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20161014
    修改描述：修改TenPayUtil.BuildRandomStr()方法
        
    修改标识：Senparc - 20171010
    修改描述：v14.8.1 修复几处GetNoncestr还在使用GBK编码
----------------------------------------------------------------*/

using System;
using System.Text;

using System.Net;
using OFoodWeChat.Infrastructure.Helpers;

namespace OFoodWeChat.TenPay.V2
{
    /// <summary>
    /// TenpayUtil 的摘要说明。
    /// 配置文件
    /// </summary>
    public class TenPayUtil
    {
        /// <summary>
        /// 随机生成Noncestr
        /// </summary>
        /// <returns></returns>
        public static string GetNoncestr()
        {
            return EncryptHelper.GetMD5(Guid.NewGuid().ToString(), "UTF-8");
        }

        public static string GetTimestamp()
        {
            TimeSpan ts = DateTimeOffset.Now - new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 对字符串进行URL编码
        /// </summary>
        /// <param name="instr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string UrlEncode(string instr, string charset)
        {
            //return instr;
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                //string res;

                try
                {
#if NET35 || NET40 || NET45 || NET461
                    return System.Web.HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));
#else
                    return WebUtility.UrlEncode(instr);
#endif
                }
                catch (Exception ex)
                {
#if NET35 || NET40 || NET45 || NET461
                    return System.Web.HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
#else
                    return WebUtility.UrlEncode(instr);
#endif
                }

                //return res;
            }
        }

        /// <summary>
        /// 对字符串进行URL解码
        /// </summary>
        /// <param name="instr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
#if NET35 || NET40 || NET45 || NET461
                    return System.Web.HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));
#else
                    return WebUtility.UrlDecode(instr);
#endif
                }
                catch (Exception ex)
                {
#if NET35 || NET40 || NET45 || NET461
                    return System.Web.HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
#else
                    return WebUtility.UrlDecode(instr);
#endif
                }

                //return res;

            }
        }

        /// <summary>
        /// 取时间戳生成随即数,替换交易单号中的后10位流水号
        /// </summary>
        /// <returns></returns>
        public static UInt32 UnixStamp()
        {
//#if NET35 || NET40 || NET45 || NET461
//            TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
//#else
            TimeSpan ts = SystemTime.Now - new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
//#endif
            return Convert.ToUInt32(ts.TotalSeconds);
        }

        /// <summary>
        /// 取随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string BuildRandomStr(int length)
        {
            Random rand = new Random();

            int num = rand.Next();

            string str = num.ToString();

            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if (str.Length < length)
            {
                int n = length - str.Length;
                while (n > 0)
                {
                    str = str.Insert(0, "0");
                    n--;
                }
            }

            return str;
        }

    }
}