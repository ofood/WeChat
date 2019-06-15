/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
 
    创建标识：Senparc - 20160808
    创建描述：安全帮助类，提供SHA-1、AES算法等

    修改标识：Senparc - 20170130
    修改描述：v4.9.15 添加AES加密、解密算法
    
    修改标识：Senparc - 20170313
    修改描述：v4.11.4 修改EncryptHelper.GetSha1(string encypStr)方法算法
      
    修改标识：Senparc - 20170313
    修改描述：v4.14.3 重构MD5生成方法，并提供小写MD5方法
    
    修改标识：Senparc - 20180101
    修改描述：v4.18.10 添加 EncryptHelper.GetHmacSha256() 方法，为“小游戏”签名提供支持
    

----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OFoodWeChat.Infrastructure.Helpers
{
    /// <summary>
    /// 安全帮助类，提供SHA-1算法等
    /// </summary>
    public class EncryptHelper
    {
        #region SHA相关

        /// <summary>
        /// 采用SHA-1算法加密字符串（小写）
        /// </summary>
        /// <param name="encypStr">需要加密的字符串</param>
        /// <returns></returns>
        public static string GetSha1(string encypStr)
        {
            var sha1 = SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(encypStr));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }

            return enText.ToString();

            //byte[] strRes = Encoding.Default.GetBytes(encypStr);
            //HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            //strRes = iSHA.ComputeHash(strRes);
            //StringBuilder enText = new StringBuilder();
            //foreach (byte iByte in strRes)
            //{
            //    enText.AppendFormat("{0:x2}", iByte);
            //}
        }


        /// <summary>
        /// HMAC SHA256 加密
        /// </summary>
        /// <param name="message">加密消息原文。当为小程序SessionKey签名提供服务时，其中message为本次POST请求的数据包（通常为JSON）。特别地，对于GET请求，message等于长度为0的字符串。</param>
        /// <param name="secret">秘钥（如小程序的SessionKey）</param>
        /// <returns></returns>
        public static string GetHmacSha256(string message, string secret)
        {
            message = message ?? "";
            secret = secret ?? "";
            byte[] keyByte = Encoding.UTF8.GetBytes(secret);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                StringBuilder enText = new StringBuilder();
                foreach (var b in hashmessage)
                {
                    enText.AppendFormat("{0:x2}", b);
                }
                return enText.ToString();
            }
        }


        #endregion

        #region MD5

        /// <summary>
        /// 获取大写的MD5签名结果
        /// </summary>
        /// <param name="encypStr">需要加密的字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string GetMD5(string encypStr, Encoding encoding)
        {
            string retStr;

            MD5 m5 = MD5.Create();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用指定编码方式把字符串转化为字节数组．
            try
            {
                inputBye = encoding.GetBytes(encypStr);
            }
            catch
            {
                inputBye = Encoding.GetEncoding("utf-8").GetBytes(encypStr);

            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }

        /// <summary>
        /// 获取大写的MD5签名结果
        /// </summary>
        /// <param name="encypStr">需要加密的字符串</param>
        /// <param name="charset">编码</param>
        /// <returns></returns>
        public static string GetMD5(string encypStr, string charset = "utf-8")
        {
            charset = charset ?? "utf-8";
            try
            {
                //使用指定编码
                return GetMD5(encypStr, Encoding.GetEncoding(charset));
            }
            catch
            {
                //使用UTF-8编码
                return GetMD5("utf-8", Encoding.GetEncoding(charset));

                //#if NET35 || NET40 || NET45
                //                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
                //#else
                //                inputBye = Encoding.GetEncoding(936).GetBytes(encypStr);
                //#endif
            }
        }

        /// <summary>
        /// 获取小写的MD5签名结果
        /// </summary>
        /// <param name="encypStr">需要加密的字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string GetLowerMD5(string encypStr, Encoding encoding)
        {
            return GetMD5(encypStr, encoding).ToLower();
        }

        #endregion

        #region AES - CBC

        /// <summary>
        /// AES加密（默认为CBC模式）
        /// </summary>
        /// <param name="inputdata">输入的数据</param>
        /// <param name="iv">向量</param>
        /// <param name="strKey">加密密钥</param>
        /// <returns></returns>
        public static byte[] AESEncrypt(byte[] inputdata, byte[] iv, string strKey)
        {
            //分组加密算法   
            SymmetricAlgorithm des = Aes.Create();

            byte[] inputByteArray = inputdata;//得到需要加密的字节数组       
                                              //设置密钥及密钥向量
            des.Key = Encoding.UTF8.GetBytes(strKey.PadRight(32));
            des.IV = iv;

            //Console.WriteLine(des.Mode);//CBC

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组   
                    //cs.Close();
                    //ms.Close();
                    return cipherBytes;
                }
            }
        }


        /// <summary>
        /// AES解密（默认为CBC模式）
        /// </summary>
        /// <param name="inputdata">输入的数据</param>
        /// <param name="iv">向量</param>
        /// <param name="strKey">key</param>
        /// <returns></returns>
        public static byte[] AESDecrypt(byte[] inputdata, byte[] iv, string strKey)
        {
            SymmetricAlgorithm des = Aes.Create();

            des.Key = Encoding.UTF8.GetBytes(strKey.PadRight(32));
            des.IV = iv;
            byte[] decryptBytes = null;// new byte[inputdata.Length];
            using (MemoryStream ms = new MemoryStream(inputdata))
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (MemoryStream originalMemory = new MemoryStream())
                    {
                        Byte[] Buffer = new Byte[1024];
                        Int32 readBytes = 0;
                        while ((readBytes = cs.Read(Buffer, 0, Buffer.Length)) > 0)
                        {
                            originalMemory.Write(Buffer, 0, readBytes);
                        }

                        decryptBytes = originalMemory.ToArray();

                        
                    }
                }
            }
            return decryptBytes;
        }
        #endregion


        #region AES - CEB

        /// <summary>
        ///  AES 加密（无向量，CEB模式，秘钥长度=128）
        /// </summary>
        /// <param name="str">明文（待加密）</param>
        /// <param name="key">密文</param>
        /// <returns></returns>
        public static string AESEncrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key.PadRight(32)),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);


            

        }

        /// <summary>  
        /// AES 解密（无向量，CEB模式，秘钥长度=128）
        /// </summary>  
        /// <param name="data">被加密的明文（注意：为Base64编码）</param>  
        /// <param name="key">密钥</param>  
        /// <returns>明文</returns>  
        public static string AESDecrypt(string data, string key)
        {
            byte[] encryptedBytes = Convert.FromBase64String(data);
            byte[] bKey = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length)), bKey, bKey.Length);

            MemoryStream mStream = new MemoryStream(encryptedBytes);
            //mStream.Write( encryptedBytes, 0, encryptedBytes.Length );  
            //mStream.Seek( 0, SeekOrigin.Begin );  


            //RijndaelManaged aes = new RijndaelManaged();
            SymmetricAlgorithm aes = Aes.Create();

            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.Key = bKey;
            //aes.IV = _iV;  
            CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            try
            {
                byte[] tmp = new byte[encryptedBytes.Length + 32];
                int len = cryptoStream.Read(tmp, 0, encryptedBytes.Length + 32);
                byte[] ret = new byte[len];
                Array.Copy(tmp, 0, ret, 0, len);
                return Encoding.UTF8.GetString(ret);
            }
            finally
            {
                //cryptoStream.();
                //mStream.Close();
                //aes.Clear();
            }
        }

        #endregion
    }
}
