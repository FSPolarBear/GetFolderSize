using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace GetFolderSize
{
    /// <summary>
    /// 调整设置的界面
    /// <para>2023.12.21</para>
    /// <para>version 1.4.0</para>
    /// </summary>
    public partial class ConfigForm : Form
    {
        Localization localization = Localization.GetInstance(Config.Language);
        public ConfigForm()
        {
            InitializeComponent();

            InitializeLocalization();
            LoadConfig();

        }

        /// <summary>
        /// 从Config类中加载配置并显示在界面上
        /// <para>2023.12.21</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        private void LoadConfig()
        {

            // 加载语言
            int index = Language.IndexOf(localization.Localizations, Config.Language);
            if (index >= 0)
            {
                comboBox_language.SelectedIndex = index;
            }
            else
            {
                comboBox_language.SelectedIndex = 0;
            }

            // 加载分批加载相关配置
            checkBox_enable_batch_load.Checked = Config.EnableBatchLoad;
            textBox_batch_load_threshold.Text = Config.BatchLoadThreshold.ToString();
            textBox_batch_size.Text = Config.BatchSize.ToString();
            textBox_batch_interval.Text = Config.BatchInterval.ToString();
            SetBatchLoadEnable(Config.EnableBatchLoad);

            //加载搜索
            comboBox_search_rule.SelectedIndex = (int)Config.SearchRule;
            checkBox_search_files.Checked = Config.SearchFiles;
            checkBox_search_folders.Checked = Config.SearchFolders;
            checkBox_recursive_search.Checked = Config.RecursiveSearch;
            checkBox_case_sensitive.Checked = Config.CaseSensitive;
            textBox_file_size_lower_limit.Text = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(Config.FileSizeLowerLimit);
            textBox_file_size_upper_limit.Text = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(Config.FileSizeUpperLimit);
            textBox_folder_size_lower_limit.Text = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(Config.FolderSizeLowerLimit);
            textBox_folder_size_upper_limit.Text = FileSizeUtil.FileSizeNumberToStringWithIntegerValue(Config.FolderSizeUpperLimit);
            textBox_file_count_lower_limit.Text = Config.FileCountLowerLimit?.ToString() ?? string.Empty;
            textBox_file_count_upper_limit.Text = Config.FileCountUpperLimit?.ToString() ?? string.Empty;
        }




        /// <summary>
        /// 设置界面中显示的文本
        /// <para>2023.12.18</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        private void InitializeLocalization()
        {
            Text = localization.Config_Title;
            label_language.Text = localization.Config_Label_Language;
            button_default.Text = localization.Config_Button_Default;
            groupBox_batch_load.Text = localization.Config_GroupBox_BatchLoad;
            checkBox_enable_batch_load.Text = localization.Config_CheckBox_EnableBatchLoad;
            label_batch_load_threshold.Text = localization.Config_Label_BatchLoadThreshold;
            label_batch_size.Text = localization.Config_Label_BatchSize;
            label_batch_interval.Text = localization.Config_Label_BatchInterval;
            groupBox_search.Text = localization.Config_GroupBox_Search;
            label_search_rule.Text = localization.Config_Label_SearchRule;
            checkBox_search_files.Text = localization.Config_CheckBox_SearchFiles;
            checkBox_search_folders.Text = localization.Config_CheckBox_SearchFolders;
            checkBox_recursive_search.Text = localization.Config_CheckBox_RecursiveSearch;
            checkBox_case_sensitive.Text = localization.Config_CheckBox_CaseSensitive;
            label_file_size_limit.Text = localization.Config_Label_FileSizeLimit;
            label_folder_size_limit.Text = localization.Config_Label_FolderSizeLimit;
            label_file_count_limit.Text = localization.Config_Label_FileCountLimit;
            label_file_size_limit_from_to.Text = localization.FromTo;
            label_folder_size_limit_from_to.Text = localization.FromTo;
            label_file_count_limit_from_to.Text = localization.FromTo;
            button_ok.Text = localization.Ok;
            button_cancel.Text = localization.Cancel;

            // 初始化选择语言的ComboBox
            //Dictionary<string, string> languages = localization.GetLanguages();
            Language[] languages = localization.Localizations;
            comboBox_language.Items.Clear();
            for (int i = 0; i < Localization.LANGUAGES.Length; i++)
            {
                comboBox_language.Items.Add(languages[i].LocalizedName);
            }

            // 初始化选择搜索规则的ComboBox
            comboBox_search_rule.Items.Clear();
            comboBox_search_rule.Items.Add(localization.Config_SearchRules_Include);
            comboBox_search_rule.Items.Add(localization.Config_SearchRules_Same);
            comboBox_search_rule.Items.Add(localization.Config_SearchRules_Regular);

        }

        /// <summary>
        /// 设置分批加载相关设置是否可用
        /// <para>2023.12.21</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="enable"></param>
        private void SetBatchLoadEnable(bool enable)
        {
            label_batch_load_threshold.Enabled = enable;
            label_batch_size.Enabled = enable;
            label_batch_interval.Enabled = enable;
            textBox_batch_load_threshold.Enabled = enable;
            textBox_batch_size.Enabled = enable;
            textBox_batch_interval.Enabled=enable;

        }

        /// <summary>
        /// 若禁用分批加载，则禁用分批加载相关设置
        /// <para>2023.12.15</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_enable_batch_load_CheckedChanged(object sender, EventArgs e)
        {
            SetBatchLoadEnable(checkBox_enable_batch_load.Checked);
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            try
            {
                CheckValidity();
                SaveConfig();
                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// 检查输入内容是否合法。若不合法则会抛出异常
        /// <para>2023.12.15</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        private void CheckValidity()
        {
            int value;
            if(!int.TryParse(textBox_batch_load_threshold.Text, out value) || value < 0)
            {
                throw new Exception(localization.Config_ExceptionMessage_BatchLoadThreshold_ShouldBeNonNegativeInteger);
            }
            if(!int.TryParse(textBox_batch_size.Text, out value) || value <= 0)
            {
                throw new Exception(localization.Config_ExceptionMessage_BatchSize_ShouldBePositiveInteger);
            }
            if(!int.TryParse(textBox_batch_interval.Text, out value) || value < 0)
            {
                throw new Exception(localization.Config_ExceptionMessage_BatchInterval_ShouldBeNonNegativeInteger);
            }
            if (!checkBox_search_files.Checked && !checkBox_search_folders.Checked)
            {
                throw new Exception(localization.Config_ExceptionMessage_SearchFilesFolders_AtLeastOneShouldBeChecked);
            }
            if(!FileSizeUtil.IsFileSizeOrEmpty(textBox_file_size_lower_limit.Text) || !FileSizeUtil.IsFileSizeOrEmpty(textBox_file_size_upper_limit.Text) ||
                !FileSizeUtil.IsFileSizeOrEmpty(textBox_folder_size_lower_limit.Text) || !FileSizeUtil.IsFileSizeOrEmpty(textBox_folder_size_upper_limit.Text))
            {
                throw new Exception(localization.Config_ExceptionMessage_FileOrFolderSizeLimit_ShouldBeNonNegativeIntegerWithUnitOrEmpty);
            }
            if(!string.IsNullOrEmpty(textBox_file_count_lower_limit.Text) && (!int.TryParse(textBox_file_count_lower_limit.Text, out value) || value < 0))
            {
                throw new Exception(localization.Config_ExceptionMessage_FileCounLimit_ShouldBeNonNegativeIntegerOrEmpty);
            }
            if (!string.IsNullOrEmpty(textBox_file_count_upper_limit.Text) && (!int.TryParse(textBox_file_count_upper_limit.Text, out value) || value < 0))
            {
                throw new Exception(localization.Config_ExceptionMessage_FileCounLimit_ShouldBeNonNegativeIntegerOrEmpty);
            }


        }

        /// <summary>
        /// 保存对Config的修改
        /// <para>2023.12.15</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        private void SaveConfig()
        {
            JObject json = new JObject()
            {
                {"Language", Localization.LANGUAGES[comboBox_language.SelectedIndex] },
                {"SearchRule", comboBox_search_rule.SelectedIndex},
                {"SearchFiles", checkBox_search_files.Checked },
                {"SearchFolders", checkBox_search_folders.Checked },
                {"RecursiveSearch", checkBox_recursive_search.Checked },
                {"CaseSensitive", checkBox_case_sensitive.Checked },
                {"EnableBatchLoad", checkBox_enable_batch_load.Checked },
                {"BatchLoadThreshold", int.Parse(textBox_batch_load_threshold.Text) },
                {"BatchSize", int.Parse(textBox_batch_size.Text) },
                {"BatchInterval", int.Parse(textBox_batch_interval.Text) },
                {"FileSizeLowerLimit", FileSizeUtil.FileSizeStringToNumber(textBox_file_size_lower_limit.Text) },
                {"FileSizeUpperLimit", FileSizeUtil.FileSizeStringToNumber(textBox_file_size_upper_limit.Text) },
                {"FolderSizeLowerLimit", FileSizeUtil.FileSizeStringToNumber(textBox_folder_size_lower_limit.Text) },
                {"FolderSizeUpperLimit", FileSizeUtil.FileSizeStringToNumber(textBox_folder_size_upper_limit.Text) },
                {"FileCountLowerLimit", string.IsNullOrEmpty(textBox_file_count_lower_limit.Text)? null : int.Parse(textBox_file_count_lower_limit.Text) },
                {"FileCountUpperLimit", string.IsNullOrEmpty(textBox_file_count_upper_limit.Text)? null : int.Parse(textBox_file_count_upper_limit.Text) },
            };
            Config.Load(json);
            Config.Save();
        }

        /// <summary>
        /// 将设置恢复为默认
        /// 保存对Config的修改
        /// <para>2023.12.20</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_default_Click(object sender, EventArgs e)
        {
            Config.LoadDefault();
            Config.Save();
            InitializeLocalization();
            LoadConfig();
        }
    }
}
