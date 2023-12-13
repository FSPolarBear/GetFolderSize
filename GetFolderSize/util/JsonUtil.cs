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
    /// <para>2023.12.12</para>
    /// <para>version 1.3.1</para>
    /// </summary>
    public static class JsonUtil
    {
        private static Dictionary<JTokenType, Type> JSON_TYPE_INDEX = new Dictionary<JTokenType, Type>()
        {
            {JTokenType.Integer, typeof(int)},
            {JTokenType.Float, typeof(double)},
            {JTokenType.String, typeof(string)},
            {JTokenType.Boolean, typeof(bool)},
            {JTokenType.Object, typeof(JObject) },
            {JTokenType.Array, typeof(JArray)},
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

    }
}
