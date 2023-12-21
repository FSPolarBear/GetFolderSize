using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFolderSize
{
    /// <summary>
    /// 
    /// <para>2023.12.20</para>
    /// <para>version 1.4.0</para>
    /// </summary>
    public class FileSizeUtil
    {
        /// <summary>
        /// 将表示文件大小的字符串转化为数字类型
        /// 字符串格式要求为 整数B/KB/MB/GB/TB
        /// 对空字符串，返回null
        /// <para>2023.12.19</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="size">表示文件大小的字符串</param>
        /// <returns>文件大小(字节)</returns>
        public static long? FileSizeStringToNumber(string size)
        {
            if (string.IsNullOrEmpty(size))
                return null;
            size = size.Trim().ToLower();

            long unit = 1L;

            if (size.EndsWith("tb"))
            {
                unit = 1024L * 1024L * 1024L * 1024L;
                size = size.Substring(0, size.Length - 2);
            }
            else if(size.EndsWith("t"))
            {
                unit = 1024L * 1024L * 1024L * 1024L;
                size = size.Substring(0, size.Length - 1);
            }
            else if (size.EndsWith("gb"))
            {
                unit = 1024L * 1024L * 1024L;
                size = size.Substring(0, size.Length - 2);
            }
            else if (size.EndsWith("g"))
            {
                unit = 1024L * 1024L * 1024L;
                size = size.Substring(0, size.Length - 1);
            }
            else if (size.EndsWith("mb"))
            {
                unit = 1024L * 1024L;
                size = size.Substring(0, size.Length - 2);
            }
            else if (size.EndsWith("m"))
            {
                unit = 1024L * 1024L;
                size = size.Substring(0, size.Length - 1);
            }
            else if (size.EndsWith("kb"))
            {
                unit = 1024L;
                size = size.Substring(0, size.Length - 2);
            }
            else if (size.EndsWith("k"))
            {
                unit = 1024L;
                size = size.Substring(0, size.Length - 1);
            }
            else if (size.EndsWith("b"))
            {
                size = size.Substring(0, size.Length - 1);
            }

            long number = long.Parse(size.Trim());

            if (number < 0L)
                throw new Exception("File size must be grater than or equal to 0");

            return number * unit;
        }


        /// <summary>
        /// 检查字符串是否是文件大小或空字符串
        /// <para>2023.12.19</para>
        /// <para>version 1.4.0</para
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static bool IsFileSizeOrEmpty(string size)
        {
            try
            {
                FileSizeStringToNumber(size);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 将文件大小转化为字符串表示形式，如10240000 -> "1000KB"。其中数值部分为整数，若使用较大的单位无法保证数值部分为整数则使用较小的单位
        /// <para>2023.12.20</para>
        /// <para>version 1.4.0</para
        /// </summary>
        /// <param name="size">文件大小(字节)</param>
        /// <returns></returns>
        public static string FileSizeNumberToStringWithIntegerValue(long? size)
        {
            if(size == null)
                return string.Empty;
            if (size < 0L)
                throw new Exception("File size must be greater than or equal to 0.");
            if (size == 0L)
                return "0B";
            string[] units = { "B", "KB", "MB", "GB", "TB" };
            for (int i = 0; i < units.Length; i++)
            {
                if (size % 1024L == 0)
                    size /= 1024L;
                else
                    return size.ToString() + units[i];

            }
            return size.ToString() + units[units.Length - 1];
        }

    }
}
