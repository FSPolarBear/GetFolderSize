using GetFolderSize.util;
using Json;

namespace GetFolderSize
{
    /// <summary>
    /// 配置信息
    /// </summary>
    /// 2024.6.28
    /// version 2.0.0
    internal static class Config
    {
        #region 配置项

        //语言
        public static string Language { get; private set; } = Localization.GetLanguageName(System.Globalization.CultureInfo.CurrentCulture.Name);

        //搜索规则
        public static SearchRules SearchRule { get; private set; } = SearchRules.Contain;

        //是否搜索文件
        public static bool SearchFiles { get; private set; } = true;

        //是否搜索文件夹
        public static bool SearchFolders { get; private set; } = true;

        //是否递归搜索
        public static bool RecursiveSearch { get; private set; } = true;

        //是否区分大小写
        public static bool CaseSensitive { get; private set; } = false;

        //是否匹配全路径
        public static bool MatchFullName { get; private set; } = false;

        //是否从根目录中搜索。若为true则从根目录中搜索；若为false则从当前显示的目录中搜索。
        public static bool SearchFromRoot { get; private set; } = false;

        //是否启用分批加载
        public static bool EnableBatchLoad { get; private set; } = true;

        //使用分批加载的项数阈值
        public static int BatchLoadThreshold { get; private set; } = 1000;

        //分批加载时每批的数量
        public static int BatchSize { get; private set; } = 1000;

        //分批加载每批间隔时间（毫秒）
        public static int BatchInterval { get; private set; } = 100;

        //文件的大小下限
        public static long? FileSizeLowerLimit { get; private set; } = null;

        //文件的大小上限
        public static long? FileSizeUpperLimit { get; private set; } = null;

        //文件夹的大小下限
        public static long? FolderSizeLowerLimit { get; private set; } = null;

        //文件夹的大小上限
        public static long? FolderSizeUpperLimit { get; private set; } = null;

        //文件夹中文件数量下限
        public static int? FileCountLowerLimit { get; private set; } = null;

        //文件夹中文件数量上限
        public static int? FileCountUpperLimit { get; private set; } = null;

        //文件或文件夹信息更新时是否更新获取的数据
        public static bool EnableUpdateGottenData { get; private set; } = false;

        //文件或文件夹信息更新时是否更新导入的数据
        public static bool EnableUpdateImportedData { get; private set; } = false;

        //是否开机启动启动程序
        public static bool Autostart { get; private set; } = false;


        //点击关闭窗口按钮时是否改为最小化窗口
        public static bool MinimizeWhenClosing { get; private set; } = true;

        //退出程序时是否自动导出当前查询结果
        public static bool ExportWhenExit { get;private set; } = true;

        //退出程序时自动导出当前查询结果的导出文件路径
        public static string ExportWhenExitPath { get; private set; } = "ExportedData.json";

        //启动程序时是否获取数据
        public static bool GetDataWhenStart { get; private set; } = false;

        ////启动程序时获取数据的待获取文件路径
        public static string GetDataWhenStartPath { get; private set; } = ""; // 初始化较复杂，默认值在DefaultConfig的静态构造方法中实现

        //启动程序时是否导入数据
        public static bool ImportDataWhenStart { get; private set; } = false;

        //启动程序时导入数据的数据文件路径
        public static string ImportDataWhenStartPath { get; private set; } = "ExportedData.json";

        // 获取时是否显示已获取文件数
        public static bool EnableShowFileCountWhenGetting { get; private set; } = true;

        // 获取时显示文件数的更新间隔时间
        public static int ShowFileCountInterval { get; private set; } = 100;

        // 是否只能打开一个此程序
        public static bool OnlyOneProcessAllowed { get; private set; } = false;

        #endregion

        /// <summary>
        /// 默认配置
        /// </summary>
        /// 2024.6.3
        /// version 2.0.0
        private static class DefaultConfig
        {
            public static readonly string Language = Localization.GetLanguageName(System.Globalization.CultureInfo.CurrentCulture.Name);
            public static readonly SearchRules SearchRule = SearchRules.Contain;
            public static readonly bool SearchFiles = true;
            public static readonly bool SearchFolders = true;
            public static readonly bool RecursiveSearch = true;
            public static readonly bool CaseSensitive = false;
            public static readonly bool MatchFullName = false;
            public static readonly bool SearchFromRoot = false;
            public static readonly bool EnableBatchLoad = true;
            public static readonly int BatchLoadThreshold = 1000;
            public static readonly int BatchSize = 1000;
            public static readonly int BatchInterval = 100;
            public static readonly long? FileSizeLowerLimit = null;
            public static readonly long? FileSizeUpperLimit = null;
            public static readonly long? FolderSizeLowerLimit = null;
            public static readonly long? FolderSizeUpperLimit = null;
            public static readonly int? FileCountLowerLimit = null;
            public static readonly int? FileCountUpperLimit = null;
            public static readonly bool EnableUpdateGottenData = false;
            public static readonly bool EnableUpdateImportedData = false;
            public static readonly bool Autostart = false;
            public static readonly bool MinimizeWhenClosing = false;
            public static readonly bool ExportWhenExit = false;
            public static readonly string ExportWhenExitPath = "ExportedData.json";
            public static readonly bool GetDataWhenStart = false;
            public static readonly string GetDataWhenStartPath;
            public static readonly bool ImportDataWhenStart = false;
            public static readonly string ImportDataWhenStartPath = "ExportedData.json";
            public static readonly bool EnableShowFileCountWhenGetting = true;
            public static readonly int ShowFileCountInterval  = 100;
            public static readonly bool OnlyOneProcessAllowed = false;

