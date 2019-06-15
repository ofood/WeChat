/*----------------------------------------------------------------  
    文件名：RequestUtility.cs
    文件功能描述：微信请求集中处理接口
----------------------------------------------------------------*/


using OFoodWeChat.Infrastructure.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OFoodWeChat.Infrastructure.MessageHandlers
{
    /// <summary>
    /// IMessageHandlerExtensionProperties 接口
    /// </summary>
    public interface IMessageHandlerBase : IMessageHandlerEnlightener, IMessageHandlerNeuralNodes
    {
        /// <summary>
        /// 发送者用户名（OpenId）
        /// </summary>
        string WeixinOpenId { get; }

        /// <summary>
        /// 取消执行Execute()方法。一般在OnExecuting()中用于临时阻止执行Execute()。
        /// 默认为False。
        /// 如果在执行OnExecuting()执行前设为True，则所有OnExecuting()、Execute()、OnExecuted()代码都不会被执行。
        /// 如果在执行OnExecuting()执行过程中设为True，则后续Execute()及OnExecuted()代码不会被执行。
        /// 建议在设为True的时候，给ResponseMessage赋值，以返回友好信息。
        /// </summary>
        bool CancelExcute { get; set; }


        /// <summary>
        /// 忽略重复发送的同一条消息（通常因为微信服务器没有收到及时的响应）
        /// </summary>
        bool OmitRepeatedMessage { get; set; }

        /// <summary>
        /// 消息是否已经被去重
        /// </summary>
        bool MessageIsRepeated { get; set; }

        /// <summary>
        /// 是否使用了MessageAgent代理
        /// </summary>
        bool UsedMessageAgent { get; set; }

        /// <summary>
        /// 是否使用了加密消息格式
        /// </summary>
        bool UsingEcryptMessage { get; set; }

        /// <summary>
        /// 是否使用了兼容模式加密信息
        /// </summary>
        bool UsingCompatibilityModelEcryptMessage { get; set; }


        /// <summary>
        /// PostModel
        /// </summary>
        IEncryptPostModel PostModel { get; set; }

        #region 同步方法

        /// <summary>
        /// 执行微信请求前触发
        /// </summary>
        void OnExecuting();

        /// <summary>
        /// 执行请求
        /// </summary>
        void Execute();

        /// <summary>
        /// 执行请求内部的消息整理逻辑
        /// </summary>
        void BuildResponseMessage();

        /// <summary>
        /// 执行微信请求后触发
        /// </summary>
        void OnExecuted();

        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】执行微信请求前触发
        /// </summary>
        Task OnExecutingAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 【异步方法】执行微信请求
        /// </summary>
        Task ExecuteAsync(CancellationToken cancellationToken);
        /// <summary>
        /// 执行请求内部的消息整理逻辑
        /// </summary>
        Task BuildResponseMessageAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 【异步方法】执行微信请求后触发
        /// </summary>
        Task OnExecutedAsync(CancellationToken cancellationToken);

        #endregion

    }

    /// <summary>
    /// IMessageHandler 接口
    /// </summary>
    /// <typeparam name="TRequest">IRequestMessageBase</typeparam>
    /// <typeparam name="TResponse">IResponseMessageBase</typeparam>
    public interface IMessageHandler<TRequest, TResponse> : IMessageHandlerDocument, IMessageHandlerBase
        where TRequest : IRequestMessageBase
        where TResponse : IResponseMessageBase
    {
        /// <summary>
        /// 请求实体
        /// </summary>
        TRequest RequestMessage { get; set; }
        /// <summary>
        /// 响应实体
        /// 只有当执行Execute()方法后才可能有值
        /// </summary>
        TResponse ResponseMessage { get; set; }

    }
}
