using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFolderSize
{



    /// <summary>
    /// 本地化信息父类。默认为英语
    /// </summary>
    /// 2024.6.14
    /// version 2.0.0
    internal abstract class Localization
    {
        public readonly static string[] LANGUAGES = new string[] { "EN", "ZHS" };


        protected Localization()
        {
            Name = "";

            Localizations = new Language[]
            {
                new Language("EN", "English"),
                new Language("ZHS", "简体中文")
            };

            Main_Title = "GetFolderSize";
            Main_Label_Path = "Path:";
            Main_Button_Ok = "Get";
            Main_Button_ViewFolder = "View";
            Main_Button_Cancel = "Cancel";
            Main_Label_Search = "Search:";
            Main_Button_Search = "Search";
            Main_Button_Root = "Root";
            Main_Button_Back = "Back";
            Main_Button_Export = "Export";
            Main_Button_ExportCurrent = "Export current";
            Main_Button_Import = "Import";
            Main_Button_ShowInExplorer = "Show in explorer";
            Main_Button_Refresh = "Refresh";
            Main_Button_Config = "Setting";
            Main_ListViewColumn_Name = "Name";
            Main_ListViewColumn_Type = "Type";
            Main_ListViewColumn_Size = "Size";
            Main_ListViewColumn_FileCount = "File count";
            Main_ListViewColumn_LastWriteTime = "Last write time";

            Main_Alert_Getting = "Getting...";
            Main_Alert_Format_GettingWithFileCount = "Getting... {0} files have been gotten";
            Main_Alert_Importing = "Importing...";
            Main_Alert_Searching = "Searching...";
            Main_Alert_FolderNotFound = "The folder is not found";
            Main_Alert_ExportSucceed = "Export succeed";
            Main_Alert_ExportFailed = "Export failed";
            Main_Alert_ImportFailed = "Import failed";
            Main_Alert_SearchTextEmpty = "The search text is empty";
            Main_Alert_TooManyItems = "Too many items to sort";
            Main_Alert_RegularExpressionIncorrect = "The regular expression is incorrect";
            Main_Alert_WildcardIncorrect = "The wildcard is incorrect";
            Main_Alert_ConditionStringIncorrect = "The string of conditions is incorrect";

            Config_Title = "Setting";
            Config_Label_Language = "Language:";
            Config_Button_Save = "Save";
            Config_Button_Load = "Load";
            Config_Button_Default = "Default";
            Config_GroupBox_BatchLoad = "Batch loading";
            Config_CheckBox_EnableBatchLoad = "Enable batch loading";
            Config_Label_BatchLoadThreshold = "Batch loading threshold:";
            Config_Label_BatchSize = "Batch size:";
            Config_Label_BatchInterval = "Batch interval (ms):";
            Config_GroupBox_Search = "Search";
            Config_Label_SearchRule = "Search rule";
            Config_CheckBox_SearchFiles = " Search files";
            Config_CheckBox_SearchFolders = "Search folders";
            Config_CheckBox_RecursiveSearch = "Recursive search";
            Config_CheckBox_CaseSensitive = "Case Sensitive";
            Config_CheckBox_MatchFullName = "Match full name";
            Config_CheckBox_SearchFromRoot = "Search From Root";
            Config_SearchRules_Contain = "Contain";
            Config_SearchRules_Same = "Same";
            Config_SearchRules_Regular = "Regular";
            Config_SearchRules_Wildcard = "Wildcard";
            Config_SearchRules_Extension = "Extension";
            Config_SearchRules_Conbination = "Conbination";
            Config_SearchRules_LocalizedNames = new string[]{ "Contain", "Same", "Regular", "Wildcard", "Extension"};
            Config_Label_FileSizeLimit = "File size limit:";
            Config_Label_FolderSizeLimit = "Folder size limit:";
            Config_Label_FileCountLimit = "File count limit:";
            Config_GroupBox_UpdateDataWhenFolderOrFileChange = "Update data when folders or files change";
            Config_CheckBox_EnableUpdateGottenData = "Enable update gotten data";
            Config_CheckBox_EnableUpdateImportedData = "Enable update imported data";
            Config_GroupBox_Program = "Program";
            Config_CheckBox_Autostart = "Autostart";
            Config_CheckBox_MinimizeWhenClosing = "Minimize when closing";
            Config_CheckBox_EnableShowFileCountWhenGetting = "Show file count when getting";
            Config_Label_ShowFileCountInterval = "Interval (ms):";
            Config_CheckBox_OnlyOneProcessAllowed = "Only one GetFolderSize process is allowed to exist";
            Config_CheckBox_ExportDataWhenExit = "Export data when exit";
            Config_CheckBox_GetDataWhenStart = "Get data when start";
            Config_CheckBox_ImportDataWhenStart = "Import data when start";
            Config_Label_Path = "Path:";
            Config_Button_ViewFolder = "View";
            Config_Button_ViewFile = "View";


            Config_ExceptionMessage_BatchLoadThreshold_ShouldBeNonNegativeInteger = "The batch loadling threshold should be a non-negative integer.";
            Config_ExceptionMessage_BatchSize_ShouldBePositiveInteger = "The batch size should be a positive integer.";
            Config_ExceptionMessage_BatchInterval_ShouldBeNonNegativeInteger = "The batch interval should be a non-negative integer.";
            Config_ExceptionMessage_SearchFilesFolders_AtLeastOneShouldBeChecked = "At least one of \"Search files\" and \"Search folders\" should be checked.";
            Config_ExceptionMessage_FileOrFolderSizeLimit_ShouldBeNonNegativeIntegerWithUnitOrEmpty = "The file or folder size limit should be a non-negative integer with unit (B, KB, MB, GB, TB) such as \"1KB\", or an empty string to indicate no limit.";
            Config_ExceptionMessage_FileCounLimit_ShouldBeNonNegativeIntegerOrEmpty = "The file count limit should be a non-negative integer, or an empty string to indicate no limit.";
            Config_ExceptionMessage_FileOrFolderSizeLimit_LowerCannotBeLargerThanHigher = "The file or fiolder size lower limit cannot be larger than the higher limit.";
            Config_ExceptionMessage_FileCounLimit_LowerCannotBeLargerThanHigher = "The file count lower limit cannot be larger than the higher limit.";
            Config_ExceptionMessage_ShowFileCountInterval_ShouldBeNonNegativeInteger = "The interval of updating file count when getting should be a non-negative integer.";
            


            FolderOrFile_Folder = "Folder";
            FolderOrFile_File = "File";
            FolderOrFile_SearchResult = "Search result";

            Ok = "Ok";
            Cancel = "Cancel";
            FromTo = "~";
            Exit = "Exit";
            Apply = "Apply";

            LocalizedColon = ':';
        }
        private static Localization? instance;

        /// <summary>
        /// 获取本地化对象实例。依据当前系统语言选择本地化
        /// </summary>
        /// 2023.12.18
        /// version 1.4.0
        /// <returns>本地化对象实例</returns>
        public static Localization GetInstance()
        {
            string local = System.Globalization.CultureInfo.CurrentCulture.Name;
            return GetInstance(GetLanguageName(local));
        }

        /// <summary>
        /// 获取本地化对象实例。依据指定语言选择本地化
        /// </summary>
        /// 2023.12.18
        /// version 1.4.0
        /// <param name="language">指定的语言</param>
        /// <param name="forceUpdate">是否强制生成新实例。若为false，则仅在实例为null时生成实例</param>
        /// <returns>本地化对象实例</returns>
        public static Localization GetInstance(string language, bool forceUpdate=false)
        {
            if (instance == null || forceUpdate)
            {
                switch (language)
                {
                    case "ZHS":
                        instance = LocalizationZHS.GetInstance();
                        break;
                    case "EN":
                    default:
                        instance = LocalizationEN.GetInstance();
                        break;
                }
            }
            return instance;
        }


        public string Name { get; protected set; }

        public Language[] Localizations { get; protected set; }

        public string Main_Title { get; protected set; }
        public string Main_Label_Path { get; protected set; }
        public string Main_Button_Ok { get; protected set; }
        public string Main_Button_ViewFolder { get; protected set; }
        public string Main_Button_Cancel {  get; protected set; }
        public string Main_Label_Search { get; protected set; }
        public string Main_Button_Search { get; protected set; }
        public string Main_Button_Root { get; protected set; }
        public string Main_Button_Back { get; protected set; }
        public string Main_Button_Export { get; protected set; }
        public string Main_Button_ExportCurrent { get; protected set; } 
        public string Main_Button_Import { get; protected set; }
        public string Main_Button_ShowInExplorer { get; protected set; }
        public string Main_Button_Refresh { get; protected set; }
        public string Main_Button_Config { get; protected set; }
        public string Main_ListViewColumn_Name { get; protected set; }
        public string Main_ListViewColumn_Type { get; protected set; }
        public string Main_ListViewColumn_Size { get; protected set; }
        public string Main_ListViewColumn_FileCount { get; protected set; }
        public string Main_ListViewColumn_LastWriteTime { get; protected set; }
        public string Main_Alert_RegularExpressionIncorrect { get; protected set; }
        public string Main_Alert_WildcardIncorrect { get; protected set; }
        public string Main_Alert_ConditionStringIncorrect { get; protected set; }

        public string Main_Alert_Getting { get; protected set; }
        public string Main_Alert_Format_GettingWithFileCount { get; protected set; }
        public string Main_Alert_Importing { get; protected set; }
        public string Main_Alert_Searching { get; protected set; }
        public string Main_Alert_FolderNotFound { get; protected set; }
        public string Main_Alert_ExportSucceed { get; protected set; }
        public string Main_Alert_ExportFailed { get; protected set; }
        public string Main_Alert_ImportFailed { get; protected set; }
        public string Main_Alert_SearchTextEmpty { get; protected set; }
        public string Main_Alert_TooManyItems { get; protected set; }

        public string FolderOrFile_Folder { get; protected set; }
        public string FolderOrFile_File { get; protected set; }
        public string FolderOrFile_SearchResult { get; protected set; }

        public string Config_Title { get; protected set; }
        public string Config_Label_Language { get; protected set; }
        public string Config_Button_Save { get; protected set; }
        public string Config_Button_Load { get; protected set; }
        public string Config_Button_Default { get; protected set; }
        public string Config_GroupBox_BatchLoad { get; protected set; }
        public string Config_CheckBox_EnableBatchLoad { get; protected set; }
        public string Config_Label_BatchLoadThreshold { get; protected set; }
        public string Config_Label_BatchSize { get; protected set; }
        public string Config_Label_BatchInterval {get;protected set;} 
        public string Config_GroupBox_Search { get; protected set; }
        public string Config_Label_SearchRule { get; protected set; }
        public string Config_CheckBox_SearchFiles { get; protected set; }
        public string Config_CheckBox_SearchFolders { get; protected set; }
        public string Config_CheckBox_RecursiveSearch { get; protected set; }
        public string Config_CheckBox_CaseSensitive { get; protected set; }
        public string Config_CheckBox_MatchFullName { get; protected set; }
        public string Config_CheckBox_SearchFromRoot { get; protected set; }
        public string Config_SearchRules_Contain { get; protected set; }
        public string Config_SearchRules_Same { get; protected set; }
        public string Config_SearchRules_Regular { get; protected set; }
        public string Config_SearchRules_Wildcard { get; protected set; }
        public string Config_SearchRules_Extension { get; protected set; }
        public string Config_SearchRules_Conbination { get; protected set; }
        public string[] Config_SearchRules_LocalizedNames { get; protected set; }
        public string Config_Label_FileSizeLimit { get; protected set; }
        public string Config_Label_FolderSizeLimit { get; protected set; }
        public string Config_Label_FileCountLimit { get; protected set; }
        public string Config_GroupBox_UpdateDataWhenFolderOrFileChange { get; protected set; }
        public string Config_CheckBox_EnableUpdateGottenData { get; protected set; }
        public string Config_CheckBox_EnableUpdateImportedData { get; protected set; }
        public string Config_GroupBox_Program { get; protected set; }
        public string Config_CheckBox_Autostart { get; protected set; }
        public string Config_CheckBox_MinimizeWhenClosing { get; protected set; }
        public string Config_CheckBox_EnableShowFileCountWhenGetting { get; protected set; }
        public string Config_Label_ShowFileCountInterval { get; protected set; }
        public string Config_CheckBox_OnlyOneProcessAllowed { get; protected set; }
        public string Config_CheckBox_ExportDataWhenExit { get; protected set; }
        public string Config_CheckBox_GetDataWhenStart { get; protected set; }
        public string Config_CheckBox_ImportDataWhenStart { get; protected set; }
        public string Config_Label_Path { get; protected set; }
        public string Config_Button_ViewFolder { get; protected set; }
        public string Config_Button_ViewFile {  get; protected set; }


        public string Config_ExceptionMessage_BatchLoadThreshold_ShouldBeNonNegativeInteger { get; protected set; }
        public string Config_ExceptionMessage_BatchSize_ShouldBePositiveInteger { get; protected set; }
        public string Config_ExceptionMessage_BatchInterval_ShouldBeNonNegativeInteger { get; protected set; }
        public string Config_ExceptionMessage_SearchFilesFolders_AtLeastOneShouldBeChecked { get; protected set; }
        public string Config_ExceptionMessage_FileOrFolderSizeLimit_ShouldBeNonNegativeIntegerWithUnitOrEmpty { get; protected set; }
        public string Config_ExceptionMessage_FileCounLimit_ShouldBeNonNegativeIntegerOrEmpty { get; protected set; }
        public string Config_ExceptionMessage_FileOrFolderSizeLimit_LowerCannotBeLargerThanHigher { get; protected set; }
        public string Config_ExceptionMessage_FileCounLimit_LowerCannotBeLargerThanHigher { get; protected set; }
        public string Config_ExceptionMessage_ShowFileCountInterval_ShouldBeNonNegativeInteger { get; protected set; }






        public string Ok { get; protected set;}
        public string Cancel { get; protected set;}
        public string FromTo { get; protected set; }
        public string Exit { get; protected set; }
        public string Apply { get; protected set; }

        public char LocalizedColon { get; protected set; }


        /// <summary>
        /// 将系统获取的语言名(如zh-CN)转化为此程序中使用的语言名(如ZHS)
        /// 如果为不支持的类型，默认返回英语("EN")
        /// </summary>
        /// <param name="local">使用System.Globalization.CultureInfo.CurrentCulture.Name得到的语言名</param>
        /// <returns></returns>
        public static string GetLanguageName(string local)
        {
            if (local == "zh-CHS" || local == "zh-CN" || local == "zh-SG")
            {
                return "ZHS";
            }

            return "EN";
        }


    }

    /// <summary>
    /// 语言
    /// </summary>
    /// 2023.12.18
    /// version 1.4.0
    public class Language
    {
        public string Name { get; private set; }
        public string LocalizedName { get; private set; }

        public Language(string name, string localizedName)
        {
            Name = name;
            LocalizedName = localizedName;
        }

        /// <summary>
        /// 查找Language数组中Name为给定值的项的位置
        /// 若不存在符合条件的项，则返回-1
        /// </summary>
        /// 2023.12.18
        /// version 1.4.0
        /// <param name="languages"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static int IndexOf(Language[] languages, string Name)
        {
            for (int i = 0; i < languages.Length; i++)
            {
                if (languages[i].Name == Name)
                    return i;
            }
            return -1;
        }

    }

}
