﻿/*----------------------------------------------------------------
    
    文件名：FileHelper.cs
    文件功能描述：处理文件
----------------------------------------------------------------*/
using System.IO;
using OFoodWeChat.Infrastructure.Http;
namespace OFoodWeChat.Infrastructure.Helpers
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 根据完整文件路径获取FileStream
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FileStream GetFileStream(string fileName)
        {
            FileStream fileStream = null;
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                fileStream = new FileStream(fileName, FileMode.Open,FileAccess.Read, FileShare.ReadWrite);
            }
            return fileStream;
        }

        /// <summary>
        /// 从Url下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fullFilePathAndName"></param>
        public static void DownLoadFileFromUrl(string url, string fullFilePathAndName)
        {
            using (FileStream fs = new FileStream(fullFilePathAndName, FileMode.OpenOrCreate))
            {
                Get.Download(url, fs);
                fs.Flush(true);
            }
        }
    }
}
