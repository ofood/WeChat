
/*----------------------------------------------------------------
    文件名：IJsonResult.cs
    文件功能描述：所有JSON返回结果基类

----------------------------------------------------------------*/

namespace OFoodWeChat.Infrastructure.Data.JsonResult
{
    /// <summary>
    /// 所有 JSON 格式返回值的API返回结果接口
    /// </summary>
    public interface IJsonResult// : IJsonResultCallback
    {
        /// <summary>
        /// 返回结果信息
        /// </summary>
        string errmsg { get; set; }

        /// <summary>
        /// errcode的
        /// </summary>
        int ErrorCodeValue { get; }
        object P2PData { get; set; }
    }
}
