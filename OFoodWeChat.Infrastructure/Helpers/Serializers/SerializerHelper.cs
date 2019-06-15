/*----------------------------------------------------------------   
    文件名：SerializerHelper.cs
    文件功能描述：unicode解码
----------------------------------------------------------------*/



using OFoodWeChat.Infrastructure.Helpers.Serializers;
using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using Newtonsoft.Json;

namespace OFoodWeChat.Infrastructure.Helpers
{

    /// <summary>
    /// 序列化帮助类
    /// </summary>
    public static class SerializerHelper
    {
        #region JSON

        /// <summary>
        /// 将对象转为JSON字符串（进行Json输出配置）
        /// </summary>
        /// <param name="data">需要生成JSON字符串的数据</param>
        /// <param name="jsonSetting">JSON输出设置</param>
        /// <returns></returns>
        public static string GetJsonString(object data, JsonSetting jsonSetting = null)
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(data, jsonSetting);

            //TODO：视情况启用

            ////解码Unicode，也可以通过设置App.Config（Web.Config）设置来做，这里只是暂时弥补一下，用到的地方不多
            //string jsonString;
            //MatchEvaluator evaluator = new MatchEvaluator(DecodeUnicode);
            //var json = Regex.Replace(jsonString, @"\\u[0123456789abcdef]{4}", evaluator);//或：[\\u007f-\\uffff]，\对应为\u000a，但一般情况下会保持\
            //return json;
        }

        /// <summary>
        /// 反序列化到对象
        /// </summary>
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="jsonString">JSON字符串</param>
        /// <returns></returns>
        public static T GetObject<T>(this string jsonString)
        {
            return (T)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString, typeof(T));
        }

        /// <summary>
        /// 反序列化到对象
        /// </summary>
        /// <param name="jsonString">JSON字符串</param>
        /// <param name="type">反序列化类型</param>
        /// <returns></returns>
        public static object GetObject(this string jsonString, Type type)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString, type);
        }

        #endregion

        #region Unicode

        /// <summary>
        /// 将字符串转为Unicode
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncodeUnicode(string str)
        {
            char[] charbuffers = str.ToCharArray();
            byte[] buffer;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charbuffers.Length; i++)
            {
                buffer = System.Text.Encoding.Unicode.GetBytes(charbuffers[i].ToString());
                sb.Append(String.Format("\\u{0:X2}{1:X2}", buffer[1], buffer[0]));
            }
            return sb.ToString();
        }

        /// <summary>
        /// unicode解码
        /// </summary>
        /// <param name="unicodeStr">unicode编码字符串</param>
        /// <returns></returns>
        public static string DecodeUnicode(string unicodeStr)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(unicodeStr))
            {
                string[] strlist = unicodeStr.Replace("\\", "").Split('u');
                try
                {
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        //将unicode字符转为10进制整数，然后转为char中文字符
                        sb.Append((char)int.Parse(strlist[i], System.Globalization.NumberStyles.HexNumber));
                    }
                }
                catch (FormatException ex)
                {
                    sb.Append("||出错：" + ex.Message);
                }
            }
            return sb.ToString();
        }


        //TODO：需要优化Match匹配条件后，可以启用
        ///// <summary>
        ///// unicode解码
        ///// </summary>
        ///// <param name="match"></param>
        ///// <returns></returns>
        //public static string DecodeUnicode(Match match)
        //{
        //    //Unicode码对照表：http://www.cnblogs.com/whiteyun/archive/2010/07/06/1772218.html

        //    if (!match.Success)
        //    {
        //        return null;
        //    }

        //    char outStr = (char)int.Parse(match.Value.Remove(0, 2), NumberStyles.HexNumber);
        //    return new string(outStr, 1);
        //}


        #endregion
    }
}
