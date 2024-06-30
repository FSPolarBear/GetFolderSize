using Json;

namespace GetFolderSize
{
    /// <summary>
    /// 文件
    /// </summary>
    /// 2024.6.19
    /// version 2.0.0
    internal class File: FolderOrFile
    {
        // 记录已获取的文件总数
        public static int TotalFileCount { get; private set; } = 0;

        public File()
        {
            FullName = String.Empty;
            Name = String.Empty;
            Size = 0;
            Parent = null;
            LastWriteTime = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.4.15
        /// version 2.0.0
        /// <param name="info"></param>
        public File(FileInfo info)
        {
            FullName = info.FullName;
            Name = info.Name;
            Size = info.Length;
            LastWriteTime = info.LastWriteTime;
            Parent = null;
            TotalFileCount++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.4.15
        /// version 2.0.0
        /// <param name="path"></param>
        public File(string path): this(new FileInfo(path)) { }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.6.19
        /// version 2.0.0
        /// <param name="json"></param>
        public File(JsonObject json)
        {
            FullName = json.Get<string>("FullName");
            Name = json.Get<string>("Name");
            Size = json.Get<long>("Size");
            string str_LastWriteTime = json.Get<string>("LastWriteTime", string.Empty);
            if (string.IsNullOrEmpty(str_LastWriteTime))
            {
                LastWriteTime = null;
            }
            else
            {
                LastWriteTime = DateTime.Parse(str_LastWriteTime);
            }
        }


        /// <summary>
        /// 由已有的对象生成json对象
        /// </summary>
        /// 2024.4.13
        /// version 2.0.0
        /// <returns>json对象</returns>
        public override JsonObject ToJson()
        {
            JsonObject jobj = new JsonObject();
            //jobj["IsFolder"] = false;
            jobj["FullName"] = FullName;
            jobj["Name"] = Name;
            jobj["Size"] = Size;
            if (LastWriteTime != null)
            {
                jobj["LastWriteTime"] = LastWriteTime.Value.ToString(DATETIME_FORMAT);
            }
            return jobj;
        }

        /// <summary>
        /// 清空已获取的文件总数
        /// </summary>
        /// 2024.5.23
        /// version 2.0.0
        public static void ClearTotalFileCount()
        {
            TotalFileCount = 0;
        }
    }
}
