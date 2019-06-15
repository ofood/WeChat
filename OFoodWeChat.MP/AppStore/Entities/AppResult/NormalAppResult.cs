/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
  
    文件名：NormalAppResult.cs
    文件功能描述：普通API返回类型
----------------------------------------------------------------*/

namespace OFoodWeChat.MP.AppStore
{
    /// <summary>
    /// 普通API返回类型
    /// </summary>
    public class NormalAppResult : AppResult<NormalAppData>
    {

    }

    public class NormalAppData : IAppResultData
    {
        public object Object { get; set; }
    }
}
