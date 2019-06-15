/*----------------------------------------------------------------
    文件名：RegisterService.cs
    文件功能描述： SDK 快捷注册流程
----------------------------------------------------------------*/
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace OFoodWeChat.Infrastructure.RegisterServices
{
    /// <summary>
    /// 快捷注册接口
    /// </summary>
    public interface IRegisterService
    {

    }

    /// <summary>
    /// 快捷注册类，IRegisterService的默认实现
    /// </summary>
    public class RegisterService : IRegisterService
    {
        //private RegisterService() : this(null) { }

        private RegisterService(OFoodSetting senparcSetting)
        {
            OFoodConfig.Setting = senparcSetting ?? new OFoodSetting();
        }


        /// <summary>
        /// 单个实例引用全局的 ServiceCollection
        /// </summary>
        public IServiceCollection ServiceCollection => OFoodDI.GlobalServiceCollection;

        /// <summary>
        /// 开始 SDK 初始化参数流程（.NET Core）
        /// </summary>
        /// <param name="env">IHostingEnvironment，控制台程序可以输入null，</param>
        /// <param name="senparcSetting"></param>
        /// <returns></returns>
        public static RegisterService Start(IHostingEnvironment env, OFoodSetting senparcSetting)
        {

            //提供网站根目录
            if (env != null && env.ContentRootPath != null)
            {
                OFoodConfig.RootDictionaryPath = env.ContentRootPath;
            }
            else
            {
                OFoodConfig.RootDictionaryPath = AppDomain.CurrentDomain.BaseDirectory;
            }

            var register = new RegisterService(senparcSetting);

            //如果不注册此线程，则AccessToken、JsTicket等都无法使用SDK自动储存和管理。
            register.RegisterThreads();//默认把线程注册好

            return register;
        }

        /// <summary>
        /// 开始 SDK 初始化参数流程（.NET Core）
        /// </summary>
        /// <param name="env">IHostingEnvironment，控制台程序可以输入null，</param>
        /// <param name="senparcSetting"></param>
        /// <returns></returns>
        public static RegisterService Start(OFoodSetting senparcSetting)
        {
            return Start(null, senparcSetting);
        }

    }
}
