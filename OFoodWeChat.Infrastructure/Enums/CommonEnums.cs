namespace OFoodWeChat.Infrastructure.Enums
{
    /// <summary>
    /// 数据类型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 列表
        /// </summary>
        List,
        /// <summary>
        /// 单项（唯一键）
        /// </summary>
        Unique
    }

    /// <summary>
    /// API 类型
    /// </summary>
    public enum ApiType
    {
        /// <summary>
        /// 用于获取 AccessToken 凭证
        /// </summary>
        AccessToken,
        /// <summary>
        /// 普通接口
        /// </summary>
        Normal
    }
    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// 本地运行时缓存（单机）
        /// </summary>
        Local,
        /// <summary>
        /// Redis缓存（支持分布式）
        /// </summary>
        Redis,
        /// <summary>
        /// Memcached（支持分布式）
        /// </summary>
        Memcached
    }

    /// <summary>
    /// 依赖注入的生命周期
    /// </summary>
    public enum DILifecycleType
    {
        Scoped,
        Singleton,
        Transient
    }
    /// <summary>
    /// 用户信息中的性别（sex）
    /// </summary>
    public enum Sex
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释   
        未知 = 0,
        男 = 1,
        女 = 2
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
    /// <summary>
    /// CommonJsonSend中的http提交类型
    /// </summary>
    public enum CommonJsonSendType
    {
        /// <summary>
        /// GET 方法
        /// </summary>
        GET,
        /// <summary>
        /// POST 方法
        /// </summary>
        POST
    }

    /// <summary>
    /// 平台类型
    /// </summary>
    public enum PlatformType
    {
        /// <summary>
        /// 通用
        /// </summary>
        General = 0,
        /// <summary>
        /// 微信公众号
        /// </summary>
        WeChat_OfficialAccount = 1,
        /// <summary>
        /// 微信小程序
        /// </summary>
        WeChat_MiniProgram = 2,
        /// <summary>
        /// 微信企业号
        /// </summary>
        WeChat_Work = 4,
        /// <summary>
        /// 微信开放平台
        /// </summary>
        WeChat_Open = 8,

        //空余：16
        //空余：32
        //空余：64

        /// <summary>
        /// QQ公众号
        /// </summary>
        QQ_OfficialAccount = 128,

        //空余：256
        //空余：512
        //空余：1024

        /// <summary>
        /// 钉钉
        /// </summary>
        DingDing = 2048,

        //空余：4096
        //空余：8192
        //空余：16384

        //待定：32768
        //待定：65536
        //待定：131072
        //待定：262144
        //待定：524288
        //待定：1048576

    }

    /// <summary>
    /// 菜单按钮类型
    /// </summary>
    public enum MenuButtonType
    {
        /// <summary>
        /// 点击
        /// </summary>
        click = 101,
        /// <summary>
        /// Url
        /// </summary>
        view = 102,
        /// <summary>
        /// 小程序
        /// </summary>
        miniprogram = 103,
        /// <summary>
        /// 扫码推事件
        /// </summary>
        scancode_push = 104,
        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框
        /// </summary>
        scancode_waitmsg = 105,
        /// <summary>
        /// 弹出系统拍照发图
        /// </summary>
        pic_sysphoto = 106,
        /// <summary>
        /// 弹出拍照或者相册发图
        /// </summary>
        pic_photo_or_album = 107,
        /// <summary>
        /// 弹出微信相册发图器
        /// </summary>
        pic_weixin = 108,
        /// <summary>
        /// 弹出地理位置选择器
        /// </summary>
        location_select = 109,
        /// <summary>
        /// 下发消息（除文本消息）
        /// </summary>
        media_id = 110,
        /// <summary>
        /// 跳转图文消息URL
        /// </summary>
        view_limited = 111
    }

    public enum NeuCharApmKind
    {
        /// <summary>
        /// 消息请求
        /// </summary>
        Message_Request,//TODO: 可进一步统计不同的消息类型
        /// <summary>
        /// 成功返回
        /// </summary>
        Message_SuccessResponse,
        /// <summary>
        /// 响应时间（毫秒）
        /// </summary>
        Message_ResponseMillisecond,
        /// <summary>
        /// 消息处理异常
        /// </summary>
        Message_Exception

    }

    /// <summary>
    /// 语言
    /// </summary>
    public enum Language
    {
        /// <summary>
        /// 中文简体
        /// </summary>
        zh_CN,
        /// <summary>
        /// 中文繁体
        /// </summary>
        zh_TW,
        /// <summary>
        /// 英文
        /// </summary>
        en
    }

    /// <summary>
    /// NeuChar 消息的乐行
    /// </summary>
    public enum NeuCharActionType
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        GetConfig = 0,
        /// <summary>
        /// 储存配置
        /// </summary>
        SaveConfig = 1,
        /// <summary>
        /// 检查NeuChar服务是否可用，同时拉取 APM 统计数据
        /// </summary>
        CheckNeuChar = 2,

        /// <summary>
        /// 推送 NeuChar App 的设置
        /// </summary>
        PushNeuCharAppConfig = 3,
        /// <summary>
        /// 拉取 NeuChar App 的设置
        /// </summary>
        PullNeuCharAppConfig = 4
    }

    /// <summary>
    /// AppStore状态
    /// </summary>
    public enum AppStoreState
    {
        /// <summary>
        /// 无状态
        /// </summary>
        None = 1,
        /// <summary>
        /// 已进入应用状态
        /// </summary>
        Enter = 2,
        /// <summary>
        /// 退出App状态（临时传输状态，退出后即为None）
        /// </summary>
        Exit = 4
    }
}
