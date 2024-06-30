
namespace GetFolderSize
{
    /// <summary>
    /// 当文件修改时，实时更改FolderOrFile中的对应数据。
    /// </summary>
    /// 2024.6.16
    /// version 2.0.0
    internal class FolderOrFileUpdater: FileSystemWatcher
    {
        private FileIndex index;

        /// <summary>
        /// 对一个文件夹进行监视。
        /// </summary>
        /// 2024.4.14
        /// version 2.0.0
        /// <param name="folder"></param>
        public FolderOrFileUpdater(Folder folder)
        {
            this.index = new FileIndex(folder);

            Path = folder.FullName;

            NotifyFilter = NotifyFilters.LastWrite
                | NotifyFilters.DirectoryName
                | NotifyFilters.FileName
                | NotifyFilters.LastWrite;

            Changed += OnChanged;
            Created += OnCreated;
            Deleted += OnDeleted;
            Renamed += OnRenamed;

            IncludeSubdirectories = true;
        }

        /// <summary>
        /// 监视文件/文件夹更改，包括文件大小更改和文件/文件夹最后修改时间更改。
        /// </summary>
        /// 2024.6.16
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            string path = e.FullPath;
            FolderOrFileUpdater self = (FolderOrFileUpdater)sender;
            FolderOrFile? changed_forf = self.index.GetFolderOrFileByPath(path, out _);
            if (changed_forf is File _file)
            {
                // 此处有可能抛出System.IO.FileNotFoundException
                try
                {
                    FileInfo finfo = new FileInfo(path);
                    _file.ChangeSizeOfParent(finfo.Length - _file.Size);
                    _file.Size = finfo.Length;
                    _file.LastWriteTime = finfo.LastWriteTime;
                }
                catch { }
            }
            else if(changed_forf is Folder _folder)
            {
                // 创建或删除文件时会更新其所在文件夹的最后修改时间
                try
                {
                    DirectoryInfo dinfo = new DirectoryInfo(path);
                    _folder.LastWriteTime = dinfo.LastWriteTime;
                }
                catch { }
            }
        }

