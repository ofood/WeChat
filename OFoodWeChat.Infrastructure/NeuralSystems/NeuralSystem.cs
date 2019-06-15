﻿using OFoodWeChat.Infrastructure.Extensions;
using OFoodWeChat.Infrastructure.Helpers;
using OFoodWeChat.Infrastructure.Trace;
using OFoodWeChat.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace OFoodWeChat.Infrastructure.NeuralSystems
{
    /// <summary>
    /// 神经系统，整个系统数据的根节点
    /// </summary>
    public class NeuralSystem
    {
        #region 单例

        //静态SearchCache
        public static NeuralSystem Instance
        {
            get
            {
                return NeuralSystemNested.instance;//返回Nested类中的静态成员instance
            }
        }

        class NeuralSystemNested
        {
            static NeuralSystemNested()
            {
            }
            //将instance设为一个初始化的BaseCacheStrategy新实例
            internal static readonly NeuralSystem instance = new NeuralSystem();
        }

        #endregion

        //TODO：开发流程：实体->JSON/XML->General

        /// <summary>
        /// 默认配置文件内容
        /// </summary>
        internal const string DEFAULT_CONFIG_FILE_CONENT = "{}";
        internal const string CHECK_CNNECTION_RESULT = "OK";

        /// <summary>
        /// NeuChar 域名
        /// </summary>
        public string NeuCharDomainName { get; set; } = "https://www.neuchar.com";

        /// <summary>
        /// 根节点
        /// </summary>
        public INeuralNode Root { get; set; }

        /// <summary>
        /// NeuChar 核心神经系统，包含所有神经节点信息
        /// </summary>
        NeuralSystem()
        {
            //获取所有配置并初始化

            //TODO:当配置文件多了之后可以遍历所有的配置文件

            //TODO:解密

            ReloadNode();
        }

        /// <summary>
        /// 初始化 Root 参数
        /// </summary>
        private void InitRoot()
        {

            INeuralNode root = new RootNeuralNode();
            Root = root;
        }

        /// <summary>
        /// 加载节点信息
        /// </summary>
        public void ReloadNode()
        {
            InitRoot();//独立放在外面强制执行

            var path = ServerUtility.ContentRootMapPath("~/App_Data/NeuChar");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var file = Path.Combine(path, "NeuCharRoot.config");
            //SenparcTrace.SendCustomLog("NeuChar file path", file);

            if (File.Exists(file))
            {
                using (var fs = new FileStream(file, FileMode.Open))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        var configRootJson = sr.ReadToEnd();
                        //TODO:可以进行格式和版本校验

                        //SenparcTrace.SendCustomLog("NeuChar Saved ConfigRoot", configRootJson);

                        var configRoot = SerializerHelper.GetObject<ConfigRoot>(configRootJson);
                        //SenparcTrace.SendCustomLog("NeuChar NeuralSystem", configRoot.ToJson());
                        if (configRoot != null && configRoot.Configs != null)
                        {
                            object[] connfigs = configRoot.Configs;//SerializerHelper.GetObject<List<BaseNeuralNode>>(configRoot.Configs);
                                                                   //SenparcTrace.SendCustomLog("NeuChar configs", connfigs.ToJson());

                            //转换成 List<BaseNeuralNode> 对象
                            var configsJsonString = connfigs.ToJson();
                            var configsList = SerializerHelper.GetObject<List<BaseNeuralNode>>(configsJsonString);

                            for (int i = 0; i < connfigs.Length; i++)
                            {
                                if (Register.NeuralNodeRegisterCollection.ContainsKey(configsList[i].Name))
                                {
                                    var configNodeType = Register.NeuralNodeRegisterCollection[configsList[i].Name];
                                    //SenparcTrace.SendCustomLog("NeuChar config type", configNodeType.FullName);

                                    var configNodeJsonStr = connfigs[i].ToJson();//转成字符串，方便再次反序列化到具体类型中

                                    var finalNeuralNode = Newtonsoft.Json.JsonConvert.DeserializeObject(configNodeJsonStr, configNodeType) as INeuralNode;

                                    //SenparcTrace.SendCustomLog("NeuChar finalNeuralNode", finalNeuralNode.ToJson());

                                    Root.SetChildNode(finalNeuralNode);

                                    //SerializerHelper.GetObject()
                                }
                            }
                        }

                        //foreach (var config in connfigs)
                        //{
                        //    SenparcTrace.SendCustomLog("NeuChar config", config.ToJson());
                        //    if (Senparc.NeuChar.Register.NeuralNodeRegisterCollection.ContainsKey(config.Name))
                        //    {
                        //        var configNodeType = Senparc.NeuChar.Register.NeuralNodeRegisterCollection[config.Name];
                        //        SenparcTrace.SendCustomLog("NeuChar config type", configNodeType.FullName);

                        //    }

                        //}

                    }
                }
            }
        }

        /// <summary>
        /// 获取指定Name的节点
        /// <para>TODO：建立索引搜索</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentNode">父节点</param>
        /// <returns></returns>
        public INeuralNode GetNode(string name, INeuralNode parentNode = null)
        {
            if (parentNode == null)
            {
                parentNode = Root;
            }

            INeuralNode foundNode = null;

            if (parentNode.Name == name)
            {
                foundNode = parentNode;
            }

            if (foundNode == null && parentNode.ChildrenNodes != null && parentNode.ChildrenNodes.Count > 0)
            {
                foreach (var node in parentNode.ChildrenNodes)
                {

                    foundNode = GetNode(name, node);//监测当前节点
                    if (foundNode != null)
                    {
                        break;
                    }
                }

            }

            return foundNode;
        }
    }
}