            static DefaultConfig()
            {
                // GetDataWhenStartPath的默认值是所有盘符。例如当前计算机包含C盘和D盘，则GetDataWhenStartPath = @"C:\|D:\"
                List<string> disks = new List<string>();
                try
                {
                    foreach (DriveInfo di in DriveInfo.GetDrives())
                    {
                        disks.Add(di.Name);
                    }
                }
                catch { }

                GetDataWhenStartPath = string.Join('|', disks);
            }

        }

        /// <summary>
        /// 读取默认的配置信息
        /// </summary>
        /// 2024.6.3
        /// version 2.0.0
        public static void LoadDefault()
        {
            Language = DefaultConfig.Language;
            SearchRule = DefaultConfig.SearchRule;
            SearchFiles = DefaultConfig.SearchFiles;
            SearchFolders = DefaultConfig.SearchFolders;
            RecursiveSearch = DefaultConfig.RecursiveSearch;
            CaseSensitive = DefaultConfig.CaseSensitive;
            MatchFullName = DefaultConfig.MatchFullName;
            SearchFromRoot = DefaultConfig.SearchFromRoot;
            EnableBatchLoad = DefaultConfig.EnableBatchLoad;
            BatchLoadThreshold = DefaultConfig.BatchLoadThreshold;
            BatchSize = DefaultConfig.BatchSize;
            BatchInterval = DefaultConfig.BatchInterval;
            FileSizeLowerLimit = DefaultConfig.FileSizeLowerLimit;
            FileSizeUpperLimit = DefaultConfig.FileSizeUpperLimit;
            FolderSizeLowerLimit = DefaultConfig.FolderSizeLowerLimit;
            FolderSizeUpperLimit = DefaultConfig.FolderSizeUpperLimit;
            FileCountLowerLimit = DefaultConfig.FileCountLowerLimit;
            FileCountUpperLimit = DefaultConfig.FileCountUpperLimit;
            EnableUpdateGottenData = DefaultConfig.EnableUpdateGottenData;
            EnableUpdateImportedData = DefaultConfig.EnableUpdateImportedData;
            Autostart = DefaultConfig.Autostart;
            MinimizeWhenClosing = DefaultConfig.MinimizeWhenClosing;
            ExportWhenExit = DefaultConfig.ExportWhenExit;
            ExportWhenExitPath = DefaultConfig.ExportWhenExitPath;
            GetDataWhenStart = DefaultConfig.GetDataWhenStart;
            GetDataWhenStartPath = DefaultConfig.GetDataWhenStartPath;
            ImportDataWhenStart = DefaultConfig.ImportDataWhenStart;
            ImportDataWhenStartPath = DefaultConfig.ImportDataWhenStartPath;
            EnableShowFileCountWhenGetting = DefaultConfig.EnableShowFileCountWhenGetting;
            ShowFileCountInterval = DefaultConfig.ShowFileCountInterval;
            OnlyOneProcessAllowed = DefaultConfig.OnlyOneProcessAllowed;
        }

