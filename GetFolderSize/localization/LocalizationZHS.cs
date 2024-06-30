using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFolderSize
{
    /// <summary>
    /// 简体中文本地化
    /// </summary>
    /// 2024.6.14
    /// version 2.0.0
    internal class LocalizationZHS: Localization
    {

        private LocalizationZHS() 
        {
            Name = "ZHS";

            Main_Title = "GetFolderSize";
            Main_Label_Path = "路径：";
            Main_Button_Ok = "获取";
            Main_Button_ViewFolder = "浏览文件夹";
            Main_Button_Cancel = "取消";
            Main_Label_Search = "搜索：";
            Main_Button_Search = "搜索";
            Main_Button_Root = "根文件夹";
            Main_Button_Back = "返回上级";
            Main_Button_Export = "导出";
            Main_Button_ExportCurrent = "导出当前页";
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
            Main_Alert_Format_GettingWithFileCount = "正在获取... 已获取{0}个文件";
            Main_Alert_Importing = "正在导入...";
            Main_Alert_Searching = "正在搜索...";
            Main_Alert_FolderNotFound = "未找到文件夹";
            Main_Alert_ExportSucceed = "导出成功";
            Main_Alert_ExportFailed = "导入成功";
            Main_Alert_ImportFailed = "导入失败";
            Main_Alert_SearchTextEmpty = "搜索内容为空";
            Main_Alert_TooManyItems = "项数太多无法排序";
            Main_Alert_RegularExpressionIncorrect = "正则表达式不正确";
            Main_Alert_WildcardIncorrect = "通配符不正确";
            Main_Alert_ConditionStringIncorrect = "条件字符串不正确";

            Config_Title = "设置";
            Config_Label_Language = "语言：";
            Config_Button_Save = "保存";
            Config_Button_Load = "读取";
            Config_Button_Default = "默认";
            Config_GroupBox_BatchLoad = "分批加载";
            Config_CheckBox_EnableBatchLoad = "启用分批加载";
            Config_Label_BatchLoadThreshold = "分批加载阈值：";
            Config_Label_BatchSize = "批大小：";
            Config_Label_BatchInterval = "批间隔(毫秒)：";
            Config_GroupBox_Search = "搜索";
            Config_Label_SearchRule = "搜索规则：";
            Config_CheckBox_SearchFiles = "搜索文件";
            Config_CheckBox_SearchFolders = "搜索文件夹";
            Config_CheckBox_RecursiveSearch = "递归搜索";
            Config_CheckBox_CaseSensitive = "区分大小写";
            Config_CheckBox_MatchFullName = "匹配全路径";
            Config_CheckBox_SearchFromRoot = "从根文件夹中搜索";
            Config_SearchRules_Contain = "包含";
            Config_SearchRules_Same = "相同";
            Config_SearchRules_Regular = "正则";
            Config_SearchRules_Wildcard = "通配符";
            Config_SearchRules_Extension = "扩展名";
            Config_SearchRules_Conbination = "组合条件";
            Config_SearchRules_LocalizedNames = new string[] { "包含", "相同", "正则", "通配符", "扩展名" };
            Config_Label_FileSizeLimit = "文件大小限制：";
            Config_Label_FolderSizeLimit = "文件夹大小限制：";
            Config_Label_FileCountLimit = "包含文件数限制：";
            Config_GroupBox_UpdateDataWhenFolderOrFileChange = "文件或文件夹变化时更新数据";
            Config_CheckBox_EnableUpdateGottenData = "更新获取的数据";
            Config_CheckBox_EnableUpdateImportedData = "更新导入的数据";
            Config_GroupBox_Program = "程序";
            Config_CheckBox_Autostart = "开机自动启动程序";
            Config_CheckBox_MinimizeWhenClosing = "关闭时最小化窗口";
            Config_CheckBox_EnableShowFileCountWhenGetting = "获取时显示已获取文件数";
            Config_Label_ShowFileCountInterval = "间隔(毫秒):";
            Config_CheckBox_OnlyOneProcessAllowed = "只允许同时启动一个GetFolderSize程序";
            Config_CheckBox_ExportDataWhenExit = "退出程序时导出数据";
            Config_CheckBox_GetDataWhenStart = "启动程序时获取数据";
            Config_CheckBox_ImportDataWhenStart = "启动程序时导入数据";
            Config_Label_Path = "路径：";
            Config_Button_ViewFolder = "浏览";
            Config_Button_ViewFile = "浏览";

            Config_ExceptionMessage_BatchLoadThreshold_ShouldBeNonNegativeInteger = "分批加载阈值应为非负整数。";
            Config_ExceptionMessage_BatchSize_ShouldBePositiveInteger = "批大小应为正整数。";
            Config_ExceptionMessage_BatchInterval_ShouldBeNonNegativeInteger = "批间隔应为非负整数。";
            Config_ExceptionMessage_SearchFilesFolders_AtLeastOneShouldBeChecked = "应选中“搜索文件”和“搜索文件夹”中的至少一个。";
            Config_ExceptionMessage_FileOrFolderSizeLimit_ShouldBeNonNegativeIntegerWithUnitOrEmpty = "文件或文件夹大小限制应为带有单位(B、KB、MB、GB、TB)的非负整数，如“1KB”，或空字符串以表示不设置限制。";
            Config_ExceptionMessage_FileCounLimit_ShouldBeNonNegativeIntegerOrEmpty = "包含文件数限制应为非负整数，或空字符串以表示不设置限制。";
            Config_ExceptionMessage_FileOrFolderSizeLimit_LowerCannotBeLargerThanHigher = "文件或文件夹大小下限不能大于上限。";
            Config_ExceptionMessage_FileCounLimit_LowerCannotBeLargerThanHigher = "包含文件数下限不能大于上限。";
            Config_ExceptionMessage_ShowFileCountInterval_ShouldBeNonNegativeInteger = "更新获取文件时文件总数的时间间隔应为非负整数。";

            FolderOrFile_Folder = "文件夹";
            FolderOrFile_File = "文件";
            FolderOrFile_SearchResult = "搜索结果";

            Ok = "确定";
            Cancel = "取消";
            FromTo = "~";
            Exit = "退出";
            Apply = "应用";

            LocalizedColon = '：';
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
