using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFolderSize
{
    /// <summary>
    /// 比较函数，用于排序
    /// <para>2022.6.8</para>
    /// <para>version 1.1.0</para>
    /// </summary>
    internal class CompareFunctions
    {
        /// <summary>
        /// 以名字升序排序
        /// <para>2022.6.8</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>负数为排名在前，正数为排名在后，0为相同</returns>
        public static int ByNameAsc(FolderOrFile f1, FolderOrFile f2)
        {
            return f1.Name.CompareTo(f2.Name);
        }

        /// <summary>
        /// 以名字降序排序
        /// <para>2022.6.8</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>负数为排名在前，正数为排名在后，0为相同</returns>
        public static int ByNameDesc(FolderOrFile f1, FolderOrFile f2)
        {
            return -f1.Name.CompareTo(f2.Name);
        }

        /// <summary>
        /// 以文件数升序排序
        /// <para>2022.6.8</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>负数为排名在前，正数为排名在后，0为相同</returns>
        public static int ByFileCountAsc(FolderOrFile f1, FolderOrFile f2)
        {
            return f1.FileCount.CompareTo(f2.FileCount);
        }

        /// <summary>
        /// 以文件数降序排序
        /// <para>2022.6.8</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>负数为排名在前，正数为排名在后，0为相同</returns>
        public static int ByFileCountDesc(FolderOrFile f1, FolderOrFile f2)
        {
            return -f1.FileCount.CompareTo(f2.FileCount);
        }

        /// <summary>
        /// 以大小升序排序
        /// <para>2022.6.8</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>负数为排名在前，正数为排名在后，0为相同</returns>
        public static int BySizeAsc(FolderOrFile f1, FolderOrFile f2)
        {
            return -f1.CompareTo(f2);//FolderOrFile的CompareTo是降序排序
        }

        /// <summary>
        /// 以大小降序排序
        /// <para>2022.6.8</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>负数为排名在前，正数为排名在后，0为相同</returns>
        public static int BySizeDesc(FolderOrFile f1, FolderOrFile f2)
        {
            return f1.CompareTo(f2);//FolderOrFile的CompareTo是降序排序
        }

        /// <summary>
        /// 优先文件夹。文件夹和文件各以大小降序排序
        /// <para>2022.6.8</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>负数为排名在前，正数为排名在后，0为相同</returns>
        public static int ByIsFolderTrueFirst(FolderOrFile f1, FolderOrFile f2)
        {
            if(f1.IsFolder && !f2.IsFolder)
                return 1;
            if (!f1.IsFolder && f2.IsFolder)
                return -1;
            return f1.CompareTo(f2);
        }

        /// <summary>
        /// 优先文件。文件夹和文件各以大小降序排序
        /// <para>2022.6.8</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>负数为排名在前，正数为排名在后，0为相同</returns>
        public static int ByIsFolderFalseFirst(FolderOrFile f1, FolderOrFile f2)
        {
            if (f1.IsFolder && !f2.IsFolder)
                return -1;
            if (!f1.IsFolder && f2.IsFolder)
                return 1;
            return f1.CompareTo(f2);
        }

    }
}
