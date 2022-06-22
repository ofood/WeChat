﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：DeviceListResultJson.cs
    文件功能描述：批量查询设备统计数据的返回结果
    
    修改标识：Senparc - 20160520
    修改描述：修改批量查询设备统计数据的返回结果
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.MP.AdvancedAPIs.WiFi;
using OFoodWeChat.MP.Entities;

namespace OFoodWeChat.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 批量查询设备统计数据的返回结果
    /// </summary>
   public  class DeviceListResultJson : WxJsonResult
    {
       public DeviceList_Data data { get; set; }
    }
   public class DeviceList_Data
   {
       /// <summary>
       /// 
       /// </summary>
       public List<DevicesList_DevicesItem> devices { get; set; }
       /// <summary>
       /// 所查询的日期时间戳
       /// </summary>
       public long date { get; set; }
       /// <summary>
       /// 设备总数
       /// </summary>
       public string total_count { get; set; }
       /// <summary>
       /// 所查询的结果页序号；返回结果按摇周边人数降序排序，每50条记录为一页   
       /// </summary>
       public string page_index { get; set; }
   }
   public class DevicesList_DevicesItem
   {
       /// <summary>
       /// 设备编号
       /// </summary>
       public string device_id { get; set; }
       /// <summary>
       /// major
       /// </summary>
       public string major { get; set; }
       /// <summary>
       /// minor
       /// </summary>
       public string minor { get; set; }
       /// <summary>
       /// uuid
       /// </summary>
       public string uuid { get; set; }
       /// <summary>
       /// 摇周边的次数
       /// </summary>
       public int shake_pv { get; set; }
       /// <summary>
       /// 摇周边的人数
       /// </summary>
       public int shake_uv { get; set; }
       /// <summary>
       /// 点击摇周边消息的次数
       /// </summary>
       public int click_pv { get; set; }
       /// <summary>
       /// 点击摇周边消息的人数
       /// </summary>
       public int click_uv { get; set; }
   }
}