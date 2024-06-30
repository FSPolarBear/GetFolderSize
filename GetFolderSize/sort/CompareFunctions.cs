using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFolderSize
{
    /// <summary>
    /// 比较函数，用于排序
    /// </summary>
    /// 2024.4.13
    /// version 2.0.0
    internal class CompareFunctions
    {
        /// <summary>
        /// 以名字升序排序
        /// </summary>
        /// 2022.6.8
        /// version 1.1.0
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>负数为排名在前，正数为排名在后，0为相同</returns>
        public static int ByNameAsc(FolderOrFile f1, FolderOrFile f2)
        {
            return f1.Name.CompareTo(f2.Name);
        }


        /// <summary>
        /// 以文件数升序排序
        /// </summary>
        /// 2024.4.13
        /// version 2.0.0
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>负数为排名在前，正数为排名在后，0为相同</returns>
        public static int ByFileCountAsc(FolderOrFile f1, FolderOrFile f2)
        {
            int f1count, f2count;
            if (f1 is Folder folder1)
                f1count = folder1.FileCount;
            else 
                f1count = 1;
            if (f2 is Folder folder2)
                f2count = folder2.FileCount;
            else
                f2count = 1;
            return f1count.CompareTo(f2count);
        }

        /// <summary>
        /// 以文件数降序排序
        /// </summary>
        /// 2024.4.13
        /// version 2.0.0
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>负数为排名在前，正数为排名在后，0为相同</returns>
        public static int ByFileCountDesc(FolderOrFile f1, FolderOrFile f2)
        {
            return -ByFileCountAsc(f1, f2);
        }


        /// <summary>
        /// 优先文件夹。文件夹和文件各以大小降序排序
        /// </summary>
        /// 2024.4.13
        /// version 2.0.0
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>负数为排名在前，正数为排名在后，0为相同</returns>
        public static int ByFolderFirst(FolderOrFile f1, FolderOrFile f2)
        {
            if(f1 is Folder && !(f2 is Folder))
                return 1;
            if (!(f1 is Folder) && f2 is Folder)
                return -1;
            return f1.CompareTo(f2);
        }

        /// <summary>
        /// 优先文件。文件夹和文件各以大小降序排序
        /// </summary>
        /// 2024.4.13
        /// version 2.0.0
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>负数为排名在前，正数为排名在后，0为相同</returns>
        public static int ByFileFirst(FolderOrFile f1, FolderOrFile f2)
        {
            if (f1 is Folder && !(f2 is Folder))
                return -1;
            if (!(f1 is Folder) && f2 is Folder)
                return 1;
            return f1.CompareTo(f2);
        }

        /// <summary>
        /// 以最后修改日期数降序排序，无日期的项排在有日期的文件项
        /// </summary>
        /// 2024.4.13
        /// version 2.0.0
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>负数为排名在前，正数为排名在后，0为相同</returns>
        public static int ByLastWriteTimeDesc(FolderOrFile f1, FolderOrFile f2)
        {
            if (f1.LastWriteTime == null && f2.LastWriteTime == null)
            {
                return 0;
            }
            else if (f1.LastWriteTime == null && f2.LastWriteTime != null)
            {
                return 1;
            }
            else if (f1.LastWriteTime != null && f2.LastWriteTime == null)
            {
                return -1;
            }
            else
            {
                return -f1.LastWriteTime!.Value.CompareTo(f2.LastWriteTime!.Value);
            }

        }


    }
}
