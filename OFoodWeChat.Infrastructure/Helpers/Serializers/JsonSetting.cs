/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：JsonSetting.cs
    文件功能描述：JSON字符串定义
    
    
    创建标识：Senparc - 20150930
    
    修改标识：Senparc - 20160722
    修改描述：增加特性，对json格式的输出内容的控制，对枚举类型字符串输出、默认值不输出、例外属性等，如会员卡卡里面的CodeType
             IDictionary中foreach中的内容的修改

    修改标识：Senparc - 20160722
    修改描述：v4.11.5 修复WeixinJsonConventer.Serialize中的错误。感谢 @jiehanlin
    
    修改标识：Senparc - 20180526
    修改描述：v4.22.0-rc1 将 JsonSetting 继承 JsonSerializerSettings，使用 Newtonsoft.Json 进行序列化
    

    ----  CO2NET   ----
    ----  split from Senparc.Weixin/Helpers/Conventers/WeixinJsonConventer.cs.cs  ----

    修改标识：Senparc - 20180602
    修改描述：v0.1.0 1、移植 JsonSetting
                     2、重命名 WeixinJsonContractResolver 为 JsonContractResolver
                     3、重命名 WeiXinJsonSetting 为 JsonSettingWrap

    修改标识：Senparc - 20180721
    修改描述：v0.2.1 优化序列化特性识别

----------------------------------------------------------------*/

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace OFoodWeChat.Infrastructure.Helpers.Serializers
{
    /// <summary>
    /// JSON输出设置
    /// </summary>
    public class JsonSetting : JsonSerializerSettings
    {
        /// <summary>
        /// 是否忽略当前类型以及具有IJsonIgnoreNull接口，且为Null值的属性。如果为true，符合此条件的属性将不会出现在Json字符串中
        /// </summary>
        public bool IgnoreNulls { get; set; }
        /// <summary>
        /// 需要特殊忽略null值的属性名称
        /// </summary>
        public List<string> PropertiesToIgnoreNull { get; set; }
        /// <summary>
        /// 指定类型（Class，非Interface）下的为null属性不生成到Json中
        /// </summary>
        public List<Type> TypesToIgnoreNull { get; set; }

        #region Add


        public class IgnoreValueAttribute : System.ComponentModel.DefaultValueAttribute
        {
            public IgnoreValueAttribute(object value) : base(value)
            {
                //Value = value;
            }
        }
        public class IgnoreNullAttribute : Attribute
        {

        }
        /// <summary>
        /// 例外属性，即不排除的属性值
        /// </summary>
        public class ExcludedAttribute : Attribute
        {

        }

        /// <summary>
        /// 枚举类型显示字符串
        /// </summary>
        public class EnumStringAttribute : Attribute
        {

        }

        #endregion
        /// <summary>
        /// JSON 输出设置 构造函数
        /// </summary>
        /// <param name="ignoreNulls">是否忽略当前类型以及具有IJsonIgnoreNull接口，且为Null值的属性。如果为true，符合此条件的属性将不会出现在Json字符串中</param>
        /// <param name="propertiesToIgnoreNull">需要特殊忽略null值的属性名称</param>
        /// <param name="typesToIgnoreNull">指定类型（Class，非Interface）下的为null属性不生成到Json中</param>
        public JsonSetting(bool ignoreNulls = false, List<string> propertiesToIgnoreNull = null, List<Type> typesToIgnoreNull = null)
        {
            IgnoreNulls = ignoreNulls;
            PropertiesToIgnoreNull = propertiesToIgnoreNull ?? new List<string>();
            TypesToIgnoreNull = typesToIgnoreNull ?? new List<Type>();
        }
    }
}
