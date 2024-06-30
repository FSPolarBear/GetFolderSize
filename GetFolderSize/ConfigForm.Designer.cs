namespace GetFolderSize
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            groupBox_BatchLoad = new GroupBox();
            textBox_BatchInterval = new TextBox();
            label_BatchInterval = new Label();
            textBox_BatchSize = new TextBox();
            label_BatchSize = new Label();
            textBox_BatchLoadThreshold = new TextBox();
            label_BatchLoadThreshold = new Label();
            checkBox_EnableBatchLoad = new CheckBox();
            label_Language = new Label();
            comboBox_Language = new ComboBox();
            groupBox_Search = new GroupBox();
            checkBox_SearchFromRoot = new CheckBox();
            checkBox_MatchFullName = new CheckBox();
            label_FileCountLimitFromTo = new Label();
            textBox_FileCountUpperLimit = new TextBox();
            textBox_FileCountLowerLimit = new TextBox();
            label_FileCountLimit = new Label();
            label_FolderSizeLimitFromTo = new Label();
            textBox_FolderSizeUpperLimit = new TextBox();
            textBox_FolderSizeLowerLimit = new TextBox();
            label_FolderSizeLimit = new Label();
            label_FileSizeLimitFromTo = new Label();
            textBox_FileSizeUpperLimit = new TextBox();
            textBox_FileSizeLowerLimit = new TextBox();
            label_FileSizeLimit = new Label();
            checkBox_CaseSensitive = new CheckBox();
            label_SearchRule = new Label();
            comboBox_SearchRule = new ComboBox();
            checkBox_RecursiveSearch = new CheckBox();
            checkBox_SearchFolders = new CheckBox();
            checkBox_SearchFiles = new CheckBox();
            button_Ok = new Button();
            button_Cancel = new Button();
            button_Default = new Button();
            checkBox_EnableUpdateGottenData = new CheckBox();
            checkBox_Autostart = new CheckBox();
            groupBox_UpdateDataWhenFolderOrFileChange = new GroupBox();
            checkBox_EnableUpdateImportedData = new CheckBox();
            groupBox_Program = new GroupBox();
            checkBox_OnlyOneProcessAllowed = new CheckBox();
            textBox_ShowFileCountInterval = new TextBox();
            label_ShowFileCountInterval = new Label();
            checkBox_EnableShowFileCountWhenGetting = new CheckBox();
            button_ImportDataWhenStartViewFile = new Button();
            button_GetDataWhenStartViewFile = new Button();
            button_ExportWhenExitViewFolder = new Button();
            label_ImportDataWhenStartPath = new Label();
            label_GetDataWhenStartPath = new Label();
            label_ExportWhenExitPath = new Label();
            textBox_ImportDataWhenStartPath = new TextBox();
            checkBox_ImportDataWhenStart = new CheckBox();
            textBox_GetDataWhenStartPath = new TextBox();
            textBox_ExportWhenExitPath = new TextBox();
            checkBox_ExportWhenExit = new CheckBox();
            checkBox_MinimizeWhenClose = new CheckBox();
            checkBox_GetDataWhenStart = new CheckBox();
            button_Load = new Button();
            button_Save = new Button();
            button_Apply = new Button();
            groupBox_BatchLoad.SuspendLayout();
            groupBox_Search.SuspendLayout();
            groupBox_UpdateDataWhenFolderOrFileChange.SuspendLayout();
            groupBox_Program.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox_BatchLoad
            // 
            groupBox_BatchLoad.Controls.Add(textBox_BatchInterval);
            groupBox_BatchLoad.Controls.Add(label_BatchInterval);
            groupBox_BatchLoad.Controls.Add(textBox_BatchSize);
            groupBox_BatchLoad.Controls.Add(label_BatchSize);
            groupBox_BatchLoad.Controls.Add(textBox_BatchLoadThreshold);
            groupBox_BatchLoad.Controls.Add(label_BatchLoadThreshold);
            groupBox_BatchLoad.Controls.Add(checkBox_EnableBatchLoad);
            groupBox_BatchLoad.Location = new Point(15, 334);
            groupBox_BatchLoad.Margin = new Padding(4);
            groupBox_BatchLoad.Name = "groupBox_BatchLoad";
            groupBox_BatchLoad.Padding = new Padding(4);
            groupBox_BatchLoad.Size = new Size(580, 190);
            groupBox_BatchLoad.TabIndex = 0;
            groupBox_BatchLoad.TabStop = false;
            groupBox_BatchLoad.Text = "batch load";
            // 
            // textBox_BatchInterval
            // 
            textBox_BatchInterval.Location = new Point(233, 150);
            textBox_BatchInterval.Margin = new Padding(4);
            textBox_BatchInterval.Name = "textBox_BatchInterval";
            textBox_BatchInterval.Size = new Size(148, 30);
            textBox_BatchInterval.TabIndex = 6;
            // 
            // label_BatchInterval
            // 
            label_BatchInterval.AutoSize = true;
            label_BatchInterval.Location = new Point(9, 150);
            label_BatchInterval.Margin = new Padding(4, 0, 4, 0);
            label_BatchInterval.Name = "label_BatchInterval";
            label_BatchInterval.Size = new Size(128, 24);
            label_BatchInterval.TabIndex = 5;
            label_BatchInterval.Text = "batch interval";
            // 
            // textBox_BatchSize
            // 
            textBox_BatchSize.Location = new Point(233, 110);
            textBox_BatchSize.Margin = new Padding(4);
            textBox_BatchSize.Name = "textBox_BatchSize";
            textBox_BatchSize.Size = new Size(148, 30);
            textBox_BatchSize.TabIndex = 4;
            // 
            // label_BatchSize
            // 
            label_BatchSize.AutoSize = true;
            label_BatchSize.Location = new Point(9, 110);
            label_BatchSize.Margin = new Padding(4, 0, 4, 0);
            label_BatchSize.Name = "label_BatchSize";
            label_BatchSize.Size = new Size(100, 24);
            label_BatchSize.TabIndex = 3;
            label_BatchSize.Text = "batch size:";
            // 
            // textBox_BatchLoadThreshold
            // 
            textBox_BatchLoadThreshold.Location = new Point(233, 70);
            textBox_BatchLoadThreshold.Margin = new Padding(4);
            textBox_BatchLoadThreshold.Name = "textBox_BatchLoadThreshold";
            textBox_BatchLoadThreshold.Size = new Size(148, 30);
            textBox_BatchLoadThreshold.TabIndex = 2;
            // 
            // label_BatchLoadThreshold
            // 
            label_BatchLoadThreshold.AutoSize = true;
            label_BatchLoadThreshold.Location = new Point(12, 70);
            label_BatchLoadThreshold.Margin = new Padding(4, 0, 4, 0);
            label_BatchLoadThreshold.Name = "label_BatchLoadThreshold";
            label_BatchLoadThreshold.Size = new Size(193, 24);
            label_BatchLoadThreshold.TabIndex = 1;
            label_BatchLoadThreshold.Text = "batch load threshold:";
            // 
            // checkBox_EnableBatchLoad
            // 
            checkBox_EnableBatchLoad.AutoSize = true;
            checkBox_EnableBatchLoad.Location = new Point(12, 30);
            checkBox_EnableBatchLoad.Margin = new Padding(4);
            checkBox_EnableBatchLoad.Name = "checkBox_EnableBatchLoad";
            checkBox_EnableBatchLoad.Size = new Size(191, 28);
            checkBox_EnableBatchLoad.TabIndex = 0;
            checkBox_EnableBatchLoad.Text = "enable batch load";
            checkBox_EnableBatchLoad.UseVisualStyleBackColor = true;
            checkBox_EnableBatchLoad.CheckedChanged += checkBox_EnableBatchLoad_CheckedChanged;
            // 
            // label_Language
            // 
            label_Language.AutoSize = true;
            label_Language.Location = new Point(15, 14);
            label_Language.Margin = new Padding(4, 0, 4, 0);
            label_Language.Name = "label_Language";
            label_Language.Size = new Size(95, 24);
            label_Language.TabIndex = 1;
            label_Language.Text = "language:";
            // 
            // comboBox_Language
            // 
            comboBox_Language.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_Language.FormattingEnabled = true;
            comboBox_Language.Location = new Point(120, 14);
            comboBox_Language.Margin = new Padding(4);
            comboBox_Language.Name = "comboBox_Language";
            comboBox_Language.Size = new Size(124, 32);
            comboBox_Language.TabIndex = 17;
            // 
            // groupBox_Search
            // 
            groupBox_Search.Controls.Add(checkBox_SearchFromRoot);
            groupBox_Search.Controls.Add(checkBox_MatchFullName);
            groupBox_Search.Controls.Add(label_FileCountLimitFromTo);
            groupBox_Search.Controls.Add(textBox_FileCountUpperLimit);
            groupBox_Search.Controls.Add(textBox_FileCountLowerLimit);
            groupBox_Search.Controls.Add(label_FileCountLimit);
            groupBox_Search.Controls.Add(label_FolderSizeLimitFromTo);
            groupBox_Search.Controls.Add(textBox_FolderSizeUpperLimit);
            groupBox_Search.Controls.Add(textBox_FolderSizeLowerLimit);
            groupBox_Search.Controls.Add(label_FolderSizeLimit);
            groupBox_Search.Controls.Add(label_FileSizeLimitFromTo);
            groupBox_Search.Controls.Add(textBox_FileSizeUpperLimit);
            groupBox_Search.Controls.Add(textBox_FileSizeLowerLimit);
            groupBox_Search.Controls.Add(label_FileSizeLimit);
            groupBox_Search.Controls.Add(checkBox_CaseSensitive);
            groupBox_Search.Controls.Add(label_SearchRule);
            groupBox_Search.Controls.Add(comboBox_SearchRule);
            groupBox_Search.Controls.Add(checkBox_RecursiveSearch);
            groupBox_Search.Controls.Add(checkBox_SearchFolders);
            groupBox_Search.Controls.Add(checkBox_SearchFiles);
            groupBox_Search.Location = new Point(15, 532);
            groupBox_Search.Margin = new Padding(4);
            groupBox_Search.Name = "groupBox_Search";
            groupBox_Search.Padding = new Padding(4);
            groupBox_Search.Size = new Size(580, 270);
            groupBox_Search.TabIndex = 18;
            groupBox_Search.TabStop = false;
            groupBox_Search.Text = "search";
            // 
            // checkBox_SearchFromRoot
            // 
            checkBox_SearchFromRoot.AutoSize = true;
            checkBox_SearchFromRoot.Location = new Point(391, 110);
            checkBox_SearchFromRoot.Margin = new Padding(4);
            checkBox_SearchFromRoot.Name = "checkBox_SearchFromRoot";
            checkBox_SearchFromRoot.Size = new Size(178, 28);
            checkBox_SearchFromRoot.TabIndex = 37;
            checkBox_SearchFromRoot.Text = "search from root";
            checkBox_SearchFromRoot.UseVisualStyleBackColor = true;
            // 
            // checkBox_MatchFullName
            // 
            checkBox_MatchFullName.AutoSize = true;
            checkBox_MatchFullName.Location = new Point(391, 70);
            checkBox_MatchFullName.Margin = new Padding(4);
            checkBox_MatchFullName.Name = "checkBox_MatchFullName";
            checkBox_MatchFullName.Size = new Size(175, 28);
            checkBox_MatchFullName.TabIndex = 36;
            checkBox_MatchFullName.Text = "match full name";
            checkBox_MatchFullName.UseVisualStyleBackColor = true;
            // 
            // label_FileCountLimitFromTo
            // 
            label_FileCountLimitFromTo.AutoSize = true;
            label_FileCountLimitFromTo.Location = new Point(339, 230);
            label_FileCountLimitFromTo.Margin = new Padding(4, 0, 4, 0);
            label_FileCountLimitFromTo.Name = "label_FileCountLimitFromTo";
            label_FileCountLimitFromTo.Size = new Size(23, 24);
            label_FileCountLimitFromTo.TabIndex = 35;
            label_FileCountLimitFromTo.Text = "~";
            // 
            // textBox_FileCountUpperLimit
            // 
            textBox_FileCountUpperLimit.Location = new Point(384, 230);
            textBox_FileCountUpperLimit.Margin = new Padding(4);
            textBox_FileCountUpperLimit.Name = "textBox_FileCountUpperLimit";
            textBox_FileCountUpperLimit.Size = new Size(121, 30);
            textBox_FileCountUpperLimit.TabIndex = 34;
            // 
            // textBox_FileCountLowerLimit
            // 
            textBox_FileCountLowerLimit.Location = new Point(197, 230);
            textBox_FileCountLowerLimit.Margin = new Padding(4);
            textBox_FileCountLowerLimit.Name = "textBox_FileCountLowerLimit";
            textBox_FileCountLowerLimit.Size = new Size(121, 30);
            textBox_FileCountLowerLimit.TabIndex = 32;
            // 
            // label_FileCountLimit
            // 
            label_FileCountLimit.AutoSize = true;
            label_FileCountLimit.Location = new Point(9, 230);
            label_FileCountLimit.Margin = new Padding(4, 0, 4, 0);
            label_FileCountLimit.Name = "label_FileCountLimit";
            label_FileCountLimit.Size = new Size(138, 24);
            label_FileCountLimit.TabIndex = 33;
            label_FileCountLimit.Text = "file count limit:";
            // 
            // label_FolderSizeLimitFromTo
            // 
            label_FolderSizeLimitFromTo.AutoSize = true;
            label_FolderSizeLimitFromTo.Location = new Point(339, 190);
            label_FolderSizeLimitFromTo.Margin = new Padding(4, 0, 4, 0);
            label_FolderSizeLimitFromTo.Name = "label_FolderSizeLimitFromTo";
            label_FolderSizeLimitFromTo.Size = new Size(23, 24);
            label_FolderSizeLimitFromTo.TabIndex = 31;
            label_FolderSizeLimitFromTo.Text = "~";
            // 
            // textBox_FolderSizeUpperLimit
            // 
            textBox_FolderSizeUpperLimit.Location = new Point(384, 190);
            textBox_FolderSizeUpperLimit.Margin = new Padding(4);
            textBox_FolderSizeUpperLimit.Name = "textBox_FolderSizeUpperLimit";
            textBox_FolderSizeUpperLimit.Size = new Size(121, 30);
            textBox_FolderSizeUpperLimit.TabIndex = 30;
            // 
            // textBox_FolderSizeLowerLimit
            // 
            textBox_FolderSizeLowerLimit.Location = new Point(197, 190);
            textBox_FolderSizeLowerLimit.Margin = new Padding(4);
            textBox_FolderSizeLowerLimit.Name = "textBox_FolderSizeLowerLimit";
            textBox_FolderSizeLowerLimit.Size = new Size(121, 30);
            textBox_FolderSizeLowerLimit.TabIndex = 28;
            // 
            // label_FolderSizeLimit
            // 
            label_FolderSizeLimit.AutoSize = true;
            label_FolderSizeLimit.Location = new Point(9, 190);
            label_FolderSizeLimit.Margin = new Padding(4, 0, 4, 0);
            label_FolderSizeLimit.Name = "label_FolderSizeLimit";
            label_FolderSizeLimit.Size = new Size(146, 24);
            label_FolderSizeLimit.TabIndex = 29;
            label_FolderSizeLimit.Text = "folder size limit:";
            // 
            // label_FileSizeLimitFromTo
            // 
            label_FileSizeLimitFromTo.AutoSize = true;
            label_FileSizeLimitFromTo.Location = new Point(339, 150);
            label_FileSizeLimitFromTo.Margin = new Padding(4, 0, 4, 0);
            label_FileSizeLimitFromTo.Name = "label_FileSizeLimitFromTo";
            label_FileSizeLimitFromTo.Size = new Size(23, 24);
            label_FileSizeLimitFromTo.TabIndex = 27;
            label_FileSizeLimitFromTo.Text = "~";
            // 
            // textBox_FileSizeUpperLimit
            // 
            textBox_FileSizeUpperLimit.Location = new Point(384, 150);
            textBox_FileSizeUpperLimit.Margin = new Padding(4);
            textBox_FileSizeUpperLimit.Name = "textBox_FileSizeUpperLimit";
            textBox_FileSizeUpperLimit.Size = new Size(121, 30);
            textBox_FileSizeUpperLimit.TabIndex = 26;
            // 
            // textBox_FileSizeLowerLimit
            // 
            textBox_FileSizeLowerLimit.Location = new Point(197, 150);
            textBox_FileSizeLowerLimit.Margin = new Padding(4);
            textBox_FileSizeLowerLimit.Name = "textBox_FileSizeLowerLimit";
            textBox_FileSizeLowerLimit.Size = new Size(121, 30);
            textBox_FileSizeLowerLimit.TabIndex = 7;
            // 
            // label_FileSizeLimit
            // 
            label_FileSizeLimit.AutoSize = true;
            label_FileSizeLimit.Location = new Point(9, 150);
            label_FileSizeLimit.Margin = new Padding(4, 0, 4, 0);
            label_FileSizeLimit.Name = "label_FileSizeLimit";
            label_FileSizeLimit.Size = new Size(121, 24);
            label_FileSizeLimit.TabIndex = 25;
            label_FileSizeLimit.Text = "file size limit:";
            // 
            // checkBox_CaseSensitive
            // 
            checkBox_CaseSensitive.AutoSize = true;
            checkBox_CaseSensitive.Location = new Point(197, 110);
            checkBox_CaseSensitive.Margin = new Padding(4);
            checkBox_CaseSensitive.Name = "checkBox_CaseSensitive";
            checkBox_CaseSensitive.Size = new Size(151, 28);
            checkBox_CaseSensitive.TabIndex = 24;
            checkBox_CaseSensitive.Text = "case sensitive";
            checkBox_CaseSensitive.UseVisualStyleBackColor = true;
            // 
            // label_SearchRule
            // 
            label_SearchRule.AutoSize = true;
            label_SearchRule.Location = new Point(12, 30);
            label_SearchRule.Margin = new Padding(4, 0, 4, 0);
            label_SearchRule.Name = "label_SearchRule";
            label_SearchRule.Size = new Size(109, 24);
            label_SearchRule.TabIndex = 23;
            label_SearchRule.Text = "Search rule:";
            // 
            // comboBox_SearchRule
            // 
            comboBox_SearchRule.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_SearchRule.FormattingEnabled = true;
            comboBox_SearchRule.Items.AddRange(new object[] { "include", "same", "regular" });
            comboBox_SearchRule.Location = new Point(140, 30);
            comboBox_SearchRule.Margin = new Padding(4);
            comboBox_SearchRule.Name = "comboBox_SearchRule";
            comboBox_SearchRule.Size = new Size(124, 32);
            comboBox_SearchRule.TabIndex = 22;
            // 
            // checkBox_RecursiveSearch
            // 
            checkBox_RecursiveSearch.AutoSize = true;
            checkBox_RecursiveSearch.Checked = true;
            checkBox_RecursiveSearch.CheckState = CheckState.Checked;
            checkBox_RecursiveSearch.Location = new Point(12, 110);
            checkBox_RecursiveSearch.Margin = new Padding(4);
            checkBox_RecursiveSearch.Name = "checkBox_RecursiveSearch";
            checkBox_RecursiveSearch.Size = new Size(172, 28);
            checkBox_RecursiveSearch.TabIndex = 21;
            checkBox_RecursiveSearch.Text = "recursive search";
            checkBox_RecursiveSearch.UseVisualStyleBackColor = true;
            // 
            // checkBox_SearchFolders
            // 
            checkBox_SearchFolders.AutoSize = true;
            checkBox_SearchFolders.Checked = true;
            checkBox_SearchFolders.CheckState = CheckState.Checked;
            checkBox_SearchFolders.Location = new Point(197, 70);
            checkBox_SearchFolders.Margin = new Padding(4);
            checkBox_SearchFolders.Name = "checkBox_SearchFolders";
            checkBox_SearchFolders.Size = new Size(155, 28);
            checkBox_SearchFolders.TabIndex = 20;
            checkBox_SearchFolders.Text = "search folders";
            checkBox_SearchFolders.UseVisualStyleBackColor = true;
            // 
            // checkBox_SearchFiles
            // 
            checkBox_SearchFiles.AutoSize = true;
            checkBox_SearchFiles.Checked = true;
            checkBox_SearchFiles.CheckState = CheckState.Checked;
            checkBox_SearchFiles.Location = new Point(12, 70);
            checkBox_SearchFiles.Margin = new Padding(4);
            checkBox_SearchFiles.Name = "checkBox_SearchFiles";
            checkBox_SearchFiles.Size = new Size(130, 28);
            checkBox_SearchFiles.TabIndex = 19;
            checkBox_SearchFiles.Text = "search files";
            checkBox_SearchFiles.UseVisualStyleBackColor = true;
            // 
            // button_Ok
            // 
            button_Ok.Location = new Point(102, 896);
            button_Ok.Margin = new Padding(4);
            button_Ok.Name = "button_Ok";
            button_Ok.Size = new Size(115, 35);
            button_Ok.TabIndex = 19;
            button_Ok.Text = "ok";
            button_Ok.UseVisualStyleBackColor = true;
            button_Ok.Click += button_Ok_Click;
            // 
            // button_Cancel
            // 
            button_Cancel.Location = new Point(248, 896);
            button_Cancel.Margin = new Padding(4);
            button_Cancel.Name = "button_Cancel";
            button_Cancel.Size = new Size(115, 35);
            button_Cancel.TabIndex = 20;
            button_Cancel.Text = "cancel";
            button_Cancel.UseVisualStyleBackColor = true;
            button_Cancel.Click += button_Cancel_Click;
            // 
            // button_Default
            // 
            button_Default.Location = new Point(495, 12);
            button_Default.Margin = new Padding(4);
            button_Default.Name = "button_Default";
            button_Default.Size = new Size(100, 35);
            button_Default.TabIndex = 21;
            button_Default.Text = "default";
            button_Default.UseVisualStyleBackColor = true;
            button_Default.Click += button_Default_Click;
            // 
            // checkBox_EnableUpdateGottenData
            // 
            checkBox_EnableUpdateGottenData.AutoSize = true;
            checkBox_EnableUpdateGottenData.Location = new Point(12, 30);
            checkBox_EnableUpdateGottenData.Margin = new Padding(4);
            checkBox_EnableUpdateGottenData.Name = "checkBox_EnableUpdateGottenData";
            checkBox_EnableUpdateGottenData.Size = new Size(268, 28);
            checkBox_EnableUpdateGottenData.TabIndex = 37;
            checkBox_EnableUpdateGottenData.Text = "enable update gotten data";
            checkBox_EnableUpdateGottenData.UseVisualStyleBackColor = true;
            // 
            // checkBox_Autostart
            // 
            checkBox_Autostart.AutoSize = true;
            checkBox_Autostart.Location = new Point(12, 30);
            checkBox_Autostart.Margin = new Padding(4);
            checkBox_Autostart.Name = "checkBox_Autostart";
            checkBox_Autostart.Size = new Size(114, 28);
            checkBox_Autostart.TabIndex = 36;
            checkBox_Autostart.Text = "autostart";
            checkBox_Autostart.UseVisualStyleBackColor = true;
            // 
            // groupBox_UpdateDataWhenFolderOrFileChange
            // 
            groupBox_UpdateDataWhenFolderOrFileChange.Controls.Add(checkBox_EnableUpdateImportedData);
            groupBox_UpdateDataWhenFolderOrFileChange.Controls.Add(checkBox_EnableUpdateGottenData);
            groupBox_UpdateDataWhenFolderOrFileChange.Location = new Point(15, 809);
            groupBox_UpdateDataWhenFolderOrFileChange.Name = "groupBox_UpdateDataWhenFolderOrFileChange";
            groupBox_UpdateDataWhenFolderOrFileChange.Size = new Size(581, 80);
            groupBox_UpdateDataWhenFolderOrFileChange.TabIndex = 38;
            groupBox_UpdateDataWhenFolderOrFileChange.TabStop = false;
            groupBox_UpdateDataWhenFolderOrFileChange.Text = "update data when folders or files change";
            // 
            // checkBox_EnableUpdateImportedData
            // 
            checkBox_EnableUpdateImportedData.AutoSize = true;
            checkBox_EnableUpdateImportedData.Location = new Point(275, 30);
            checkBox_EnableUpdateImportedData.Margin = new Padding(4);
            checkBox_EnableUpdateImportedData.Name = "checkBox_EnableUpdateImportedData";
            checkBox_EnableUpdateImportedData.Size = new Size(291, 28);
            checkBox_EnableUpdateImportedData.TabIndex = 38;
            checkBox_EnableUpdateImportedData.Text = "enable update imported data";
            checkBox_EnableUpdateImportedData.UseVisualStyleBackColor = true;
            // 
            // groupBox_Program
            // 
            groupBox_Program.Controls.Add(checkBox_OnlyOneProcessAllowed);
            groupBox_Program.Controls.Add(textBox_ShowFileCountInterval);
            groupBox_Program.Controls.Add(label_ShowFileCountInterval);
            groupBox_Program.Controls.Add(checkBox_EnableShowFileCountWhenGetting);
            groupBox_Program.Controls.Add(button_ImportDataWhenStartViewFile);
            groupBox_Program.Controls.Add(button_GetDataWhenStartViewFile);
            groupBox_Program.Controls.Add(button_ExportWhenExitViewFolder);
            groupBox_Program.Controls.Add(label_ImportDataWhenStartPath);
            groupBox_Program.Controls.Add(label_GetDataWhenStartPath);
            groupBox_Program.Controls.Add(label_ExportWhenExitPath);
            groupBox_Program.Controls.Add(textBox_ImportDataWhenStartPath);
            groupBox_Program.Controls.Add(checkBox_ImportDataWhenStart);
            groupBox_Program.Controls.Add(textBox_GetDataWhenStartPath);
            groupBox_Program.Controls.Add(textBox_ExportWhenExitPath);
            groupBox_Program.Controls.Add(checkBox_ExportWhenExit);
            groupBox_Program.Controls.Add(checkBox_MinimizeWhenClose);
            groupBox_Program.Controls.Add(checkBox_GetDataWhenStart);
            groupBox_Program.Controls.Add(checkBox_Autostart);
            groupBox_Program.Location = new Point(15, 57);
            groupBox_Program.Name = "groupBox_Program";
            groupBox_Program.Size = new Size(580, 270);
            groupBox_Program.TabIndex = 39;
            groupBox_Program.TabStop = false;
            groupBox_Program.Text = "program";
            // 
            // checkBox_OnlyOneProcessAllowed
            // 
            checkBox_OnlyOneProcessAllowed.AutoSize = true;
            checkBox_OnlyOneProcessAllowed.Location = new Point(12, 110);
            checkBox_OnlyOneProcessAllowed.Margin = new Padding(4);
            checkBox_OnlyOneProcessAllowed.Name = "checkBox_OnlyOneProcessAllowed";
            checkBox_OnlyOneProcessAllowed.Size = new Size(461, 28);
            checkBox_OnlyOneProcessAllowed.TabIndex = 49;
            checkBox_OnlyOneProcessAllowed.Text = "only one GetFolderSize process is allowed to exist";
            checkBox_OnlyOneProcessAllowed.UseVisualStyleBackColor = true;
            // 
            // textBox_ShowFileCountInterval
            // 
            textBox_ShowFileCountInterval.Location = new Point(452, 70);
            textBox_ShowFileCountInterval.Margin = new Padding(4);
            textBox_ShowFileCountInterval.Name = "textBox_ShowFileCountInterval";
            textBox_ShowFileCountInterval.Size = new Size(122, 30);
            textBox_ShowFileCountInterval.TabIndex = 48;
            // 
            // label_ShowFileCountInterval
            // 
            label_ShowFileCountInterval.AutoSize = true;
            label_ShowFileCountInterval.Location = new Point(330, 70);
            label_ShowFileCountInterval.Margin = new Padding(4, 0, 4, 0);
            label_ShowFileCountInterval.Name = "label_ShowFileCountInterval";
            label_ShowFileCountInterval.Size = new Size(78, 24);
            label_ShowFileCountInterval.TabIndex = 47;
            label_ShowFileCountInterval.Text = "interval:";
            // 
            // checkBox_EnableShowFileCountWhenGetting
            // 
            checkBox_EnableShowFileCountWhenGetting.AutoSize = true;
            checkBox_EnableShowFileCountWhenGetting.Location = new Point(12, 70);
            checkBox_EnableShowFileCountWhenGetting.Margin = new Padding(4);
            checkBox_EnableShowFileCountWhenGetting.Name = "checkBox_EnableShowFileCountWhenGetting";
            checkBox_EnableShowFileCountWhenGetting.Size = new Size(285, 28);
            checkBox_EnableShowFileCountWhenGetting.TabIndex = 46;
            checkBox_EnableShowFileCountWhenGetting.Text = "show file count when getting";
            checkBox_EnableShowFileCountWhenGetting.UseVisualStyleBackColor = true;
            checkBox_EnableShowFileCountWhenGetting.CheckedChanged += checkBox_EnableShowFileCountWhenGetting_CheckedChanged;
            // 
            // button_ImportDataWhenStartViewFile
            // 
            button_ImportDataWhenStartViewFile.Location = new Point(507, 230);
            button_ImportDataWhenStartViewFile.Margin = new Padding(4);
            button_ImportDataWhenStartViewFile.Name = "button_ImportDataWhenStartViewFile";
            button_ImportDataWhenStartViewFile.Size = new Size(67, 35);
            button_ImportDataWhenStartViewFile.TabIndex = 45;
            button_ImportDataWhenStartViewFile.Text = "view";
            button_ImportDataWhenStartViewFile.UseVisualStyleBackColor = true;
            button_ImportDataWhenStartViewFile.Click += button_ImportDataWhenStartViewFile_Click;
            // 
            // button_GetDataWhenStartViewFile
            // 
            button_GetDataWhenStartViewFile.Location = new Point(507, 190);
            button_GetDataWhenStartViewFile.Margin = new Padding(4);
            button_GetDataWhenStartViewFile.Name = "button_GetDataWhenStartViewFile";
            button_GetDataWhenStartViewFile.Size = new Size(67, 35);
            button_GetDataWhenStartViewFile.TabIndex = 44;
            button_GetDataWhenStartViewFile.Text = "view";
            button_GetDataWhenStartViewFile.UseVisualStyleBackColor = true;
            button_GetDataWhenStartViewFile.Click += button_GetDataWhenStartViewFile_Click;
            // 
            // button_ExportWhenExitViewFolder
            // 
            button_ExportWhenExitViewFolder.Location = new Point(507, 150);
            button_ExportWhenExitViewFolder.Margin = new Padding(4);
            button_ExportWhenExitViewFolder.Name = "button_ExportWhenExitViewFolder";
            button_ExportWhenExitViewFolder.Size = new Size(67, 35);
            button_ExportWhenExitViewFolder.TabIndex = 43;
            button_ExportWhenExitViewFolder.Text = "view";
            button_ExportWhenExitViewFolder.UseVisualStyleBackColor = true;
            button_ExportWhenExitViewFolder.Click += button_ExportWhenExitViewFolder_Click;
            // 
            // label_ImportDataWhenStartPath
            // 
            label_ImportDataWhenStartPath.AutoSize = true;
            label_ImportDataWhenStartPath.Location = new Point(260, 230);
            label_ImportDataWhenStartPath.Margin = new Padding(4, 0, 4, 0);
            label_ImportDataWhenStartPath.Name = "label_ImportDataWhenStartPath";
            label_ImportDataWhenStartPath.Size = new Size(54, 24);
            label_ImportDataWhenStartPath.TabIndex = 42;
            label_ImportDataWhenStartPath.Text = "path:";
            // 
            // label_GetDataWhenStartPath
            // 
            label_GetDataWhenStartPath.AutoSize = true;
            label_GetDataWhenStartPath.Location = new Point(260, 190);
            label_GetDataWhenStartPath.Margin = new Padding(4, 0, 4, 0);
            label_GetDataWhenStartPath.Name = "label_GetDataWhenStartPath";
            label_GetDataWhenStartPath.Size = new Size(54, 24);
            label_GetDataWhenStartPath.TabIndex = 41;
            label_GetDataWhenStartPath.Text = "path:";
            // 
            // label_ExportWhenExitPath
            // 
            label_ExportWhenExitPath.AutoSize = true;
            label_ExportWhenExitPath.Location = new Point(260, 150);
            label_ExportWhenExitPath.Margin = new Padding(4, 0, 4, 0);
            label_ExportWhenExitPath.Name = "label_ExportWhenExitPath";
            label_ExportWhenExitPath.Size = new Size(54, 24);
            label_ExportWhenExitPath.TabIndex = 40;
            label_ExportWhenExitPath.Text = "path:";
            // 
            // textBox_ImportDataWhenStartPath
            // 
            textBox_ImportDataWhenStartPath.Location = new Point(330, 230);
            textBox_ImportDataWhenStartPath.Margin = new Padding(4);
            textBox_ImportDataWhenStartPath.Name = "textBox_ImportDataWhenStartPath";
            textBox_ImportDataWhenStartPath.Size = new Size(170, 30);
            textBox_ImportDataWhenStartPath.TabIndex = 9;
            // 
            // checkBox_ImportDataWhenStart
            // 
            checkBox_ImportDataWhenStart.AutoSize = true;
            checkBox_ImportDataWhenStart.Location = new Point(12, 230);
            checkBox_ImportDataWhenStart.Margin = new Padding(4);
            checkBox_ImportDataWhenStart.Name = "checkBox_ImportDataWhenStart";
            checkBox_ImportDataWhenStart.Size = new Size(234, 28);
            checkBox_ImportDataWhenStart.TabIndex = 40;
            checkBox_ImportDataWhenStart.Text = "import data when start";
            checkBox_ImportDataWhenStart.UseVisualStyleBackColor = true;
            checkBox_ImportDataWhenStart.CheckedChanged += checkBox_ImportDataWhenStart_CheckedChanged;
            // 
            // textBox_GetDataWhenStartPath
            // 
            textBox_GetDataWhenStartPath.Location = new Point(330, 190);
            textBox_GetDataWhenStartPath.Margin = new Padding(4);
            textBox_GetDataWhenStartPath.Name = "textBox_GetDataWhenStartPath";
            textBox_GetDataWhenStartPath.Size = new Size(170, 30);
            textBox_GetDataWhenStartPath.TabIndex = 8;
            // 
            // textBox_ExportWhenExitPath
            // 
            textBox_ExportWhenExitPath.Location = new Point(330, 150);
            textBox_ExportWhenExitPath.Margin = new Padding(4);
            textBox_ExportWhenExitPath.Name = "textBox_ExportWhenExitPath";
            textBox_ExportWhenExitPath.Size = new Size(170, 30);
            textBox_ExportWhenExitPath.TabIndex = 7;
            // 
            // checkBox_ExportWhenExit
            // 
            checkBox_ExportWhenExit.AutoSize = true;
            checkBox_ExportWhenExit.Location = new Point(12, 150);
            checkBox_ExportWhenExit.Margin = new Padding(4);
            checkBox_ExportWhenExit.Name = "checkBox_ExportWhenExit";
            checkBox_ExportWhenExit.Size = new Size(223, 28);
            checkBox_ExportWhenExit.TabIndex = 39;
            checkBox_ExportWhenExit.Text = "export data when exit";
            checkBox_ExportWhenExit.UseVisualStyleBackColor = true;
            checkBox_ExportWhenExit.CheckedChanged += checkBox_ExportWhenExit_CheckedChanged;
            // 
            // checkBox_MinimizeWhenClose
            // 
            checkBox_MinimizeWhenClose.AutoSize = true;
            checkBox_MinimizeWhenClose.Checked = true;
            checkBox_MinimizeWhenClose.CheckState = CheckState.Checked;
            checkBox_MinimizeWhenClose.Location = new Point(233, 30);
            checkBox_MinimizeWhenClose.Margin = new Padding(4);
            checkBox_MinimizeWhenClose.Name = "checkBox_MinimizeWhenClose";
            checkBox_MinimizeWhenClose.Size = new Size(214, 28);
            checkBox_MinimizeWhenClose.TabIndex = 38;
            checkBox_MinimizeWhenClose.Text = "minimize when close";
            checkBox_MinimizeWhenClose.UseVisualStyleBackColor = true;
            // 
            // checkBox_GetDataWhenStart
            // 
            checkBox_GetDataWhenStart.AutoSize = true;
            checkBox_GetDataWhenStart.Location = new Point(12, 190);
            checkBox_GetDataWhenStart.Margin = new Padding(4);
            checkBox_GetDataWhenStart.Name = "checkBox_GetDataWhenStart";
            checkBox_GetDataWhenStart.Size = new Size(204, 28);
            checkBox_GetDataWhenStart.TabIndex = 37;
            checkBox_GetDataWhenStart.Text = "get data when start";
            checkBox_GetDataWhenStart.UseVisualStyleBackColor = true;
            checkBox_GetDataWhenStart.CheckedChanged += checkBox_GetDataWhenStart_CheckedChanged;
            // 
            // button_Load
            // 
            button_Load.Location = new Point(387, 13);
            button_Load.Margin = new Padding(4);
            button_Load.Name = "button_Load";
            button_Load.Size = new Size(100, 35);
            button_Load.TabIndex = 40;
            button_Load.Text = "load";
            button_Load.UseVisualStyleBackColor = true;
            button_Load.Click += button_Load_Click;
            // 
            // button_Save
            // 
            button_Save.Location = new Point(277, 14);
            button_Save.Margin = new Padding(4);
            button_Save.Name = "button_Save";
            button_Save.Size = new Size(100, 35);
            button_Save.TabIndex = 41;
            button_Save.Text = "save";
            button_Save.UseVisualStyleBackColor = true;
            button_Save.Click += button_Save_Click;
            // 
            // button_Apply
            // 
            button_Apply.Location = new Point(394, 896);
            button_Apply.Margin = new Padding(4);
            button_Apply.Name = "button_Apply";
            button_Apply.Size = new Size(115, 35);
            button_Apply.TabIndex = 42;
            button_Apply.Text = "apply";
            button_Apply.UseVisualStyleBackColor = true;
            button_Apply.Click += button_Apply_Click;
            // 
            // ConfigForm
            // 
            AcceptButton = button_Ok;
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button_Cancel;
            ClientSize = new Size(608, 954);
            Controls.Add(button_Apply);
            Controls.Add(button_Save);
            Controls.Add(button_Load);
            Controls.Add(groupBox_Program);
            Controls.Add(groupBox_UpdateDataWhenFolderOrFileChange);
            Controls.Add(button_Default);
            Controls.Add(button_Cancel);
            Controls.Add(button_Ok);
            Controls.Add(groupBox_Search);
            Controls.Add(comboBox_Language);
            Controls.Add(label_Language);
            Controls.Add(groupBox_BatchLoad);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfigForm";
            Text = "ConfigForm";
            groupBox_BatchLoad.ResumeLayout(false);
            groupBox_BatchLoad.PerformLayout();
            groupBox_Search.ResumeLayout(false);
            groupBox_Search.PerformLayout();
            groupBox_UpdateDataWhenFolderOrFileChange.ResumeLayout(false);
            groupBox_UpdateDataWhenFolderOrFileChange.PerformLayout();
            groupBox_Program.ResumeLayout(false);
            groupBox_Program.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox_BatchLoad;
        private Label label_Language;
        private CheckBox checkBox_EnableBatchLoad;
        private Label label_BatchLoadThreshold;
        private TextBox textBox_BatchSize;
        private Label label_BatchSize;
        private TextBox textBox_BatchLoadThreshold;
        private ComboBox comboBox_Language;
        private GroupBox groupBox_Search;
        private CheckBox checkBox_RecursiveSearch;
        private CheckBox checkBox_SearchFolders;
        private CheckBox checkBox_SearchFiles;
        private CheckBox checkBox_CaseSensitive;
        private Label label_SearchRule;
        private ComboBox comboBox_SearchRule;
        private Button button_Ok;
        private Button button_Cancel;
        private TextBox textBox_BatchInterval;
        private Label label_BatchInterval;
        private Button button_Default;
        private Label label_FileCountLimitFromTo;
        private TextBox textBox_FileCountUpperLimit;
        private TextBox textBox_FileCountLowerLimit;
        private Label label_FileCountLimit;
        private Label label_FolderSizeLimitFromTo;
        private TextBox textBox_FolderSizeUpperLimit;
        private TextBox textBox_FolderSizeLowerLimit;
        private Label label_FolderSizeLimit;
        private Label label_FileSizeLimitFromTo;
        private TextBox textBox_FileSizeUpperLimit;
        private TextBox textBox_FileSizeLowerLimit;
        private Label label_FileSizeLimit;
        private CheckBox checkBox_EnableUpdateGottenData;
        private CheckBox checkBox_Autostart;
        private GroupBox groupBox_UpdateDataWhenFolderOrFileChange;
        private GroupBox groupBox_Program;
        private CheckBox checkBox_GetDataWhenStart;
        private CheckBox checkBox_MinimizeWhenClose;
        private CheckBox checkBox_EnableUpdateImportedData;
        private CheckBox checkBox_MatchFullName;
        private CheckBox checkBox_ExportWhenExit;
        private TextBox textBox_ImportDataWhenStartPath;
        private CheckBox checkBox_ImportDataWhenStart;
        private TextBox textBox_GetDataWhenStartPath;
        private TextBox textBox_ExportWhenExitPath;
        private Label label_ExportWhenExitPath;
        private Label label_ImportDataWhenStartPath;
        private Label label_GetDataWhenStartPath;
        private Button button_ImportDataWhenStartViewFile;
        private Button button_GetDataWhenStartViewFile;
        private Button button_ExportWhenExitViewFolder;
        private CheckBox checkBox_SearchFromRoot;
        private CheckBox checkBox_EnableShowFileCountWhenGetting;
        private Label label_ShowFileCountInterval;
        private TextBox textBox_ShowFileCountInterval;
        private Button button_Load;
        private Button button_Save;
        private Button button_Apply;
        private CheckBox checkBox_OnlyOneProcessAllowed;
    }
}