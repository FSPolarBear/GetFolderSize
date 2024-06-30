using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFolderSize.util
{
    /// <summary>
    /// 
    /// </summary>
    /// 2024.6.2
    /// version 2.0.0
    internal static class ConvertIntToEnumUtil
    {
        /// <summary>
        /// 将int转化为指定的枚举类型并返回。若无法转换则返回提供的默认值。
        /// </summary>
        /// 2024.6.2
        /// version 2.0.0
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Convert<T>(int index, T defaultValue) where T: Enum
        {
            if (Enum.IsDefined(typeof(T), index))
                return (T)(object)index;
            else
                return defaultValue;
        }
    }
}
