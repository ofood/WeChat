﻿/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：MessageHandler.NeuChar.cs
    文件功能描述：微信请求中有关 NeuChar 方法的集中处理方法
 
----------------------------------------------------------------*/

using OFoodWeChat.Infrastructure.Data;
using OFoodWeChat.Infrastructure.Entities;
using OFoodWeChat.Infrastructure.Entities.App;
using OFoodWeChat.Infrastructure.Enums;
using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Infrastructure.Helpers;
using OFoodWeChat.Infrastructure.NeuralSystems;
using OFoodWeChat.Infrastructure.Trace;
using OFoodWeChat.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoodWeChat.Infrastructure.MessageHandlers
{
    public abstract partial class MessageHandler<TC, TRequest, TResponse>
    {
        static MessageHandler()
        {
            //注册节点类型
            Register.RegisterNeuralNode("MessageHandlerNode", typeof(MessageHandlerNode));
            Register.RegisterNeuralNode("AppDataNode", typeof(AppDataNode));
        }

        #region NeuChar 方法

        /// <summary>
        /// NeuChar 请求
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnNeuCharRequestAsync(RequestMessageNeuChar requestMessage)
        {
            try
            {
                var path = ServerUtility.ContentRootMapPath("~/App_Data/NeuChar");
                //SenparcTrace.SendCustomLog("OnNeuCharRequest path", path);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var file = Path.Combine(path, "NeuCharRoot.config");
                bool success = true;
                string result = null;

                var configFileExisted = File.Exists(file);
                if (!configFileExisted)
                {
                    using (var fs = new FileStream(file, FileMode.CreateNew))
                    {
                        using (var sw = new StreamWriter(fs))
                        {
                            await sw.WriteLineAsync(NeuralSystem.DEFAULT_CONFIG_FILE_CONENT).ConfigureAwait(false);
                        }
                        await fs.FlushAsync().ConfigureAwait(false);
                    }
                }

                switch (requestMessage.NeuCharMessageType)
                {
                    case NeuCharActionType.GetConfig:
                        {
                            if (configFileExisted)
                            {
                                //文件刚创建，但不再读取，此时读取可能会发生“无法访问已关闭文件”的错误
                                using (var fs = FileHelper.GetFileStream(file))
                                {
                                    using (var sr = new StreamReader(fs, Encoding.UTF8))
                                    {
                                        var json = await sr.ReadToEndAsync().ConfigureAwait(false);
                                        result = json;
                                    }
                                }
                            }
                            else
                            {
                                result = NeuralSystem.DEFAULT_CONFIG_FILE_CONENT;//TODO:初始化一个对象
                            }
                        }
                        break;
                    case NeuCharActionType.SaveConfig:
                        {
                            var configRootJson = requestMessage.ConfigRoot;
                            LogTrace.SendCustomLog("收到NeuCharRequest", "字符串长度：" + configRootJson.Length.ToString());
                            var configRoot = SerializerHelper.GetObject<ConfigRoot>(configRootJson);//这里只做序列化校验

                            //TODO:进行验证


                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            var fileTemp = Path.Combine(path, $"NeuCharRoot.temp.{SystemTime.Now.ToString("yyyyMMdd-HHmmss")}.config");
                            //TODO：后期也可以考虑把不同模块分离到不同的文件中

                            using (var fs = new FileStream(fileTemp, FileMode.Create))
                            {
                                using (var sw = new StreamWriter(fs))
                                {
                                    await sw.WriteAsync(configRootJson).ConfigureAwait(false);
                                    await sw.FlushAsync().ConfigureAwait(false);
                                }
                            }

                            //历史文件备份，并替换临时文件
                            File.Move(file, file.Replace(".config", $".bak.{SystemTime.Now.ToString("yyyyMMdd-HHmmss")}.config"));
                            File.Move(fileTemp, file);

                            //刷新数据
                            var neuralSystem = NeuralSystem.Instance;
                            neuralSystem.ReloadNode();
                        }
                        break;
                    case NeuCharActionType.CheckNeuChar:
                        {
                            //TODO：进行有效性检验
                            var configRoot = requestMessage.ConfigRoot?.GetObject<APMDomainConfig>();

                            if (configRoot == null || configRoot.Domain.IsNullOrWhiteSpace())
                            {
                                success = false;
                                result = "未指定 Domain!";
                                break;
                            }

                            var co2netDataOperation = new DataOperation(configRoot.Domain);

                            //获取所有数据
                            var dataItems = await co2netDataOperation.ReadAndCleanDataItemsAsync(configRoot.RemoveData, true).ConfigureAwait(false);
                            result = dataItems.ToJson();
                        }
                        break;
                    case NeuCharActionType.PushNeuCharAppConfig://推送 NeuChar App 配置
                        {
                            var configFileDir = Path.Combine(path, "AppConfig");
                            if (!Directory.Exists(configFileDir))
                            {
                                Directory.CreateDirectory(configFileDir);//这里也可以不创建，除非是为了推送
                            }

                            //还原一次，为了统一格式，并未后续处理提供能力（例如调整缩进格式）
                            var requestData = requestMessage.RequestData.GetObject<PushConfigRequestData>();
                            var mainVersion = requestData.Version.Split('.')[0];//主版本号
                            //配置文件路径：~/App_Data/NeuChar/AppConfig/123-v1.config
                            var configFilePath = Path.Combine(configFileDir, $"{requestData.AppId}-v{mainVersion}.config");

                            using (var fs = new FileStream(configFilePath, FileMode.Create))
                            {
                                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                                {
                                    var json = requestData.Config.ToJson(true);//带缩进格式的 JSON 字符串
                                    await sw.WriteAsync(json).ConfigureAwait(false);//写入 Json 文件
                                    await sw.FlushAsync().ConfigureAwait(false);
                                }
                            }
                            result = "OK";
                        }
                        break;
                    case NeuCharActionType.PullNeuCharAppConfig://拉取 NeuCharApp 配置
                        {
                            var requestData = requestMessage.RequestData.GetObject<PullConfigRequestData>();
                            var mainVersion = requestData.Version.Split('.')[0];//主版本号

                            var configFileDir = Path.Combine(path, "AppConfig");
                            //配置文件路径：~/App_Data/NeuChar/AppConfig/123-v1.config
                            var configFilePath = Path.Combine(configFileDir, $"{requestData.AppId}-v{mainVersion}.config");
                            if (!File.Exists(configFilePath))
                            {
                                //文件不存在
                                result = $"配置文件不存在，请先推送或设置配置文件，地址：{configFilePath}";
                                success = false;
                            }
                            else
                            {
                                //读取内容
                                using (var fs = FileHelper.GetFileStream(configFilePath))
                                {
                                    using (var sr = new StreamReader(fs, Encoding.UTF8))
                                    {
                                        var json = await sr.ReadToEndAsync().ConfigureAwait(false);//带缩进格式的 JSON 字符串（文件中的原样）
                                        result = json;
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }

                var successMsg = new
                {
                    success = success,
                    result = result
                };
                TextResponseMessage = successMsg.ToJson();
            }
            catch (Exception ex)
            {
                var errMsg = new
                {
                    success = false,
                    result = ex.Message
                };
                TextResponseMessage = errMsg.ToJson();
            }

            return null;
        }

        #endregion

    }
}
