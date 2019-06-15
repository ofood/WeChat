/*----------------------------------------------------------------
    文件名：Register.cs
    文件功能描述：快捷注册流程（包括Thread、TraceLog等）
----------------------------------------------------------------*/

using System;
using OFoodWeChat.Infrastructure.Threads;
using OFoodWeChat.Infrastructure.RegisterServices;
using OFoodWeChat.Infrastructure.Cache;
using System.Collections.Generic;
using System.Linq;
using OFoodWeChat.Infrastructure.NeuralSystems;
using OFoodWeChat.Infrastructure.Trace;
using OFoodWeChat.Infrastructure.ApiBind;
using System.Reflection;

namespace OFoodWeChat.Infrastructure
{
    /// <summary>
    /// 基础信息注册
    /// </summary>
    public static partial class Register
    {
        /// <summary>
        /// 修改默认缓存命名空间
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="customNamespace">自定义命名空间名称</param>
        /// <returns></returns>
        public static IRegisterService ChangeDefaultCacheNamespace(this IRegisterService registerService, string customNamespace)
        {
            OFoodConfig.DefaultCacheNamespace = customNamespace;
            return registerService;
        }


        /// <summary>
        /// 注册 Threads 的方法（如果不注册此线程，则AccessToken、JsTicket等都无法使用SDK自动储存和管理）
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <returns></returns>
        public static IRegisterService RegisterThreads(this IRegisterService registerService)
        {
            ThreadUtility.Register();//如果不注册此线程，则AccessToken、JsTicket等都无法使用SDK自动储存和管理。
            return registerService;
        }

        /// <summary>
        /// 注册 TraceLog 的方法
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IRegisterService RegisterTraceLog(this IRegisterService registerService, Action action)
        {
            action();
            return registerService;
        }

        /// <summary>
        /// 初始化参数流程
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="autoScanExtensionCacheStrategies">是否自动扫描全局的扩展缓存（会增加系统启动时间）</param>
        /// <param name="extensionCacheStrategiesFunc"><para>需要手动注册的扩展缓存策略</para>
        /// <para>（LocalContainerCacheStrategy、RedisContainerCacheStrategy、MemcacheContainerCacheStrategy已经自动注册），</para>
        /// <para>如果设置为 null（注意：不适委托返回 null，是整个委托参数为 null），则自动使用反射扫描所有可能存在的扩展缓存策略</para></param>
        /// <returns></returns>
        public static IRegisterService UseSenparcGlobal(this IRegisterService registerService, bool autoScanExtensionCacheStrategies = false, Func<IList<IDomainExtensionCacheStrategy>> extensionCacheStrategiesFunc = null)
        {
            //注册扩展缓存策略
            CacheStrategyDomainWarehouse.AutoScanDomainCacheStrategy(autoScanExtensionCacheStrategies, extensionCacheStrategiesFunc);

            return registerService;
        }
    }

    public static partial class Register
    {
        /// <summary>
        /// 是否API绑定已经执行完
        /// </summary>
        public static bool RegisterApiBindFinished { get; private set; } = false;

        /// <summary>
        /// 节点类型注册集合
        /// </summary>
        public static Dictionary<string, Type> NeuralNodeRegisterCollection = new Dictionary<string, Type>();
        //TODO: public static Dictionary<string, Type> NeuralNodeRegisterCollection { get; set; } = new Dictionary<string, Type>();


        static Register()
        {
            //RegisterApiBind(false);//注意：此处注册可能并不能获取到足够数量的程序集，需要测试并确定是否使用 RegisterApiBind(true) 方法

            //注册节点类型
            RegisterNeuralNode("MessageHandlerNode", typeof(MessageHandlerNode));
            RegisterNeuralNode("AppDataNode", typeof(AppDataNode));
        }

        /// <summary>
        /// 注册节点
        /// </summary>
        /// <param name="name">唯一名称</param>
        /// <param name="type">节点类型</param>
        public static void RegisterNeuralNode(string name, Type type)
        {
            NeuralNodeRegisterCollection[name] = type;
        }

        /// <summary>
        /// RegisterApiBind 执行锁
        /// </summary>
        private static object RegisterApiBindLck = new object();

        /// <summary>
        /// 自动扫描并注册 ApiBind
        /// </summary>
        /// <param name="forceBindAgain">是否强制重刷新</param>
        public static void RegisterApiBind(bool forceBindAgain)
        {
            var dt1 = SystemTime.Now;

            //var cacheStragegy = CacheStrategyFactory.GetObjectCacheStrategyInstance();
            //using (cacheStragegy.BeginCacheLock("Senparc.NeuChar.Register", "RegisterApiBind"))
            lock (RegisterApiBindLck)//由于使用的是本地内存进行记录，所以这里不需要使用同步锁，这样就不需要依“缓存注册”等先决条件
            {
                if (RegisterApiBindFinished == true && forceBindAgain == false)
                {
                    Console.WriteLine($"RegisterApiBind has been finished, and doesn't require [forceBindAgain]. Quit build.");

                    return;
                }

                //查找所有扩展缓存
                var scanTypesCount = 0;

                var assembiles = AppDomain.CurrentDomain.GetAssemblies();

                var errorCount = 0;

                foreach (var assembly in assembiles)
                {
                    try
                    {
                        scanTypesCount++;
                        var classTypes = assembly.GetTypes()
                                    .Where(z => z.Name.EndsWith("api", StringComparison.OrdinalIgnoreCase) ||
                                                z.Name.EndsWith("apis", StringComparison.OrdinalIgnoreCase))
                                    .ToArray();

                        foreach (var type in classTypes)
                        {
                            if (/*type.IsAbstract || 静态类会被识别为 IsAbstract*/
                                !type.IsPublic || !type.IsClass || type.IsEnum)
                            {
                                continue;
                            }

                            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod);
                            foreach (var method in methods)
                            {
                                var attrs = method.GetCustomAttributes(typeof(ApiBindAttribute), false);
                                foreach (var attr in attrs)
                                {
                                    ApiBindInfoCollection.Instance.Add(method, attr as ApiBindAttribute);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        LogTrace.SendCustomLog("RegisterApiBind() 自动扫描程序集报告（非程序异常）：" + assembly.FullName, ex.ToString());
                    }
                }

                RegisterApiBindFinished = true;

                var dt2 = SystemTime.Now;
                Console.WriteLine($"RegisterApiBind Time: {(dt2 - dt1).TotalMilliseconds}ms, Api Count:{ApiBindInfoCollection.Instance.Count()}, Error Count: {errorCount}");
            }
        }
    }
}
