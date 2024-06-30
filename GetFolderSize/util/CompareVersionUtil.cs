using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GetFolderSize.util
{
    /// <summary>
    /// 比较给定版本是否低于指定版本
    /// </summary>
    /// 2024.5.18
    /// version 2.0.0
    internal static class CompareVersionUtil
    {
        /// <summary>
        /// 给定两个版本号version和specified_version，若version版本号低于specified_version则返回true，否则返回false。
        /// 版本号的形式类如2.0.0。若version不符合此形式或version为null则返回true/
        /// </summary>
        /// 2024.5.18
        /// version 2.0.0
        /// <param name="version"></param>
        /// <param name="specified_version"></param>
        /// <returns></returns>
        public static bool isLowerVersion(string? version, string specified_version)
        {
            int LENGTH = 3; // 版本号由3个数字构成
            if (version == null)
                return true;
            try
            {
                // 将version和specified_version转化为数字
                int i;
                string[] _version = version.Split('.');
                int[] __version = new int[LENGTH];
                for (i = 0; i < LENGTH; i++)
                    __version[i] = int.Parse(_version[i]);
                string[] _specified_version = specified_version.Split('.');
                int[] __specified_version = new int[LENGTH];
                for (i = 0; i < LENGTH; i++)
                    __specified_version[i] = int.Parse(_specified_version[i]);

                for (i = 0; i < LENGTH; i++)
                    if (__version[i] < __specified_version[i])
                        return true;
                return false;
            }catch (Exception) { return true; }
        }
    }
}
