using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;

namespace GetFolderSize
{
    /// <summary>
    /// 搜索规则
    /// <para>2023.12.15</para>
    /// <para>version 1.4.0</para>
    /// </summary>
    public enum SearchRules
    {
        Include,    // 包含
        Same,       // 相同
        Regular,    // 正则
    }


    /// <summary>
    /// 配置信息
    /// <para>2023.12.20</para>
    /// <para>version 1.4.0</para>
    /// </summary>
    public static class Config
    {
        //语言
        private static string language = Localization.GetLanguageName(System.Globalization.CultureInfo.CurrentCulture.Name);
        public static string Language { get { return language; } private set { language = value; } }

        //搜索规则
        private static SearchRules searchRule = SearchRules.Include;
        public static SearchRules SearchRule { get { return searchRule; } private set { searchRule = value; } }

        //是否搜索文件
        private static bool searchFiles = true;
        public static bool SearchFiles { get { return searchFiles; } private set { searchFiles = value; } }

        //是否搜索文件夹
        private static bool searchFolders = true;
        public static bool SearchFolders { get { return searchFolders; } private set { searchFolders = value; } }

        //是否递归搜索
        private static bool recursiveSearch = true;
        public static bool RecursiveSearch { get { return recursiveSearch; } private set { recursiveSearch = value; } }

        //是否区分大小写
        private static bool caseSensitive = false;
        public static bool CaseSensitive { get { return caseSensitive; } private set { caseSensitive = value; } }

        //是否启用分批加载
        private static bool enableBatchLoad = true;
        public static bool EnableBatchLoad { get { return enableBatchLoad; } private set { enableBatchLoad = value; } }

        //使用分批加载的项数阈值
        private static int batchLoadThreshold = 1000;
        public static int BatchLoadThreshold { get { return batchLoadThreshold; } private set { batchLoadThreshold = value; } }

        //分批加载时每批的数量
        private static int batchSize = 1000;
        public static int BatchSize { get { return batchSize; } private set { batchSize = value; } }

        //分批加载每批间隔时间（毫秒）
        private static int batchInterval = 100;
        public static int BatchInterval { get { return batchInterval; } private set { batchInterval = value; } }

        //文件的大小下限
        private static long? fileSizeLowerLimit = null;
        public static long? FileSizeLowerLimit { get { return fileSizeLowerLimit; } private set { fileSizeLowerLimit = value; } }

        //文件的大小上限
        private static long? fileSizeUpperLimit = null;
        public static long? FileSizeUpperLimit { get { return fileSizeUpperLimit; } private set { fileSizeUpperLimit = value; } }

        //文件夹的大小下限
        private static long? folderSizeLowerLimit = null;
        public static long? FolderSizeLowerLimit { get { return folderSizeLowerLimit; } private set { folderSizeLowerLimit = value; } }

        //文件夹的大小上限
        private static long? folderSizeUpperLimit = null;
        public static long? FolderSizeUpperLimit { get { return folderSizeUpperLimit; } private set { folderSizeUpperLimit = value; } }

        //文件夹中文件数量下限
        private static int? fileCountLowerLimit = null;
        public static int? FileCountLowerLimit { get { return fileCountLowerLimit; } private set { fileCountLowerLimit = value; } }

        //文件夹中文件数量上限
        private static int? fileCountUpperLimit = null;
        public static int? FileCountUpperLimit { get { return fileCountUpperLimit; } private set { fileCountUpperLimit = value; } }

        /// <summary>
        /// 默认配置
        /// <para>2023.12.20</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        private static class DefaultConfig
        {
            public static string language = Localization.GetLanguageName(System.Globalization.CultureInfo.CurrentCulture.Name);
            public static SearchRules searchRule = SearchRules.Include;
            public static bool searchFiles = true;
            public static bool searchFolders = true;
            public static bool recursiveSearch = true;
            public static bool caseSensitive = false;
            public static bool enableBatchLoad = true;
            public static int batchLoadThreshold = 1000;
            public static int batchSize = 1000;
            public static int batchInterval = 100;
            public static long? fileSizeLowerLimit = null;
            public static long? fileSizeUpperLimit = null;
            public static long? folderSizeLowerLimit = null;
            public static long? folderSizeUpperLimit = null;
            public static int? fileCountLowerLimit = null;
            public static int? fileCountUpperLimit = null;
            
        }

        

        /// <summary>
        /// 读取默认的配置信息
        /// <para>2023.12.20</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        public static void LoadDefault()
        {
            language = DefaultConfig.language;
            searchRule = DefaultConfig.searchRule;
            searchFiles = DefaultConfig.searchFiles;
            searchFolders = DefaultConfig.searchFolders;
            recursiveSearch = DefaultConfig.recursiveSearch;
            caseSensitive = DefaultConfig.caseSensitive;
            enableBatchLoad = DefaultConfig.enableBatchLoad;
            batchLoadThreshold = DefaultConfig.batchLoadThreshold;
            batchSize = DefaultConfig.batchSize;
            batchInterval = DefaultConfig.batchInterval;
            fileSizeLowerLimit = DefaultConfig.fileSizeLowerLimit;
            fileSizeUpperLimit = DefaultConfig.fileSizeUpperLimit;
            folderSizeLowerLimit = DefaultConfig.folderSizeLowerLimit;
            folderSizeUpperLimit = DefaultConfig.folderSizeUpperLimit;
            fileCountLowerLimit = DefaultConfig.fileCountLowerLimit;
            fileCountUpperLimit = DefaultConfig.fileCountUpperLimit;
        }

