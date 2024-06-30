

namespace GetFolderSize
{
    /// <summary>
    /// 调整设置的界面
    /// </summary>
    /// 2024.6.14
    /// version 2.0.0
    public partial class ConfigForm : Form
    {
        #region 字段

        Localization localization = Localization.GetInstance(Config.Language);

        #endregion

        #region 初始化

        public ConfigForm()
        {
            InitializeComponent();
            LoadConfig();
        }

        /// <summary>
        /// 从Config类中加载配置并显示在界面上
        /// </summary>
        /// 2024.6.3
        /// version 2.0.0
        private void LoadConfig()
        {
            if (Config.Language != localization.Name)
            {
                localization = Localization.GetInstance(Config.Language, true);
            }
            InitializeLocalization();

            // 加载语言
            int index = Language.IndexOf(localization.Localizations, Config.Language);
            if (index >= 0)
            {
                comboBox_Language.SelectedIndex = index;
            }
            else
            {
                comboBox_Language.SelectedIndex = 0;
            }

            //加载程序相关配置
            checkBox_Autostart.Checked = Config.Autostart;
            AutostartUtil.SetAutostart(Config.Autostart);
            checkBox_MinimizeWhenClose.Checked = Config.MinimizeWhenClosing;
            checkBox_EnableShowFileCountWhenGetting.Checked = Config.EnableShowFileCountWhenGetting;
            SetShowFileCountEnabled(checkBox_EnableShowFileCountWhenGetting.Checked);
            textBox_ShowFileCountInterval.Text = Config.ShowFileCountInterval.ToString();
            checkBox_OnlyOneProcessAllowed.Checked = Config.OnlyOneProcessAllowed;
            checkBox_ExportWhenExit.Checked = Config.ExportWhenExit;
            checkBox_GetDataWhenStart.Checked = Config.GetDataWhenStart;
            checkBox_ImportDataWhenStart.Checked = Config.ImportDataWhenStart;
            textBox_ExportWhenExitPath.Text = Config.ExportWhenExitPath;
            textBox_GetDataWhenStartPath.Text = Config.GetDataWhenStartPath;
            textBox_ImportDataWhenStartPath.Text = Config.ImportDataWhenStartPath;
            SetExportWhenExitEnabled(checkBox_ExportWhenExit.Checked);
            SetGetDataWhenStartEnabled(checkBox_GetDataWhenStart.Checked);
            SetImportDataWhenStartEnabled(checkBox_ImportDataWhenStart.Checked);

            //加载分批加载相关配置
            checkBox_EnableBatchLoad.Checked = Config.EnableBatchLoad;
            textBox_BatchLoadThreshold.Text = Config.BatchLoadThreshold.ToString();
            textBox_BatchSize.Text = Config.BatchSize.ToString();
            textBox_BatchInterval.Text = Config.BatchInterval.ToString();
            SetBatchLoadEnable(Config.EnableBatchLoad);

            //加载搜索
            comboBox_SearchRule.SelectedIndex = (int)Config.SearchRule;
            checkBox_SearchFiles.Checked = Config.SearchFiles;
            checkBox_SearchFolders.Checked = Config.SearchFolders;
            checkBox_RecursiveSearch.Checked = Config.RecursiveSearch;
            checkBox_CaseSensitive.Checked = Config.CaseSensitive;
            checkBox_MatchFullName.Checked = Config.MatchFullName;
            checkBox_SearchFromRoot.Checked = Config.SearchFromRoot;
            textBox_FileSizeLowerLimit.Text = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(Config.FileSizeLowerLimit);
            textBox_FileSizeUpperLimit.Text = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(Config.FileSizeUpperLimit);
            textBox_FolderSizeLowerLimit.Text = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(Config.FolderSizeLowerLimit);
            textBox_FolderSizeUpperLimit.Text = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(Config.FolderSizeUpperLimit);
            textBox_FileCountLowerLimit.Text = Config.FileCountLowerLimit?.ToString() ?? string.Empty;
            textBox_FileCountUpperLimit.Text = Config.FileCountUpperLimit?.ToString() ?? string.Empty;

            //加载文件或文件夹变化时更新数据
            checkBox_EnableUpdateGottenData.Checked = Config.EnableUpdateGottenData;
            checkBox_EnableUpdateImportedData.Checked = Config.EnableUpdateImportedData;
        }