        /// <summary>
        /// 将配置信息导出为json
        /// </summary>
        /// 2024.6.3
        /// version 2.0.0
        /// <param name="path"></param>
        public static JsonObject ToJson()
        {
            JsonObject json = new JsonObject();
            json["Version"] = Version.CurrentVersion;
            json["Language"] = Language;
            json["SearchRule"] = (int)SearchRule;
            json["SearchFiles"] = SearchFiles;
            json["SearchFolders"] = SearchFolders;
            json["RecursiveSearch"] = RecursiveSearch;
            json["CaseSensitive"] = CaseSensitive;
            json["MatchFullName"] = MatchFullName;
            json["SearchFromRoot"] = SearchFromRoot;
            json["EnableBatchLoad"] = EnableBatchLoad;
            json["BatchLoadThreshold"] = BatchLoadThreshold;
            json["BatchSize"] = BatchSize;
            json["BatchInterval"] = BatchInterval;
            json["FileSizeLowerLimit"] = FileSizeLowerLimit;
            json["FileSizeUpperLimit"] = FileSizeUpperLimit;
            json["FolderSizeLowerLimit"] = FolderSizeLowerLimit;
            json["FolderSizeUpperLimit"] = FolderSizeUpperLimit;
            json["FileCountLowerLimit"] = FileCountLowerLimit;
            json["FileCountUpperLimit"] = FileCountUpperLimit;
            json["EnableUpdateGottenData"] = EnableUpdateGottenData;
            json["EnableUpdateImportedData"] = EnableUpdateImportedData;
            json["Autostart"] = Autostart;
            json["MinimizeWhenClosing"] = MinimizeWhenClosing;
            json["ExportWhenExit"] = ExportWhenExit;
            json["ExportWhenExitPath"] = ExportWhenExitPath;
            json["GetDataWhenStart"] = GetDataWhenStart;
            json["GetDataWhenStartPath"] = GetDataWhenStartPath;
            json["ImportDataWhenStart"] = ImportDataWhenStart;
            json["ImportDataWhenStartPath"] = ImportDataWhenStartPath;
            json["EnableShowFileCountWhenGetting"] = EnableShowFileCountWhenGetting;
            json["ShowFileCountInterval"] = ShowFileCountInterval;
            json["OnlyOneProcessAllowed"] = OnlyOneProcessAllowed;
            return json;
        }

