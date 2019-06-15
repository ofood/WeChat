
/*----------------------------------------------------------------
    文件名：SenparcDI.cs
    文件功能描述：针对 .NET Core 的依赖注入扩展类
----------------------------------------------------------------*/

using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using OFoodWeChat.Infrastructure.Cache;

namespace OFoodWeChat.Infrastructure
{
    /// <summary>
    /// 针对 .NET Core 的依赖注入扩展类
    /// </summary>
    public static class OFoodDI
    {
        private static ServiceProvider _globalServiceProvider;

        public const string SENPARC_DI_THREAD_SERVICE_Scope = "___SenparcDIThreadScope";

        /// <summary>
        /// 全局 ServiceCollection
        /// </summary>
        public static IServiceCollection GlobalServiceCollection { get; set; }

        private static object _globalIServiceProviderLock = new object();
        private static object _threadIServiceProviderLock = new object();

        /// <summary>
        /// 全局 IServiceCollection 对象
        /// </summary>
        public static IServiceProvider GlobalIServiceProvider { get; private set; }

        /// <summary>
        /// 线程内的 单一 Scope 范围 ServiceScope
        /// </summary>
        public static IServiceScope ThreadServiceScope
        {
            get
            {
                var threadServiceScope = Thread.GetData(Thread.GetNamedDataSlot(SENPARC_DI_THREAD_SERVICE_Scope)) as IServiceScope;
                return threadServiceScope;
            }
        }


        /// <summary>
        /// 获取 ServiceProvider
        /// </summary>
        /// <param name="useGlobalScope">是否使用全局唯一 ServiceScope 对象。
        /// <para>默认为 true，即使用全局唯一 ServiceScope。</para>
        /// <para>如果为 false，即使用线程内唯一 ServiceScope 对象</para>
        /// </param>
        /// <returns></returns>
        public static IServiceProvider GetIServiceProvider(bool useGlobalScope = true)
        {
            if (useGlobalScope)
            {
                if (GlobalIServiceProvider == null)
                {
                    //加锁确保唯一
                    lock (_globalIServiceProviderLock)
                    {
                        if (GlobalIServiceProvider == null)
                        {
                            //注意：BuildServiceProvider() 方法每次会生成不同的 ServiceProvider 对象！
                            GlobalIServiceProvider = GlobalServiceCollection.BuildServiceProvider();
                        }
                    }
                }
                return GlobalIServiceProvider;
            }
            else
            {
                if (ThreadServiceScope == null)
                {
                    //加锁确保唯一
                    lock (_threadIServiceProviderLock)
                    {
                        if (ThreadServiceScope == null)
                        {
                            //注意：BuildServiceProvider() 方法每次会生成不同的 ServiceProvider 对象！
                            //GlobalIServiceProvider = GetServiceCollection().BuildServiceProvider();

                            var globalServiceProvider = GetIServiceProvider(true);

                            Thread.SetData(Thread.GetNamedDataSlot(SENPARC_DI_THREAD_SERVICE_Scope), globalServiceProvider.CreateScope());
                        }
                    }
                }
                return ThreadServiceScope.ServiceProvider;
            }
        }


        /// <summary>
        /// 使用 .net core 默认的 DI 方法获得实例
        /// <para>如果未注册，返回 null</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <param name="useGlobalScope">是否使用全局唯一 ServiceScope 对象，默认为 false，即使用线程内唯一 ServiceScope 对象</param>
        public static T GetService<T>(bool useGlobalScope = true)
        {
            return GetIServiceProvider(useGlobalScope).GetService<T>();
        }

        /// <summary>
        /// 使用 .net core 默认的 DI 方法获得实例（推荐）
        /// <para>如果未注册，抛出异常 </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <param name="useGlobalScope">是否使用全局唯一 ServiceScope 对象，默认为 false，即使用线程内唯一 ServiceScope 对象</param>
        public static T GetRequiredService<T>(bool useGlobalScope = true)
        {
            return GetIServiceProvider(useGlobalScope).GetRequiredService<T>();
        }

        /// <summary>
        /// 重置 GlobalIServiceProvider 对象，重新从 GlobalServiceCollection..BuildServiceProvider() 生成对象
        /// </summary>
        public static void ResetGlobalIServiceProvider()
        {
            GlobalIServiceProvider = null;
        }

        /// <summary>
        /// 重置 GlobalIServiceProvider 对象，重新从 GlobalServiceCollection..BuildServiceProvider() 生成对象
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection ResetGlobalIServiceProvider(this IServiceCollection serviceCollection)
        {
            ResetGlobalIServiceProvider();
            return serviceCollection;
        }
    }
}