        /// <summary>
        /// 设置界面中显示的文本
        /// </summary>
        /// 2024.6.3
        /// version 2.0.0
        private void InitializeLocalization()
        {
            // 标题
            Text = localization.Config_Title;

            // 语言和保存、读取、默认按钮
            label_Language.Text = localization.Config_Label_Language;
            button_Save.Text = localization.Config_Button_Save;
            button_Load.Text = localization.Config_Button_Load;
            button_Default.Text = localization.Config_Button_Default;

            // 程序相关配置
            groupBox_Program.Text = localization.Config_GroupBox_Program;
            checkBox_Autostart.Text = localization.Config_CheckBox_Autostart;
            checkBox_MinimizeWhenClose.Text = localization.Config_CheckBox_MinimizeWhenClosing;
            checkBox_EnableShowFileCountWhenGetting.Text = localization.Config_CheckBox_EnableShowFileCountWhenGetting;
            label_ShowFileCountInterval.Text = localization.Config_Label_ShowFileCountInterval;
            checkBox_OnlyOneProcessAllowed.Text = localization.Config_CheckBox_OnlyOneProcessAllowed;
            checkBox_ExportWhenExit.Text = localization.Config_CheckBox_ExportDataWhenExit;
            checkBox_GetDataWhenStart.Text = localization.Config_CheckBox_GetDataWhenStart;
            checkBox_ImportDataWhenStart.Text = localization.Config_CheckBox_ImportDataWhenStart;
            label_ExportWhenExitPath.Text = localization.Config_Label_Path;
            label_GetDataWhenStartPath.Text = localization.Config_Label_Path;
            label_ImportDataWhenStartPath.Text = localization.Config_Label_Path;
            button_ExportWhenExitViewFolder.Text = localization.Config_Button_ViewFolder;
            button_GetDataWhenStartViewFile.Text = localization.Config_Button_ViewFile;
            button_ImportDataWhenStartViewFile.Text = localization.Config_Button_ViewFile;

            // 分批加载相关配置
            groupBox_BatchLoad.Text = localization.Config_GroupBox_BatchLoad;
            checkBox_EnableBatchLoad.Text = localization.Config_CheckBox_EnableBatchLoad;
            label_BatchLoadThreshold.Text = localization.Config_Label_BatchLoadThreshold;
            label_BatchSize.Text = localization.Config_Label_BatchSize;
            label_BatchInterval.Text = localization.Config_Label_BatchInterval;

            // 搜索相关配置
            groupBox_Search.Text = localization.Config_GroupBox_Search;
            label_SearchRule.Text = localization.Config_Label_SearchRule;
            checkBox_SearchFiles.Text = localization.Config_CheckBox_SearchFiles;
            checkBox_SearchFolders.Text = localization.Config_CheckBox_SearchFolders;
            checkBox_RecursiveSearch.Text = localization.Config_CheckBox_RecursiveSearch;
            checkBox_CaseSensitive.Text = localization.Config_CheckBox_CaseSensitive;
            checkBox_MatchFullName.Text = localization.Config_CheckBox_MatchFullName;
            checkBox_SearchFromRoot.Text = localization.Config_CheckBox_SearchFromRoot;
            label_FileSizeLimit.Text = localization.Config_Label_FileSizeLimit;
            label_FolderSizeLimit.Text = localization.Config_Label_FolderSizeLimit;
            label_FileCountLimit.Text = localization.Config_Label_FileCountLimit;
            label_FileSizeLimitFromTo.Text = localization.FromTo;
            label_FolderSizeLimitFromTo.Text = localization.FromTo;
            label_FileCountLimitFromTo.Text = localization.FromTo;

            // 文件或文件夹变化时更新数据相关配置
            groupBox_UpdateDataWhenFolderOrFileChange.Text = localization.Config_GroupBox_UpdateDataWhenFolderOrFileChange;
            checkBox_EnableUpdateGottenData.Text = localization.Config_CheckBox_EnableUpdateGottenData;
            checkBox_EnableUpdateImportedData.Text = localization.Config_CheckBox_EnableUpdateImportedData;

            // 确认取消应用按钮
            button_Ok.Text = localization.Ok;
            button_Cancel.Text = localization.Cancel;
            button_Apply.Text = localization.Apply;

            // 初始化选择语言的ComboBox
            Language[] languages = localization.Localizations;
            comboBox_Language.Items.Clear();
            for (int i = 0; i < Localization.LANGUAGES.Length; i++)
            {
                comboBox_Language.Items.Add(languages[i].LocalizedName);
            }

            // 初始化选择搜索规则的ComboBox
            comboBox_SearchRule.Items.Clear();
            comboBox_SearchRule.Items.Add(localization.Config_SearchRules_Contain);
            comboBox_SearchRule.Items.Add(localization.Config_SearchRules_Same);
            comboBox_SearchRule.Items.Add(localization.Config_SearchRules_Regular);
            comboBox_SearchRule.Items.Add(localization.Config_SearchRules_Wildcard);
            comboBox_SearchRule.Items.Add(localization.Config_SearchRules_Extension);
            comboBox_SearchRule.Items.Add(localization.Config_SearchRules_Conbination);
        }

