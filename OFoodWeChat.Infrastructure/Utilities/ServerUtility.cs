/*----------------------------------------------------------------
   
    文件名：ServerUtility.cs
    文件功能描述：服务器工具类
----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace OFoodWeChat.Infrastructure.Utilities
{
    /// <summary>
    /// 服务器工具类
    /// </summary>
    public class ServerUtility
    {
        private static string _appDomainAppPath;

        /// <summary>
        /// dll 项目根目录
        /// </summary>
        public static string AppDomainAppPath
        {
            get
            {
                if (_appDomainAppPath == null)
                {
                    _appDomainAppPath = AppContext.BaseDirectory; //dll所在目录：;
                }
                return _appDomainAppPath;
            }
            set
            {
                _appDomainAppPath = value;
                var pathSeparator = Path.DirectorySeparatorChar.ToString();
                var altPathSeparator = Path.AltDirectorySeparatorChar.ToString();
                if (!_appDomainAppPath.EndsWith(pathSeparator) && !_appDomainAppPath.EndsWith(altPathSeparator))
                {
                    _appDomainAppPath += pathSeparator;
                }
            }
        }

        /// <summary>
        /// 获取相对于网站根目录的文件路径
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public static string ContentRootMapPath(string virtualPath)
        {
            if (virtualPath == null)
            {
                return "";
            }
            else
            {
                //if (!Config.RootDictionaryPath.EndsWith("/") || Config.RootDictionaryPath.EndsWith("\\"))
                var pathSeparator = Path.DirectorySeparatorChar.ToString();
                var altPathSeparator = Path.AltDirectorySeparatorChar.ToString();
                if (!OFoodConfig.RootDictionaryPath.EndsWith(pathSeparator) && !OFoodConfig.RootDictionaryPath.EndsWith(altPathSeparator))
                {
                    OFoodConfig.RootDictionaryPath += pathSeparator;
                }

                if (virtualPath.StartsWith("~/"))
                {
                    return virtualPath.Replace("~/", OFoodConfig.RootDictionaryPath).Replace("/", pathSeparator);
                }
                else
                {
                    return Path.Combine(OFoodConfig.RootDictionaryPath, virtualPath);
                }
            }
        }

        /// <summary>
        /// 获取相对于dll目录的文件绝对路径
        /// </summary>
        /// <param name="virtualPath">虚拟路径，如~/App_Data/</param>
        /// <returns></returns>
        public static string DllMapPath(string virtualPath)
        {
            if (virtualPath == null)
            {
                return "";
            }
            else if (virtualPath.StartsWith("~/"))
            {
                var pathSeparator = Path.DirectorySeparatorChar.ToString();
                return virtualPath.Replace("~/", AppDomainAppPath).Replace("/", pathSeparator);
            }
            else
            {
                return Path.Combine(AppDomainAppPath, virtualPath);
            }
        }

    }
}
