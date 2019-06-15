/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：ThreadUtility.cs
    文件功能描述：线程工具类

----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OFoodWeChat.Infrastructure.Threads
{
    /// <summary>
    /// 线程处理类
    /// </summary>
    public static class ThreadUtility
    {
        /// <summary>
        /// 异步线程容器
        /// </summary>
        public static Dictionary<string, Thread> AsynThreadCollection = new Dictionary<string, Thread>();//后台运行线程

        private static object AsynThreadCollectionLock = new object();

        /// <summary>
        /// 注册线程
        /// </summary>
        public static void Register()
        {
            lock (AsynThreadCollectionLock)
            {
                if (AsynThreadCollection.Count == 0)
                {
                    //队列线程
                    {
                        MessageQueueThreadUtility senparcMessageQueue = new MessageQueueThreadUtility();
                        Thread senparcMessageQueueThread = new Thread(senparcMessageQueue.Run) { Name = "SenparcMessageQueue" };
                        AsynThreadCollection.Add(senparcMessageQueueThread.Name, senparcMessageQueueThread);
                    }

                    AsynThreadCollection.Values.ToList().ForEach(z =>
                    {
                        z.IsBackground = true;
                        z.Start();
                    });//全部运行
                }
            }
        }
    }
}
