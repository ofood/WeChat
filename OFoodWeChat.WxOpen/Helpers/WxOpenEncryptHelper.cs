﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：EncryptHelper.cs
    文件功能描述：加密、解密处理类
    修改标识：Senparc - 20190402
    修改描述：v3.3.10 添加“微信小程序运动步数解密”功能：EncryptHelper.DecryptRunData()


----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.Infrastructure.Helpers;
using OFoodWeChat.WxOpen.Containers;
using OFoodWeChat.WxOpen.Entities;

namespace OFoodWeChat.WxOpen.Helpers
{
    /// <summary>
    /// 签名及加密帮助类
    /// </summary>
    public static class WxOpenEncryptHelper
    {
   

        #region 签名


        /// <summary>
        /// 获得签名
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        public static string GetSignature(string rawData, string sessionKey)
        {
            var signature = EncryptHelper.GetSha1(rawData + sessionKey);
            //Senparc.Weixin.Helpers.EncryptHelper.SHA1_Encrypt(rawData + sessionKey);
            return signature;
        }

        /// <summary>
        /// 比较签名是否正确
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="rawData"></param>
        /// <param name="compareSignature"></param>
        /// <exception cref="WxOpenException">当SessionId或SessionKey无效时抛出异常</exception>
        /// <returns></returns>
        public static bool CheckSignature(string sessionId, string rawData, string compareSignature)
        {
            var sessionBag = SessionContainer.GetSession(sessionId);
            if (sessionBag == null)
            {
                throw new WxOpenException("SessionId无效");
            }

            if (string.IsNullOrEmpty(sessionBag.SessionKey))
            {
                throw new WxOpenException("SessionKey无效");
            }

            var signature = GetSignature(rawData, sessionBag.SessionKey);
            return signature == compareSignature;
        }

        #endregion

        #region 解密

        #region 私有方法

        private static byte[] AES_Decrypt(String Input, byte[] Iv, byte[] Key)
        {
            SymmetricAlgorithm aes = Aes.Create();
            aes.KeySize = 128;//原始：256
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Key;
            aes.IV = Iv;
            var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    //        cs.Read(decryptBytes, 0, decryptBytes.Length);
                    //        cs.Close();
                    //        ms.Close();

                    //cs.FlushFinalBlock();//用于解决第二次获取小程序Session解密出错的情况

                    byte[] xXml = Convert.FromBase64String(Input);
                    byte[] msg = new byte[xXml.Length + 32 - xXml.Length % 32];
                    Array.Copy(xXml, msg, xXml.Length);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = decode2(ms.ToArray());
            }
            return xBuff;
        }

        private static byte[] decode2(byte[] decrypted)
        {
            int pad = (int)decrypted[decrypted.Length - 1];
            if (pad < 1 || pad > 32)
            {
                pad = 0;
            }
            byte[] res = new byte[decrypted.Length - pad];
            Array.Copy(decrypted, 0, res, 0, decrypted.Length - pad);
            return res;
        }


        #endregion

        /// <summary>
        /// 解密所有消息的基础方法
        /// </summary>
        /// <param name="sessionKey">储存在 SessionBag 中的当前用户 会话 SessionKey</param>
        /// <param name="encryptedData">接口返回数据中的 encryptedData 参数</param>
        /// <param name="iv">接口返回数据中的 iv 参数，对称解密算法初始向量</param>
        /// <returns></returns>
        public static string DecodeEncryptedData(string sessionKey, string encryptedData, string iv)
        {
            var aesCipher = Convert.FromBase64String(encryptedData);
            var aesKey = Convert.FromBase64String(sessionKey);
            var aesIV = Convert.FromBase64String(iv);

            var result = AES_Decrypt(encryptedData, aesIV, aesKey);
            var resultStr = Encoding.UTF8.GetString(result);
            return resultStr;
        }

        /// <summary>
        /// 解密消息（通过SessionId获取）
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="encryptedData"></param>
        /// <param name="iv"></param>
        /// <exception cref="WxOpenException">当SessionId或SessionKey无效时抛出异常</exception>
        /// <returns></returns>
        public static string DecodeEncryptedDataBySessionId(string sessionId, string encryptedData, string iv)
        {
            var sessionBag = SessionContainer.GetSession(sessionId);
            if (sessionBag == null)
            {
                throw new WxOpenException("SessionId无效");
            }

            if (string.IsNullOrEmpty(sessionBag.SessionKey))
            {
                throw new WxOpenException("SessionKey无效");
            }

            var resultStr = DecodeEncryptedData(sessionBag.SessionKey, encryptedData, iv);
            return resultStr;
        }


        /// <summary>
        /// 检查解密消息水印
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="appId"></param>
        /// <returns>entity为null时也会返回false</returns>
        public static bool CheckWatermark(this DecodeEntityBase entity, string appId)
        {
            if (entity == null)
            {
                return false;
            }
            return entity.watermark.appid == appId;
        }

        #region 解密实例信息

        /// <summary>
        /// 解密到实例信息
        /// </summary>
        /// <typeparam name="T">DecodeEntityBase</typeparam>
        /// <param name="sessionId"></param>
        /// <param name="encryptedData"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static T DecodeEncryptedDataToEntity<T>(string sessionId, string encryptedData, string iv)
        where T : DecodeEntityBase
        {
            var jsonStr = DecodeEncryptedDataBySessionId(sessionId, encryptedData, iv);
            var entity = SerializerHelper.GetObject<T>(jsonStr);
            return entity;
        }

        /// <summary>
        /// 解密UserInfo消息（通过SessionId获取）
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="encryptedData"></param>
        /// <param name="iv"></param>
        /// <exception cref="WxOpenException">当SessionId或SessionKey无效时抛出异常</exception>
        /// <returns></returns>
        public static DecodedUserInfo DecodeUserInfoBySessionId(string sessionId, string encryptedData, string iv)
        {
            return DecodeEncryptedDataToEntity<DecodedUserInfo>(sessionId, encryptedData, iv);
        }

        /// <summary>
        /// 解密手机号
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static DecodedPhoneNumber DecryptPhoneNumber(string sessionId, string encryptedData, string iv)
        {
            return DecodeEncryptedDataToEntity<DecodedPhoneNumber>(sessionId, encryptedData, iv);
        }

        /// <summary>
        /// 解密微信小程序运动步数
        /// 2019-04-02
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="encryptedData"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static DecodedRunData DecryptRunData(string sessionId, string encryptedData, string iv)
        {
            return DecodeEncryptedDataToEntity<DecodedRunData>(sessionId, encryptedData, iv);
        }


        #endregion

        #endregion
    }
}
