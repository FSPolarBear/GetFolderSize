using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GetFolderSize
{

    /// <summary>
    /// 文件夹或文件。数据项，用于在列表中展示
    /// <para>2022.6.7</para>
    /// <para>version 1.0.0</para>
    /// 
    /// </summary>
    public class FolderOrFile : IComparable<FolderOrFile>
    {
        
        public static readonly string FILE = "File";
        public static readonly string FOLDER = "Folder";
 
        public bool IsFolder { get; set; } //此项为true表示是文件夹，为false表示是文件
        public string FullName { get; set; } //包含路径的文件名或文件夹名
        public string Name { get; set; } //文件名或文件夹名

        //大小。单位为字节
        public long Size { get; set; }
        
        public FolderOrFile Parent { get; set; }

        //文件夹内的文件和文件夹
        public FolderOrFile[] Children { get; set; }

        //文件数
        public int FileCount { get; set; }



        /// <summary>
        /// 将文件/文件夹大小转换为合适的单位，并保留小数点后2位
        /// <para>2022.6.7</para>
        /// <para>version 1.0.0</para>
        /// </summary>
        /// <returns>转化后的文件/文件夹大小字符串</returns>
        public string SizeFormat()
        {
            string unit = "B";
            double size_d = (double)Size;
            if (size_d > 1024.0)
            {
                size_d /= 1024.0;
                unit = "KB";
            }
            if (size_d > 1024.0)
            {
                size_d /= 1024.0;
                unit = "MB";
            }
            if (size_d > 1024.0)
            {
                size_d /= 1024.0;
                unit = "GB";
            }

            return string.Format("{0:f2}{1}", size_d, unit);
        }



        /// <summary>
        /// 依据文件大小，降序排序
        /// <para>2022.6.7</para>
        /// <para>version 1.0.0</para>
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public int CompareTo(FolderOrFile f)
        {
            if (this.Size < f.Size)
                return 1;
            if (this.Size > f.Size)
                return -1;
            return 0;
        }


        /// <summary>
        /// 给定文件夹路径，求其大小
        /// <para>2022.6.7</para>
        /// <para>version 1.0.0</para>
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns></returns>
        /// <exception cref="Exception">文件夹不存在</exception>
        public static FolderOrFile GetObjectFromPath(string path)
        {
            if (!Directory.Exists(path))
                throw new Exception("Folder not found");


            //文件夹基本信息
            DirectoryInfo dinfo = new DirectoryInfo(path);
            FolderOrFile f = new FolderOrFile();
            f.FullName = dinfo.FullName;
            f.Name = dinfo.Name;
            f.IsFolder = true;
            f.Size = 0;
            f.FileCount = 0;
            
            //计算文件夹下文件和文件夹的数量。若出错则为0
            int dir_count = 0, file_count = 0;
            DirectoryInfo[] dirs = null;
            FileInfo[] files = null;
            try
            {
                dirs = dinfo.GetDirectories();
                dir_count = dirs.Length;
                files = dinfo.GetFiles();
                file_count = files.Length;
            }
            catch (Exception)
            {

             }

            f.Children = new FolderOrFile[dir_count + file_count];

            int j = 0;
            //遍历文件夹内的文件
            for (int i = 0; i < file_count; i++)
            {
                f.Children[j] = new FolderOrFile();
                f.Children[j].IsFolder = false;
                f.Children[j].FullName = files[i].FullName;
                f.Children[j].Name = files[i].Name;
                f.Children[j].Size = files[i].Length;
                f.Children[j].FileCount = 1;
                f.Children[j].Parent = f;
                j++;
            }

            //遍历文件夹内的文件夹
            for (int i = 0; i < dir_count; i++)
            {
                f.Children[j] = GetObjectFromPath(dirs[i].FullName);
                f.Children[j].Parent = f;
                j++;
            }
            //计算文件夹大小和文件数量
            f.Size = 0;
            for (int i = 0; i < f.Children.Length; i++)
            {
                f.Size += f.Children[i].Size;
                f.FileCount += f.Children[i].FileCount;
            }
                  

            //对Children以Size大小进行排序
            Array.Sort<FolderOrFile>(f.Children);

            return f;
        }
    }
}