        #endregion

        #region 组件

        /// <summary>
        /// 
        /// </summary>
        /// 2024.5.30
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "json files(*.json)|*.json|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Config.Save(dialog.FileName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.5.31
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "json files(*.json)|*.json|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadConfig();
            }
        }

        /// <summary>
        /// 将设置恢复为默认
        /// </summary>
        /// 2024.5.31
        /// version 1.4.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Default_Click(object sender, EventArgs e)
        {
            Config.LoadDefault();
            Config.Save();
            LoadConfig();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.5.31
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Ok_Click(object sender, EventArgs e)
        {
            if (Apply()) DialogResult = DialogResult.OK;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.5.31
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Apply_Click(object sender, EventArgs e)
        {
            Apply();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.5.28
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_EnableShowFileCountWhenGetting_CheckedChanged(object sender, EventArgs e)
        {
            SetShowFileCountEnabled(checkBox_EnableShowFileCountWhenGetting.Checked);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.5.18
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_ExportWhenExit_CheckedChanged(object sender, EventArgs e)
        {
            SetExportWhenExitEnabled(checkBox_ExportWhenExit.Checked);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.5.18
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_GetDataWhenStart_CheckedChanged(object sender, EventArgs e)
        {
            SetGetDataWhenStartEnabled(checkBox_GetDataWhenStart.Checked);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.5.18
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_ImportDataWhenStart_CheckedChanged(object sender, EventArgs e)
        {
            SetImportDataWhenStartEnabled(checkBox_ImportDataWhenStart.Checked);
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.5.18
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ExportWhenExitViewFolder_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "json files(*.json)|*.json|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox_ExportWhenExitPath.Text = dialog.FileName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.5.18
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_GetDataWhenStartViewFile_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox_GetDataWhenStartPath.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.5.18
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ImportDataWhenStartViewFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "json files(*.json)|*.json|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox_ImportDataWhenStartPath.Text = dialog.FileName;
            }
        }

        /// <summary>
        /// 若禁用分批加载，则禁用分批加载相关设置
        /// </summary>
        /// 2023.12.15
        /// version 1.4.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_EnableBatchLoad_CheckedChanged(object sender, EventArgs e)
        {
            SetBatchLoadEnable(checkBox_EnableBatchLoad.Checked);
        }

        #endregion

        #region 设置组件是否可用

        private void SetShowFileCountEnabled(bool enabled)
        {
            textBox_ShowFileCountInterval.Enabled = enabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.5.18
        /// version 2.0.0
        /// <param name="enabled"></param>
        private void SetExportWhenExitEnabled(bool enabled)
        {
            textBox_ExportWhenExitPath.Enabled = enabled;
            button_ExportWhenExitViewFolder.Enabled = enabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.5.18
        /// version 2.0.0
        /// <param name="enabled"></param>
        private void SetGetDataWhenStartEnabled(bool enabled)
        {
            textBox_GetDataWhenStartPath.Enabled = enabled;
            button_GetDataWhenStartViewFile.Enabled = enabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 2024.5.18
        /// version 2.0.0
        /// <param name="enabled"></param>
        private void SetImportDataWhenStartEnabled(bool enabled)
        {
            textBox_ImportDataWhenStartPath.Enabled = enabled;
            button_ImportDataWhenStartViewFile.Enabled = enabled;
        }

        /// <summary>
        /// 设置分批加载相关设置是否可用
        /// </summary>
        /// 2023.12.21
        /// version 1.4.0
        /// <param name="enable"></param>
        private void SetBatchLoadEnable(bool enable)
        {
            label_BatchLoadThreshold.Enabled = enable;
            label_BatchSize.Enabled = enable;
            label_BatchInterval.Enabled = enable;
            textBox_BatchLoadThreshold.Enabled = enable;
            textBox_BatchSize.Enabled = enable;
            textBox_BatchInterval.Enabled = enable;
        }

        #endregion

        #region 功能方法

        /// <summary>
        /// 应用输入的设置，若输入无效则提示失败原因。
        /// </summary>
        /// 2024.5.31
        /// version 2.0.0
        /// <returns>若所有输入内容均有效则返回true，否则返回false</returns>
        private bool Apply()
        {
            try
            {
                CheckValidity();
                SaveConfig();
                LoadConfig();
                AutostartUtil.SetAutostart(Config.Autostart);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 检查输入内容是否有效。若不有效则会抛出异常
        /// </summary>
        /// 2023.12.28
        /// version 1.4.0
        private void CheckValidity()
        {
            int value;
            if (!int.TryParse(textBox_ShowFileCountInterval.Text, out value) || value < 0)
            {
                throw new Exception();
            }
            if (!int.TryParse(textBox_BatchLoadThreshold.Text, out value) || value < 0)
            {
                throw new Exception(localization.Config_ExceptionMessage_BatchLoadThreshold_ShouldBeNonNegativeInteger);
            }
            if (!int.TryParse(textBox_BatchSize.Text, out value) || value <= 0)
            {
                throw new Exception(localization.Config_ExceptionMessage_BatchSize_ShouldBePositiveInteger);
            }
            if (!int.TryParse(textBox_BatchInterval.Text, out value) || value < 0)
            {
                throw new Exception(localization.Config_ExceptionMessage_BatchInterval_ShouldBeNonNegativeInteger);
            }
            if (!checkBox_SearchFiles.Checked && !checkBox_SearchFolders.Checked)
            {
                throw new Exception(localization.Config_ExceptionMessage_SearchFilesFolders_AtLeastOneShouldBeChecked);
            }
            if (!FileSizeUtil.IsFileSizeOrEmpty(textBox_FileSizeLowerLimit.Text) || !FileSizeUtil.IsFileSizeOrEmpty(textBox_FileSizeUpperLimit.Text) ||
                !FileSizeUtil.IsFileSizeOrEmpty(textBox_FolderSizeLowerLimit.Text) || !FileSizeUtil.IsFileSizeOrEmpty(textBox_FolderSizeUpperLimit.Text))
            {
                throw new Exception(localization.Config_ExceptionMessage_FileOrFolderSizeLimit_ShouldBeNonNegativeIntegerWithUnitOrEmpty);
            }
            if (!string.IsNullOrEmpty(textBox_FileCountLowerLimit.Text) && (!int.TryParse(textBox_FileCountLowerLimit.Text, out value) || value < 0))
            {
                throw new Exception(localization.Config_ExceptionMessage_FileCounLimit_ShouldBeNonNegativeIntegerOrEmpty);
            }
            if (!string.IsNullOrEmpty(textBox_FileCountUpperLimit.Text) && (!int.TryParse(textBox_FileCountUpperLimit.Text, out value) || value < 0))
            {
                throw new Exception(localization.Config_ExceptionMessage_FileCounLimit_ShouldBeNonNegativeIntegerOrEmpty);
            }
            if (!FileSizeUtil.AreLowerAndHigherLimitValid(textBox_FileSizeLowerLimit.Text, textBox_FileSizeUpperLimit.Text, true))
            {
                throw new Exception(localization.Config_ExceptionMessage_FileOrFolderSizeLimit_LowerCannotBeLargerThanHigher);
            }
            if (!FileSizeUtil.AreLowerAndHigherLimitValid(textBox_FolderSizeLowerLimit.Text, textBox_FolderSizeUpperLimit.Text, true))
            {
                throw new Exception(localization.Config_ExceptionMessage_FileOrFolderSizeLimit_LowerCannotBeLargerThanHigher);
            }
            if (!FileSizeUtil.AreLowerAndHigherLimitValid(textBox_FileCountLowerLimit.Text, textBox_FileCountUpperLimit.Text, false))
            {
                throw new Exception(localization.Config_ExceptionMessage_FileCounLimit_LowerCannotBeLargerThanHigher);
            }
        }

        /// <summary>
        /// 保存对Config的修改
        /// </summary>
        /// 2024.6.3
        /// version 2.0.0
        private void SaveConfig()
        {
            Json.JsonObject json = new Json.JsonObject()
            {
                {"Language", Localization.LANGUAGES[comboBox_Language.SelectedIndex] },
                {"SearchRule", comboBox_SearchRule.SelectedIndex},
                {"SearchFiles", checkBox_SearchFiles.Checked },
                {"SearchFolders", checkBox_SearchFolders.Checked },
                {"RecursiveSearch", checkBox_RecursiveSearch.Checked },
                {"CaseSensitive", checkBox_CaseSensitive.Checked },
                {"MatchFullName", checkBox_MatchFullName.Checked },
                {"SearchFromRoot", checkBox_SearchFromRoot.Checked },
                {"EnableBatchLoad", checkBox_EnableBatchLoad.Checked },
                {"BatchLoadThreshold", int.Parse(textBox_BatchLoadThreshold.Text) },
                {"BatchSize", int.Parse(textBox_BatchSize.Text) },
                {"BatchInterval", int.Parse(textBox_BatchInterval.Text) },
                {"FileSizeLowerLimit", FileSizeUtil.FileSizeStringToNumber(textBox_FileSizeLowerLimit.Text) },
                {"FileSizeUpperLimit", FileSizeUtil.FileSizeStringToNumber(textBox_FileSizeUpperLimit.Text) },
                {"FolderSizeLowerLimit", FileSizeUtil.FileSizeStringToNumber(textBox_FolderSizeLowerLimit.Text) },
                {"FolderSizeUpperLimit", FileSizeUtil.FileSizeStringToNumber(textBox_FolderSizeUpperLimit.Text) },
                {"FileCountLowerLimit", string.IsNullOrEmpty(textBox_FileCountLowerLimit.Text)? null : int.Parse(textBox_FileCountLowerLimit.Text) },
                {"FileCountUpperLimit", string.IsNullOrEmpty(textBox_FileCountUpperLimit.Text)? null : int.Parse(textBox_FileCountUpperLimit.Text) },
                {"EnableUpdateGottenData", checkBox_EnableUpdateGottenData.Checked },
                {"EnableUpdateImportedData", checkBox_EnableUpdateImportedData.Checked },
                {"Autostart", checkBox_Autostart.Checked },
                {"MinimizeWhenClosing", checkBox_MinimizeWhenClose.Checked },
                {"EnableShowFileCountWhenGetting", checkBox_EnableShowFileCountWhenGetting.Checked },
                {"ShowFileCountInterval", int.Parse(textBox_ShowFileCountInterval.Text) },
                {"OnlyOneProcessAllowed", checkBox_OnlyOneProcessAllowed.Checked },
                {"ExportWhenExit", checkBox_ExportWhenExit.Checked },
                {"ExportWhenExitPath", textBox_ExportWhenExitPath.Text },
                {"GetDataWhenStart", checkBox_GetDataWhenStart.Checked },
                {"GetDataWhenStartPath", textBox_GetDataWhenStartPath.Text },
                {"ImportDataWhenStart", checkBox_ImportDataWhenStart.Checked },
                {"ImportDataWhenStartPath", textBox_ImportDataWhenStartPath.Text },
            };
            Config.Load(json);
            Config.Save();
        }
        #endregion
    }
}