        /// <summary>
        /// 将配置信息保存到配置文件
        /// </summary>
        /// 2024.2.8
        /// version 2.0.0
        /// <param name="path"></param>
        public static void Save(string path = "config.json")
        {
            JsonObject json = ToJson();
            try
            {
                System.IO.File.WriteAllText(path, json.ToString());
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 从json对象中读取配置信息。若不存在对应项则使用默认值
        /// 程序开始时调用此方法
        /// </summary>
        /// 2024.6.3
        /// version 2.0.0
        /// <param name="json"></param>
        public static void Load(JsonObject json)
        {
            Language = json.Get<string>("Language", DefaultConfig.Language);
            SearchRule = ConvertIntToEnumUtil.Convert(json.Get<int>("SearchRule", (int)DefaultConfig.SearchRule), DefaultConfig.SearchRule);
            SearchFiles = json.Get<bool>("SearchFiles", DefaultConfig.SearchFiles);
            SearchFolders = json.Get<bool>("SearchFolders", DefaultConfig.SearchFolders);
            RecursiveSearch = json.Get<bool>("RecursiveSearch", DefaultConfig.RecursiveSearch);
            CaseSensitive = json.Get<bool>("CaseSensitive", DefaultConfig.CaseSensitive);
            MatchFullName = json.Get<bool>("MatchFullName", DefaultConfig.MatchFullName);
            SearchFromRoot = json.Get<bool>("SearchFromRoot", DefaultConfig.SearchFromRoot);
            EnableBatchLoad = json.Get<bool>("EnableBatchLoad", DefaultConfig.EnableBatchLoad);
            BatchLoadThreshold = json.Get<int>("BatchLoadThreshold", DefaultConfig.BatchLoadThreshold);
            BatchSize = json.Get<int>("BatchSize", DefaultConfig.BatchSize);
            BatchInterval = json.Get<int>("BatchInterval", DefaultConfig.BatchInterval);
            FileSizeLowerLimit = json.Get<long?>("FileSizeLowerLimit", DefaultConfig.FileSizeLowerLimit);
            FileSizeUpperLimit = json.Get<long?>("FileSizeUpperLimit", DefaultConfig.FileSizeUpperLimit);
            FolderSizeLowerLimit = json.Get<long?>("FolderSizeLowerLimit", DefaultConfig.FolderSizeLowerLimit);
            FolderSizeUpperLimit = json.Get<long?>("FolderSizeUpperLimit", DefaultConfig.FolderSizeUpperLimit);
            FileCountLowerLimit = json.Get<int?>("FileCountLowerLimit", DefaultConfig.FileCountLowerLimit);
            FileCountUpperLimit = json.Get<int?>("FileCountUpperLimit", DefaultConfig.FileCountUpperLimit);
            EnableUpdateGottenData = json.Get<bool>("EnableUpdateGottenData", DefaultConfig.EnableUpdateGottenData);
            EnableUpdateImportedData = json.Get<bool>("EnableUpdateImportedData", DefaultConfig.EnableUpdateImportedData);
            Autostart = json.Get<bool>("Autostart", DefaultConfig.Autostart);
            MinimizeWhenClosing = json.Get<bool>("MinimizeWhenClosing", DefaultConfig.MinimizeWhenClosing);
            ExportWhenExit = json.Get<bool>("ExportWhenExit", DefaultConfig.ExportWhenExit);
            ExportWhenExitPath = json.Get<string>("ExportWhenExitPath", DefaultConfig.ExportWhenExitPath);
            GetDataWhenStart = json.Get<bool>("GetDataWhenStart", DefaultConfig.GetDataWhenStart);
            GetDataWhenStartPath = json.Get<string>("GetDataWhenStartPath", DefaultConfig.GetDataWhenStartPath);
            ImportDataWhenStart = json.Get<bool>("ImportDataWhenStart", DefaultConfig.ImportDataWhenStart);
            ImportDataWhenStartPath = json.Get<string>("ImportDataWhenStartPath", DefaultConfig.ImportDataWhenStartPath);
            EnableShowFileCountWhenGetting = json.Get<bool>("EnableShowFileCountWhenGetting", DefaultConfig.EnableShowFileCountWhenGetting);
            ShowFileCountInterval = json.Get<int>("ShowFileCountInterval", DefaultConfig.ShowFileCountInterval);
            OnlyOneProcessAllowed = json.Get<bool>("OnlyOneProcessAllowed", DefaultConfig.OnlyOneProcessAllowed);

            CheckValidation();
        }

        /// <summary>
        /// 从json字符串中读取配置信息。若不存在对应项则使用默认值
        /// 程序开始时调用此方法
        /// </summary>
        /// 2024.2.8
        /// version 2.0.0
        /// <param name="json"></param>
        public static void Load(string json)
        {
            try
            {
                Load(JsonObject.Parse(json));

            }
            catch (Exception)
            {
                LoadDefault();
            }
        }

        /// <summary>
        /// 从配置文件中读取配置信息。若不存在配置文件或配置文件中不存在对应项则使用默认值
        /// 程序开始时调用此方法
        /// </summary>
        /// 2023.12.20
        /// version 1.4.0
        /// <param name="path"></param>
        public static void LoadFromFile(string path = "config.json")
        {
            try
            {
                Load(System.IO.File.ReadAllText(path));
            }
            catch (Exception)
            {
                LoadDefault();
            }
        }

        /// <summary>
        /// 加载数据完成后检验加载的数据是否有效，并将无效的数据设置为默认值
        /// </summary>
        /// 2024.6.2
        /// version 2.0.0
        private static void CheckValidation()
        {
            if (BatchLoadThreshold < 0) BatchLoadThreshold = DefaultConfig.BatchLoadThreshold;
            if (BatchSize < 0) BatchSize = DefaultConfig.BatchSize;
            if (BatchInterval < 0) BatchInterval = DefaultConfig.BatchInterval;
            if (!SearchFiles && !SearchFolders) { SearchFiles = DefaultConfig.SearchFiles; SearchFolders = DefaultConfig.SearchFolders; }
            if (FileSizeLowerLimit != null && FileSizeLowerLimit < 0L) FileSizeLowerLimit = DefaultConfig.FileSizeLowerLimit;
            if (FileSizeUpperLimit != null && FileSizeUpperLimit < 0L) FileSizeUpperLimit = DefaultConfig.FileSizeUpperLimit;
            if (FileSizeLowerLimit != null && FileSizeUpperLimit != null && FileSizeLowerLimit > FileSizeUpperLimit)
            {
                FileSizeLowerLimit = DefaultConfig.FileSizeLowerLimit;
                FileSizeUpperLimit = DefaultConfig.FileSizeUpperLimit;
            }
            if (FolderSizeLowerLimit != null && FolderSizeLowerLimit < 0L) FolderSizeLowerLimit = DefaultConfig.FolderSizeLowerLimit;
            if (FolderSizeUpperLimit != null && FolderSizeUpperLimit < 0L) FolderSizeUpperLimit = DefaultConfig.FolderSizeUpperLimit;
            if (FolderSizeLowerLimit != null && FolderSizeUpperLimit != null && FolderSizeLowerLimit > FolderSizeUpperLimit)
            {
                FolderSizeLowerLimit = DefaultConfig.FolderSizeLowerLimit;
                FileSizeUpperLimit = DefaultConfig.FileSizeUpperLimit;
            }
            if (FileCountLowerLimit != null && FileCountLowerLimit < 0) FileCountLowerLimit = DefaultConfig.FileCountLowerLimit;
            if (FileCountUpperLimit != null && FileCountUpperLimit < 0) FileCountUpperLimit = DefaultConfig.FileCountUpperLimit;
            if (FileCountLowerLimit != null && FileCountUpperLimit != null && FileCountLowerLimit > FileCountUpperLimit)
            {
                FileCountLowerLimit = DefaultConfig.FileCountLowerLimit;
                FileCountUpperLimit = DefaultConfig.FileCountUpperLimit;
            }
            if (ShowFileCountInterval < 0) ShowFileCountInterval = DefaultConfig.ShowFileCountInterval;

        }
    }
}
