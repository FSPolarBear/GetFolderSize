
using Json;

namespace GetFolderSize
{
    /// <summary>
    /// 文件夹
    /// </summary>
    /// 2024.6.28
    /// version 2.0.0
    internal class Folder: FolderOrFile
    {
        //文件夹内的文件和文件夹
        public List<FolderOrFile> Children { get; set; }

        //文件数
        public int FileCount { get; set; }

        //Children是否已按字节大小排序排序
        public bool Sorted {  get; set; }

        public Folder()
        {
            FullName = String.Empty;
            Name = String.Empty;
            Size = 0;
            Parent = null;
            Children = new List<FolderOrFile>();
            FileCount = 0;
            LastWriteTime = null;
            Sorted = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.5.19
        /// version 2.0.0
        /// <param name="info"></param>
        public Folder(DirectoryInfo info)
        {
            // 当需要获取的文件夹中文件较多时，调用此构造函数以递归获取其中所有文件和文件夹的耗时可能会较长。
            // 为了防止调用此构造函数期间程序完全无响应，使用一个子线程thread调用此构造函数。
            // 当点击“取消”按钮时，需要调用thread.Interrupt()终止此构造函数。
            // 然而Interrupt仅能使线程在下次被阻塞时中断，若线程一直不被阻塞则Interrupt无法中断线程,
            // 因此在此处添加Thread.Sleep(0)使子线程在每次递归开始时阻塞。
            Thread.Sleep(0);

            if (!info.Exists)
                throw new Exception("folder not found");

            //文件夹基本信息
            FullName = info.FullName;
            Name = info.Name;
            Size = 0;
            Parent = null;
            Children = new List<FolderOrFile>();
            FileCount = 0;
            LastWriteTime = info.LastWriteTime;
            Sorted = false;
            
            //查找文件夹下文件和文件夹。无法获取则为空。
            DirectoryInfo[] dirs;
            FileInfo[] files;
            try
            {
                dirs = info.GetDirectories();
            }
            catch (Exception)
            {
                dirs = Array.Empty<DirectoryInfo>();
            }
            try
            {
                files = info.GetFiles();
            }
            catch (Exception)
            {
                files = Array.Empty<FileInfo>();
            }

            //遍历文件夹内的文件
            foreach (FileInfo finfo in files)
            {
                File file = new File(finfo);
                file.Parent = this;
                Children.Add(file);
                Size += file.Size;
            }
            FileCount += files.Length; // 等价于每次循环FileCount += 1



            //遍历文件夹内的文件夹
            foreach(DirectoryInfo dinfo in dirs)
            {
                Folder folder = new Folder(dinfo);
                folder.Parent = this;
                Children.Add(folder);
                Size += folder.Size;
                FileCount += folder.FileCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.4.15
        /// version 2.0.0
        /// <param name="path"></param>
        public Folder(string path) : this(new DirectoryInfo(path)) { }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.6.28
        /// version 2.0.0
        /// <param name="json"></param>
        public Folder(JsonObject json)
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
            FileCount = json.Get<int>("FileCount");
            Sorted = json.Get<bool>("Sorted", false);
            Children = json.Get<List<FolderOrFile>>("Children");
            foreach(FolderOrFile forf in Children) 
                forf.Parent = this;
        }

        /// <summary>
        /// 由已有的对象生成json对象
        /// </summary>
        /// 2024.6.28
        /// version 2.0.0
        /// <returns>json对象</returns>
        public override JsonObject ToJson()
        {
            JsonObject jobj = new JsonObject();
            //jobj["IsFolder"] = true;
            jobj["FullName"] = FullName;
            jobj["Name"] = Name;
            jobj["Size"] = Size;
            jobj["FileCount"] = FileCount;
            jobj["Sorted"] = Sorted;
            jobj["Children"] = Children;
            if (LastWriteTime != null)
            {
                jobj["LastWriteTime"] = LastWriteTime.Value.ToString(DATETIME_FORMAT);
            }
            return jobj;
        }

        /// <summary>
        /// 搜索此文件夹下名字匹配的文件或文件夹，并返回查找到的内容列表。用于递归
        /// </summary>
        /// 2024.5.19
        /// version 2.0.0
        /// <param name="args">参数</param>
        /// <returns>查找到的内容列表</returns>
        private HashSet<FolderOrFile> _Search(SearchArgs args)
        {
            string str = args.str;

            HashSet<FolderOrFile> result = new HashSet<FolderOrFile>();

            if (!args.caseSensitive)
            {
                if (args.searchRule != SearchRules.Regular)
                    str = str.ToLower();
            }

            foreach (FolderOrFile child in this.Children)
            {
                // 对文件夹递归查询
                if (args.recursiveSearch && child is Folder child_for_recursive_search)
                {
                    result.UnionWith(child_for_recursive_search._Search(args));
                }

                // 检查文件或文件夹是否符合要求
                if ((args.searchFile && !(child is Folder)) || (args.searchFolder && (child is Folder)))
                {
                    string name;
                    if (args.matchFullName)
                        name = child.FullName;
                    else 
                        name = child.Name;

                    //检查是否符合文件、文件夹大小要求
                    long? sizeLowerLimit, sizeUpperLimit;
                    if(child is Folder)
                    {
                        sizeLowerLimit = args.folderSizeLowerLimit;
                        sizeUpperLimit = args.folderSizeUpperLimit;
                    }
                    else
                    {
                        sizeLowerLimit = args.fileSizeLowerLimit;
                        sizeUpperLimit = args.fileSizeUpperLimit;
                    }
                    if (!child.InLimitedSize(sizeLowerLimit, sizeUpperLimit))
                        continue;

                    // 检查文件夹是否符合文件数量要求
                    if (child is Folder child_folder && !child_folder.InLimitedFileCount(args.fileCountLowerLimit, args.fileCountUpperLimit))
                        continue;

                    // 检查是否匹配
                    if (args.searchRule == SearchRules.Combination)
                    {
                        if (SearchUtil.CombinationMatch(name, str, args.caseSensitive, args.localizedSearchRuleNames, args.localizedColon))
                        {
                            result.Add(child);
                        }
                    }
                    else 
                    {
                        if (SearchUtil.Match(name, str, args.searchRule, args.caseSensitive))
                        {
                            result.Add(child);
                        }
                    }
                   
                }
            }
            return result;

        }

        /// <summary>
        /// 搜索此文件夹下名字匹配的文件或文件夹，并返回一个包含查找内容的文件夹对象
        /// </summary>
        /// 2024.5.19
        /// version 2.0.0
        /// <param name="args">参数</param>
        /// <returns>包含查找内容的文件夹对象</returns>
        public Folder Search(SearchArgs args)
        {
            Folder result = new Folder();
            result.Name = "Search result";
            result.Children = _Search(args).ToList();
            result.FileCount = result.Children.Count;
            result.Children.Sort();  // 对搜索的结果按大小进行排序

            //添加一个父节点，以显示查找到的文件总数
            Folder root = new Folder();
            result.Parent = root;
            root.Children.Add(result);
            return result;
        }



        /// <summary>
        /// 检查当前文件夹的文件数是否在指定范围内
        /// </summary>
        /// 2024.4.13
        /// version 2.0.0
        /// <param name="lowerLimit">文件数下限。为null则不设下限</param>
        /// <param name="upperLimit">文件数上限。为null则不设上限</param>
        /// <returns></returns>
        public bool InLimitedFileCount(int? lowerLimit, int? upperLimit)
        {
            if (lowerLimit != null && FileCount < lowerLimit)
                return false;
            if (upperLimit != null && FileCount > upperLimit)
                return false;
            return true;
        }
    }
}
