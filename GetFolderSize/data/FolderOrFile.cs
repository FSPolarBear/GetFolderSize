
using Json;

namespace GetFolderSize
{

    /// <summary>
    /// 文件夹或文件。数据项，用于在列表中展示
    /// </summary>
    /// 2024.6.19
    /// version 2.0.0
    internal abstract class FolderOrFile : IComparable<FolderOrFile>, IJsonObject
    {
        
        public static readonly string DATETIME_FORMAT = "yyyy/M/d H:mm:ss";
 
        public string FullName { get; set; } //包含路径的文件名或文件夹名
        public string Name { get; set; } //文件名或文件夹名

        //大小。单位为字节
        public long Size { get; set; }

        public Folder? Parent { get; set; }

        public DateTime? LastWriteTime { get; set; }


        /// <summary>
        /// 将文件/文件夹大小转换为合适的单位，并保留小数点后2位
        /// </summary>
        /// 2023.12.20
        /// version 1.4.0
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
            if (size_d > 1024.0)
            {
                size_d /= 1024.0;
                unit = "TB";
            }
            if (unit == "B") // 单位是B的时候不需要表示为小数的形式
                return Size.ToString() + "B";
            else
                return string.Format("{0:f2}{1}", size_d, unit);
        }

        /// <summary>
        /// 依据文件大小，降序排序
        /// </summary>
        /// 2024.4.13
        /// version 2.0.0
        /// <param name="f"></param>
        /// <returns></returns>
        public int CompareTo(FolderOrFile? f)
        {
            if (f == null || this.Size < f.Size)
                return 1;
            else if (this.Size > f.Size)
                return -1;
            else
                return 0;
        }



        /// <summary>
        /// 给定多个文件夹路径，获取这些文件夹的信息
        /// </summary>
        /// 2024.5.26
        /// version 2.0.0
        /// <param name="paths"></param>
        /// <returns></returns>
        public static Folder GetObjectsFromPaths(string[] paths)
        {
            Folder result = new Folder();
            result.Name = string.Join("|", paths);
            foreach(string path in paths)
            {
                Folder folder = new Folder(path);
                folder.Parent = result;
                result.Children.Add(folder);
                result.FileCount += folder.FileCount;
                result.Size += folder.Size;
            }
            return result;
        }

        /// <summary>
        /// 由已有的对象生成json对象
        /// </summary>
        /// 2024.4.13
        /// version 2.0.0
        /// <returns>json对象</returns>
        public abstract JsonObject ToJson();
        

        /// <summary>
        /// 
        /// </summary>
        /// 2024.6.28
        /// version 2.0.0
        /// <returns>ToJson得到的json的字符串</returns>
        public override string ToString()
        {
            JsonObject json = new JsonObject(this);
            json["Version"] = Version.CurrentVersion;
            return json.ToString();
        }

        /// <summary>
        /// 检查当前文件或文件夹的大小是否在指定范围内
        /// </summary>
        /// 2023.12.20
        /// version 1.4.0
        /// <param name="lowerLimit">大小下限。为null则不设下限</param>
        /// <param name="upperLimit">大小上限。为null则不设上限</param>
        /// <returns></returns>
        public bool InLimitedSize(long? lowerLimit, long? upperLimit)
        {
            if (lowerLimit != null && Size < lowerLimit)
                return false;
            if (upperLimit != null && Size > upperLimit)
                return false;
            return true;
        }

        /// <summary>
        /// 全路径相同的两个对象视为相等
        /// </summary>
        /// 2023.12.27
        /// version 1.4.1
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            FolderOrFile? f = obj as FolderOrFile;
            if (f == null)
                return false;
            return f.FullName == this.FullName;
        }

        /// <summary>
        /// 重写Equals需重写GetHashCode
        /// </summary>
        /// 2023.12.27
        /// version 1.4.1
        /// <returns></returns>
        public override int GetHashCode()
        {
            return FullName.GetHashCode();
        }

        /// <summary>
        /// 添加或删除文件、文件夹，以及修改文件、文件夹大小后调用此方法，将修改的大小反映至每级父文件夹。
        /// </summary>
        /// 2024.5.8
        /// version 2.0.0
        /// <param name="addedSize">修改前后文件大小差值，单位是字节。addedSize为正表示文件变大，addedSize为负表示文件变小。</param>
        public void ChangeSizeOfParent(long addedSize)
        {
            if (addedSize == 0L)
                return;
            Folder? parent = this.Parent;
            while (parent != null)
            {
                parent.Size += addedSize;
                parent = parent.Parent;
            }
        }

        /// <summary>
        /// 添加或删除文件、文件夹时调用此方法，将修改的文件数量反映至每级父文件夹。
        /// </summary>
        /// 2024.5.8
        /// version 2.0.0
        /// <param name="addedFileCount">修改前后文件数量差值。addedFileCount为正表示文件数量变多，addedFileCount为负表示文件数量变少。</param>
        private void ChangeFileCountOfParent(int addedFileCount)
        {
            {
                if (addedFileCount == 0)
                    return;
                Folder? parent = this.Parent;
                while (parent != null)
                {
                    parent.FileCount += addedFileCount;
                    parent = parent.Parent;
                }
            }
        }

        /// <summary>
        /// 添加文件时调用此函数，将增加的文件大小和文件数反映至每级父文件夹。
        /// </summary>
        /// 2024.5.8
        /// version 2.0.0
        public void AddSizeAndFileCountToParent()
        {
            ChangeSizeOfParent(Size);
            int addedFileCount;
            if (this is Folder folder)
                addedFileCount = folder.FileCount;
            else
                addedFileCount = 1;
            ChangeFileCountOfParent(addedFileCount);   
        }

        /// <summary>
        /// 删除文件时调用此函数，将减少的文件大小和文件数反映至每级父文件夹。
        /// </summary>
        /// 2024.5.8
        /// version 2.0.0
        public void SubtractSizeAndFileCountFromParent()
        {
            ChangeSizeOfParent(-Size);
            int addedFileCount;
            if (this is Folder folder)
                addedFileCount = -folder.FileCount;
            else
                addedFileCount = -1;
            ChangeFileCountOfParent(addedFileCount);
        }
    }
}
