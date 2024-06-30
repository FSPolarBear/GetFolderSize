


namespace GetFolderSize
{
    /// <summary>
    /// 
    /// </summary>
    /// 2024.5.31
    /// version 2.0.0
    internal static class FileSizeUtil
    {
        /// <summary>
        /// 将表示文件大小的字符串转化为数字类型
        /// 字符串格式要求为 整数B/KB/MB/GB/TB
        /// 对空字符串，返回null
        /// </summary>
        /// 2023.12.19
        /// version 1.4.0
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
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        /// 2023.12.19
        /// version 1.4.0
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
        /// </summary>
        /// 2023.12.20
        /// version 1.4.0
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

        /// <summary>
        /// 判断上下限是否有效。
        /// 若上限或下限不存在，则有效。
        /// 若上限和下限均存在，且下限不大于上限，则有效。
        /// 若上限和下限均存在，且下限大于上限，则无效。
        /// 若转换过程出错则无效。（由于实际调用此方法前先调用IsFileSizeOrEmpty或int.TryParse检查了转换过程，因此此过程中不会出现转换错误）。
        /// </summary>
        /// 2024.5.31
        /// version 2.0.0
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <param name="isFileSize">若上下限是文件大小，此参数为true；若上下限是int，此参数为false。</param>
        /// <returns></returns>
        public static bool AreLowerAndHigherLimitValid(string lower, string upper, bool isFileSize)
        {
            try
            {
                if (string.IsNullOrEmpty(lower) || string.IsNullOrEmpty(upper))
                {
                    return true;
                }

                if (isFileSize)
                {
                    long _lower = FileSizeStringToNumber(lower)!.Value;
                    long  _upper = FileSizeStringToNumber(upper)!.Value;
                    return _lower <= _upper;
                }
                else
                {
                    int _lower = int.Parse(lower);
                    int _upper = int.Parse(upper);
                    return _lower <= _upper;
                }

            }catch (Exception) // 由于实际调用此方法前先调用IsFileSizeOrEmpty或int.TryParse检查了转换过程，因此正常情况此处不会抛出异常
            {
                return false;
            }
            
        }

    }
}