        /// <summary>
        /// 将配置信息导出为json
        /// <para>2023.12.20</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="path"></param>
        public static JObject ToJson()
        {
            JObject json = new JObject();
            json["Language"] = language;
            json["SearchRule"] = (int)searchRule;
            json["SearchFiles"] = searchFiles;
            json["SearchFolders"] = searchFolders;
            json["RecursiveSearch"] = recursiveSearch;
            json["CaseSensitive"] = caseSensitive;
            json["EnableBatchLoad"] = enableBatchLoad;
            json["BatchLoadThreshold"] = batchLoadThreshold;
            json["BatchSize"] = batchSize;
            json["BatchInterval"] = batchInterval;
            json["FileSizeLowerLimit"] = fileSizeLowerLimit;
            json["FileSizeUpperLimit"] = fileSizeUpperLimit;
            json["FolderSizeLowerLimit"] = folderSizeLowerLimit;
            json["FolderSizeUpperLimit"] = folderSizeUpperLimit;
            json["FileCountLowerLimit"] = fileCountLowerLimit;
            json["FileCountUpperLimit"] = fileCountUpperLimit;
            return json;
        }

        /// <summary>
        /// 将配置信息保存到配置文件
        /// <para>2023.12.20</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="path"></param>
        public static void Save(string path="config.json")
        {
            JObject json = ToJson();
            try
            {
                File.WriteAllText(path, json.ToString());
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 从json对象中读取配置信息。若不存在对应项则使用默认值
        /// 程序开始时调用此方法
        /// <para>2023.12.20</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="json"></param>
        public static void Load(JObject json)
        {
            language = JsonUtil.GetValue<string>(json, "Language", DefaultConfig.language);
            searchRule = (SearchRules)(JsonUtil.GetValue<int>(json, "SearchRule", (int)DefaultConfig.searchRule));
            searchFiles = JsonUtil.GetValue<bool>(json, "SearchFiles", DefaultConfig.searchFiles);
            searchFolders = JsonUtil.GetValue<bool>(json, "SearchFolders", DefaultConfig.searchFolders);
            recursiveSearch = JsonUtil.GetValue<bool>(json, "RecursiveSearch", DefaultConfig.recursiveSearch);
            caseSensitive = JsonUtil.GetValue<bool>(json, "CaseSensitive", DefaultConfig.caseSensitive);
            enableBatchLoad = JsonUtil.GetValue<bool>(json, "EnableBatchLoad", DefaultConfig.enableBatchLoad);
            batchLoadThreshold = JsonUtil.GetValue<int>(json, "BatchLoadThreshold", DefaultConfig.batchLoadThreshold);
            batchSize = JsonUtil.GetValue<int>(json, "BatchSize", DefaultConfig.batchSize);
            batchInterval = JsonUtil.GetValue<int>(json, "BatchInterval", DefaultConfig.batchInterval);
            fileSizeLowerLimit = JsonUtil.GetLong(json, "FileSizeLowerLimit", DefaultConfig.fileSizeLowerLimit);
            fileSizeUpperLimit = JsonUtil.GetLong(json, "FileSizeUpperLimit", DefaultConfig.fileSizeUpperLimit);
            folderSizeLowerLimit = JsonUtil.GetLong(json, "FolderSizeLowerLimit", DefaultConfig.folderSizeLowerLimit);
            folderSizeUpperLimit = JsonUtil.GetLong(json, "FolderSizeUpperLimit", DefaultConfig.folderSizeUpperLimit);
            fileCountLowerLimit = JsonUtil.GetInt(json, "FileCountLowerLimit", DefaultConfig.fileCountLowerLimit);
            fileCountUpperLimit = JsonUtil.GetInt(json, "FileCountUpperLimit", DefaultConfig.fileCountUpperLimit);

        }

        /// <summary>
        /// 从json字符串中读取配置信息。若不存在对应项则使用默认值
        /// 程序开始时调用此方法
        /// <para>2023.12.20</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="json"></param>
        public static void Load(string json)
        {
            try
            {
                Load(JObject.Parse(json));
                
            }
            catch (Exception) {
                LoadDefault(); 
            }
        }


        /// <summary>
        /// 从配置文件中读取配置信息。若不存在配置文件或配置文件中不存在对应项则使用默认值
        /// 程序开始时调用此方法
        /// <para>2023.12.20</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="path"></param>
        public static void LoadFromFile(string path = "config.json")
        {
            try
            {
                Load(File.ReadAllText(path));
            }
            catch(Exception) {
                LoadDefault();
            }
        }


    }
}
