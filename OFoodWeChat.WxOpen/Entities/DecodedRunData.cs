/*----------------------------------------------------------------
   
    文件名：DecodedRunData
    文件功能描述：小程序运动步数解密类
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OFoodWeChat.WxOpen.Entities
{
    //  "stepInfoList": [
    //  {
    //    "timestamp": 1445866601,
    //    "step": 100
    //  },
    //  {
    //    "timestamp": 1445876601,
    //    "step": 120
    //  }
    //]
    [Serializable]
    public class DecodedRunData : DecodeEntityBase
    {
        public List<DecodedRunData_StepModel> stepInfoList { get; set; }
    }

    [Serializable]
    public class DecodedRunData_StepModel
    {
        public long timestamp { get; set; }
        public long step { get; set; }
    }
}