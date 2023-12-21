using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GetFolderSize
{

    

    /// <summary>
    /// 
    /// <para>2023.12.18</para>
    /// <para>version 1.4.0</para>
    /// </summary>
    public static class JsonUtil
    {
        private class NullType { }
        private static Dictionary<JTokenType, Type> JSON_TYPE_INDEX = new Dictionary<JTokenType, Type>()
        {
            {JTokenType.Integer, typeof(int)},
            {JTokenType.Float, typeof(double)},
            {JTokenType.String, typeof(string)},
            {JTokenType.Boolean, typeof(bool)},
            {JTokenType.Object, typeof(JObject) },
            {JTokenType.Array, typeof(JArray)},
            {JTokenType.Null, typeof(NullType) },
        };


        /// <summary>
        /// 从json对象中读取指定键对应的值。如果json对象为null、json对象中不存在指定的键或指定键对应的值与读取类型不符则返回默认值
        /// <para>2023.12.12</para>
        /// <para>version 1.3.1</para>
        /// </summary>
        /// <typeparam name="T">需要读取的数据类型。支持读取整数(int)、浮点数(double)、字符串(string)、布尔(bool)、json对象(JObject)、json数组(JArray)，不支持读取Null</typeparam>
        /// <param name="jobj">json对象</param>
        /// <param name="key">键</param>
        /// <param name="default_value">无法成功读取时的默认值</param>
        /// <returns></returns>
        public static T GetValue<T>(JObject? jobj, string key, T default_value)
        {
            if (jobj == null)
            {
                return default_value;
            }
            JToken? token = jobj.GetValue(key);
            if (token != null && JSON_TYPE_INDEX[token.Type] == typeof(T))
            {

                return token.Value<T>();
            }

            return default_value;
        }


        /// <summary>
        /// 从json对象中读取int或null值
        /// <para>2023.12.18</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="jobj">json对象</param>
        /// <param name="key">键</param>
        /// <param name="default_value">无法成功读取时的默认值</param>
        /// <returns></returns>
        public static int? GetInt(JObject? jobj, string key, int? default_value=null)
        {
            if (jobj == null)
            {
                return default_value;
            }
            JToken? token = jobj.GetValue(key);
            if (token != null && JSON_TYPE_INDEX[token.Type] == typeof(int))
            {

                return token.Value<int>();
            }
            return default_value;
        }

        /// <summary>
        /// 从json对象中读取long或null值
        /// <para>2023.12.18</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="jobj">json对象</param>
        /// <param name="key">键</param>
        /// <param name="default_value">无法成功读取时的默认值</param>
        /// <returns></returns>
        public static long? GetLong(JObject? jobj, string key, long? default_value = null)
        {
            if (jobj == null)
            {
                return default_value;
            }
            JToken? token = jobj.GetValue(key);
            if (token != null && JSON_TYPE_INDEX[token.Type] == typeof(int))
            {
                return token.Value<long>();
            }
            return default_value;
        }

        /// <summary>
        /// 从json对象中读取double或null值
        /// <para>2023.12.18</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="jobj">json对象</param>
        /// <param name="key">键</param>
        /// <param name="default_value">无法成功读取时的默认值</param>
        /// <returns></returns>
        public static double? GetDouble(JObject? jobj, string key, double? default_value = null)
        {
            if (jobj == null)
            {
                return default_value;
            }
            JToken? token = jobj.GetValue(key);
            if (token != null && JSON_TYPE_INDEX[token.Type] == typeof(double))
            {

                return token.Value<double>();
            }
            return default_value;
        }

        /// <summary>
        /// 从json对象中读取bool或null值
        /// <para>2023.12.18</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="jobj">json对象</param>
        /// <param name="key">键</param>
        /// <param name="default_value">无法成功读取时的默认值</param>
        /// <returns></returns>
        public static bool? GetBool(JObject? jobj, string key, bool? default_value = null)
        {
            if (jobj == null)
            {
                return default_value;
            }
            JToken? token = jobj.GetValue(key);
            if (token != null && JSON_TYPE_INDEX[token.Type] == typeof(bool))
            {

                return token.Value<bool>();
            }
            return default_value;
        }

        /// <summary>
        /// 从json对象中读取string或null值
        /// <para>2023.12.18</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="jobj">json对象</param>
        /// <param name="key">键</param>
        /// <param name="default_value">无法成功读取时的默认值</param>
        /// <returns></returns>
        public static string? GetString(JObject? jobj, string key, string? default_value = null)
        {
            if (jobj == null)
            {
                return default_value;
            }
            JToken? token = jobj.GetValue(key);
            if (token != null && JSON_TYPE_INDEX[token.Type] == typeof(string))
            {

                return token.Value<string>();
            }
            return default_value;
        }


        /// <summary>
        /// 从json对象中读取JObject或null值
        /// <para>2023.12.18</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="jobj">json对象</param>
        /// <param name="key">键</param>
        /// <param name="default_value">无法成功读取时的默认值</param>
        /// <returns></returns>
        public static JObject? GetJObject(JObject? jobj, string key, JObject? default_value = null)
        {
            if (jobj == null)
            {
                return default_value;
            }
            JToken? token = jobj.GetValue(key);
            if (token != null && JSON_TYPE_INDEX[token.Type] == typeof(JObject))
            {

                return token.Value<JObject>();
            }
            return default_value;
        }

        /// <summary>
        /// 从json对象中读取JArray或null值
        /// <para>2023.12.18</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="jobj">json对象</param>
        /// <param name="key">键</param>
        /// <param name="default_value">无法成功读取时的默认值</param>
        /// <returns></returns>
        public static JArray? GetJArray(JObject? jobj, string key, JArray? default_value = null)
        {
            if (jobj == null)
            {
                return default_value;
            }
            JToken? token = jobj.GetValue(key);
            if (token != null && JSON_TYPE_INDEX[token.Type] == typeof(JArray))
            {

                return token.Value<JArray>();
            }
            return default_value;
        }

    }
}
