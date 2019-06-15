/*----------------------------------------------------------------
    文件名：SenparcMessageQueueThreadUtility.cs
    文件功能描述：SenparcMessageQueue消息队列线程处理

----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OFoodWeChat.Infrastructure.Queue;

namespace OFoodWeChat.Infrastructure.Threads
{
    /// <summary>
    /// SenparcMessageQueue线程自动处理
    /// </summary>
    public class MessageQueueThreadUtility
    {
        private readonly int _sleepMilliSeconds;


        public MessageQueueThreadUtility(int sleepMilliSeconds = 500)
        {
            _sleepMilliSeconds = sleepMilliSeconds;
        }

        /// <summary>
        /// 析构函数，将未处理的队列处理掉
        /// </summary>
        ~MessageQueueThreadUtility()
        {
            try
            {
                var mq = new MessageQueue();
                
                MessageQueue.OperateQueue();//处理队列
            }
            catch (Exception ex)
            {
                //此处可以添加日志
            }
        }

        /// <summary>
        /// 启动线程轮询
        /// </summary>
        public void Run()
        {
            do
            {
                MessageQueue.OperateQueue();
                Thread.Sleep(_sleepMilliSeconds);
            } while (true);
        }
    }
}
