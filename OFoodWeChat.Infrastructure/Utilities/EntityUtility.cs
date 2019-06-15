/*----------------------------------------------------------------
    文件名：EntityUtility.cs
    文件功能描述：实体工具类
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using OFoodWeChat.Infrastructure.Helpers;

namespace OFoodWeChat.Infrastructure.Utilities
{
    /// <summary>
    /// 实体工具类
    /// </summary>
    public static class EntityUtility
    {
        /// <summary>
        /// 将对象转换到指定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="convertibleValue"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(this IConvertible convertibleValue)
        {
            if (null == convertibleValue)
            {
                return default(T);
            }
            
            var t = typeof(T);
            if (t.GetTypeInfo().IsGenericType)
            {
                if (t.GetGenericTypeDefinition() != typeof(Nullable<>))
                {
                    throw new InvalidCastException(string.Format("Invalid cast from type \"{0}\" to type \"{1}\".", convertibleValue.GetType().FullName, typeof(T).FullName));
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return (T)Convert.ChangeType(convertibleValue, t);
        }


        /// <summary>
        /// 向属性填充值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        public static void FillSystemType<T>(T entity, PropertyInfo prop, IConvertible value)
        {
            FillSystemType(entity, prop, value, prop.PropertyType);
        }

        /// <summary>
        /// 向属性填充值（强制使用指定的类型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        /// <param name="specialType"></param>
        public static void FillSystemType<T>(T entity, PropertyInfo prop, IConvertible value, Type specialType)
        {
            object setValue = null;
            if (value.GetType() != specialType)
            {
                switch (specialType.Name)
                {
                    case "Boolean":
                        setValue = value.ConvertTo<bool>();
                        break;
                    case "DateTime":
                        setValue = DateTimeHelper.GetDateTimeFromXml(value.ToString());
                        break;
                    case "DateTimeOffset":
                        setValue = DateTimeHelper.GetDateTimeOffsetFromXml(value.ToString());
                        break;
                    case "Int32":
                        setValue = value.ConvertTo<int>();
                        break;
                    case "Int32[]":
                        setValue = Array.ConvertAll(value.ConvertTo<string>().Split(','), int.Parse);
                        break;
                    case "Int64":
                        setValue = value.ConvertTo<long>();
                        break;
                    case "Int64[]":
                        setValue = Array.ConvertAll(value.ConvertTo<string>().Split(','), long.Parse);
                        break;
                    case "Double":
                        setValue = value.ConvertTo<double>();
                        break;
                    case "String":
                        setValue = value.ToString(CultureInfo.InvariantCulture);
                        break;
                    default:
                        setValue = value;
                        break;
                }
            }

            switch (specialType.Name)
            {
                case "Nullable`1": //可为空对象
                    {
                        if (!string.IsNullOrEmpty(value as string))
                        {
                            var genericArguments = prop.PropertyType.GetGenericArguments();
                            FillSystemType(entity, prop, value, genericArguments[0]);
                        }
                        else
                        {
                            prop.SetValue(entity, null, null);//默认通常为null
                        }
                        break;
                    }
                //case "String":
                //    goto default;
                //case "Boolean":
                //case "DateTime":
                //case "Int32":
                //case "Int64":
                //case "Double":
                default:
                    prop.SetValue(entity, setValue ?? value, null);
                    break;
            }
        }

    }
}
