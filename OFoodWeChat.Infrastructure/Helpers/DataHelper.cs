/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：DataHelper.cs
    文件功能描述：数据帮助类

 ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace OFoodWeChat.Infrastructure.Helpers
{
    /// <summary>
    /// 数据帮助类
    /// </summary>
    public class DataHelper
    {

        /// <summary>
        /// 确保已经到达下一分钟
        /// </summary>
        /// <param name="lastTime"></param>
        /// <param name="currentDateTime"></param>
        /// <returns></returns>
        public static bool IsLaterMinute(DateTimeOffset lastTime, DateTimeOffset currentDateTime)
        {
            return lastTime.Year < currentDateTime.Year ||
                   lastTime.Month < currentDateTime.Month ||
                   lastTime.Day < currentDateTime.Day ||
                   lastTime.Hour < currentDateTime.Hour ||
                   lastTime.Minute < currentDateTime.Minute;
        }

        /// <summary>
        /// 获取CPU信息
        /// </summary>
        /// <returns></returns>
        public static object GetCPUCounter()
        {
            Process[] p = Process.GetProcesses();//获取进程信息
            Int64 totalMem = 0;
            string info = "";
            foreach (Process pr in p)
            {
                totalMem += pr.WorkingSet64 / 1024;
                info += pr.ProcessName + "内存：-----------" + (pr.WorkingSet64 / 1024).ToString() + "KB\r\n";//得到进程内存
            }
            return info;

        }

        /// <summary>
        /// 获取 系统名称
        /// </summary>
        /// <returns></returns>
        public static string GetOSPlatform()
        {
            //return Environment.OSVersion.Platform.ToString();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OSPlatform.Windows.ToString();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OSPlatform.Linux.ToString();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OSPlatform.OSX.ToString();
            }
            else
            {
                return "Unknown";
            }
        }
    }
}
