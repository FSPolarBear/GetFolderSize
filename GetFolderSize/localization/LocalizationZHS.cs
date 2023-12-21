using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFolderSize
{
    /// <summary>
    /// 简体中文本地化
    /// <para>2023.12.20</para>
    /// <para>version 1.4.0</para>
    /// </summary>
    public class LocalizationZHS: Localization
    {

        protected LocalizationZHS() 
        {
            Name = "ZHS";

            Main_Title = "GetFolderSize";
            Main_Label_Path = "路径:";
            Main_Button_Ok = "获取";
            Main_Label_Search = "搜索:";
            Main_Button_Search = "搜索";
            Main_Button_Root = "根文件夹";
            Main_Button_Back = "返回上级";
            Main_Button_Export = "导出";
            Main_Button_Import = "导入";
            Main_Button_ShowInExplorer = "在资源管理器中显示";
            Main_Button_Refresh = "刷新";
            Main_Button_Config = "设置";
            Main_ListViewColumn_Name = "名字";
            Main_ListViewColumn_Type = "类型";
            Main_ListViewColumn_Size = "大小";
            Main_ListViewColumn_FileCount = "文件数";
            Main_ListViewColumn_LastWriteTime = "最后修改时间";

            Main_Alert_Getting = "正在获取...";
            Main_Alert_Importing = "正在导入...";
            Main_Alert_Searching = "正在搜索...";
            Main_Alert_FolderNotFound = "未找到文件夹";
            Main_Alert_ExportSucceed = "导出成功";
            Main_Alert_ExportFailed = "导入成功";
            Main_Alert_ImportFailed = "导入失败";
            Main_Alert_SearchTextEmpty = "搜索内容为空";
            Main_Alert_TooManyItems = "项数太多无法排序";

            Config_Title = "设置";
            Config_Label_Language = "语言:";
            Config_Button_Default = "默认";
            Config_GroupBox_BatchLoad = "分批加载";
            Config_CheckBox_EnableBatchLoad = "启用分批加载";
            Config_Label_BatchLoadThreshold = "分批加载阈值:";
            Config_Label_BatchSize = "批大小:";
            Config_Label_BatchInterval = "批间隔(毫秒):";
            Config_GroupBox_Search = "搜索";
            Config_Label_SearchRule = "搜索规则:";
            Config_CheckBox_SearchFiles = "搜索文件";
            Config_CheckBox_SearchFolders = "搜索文件夹";
            Config_CheckBox_RecursiveSearch = "递归搜索";
            Config_CheckBox_CaseSensitive = "区分大小写";
            Config_SearchRules_Include = "包含";
            Config_SearchRules_Same = "相同";
            Config_SearchRules_Regular = "正则";
            Config_Label_FileSizeLimit = "文件大小限制:";
            Config_Label_FolderSizeLimit = "文件夹大小限制:";
            Config_Label_FileCountLimit = "包含文件数限制:";
            Config_ExceptionMessage_BatchLoadThreshold_ShouldBeNonNegativeInteger = "分批加载阈值应为非负整数。";
            Config_ExceptionMessage_BatchSize_ShouldBePositiveInteger = "批大小应为正整数。";
            Config_ExceptionMessage_BatchInterval_ShouldBeNonNegativeInteger = "批间隔应为非负整数。";
            Config_ExceptionMessage_SearchFilesFolders_AtLeastOneShouldBeChecked = "应选中“搜索文件”和“搜索文件夹”中的至少一个。";
            Config_ExceptionMessage_FileOrFolderSizeLimit_ShouldBeNonNegativeIntegerWithUnitOrEmpty = "文件或文件夹大小限制应为带有单位(B、KB、MB、GB、TB)的非负整数，如“1KB”，或空字符串以表示不设置限制。";
            Config_ExceptionMessage_FileCounLimit_ShouldBeNonNegativeIntegerOrEmpty = "包含文件数限制应为非负整数，或空字符串以表示不设置限制。";

            FolderOrFile_Folder = "文件夹";
            FolderOrFile_File = "文件";
            FolderOrFile_SearchResult = "搜索结果";

            Ok = "确定";
            Cancel = "取消";
            FromTo = "~";
    }
        private static Localization? instance;

        public new static Localization GetInstance()
        {
            if (instance == null)
            {
                instance = new LocalizationZHS();
            }
            return instance;
        }




    }
}
