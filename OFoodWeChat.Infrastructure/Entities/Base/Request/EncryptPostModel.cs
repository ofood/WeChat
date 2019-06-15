/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：EncryptPostModel.cs
    文件功能描述：加解密消息统一基类 接口
----------------------------------------------------------------*/



namespace OFoodWeChat.Infrastructure
{
    /// <summary>
    /// 接收加密信息统一接口（同时也支持非加密信息）
    /// </summary>
    public interface IEncryptPostModel
    {
        /// <summary>
        /// 指定当前服务账号的唯一领域定义（主要为 APM 服务），例如 AppId
        /// </summary>
        string DomainId { get; set; }

        /// <summary>
        /// Signature
        /// </summary>
        string Signature { get; set; }
        /// <summary>
        /// Msg_Signature
        /// </summary>
        string Msg_Signature { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        string Timestamp { get; set; }
        /// <summary>
        /// Nonce
        /// </summary>
        string Nonce { get; set; }

        //以下信息不会出现在微信发过来的信息中，都是企业号后台需要设置（获取的）的信息，用于扩展传参使用

        /// <summary>
        /// Token
        /// </summary>
        string Token { get; set; }
        /// <summary>
        /// EncodingAESKey
        /// </summary>
        string EncodingAESKey { get; set; }
    }

    /// <summary>
    /// 接收加密信息统一基类（同时也支持非加密信息）
    /// </summary>
    public abstract class EncryptPostModel : IEncryptPostModel
    {
        /// <summary>
        /// 指定当前服务账号的唯一领域定义（主要为 APM 服务），例如 AppId
        /// </summary>
       public abstract string DomainId { get; set; }

        /// <summary>
        /// Signature
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// Msg_Signature
        /// </summary>
        public string Msg_Signature { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        public string Timestamp { get; set; }
        /// <summary>
        /// Nonce
        /// </summary>
        public string Nonce { get; set; }

        //以下信息不会出现在微信发过来的信息中，都是企业号后台需要设置（获取的）的信息，用于扩展传参使用

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// EncodingAESKey
        /// </summary>
        public string EncodingAESKey { get; set; }

        /// <summary>
        /// 设置服务器内部保密信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="encodingAESKey"></param>
        public virtual void SetSecretInfo(string token, string encodingAESKey)
        {
            Token = token;
            EncodingAESKey = encodingAESKey;
        }
    }
}