        /// <summary>
        /// 监视文件和文件夹创建
        /// </summary>
        /// 2024.4.15
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string path = e.FullPath;
            FolderOrFileUpdater self = (FolderOrFileUpdater)sender;
            self.AddFolderOrFile(path);
        }

        /// <summary>
        /// 监视文件和文件夹删除。
        /// </summary>
        /// 2024.4.15
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            string path = e.FullPath;
            FolderOrFileUpdater self = (FolderOrFileUpdater)sender;
            self.RemoveFolderOrFile(path);
        }


        /// <summary>
        /// 监视文件和文件夹重命名。
        /// </summary>
        /// 2024.4.15
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            string old_path = e.OldFullPath;
            string new_path = e.FullPath;
            FolderOrFileUpdater self = (FolderOrFileUpdater)sender;
            self.RemoveFolderOrFile(old_path);
            self.AddFolderOrFile(new_path);
        }


        /// <summary>
        /// 添加文件或文件夹。
        /// <param name="path"></param>
        /// </summary>
        /// 2024.5.26
        /// version 2.0.0
        private void AddFolderOrFile(string path)
        {
            FileIndex? parent;
            parent = index.GetParentOfFolderOrFileByPath(path);

            // parent是创建的文件或文件夹所在的文件夹，若不存在则不更新。
            if (parent == null)
                return;

            // 文件
            if (System.IO.File.Exists(path))
            {
                try // 无访问权限等情况时会遇到System.IO.File.Exists返回true但new FileInfo抛出文件不存在异常的情况
                {
                    File file = new File(path);
                    file.Parent = parent.WatchedFolder;
                    parent.WatchedFolder.Children.Add(file);
                    parent.Files[file.Name] = file;
                    file.AddSizeAndFileCountToParent();
                }
                catch { }
            }
            // 文件夹
            else if (System.IO.Directory.Exists(path))
            {
                try
                {
                    Folder folder = new Folder(path);
                    folder.Parent = parent.WatchedFolder;
                    parent.WatchedFolder.Children.Add(folder);
                    parent.Folders[folder.Name] = new FileIndex(folder);
                    folder.AddSizeAndFileCountToParent();
                }
                catch { }
            }
        }

        /// <summary>
        /// 移除文件或文件夹。
        /// </summary>
        /// 2024.5.8
        /// version 2.0.0
        /// <param name="path"></param>
        private void RemoveFolderOrFile(string path)
        {
            FileIndex? parent;
            FolderOrFile? deleted_forf = index.GetFolderOrFileByPath(path, out parent);

            if (deleted_forf is File file)
            {
                file.SubtractSizeAndFileCountFromParent();
                file.Parent = null;
                if (parent != null)
                {
                    parent.WatchedFolder.Children.Remove(file);
                    if (parent.Files.ContainsKey(file.Name))
                        parent.Files.Remove(file.Name);
                }
            }
            else if (deleted_forf is Folder folder)
            {
                folder.SubtractSizeAndFileCountFromParent();
                folder.Parent = null;
                folder.Children.Clear();
                if (parent != null)
                {
                    parent.WatchedFolder.Children.Remove(folder);
                    if (parent.Files.ContainsKey(folder.Name))
                        parent.Files.Remove(folder.Name);
                }
            }
        }

    }

    /// <summary>
    /// 在实时更新时，使用FileIndex以字典形式记录文件夹中包含的每个文件和文件夹，以供随机访问。
    /// </summary>
    /// 2024.4.15
    /// version 2.0.0
    internal class FileIndex
    {
        public Folder WatchedFolder { get; set; }
        public Dictionary<string, FileIndex> Folders { get; set; }
        public Dictionary<string, File> Files { get; set; }

        public FileIndex(Folder folder)
        {
            WatchedFolder = folder;
            Folders = new Dictionary<string, FileIndex>();
            Files = new Dictionary<string, File>();
            foreach (FolderOrFile forf in folder.Children)
            {
                if( forf is Folder _folder)
                {
                    Folders[_folder.Name] = new FileIndex(_folder);
                }
                else if (forf is File _file)
                {
                    Files[_file.Name] = _file;
                }
            }
        }

        /// <summary>
        /// 依据绝对路径获取监视FolderOrFile所在文件夹的对象。
        /// this.WatchedFolder需为Folder的某一级父目录，否则将返回null。
        /// </summary>
        /// 2024.5.26
        /// version 2.0.0
        /// <param name="path"></param>
        /// <returns></returns>
        public FileIndex? GetParentOfFolderOrFileByPath(string path)
        {
            // this.WatchedFolder需为Folder的某一级父目录，否则将返回null。
            if (!path.StartsWith(WatchedFolder.FullName))
                return null;

            int pos = WatchedFolder.FullName.Length; // 截取位置。
            // 若截掉this.WatchedFolder的全路径后下一个字符是反斜杠(如D:\folder1\file1截掉D:\folder1)则额外截一位，将反斜杠也截掉；
            // 若截掉this.WatchedFolder的全路径后下一个字符不是反斜杠(如D:\folder1\file1截掉D:\)则不需要额外额外截一位。
            if (path[pos] == '\\' || path[pos] == '/') 
                pos++;
            path = path.Substring(pos); // 截掉this.WatchedFolder的全路径及其后面的\

            FileIndex now = this;
            string[] names = path.Split(System.IO.Path.DirectorySeparatorChar);
            for (int i = 0; i < names.Length - 1; i++)
            {
                if (!now.Folders.ContainsKey(names[i]))
                    return null;
                now = now.Folders[names[i]];
            }
            return now;
        }

        /// <summary>
        /// 依据绝对路径获取Folder对象，并以out参数形式输出其所在文件夹。
        /// this.WatchedFolder需为Folder的某一级父目录，否则将返回null。
        /// 当函数返回值不为null时，dir不为null。
        /// 当所查找的文件夹不存在但其所在文件夹存在时，返回值为null但dir不为null。
        /// </summary>
        /// 2024.4.16
        /// version 2.0.0
        /// <param name="path"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public Folder? GetFolderByPath(string path, out FileIndex? dir)
        {
            string name = path.Substring(path.LastIndexOf('\\') + 1);
            dir = GetParentOfFolderOrFileByPath(path);
            if (dir != null && dir.Folders.ContainsKey(name))
                return dir.Folders[name].WatchedFolder;
            else
                return null;
        }

        /// <summary>
        /// 依据绝对路径获取File对象，并以out参数形式输出其所在文件夹。
        /// this.WatchedFolder需为File的某一级父目录，否则将返回null。
        /// 当函数返回值不为null时，dir不为null.
        /// 当所查找的文件夹不存在但其所在文件夹存在时，返回值为null但dir不为null。
        /// </summary>
        /// 2024.4.16
        /// version 2.0.0
        /// <param name="path"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public File? GetFileByPath(string path, out FileIndex? dir)
        {
            string name = path.Substring(path.LastIndexOf('\\') + 1);
            dir = GetParentOfFolderOrFileByPath(path);
            if (dir != null && dir.Folders.ContainsKey(name))
                return dir.Files[name];
            else
                return null;
        }

        /// <summary>
        /// 依据绝对路径获取FolderOrFile对象。
        /// this.WatchedFolder需为FolderOrFile的某一级父目录，否则将返回null。
        /// 当函数返回值不为null时，dir不为null。
        /// 当所查找的文件夹不存在但其所在文件夹存在时，返回值为null但dir不为null。
        /// </summary>
        /// 2024.4.16
        /// version 2.0.0
        /// <param name="path"></param>
        /// <returns></returns>
        public FolderOrFile? GetFolderOrFileByPath(string path, out FileIndex? dir)
        {
            string name = path.Substring(path.LastIndexOf('\\') + 1);
            dir = GetParentOfFolderOrFileByPath(path);
            if (dir == null) 
                return null;
            else if (dir.Folders.ContainsKey(name))
                return dir.Folders[name].WatchedFolder;
            else if (dir.Files.ContainsKey(name))
                return dir.Files[name];
            else
                return null;
        }
    }

}
