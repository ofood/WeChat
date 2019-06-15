namespace OFoodWeChat.Infrastructure.Context
{

    /// <summary>
    /// 消息上下文全局设置
    /// </summary>
    public static class MessageContextGlobalConfig//TODO:所有设置可以整合到一起
    {
        /// <summary>
        /// 上下文操作使用的同步锁
        /// </summary>
        public static object Lock = new object();//TODO:转为同步锁

        /// <summary>
        /// 去重专用锁
        /// </summary>
        public static object OmitRepeatLock = new object();//TODO:转为同步锁
        /// <summary>
        /// 是否开启上下文记录
        /// </summary>
        public static bool UseMessageContext = true;

    }
}
