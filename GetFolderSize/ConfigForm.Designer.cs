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
            this.groupBox_batch_load = new System.Windows.Forms.GroupBox();
            this.textBox_batch_interval = new System.Windows.Forms.TextBox();
            this.label_batch_interval = new System.Windows.Forms.Label();
            this.textBox_batch_size = new System.Windows.Forms.TextBox();
            this.label_batch_size = new System.Windows.Forms.Label();
            this.textBox_batch_load_threshold = new System.Windows.Forms.TextBox();
            this.label_batch_load_threshold = new System.Windows.Forms.Label();
            this.checkBox_enable_batch_load = new System.Windows.Forms.CheckBox();
            this.label_language = new System.Windows.Forms.Label();
            this.comboBox_language = new System.Windows.Forms.ComboBox();
            this.groupBox_search = new System.Windows.Forms.GroupBox();
            this.label_file_count_limit_from_to = new System.Windows.Forms.Label();
            this.textBox_file_count_upper_limit = new System.Windows.Forms.TextBox();
            this.textBox_file_count_lower_limit = new System.Windows.Forms.TextBox();
            this.label_file_count_limit = new System.Windows.Forms.Label();
            this.label_folder_size_limit_from_to = new System.Windows.Forms.Label();
            this.textBox_folder_size_upper_limit = new System.Windows.Forms.TextBox();
            this.textBox_folder_size_lower_limit = new System.Windows.Forms.TextBox();
            this.label_folder_size_limit = new System.Windows.Forms.Label();
            this.label_file_size_limit_from_to = new System.Windows.Forms.Label();
            this.textBox_file_size_upper_limit = new System.Windows.Forms.TextBox();
            this.textBox_file_size_lower_limit = new System.Windows.Forms.TextBox();
            this.label_file_size_limit = new System.Windows.Forms.Label();
            this.checkBox_case_sensitive = new System.Windows.Forms.CheckBox();
            this.label_search_rule = new System.Windows.Forms.Label();
            this.comboBox_search_rule = new System.Windows.Forms.ComboBox();
            this.checkBox_recursive_search = new System.Windows.Forms.CheckBox();
            this.checkBox_search_folders = new System.Windows.Forms.CheckBox();
            this.checkBox_search_files = new System.Windows.Forms.CheckBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_default = new System.Windows.Forms.Button();
            this.groupBox_batch_load.SuspendLayout();
            this.groupBox_search.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_batch_load
            // 
            this.groupBox_batch_load.Controls.Add(this.textBox_batch_interval);
            this.groupBox_batch_load.Controls.Add(this.label_batch_interval);
            this.groupBox_batch_load.Controls.Add(this.textBox_batch_size);
            this.groupBox_batch_load.Controls.Add(this.label_batch_size);
            this.groupBox_batch_load.Controls.Add(this.textBox_batch_load_threshold);
            this.groupBox_batch_load.Controls.Add(this.label_batch_load_threshold);
            this.groupBox_batch_load.Controls.Add(this.checkBox_enable_batch_load);
            this.groupBox_batch_load.Location = new System.Drawing.Point(12, 48);
            this.groupBox_batch_load.Name = "groupBox_batch_load";
            this.groupBox_batch_load.Size = new System.Drawing.Size(450, 165);
            this.groupBox_batch_load.TabIndex = 0;
            this.groupBox_batch_load.TabStop = false;
            this.groupBox_batch_load.Text = "batch load";
            // 
            // textBox_batch_interval
            // 
            this.textBox_batch_interval.Location = new System.Drawing.Point(191, 120);
            this.textBox_batch_interval.Name = "textBox_batch_interval";
            this.textBox_batch_interval.Size = new System.Drawing.Size(122, 27);
            this.textBox_batch_interval.TabIndex = 6;
            // 
            // label_batch_interval
            // 
            this.label_batch_interval.AutoSize = true;
            this.label_batch_interval.Location = new System.Drawing.Point(7, 123);
            this.label_batch_interval.Name = "label_batch_interval";
            this.label_batch_interval.Size = new System.Drawing.Size(108, 20);
            this.label_batch_interval.TabIndex = 5;
            this.label_batch_interval.Text = "batch interval";
            // 
            // textBox_batch_size
            // 
            this.textBox_batch_size.Location = new System.Drawing.Point(191, 87);
            this.textBox_batch_size.Name = "textBox_batch_size";
            this.textBox_batch_size.Size = new System.Drawing.Size(122, 27);
            this.textBox_batch_size.TabIndex = 4;
            // 
            // label_batch_size
            // 
            this.label_batch_size.AutoSize = true;
            this.label_batch_size.Location = new System.Drawing.Point(7, 90);
            this.label_batch_size.Name = "label_batch_size";
            this.label_batch_size.Size = new System.Drawing.Size(85, 20);
            this.label_batch_size.TabIndex = 3;
            this.label_batch_size.Text = "batch size:";
            // 
            // textBox_batch_load_threshold
            // 
            this.textBox_batch_load_threshold.Location = new System.Drawing.Point(191, 54);
            this.textBox_batch_load_threshold.Name = "textBox_batch_load_threshold";
            this.textBox_batch_load_threshold.Size = new System.Drawing.Size(122, 27);
            this.textBox_batch_load_threshold.TabIndex = 2;
            // 
            // label_batch_load_threshold
            // 
            this.label_batch_load_threshold.AutoSize = true;
            this.label_batch_load_threshold.Location = new System.Drawing.Point(7, 57);
            this.label_batch_load_threshold.Name = "label_batch_load_threshold";
            this.label_batch_load_threshold.Size = new System.Drawing.Size(164, 20);
            this.label_batch_load_threshold.TabIndex = 1;
            this.label_batch_load_threshold.Text = "batch load threshold:";
            // 
            // checkBox_enable_batch_load
            // 
            this.checkBox_enable_batch_load.AutoSize = true;
            this.checkBox_enable_batch_load.Location = new System.Drawing.Point(6, 26);
            this.checkBox_enable_batch_load.Name = "checkBox_enable_batch_load";
            this.checkBox_enable_batch_load.Size = new System.Drawing.Size(161, 24);
            this.checkBox_enable_batch_load.TabIndex = 0;
            this.checkBox_enable_batch_load.Text = "enable batch load";
            this.checkBox_enable_batch_load.UseVisualStyleBackColor = true;
            this.checkBox_enable_batch_load.CheckedChanged += new System.EventHandler(this.checkBox_enable_batch_load_CheckedChanged);
            // 
            // label_language
            // 
            this.label_language.AutoSize = true;
            this.label_language.Location = new System.Drawing.Point(12, 15);
            this.label_language.Name = "label_language";
            this.label_language.Size = new System.Drawing.Size(80, 20);
            this.label_language.TabIndex = 1;
            this.label_language.Text = "language:";
            // 
            // comboBox_language
            // 
            this.comboBox_language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_language.FormattingEnabled = true;
            this.comboBox_language.Location = new System.Drawing.Point(98, 12);
            this.comboBox_language.Name = "comboBox_language";
            this.comboBox_language.Size = new System.Drawing.Size(102, 28);
            this.comboBox_language.TabIndex = 17;
            // 
            // groupBox_search
            // 
            this.groupBox_search.Controls.Add(this.label_file_count_limit_from_to);
            this.groupBox_search.Controls.Add(this.textBox_file_count_upper_limit);
            this.groupBox_search.Controls.Add(this.textBox_file_count_lower_limit);
            this.groupBox_search.Controls.Add(this.label_file_count_limit);
            this.groupBox_search.Controls.Add(this.label_folder_size_limit_from_to);
            this.groupBox_search.Controls.Add(this.textBox_folder_size_upper_limit);
            this.groupBox_search.Controls.Add(this.textBox_folder_size_lower_limit);
            this.groupBox_search.Controls.Add(this.label_folder_size_limit);
            this.groupBox_search.Controls.Add(this.label_file_size_limit_from_to);
            this.groupBox_search.Controls.Add(this.textBox_file_size_upper_limit);
            this.groupBox_search.Controls.Add(this.textBox_file_size_lower_limit);
            this.groupBox_search.Controls.Add(this.label_file_size_limit);
            this.groupBox_search.Controls.Add(this.checkBox_case_sensitive);
            this.groupBox_search.Controls.Add(this.label_search_rule);
            this.groupBox_search.Controls.Add(this.comboBox_search_rule);
            this.groupBox_search.Controls.Add(this.checkBox_recursive_search);
            this.groupBox_search.Controls.Add(this.checkBox_search_folders);
            this.groupBox_search.Controls.Add(this.checkBox_search_files);
            this.groupBox_search.Location = new System.Drawing.Point(12, 219);
            this.groupBox_search.Name = "groupBox_search";
            this.groupBox_search.Size = new System.Drawing.Size(450, 237);
            this.groupBox_search.TabIndex = 18;
            this.groupBox_search.TabStop = false;
            this.groupBox_search.Text = "search";
            // 
            // label_file_count_limit_from_to
            // 
            this.label_file_count_limit_from_to.AutoSize = true;
            this.label_file_count_limit_from_to.Location = new System.Drawing.Point(277, 199);
            this.label_file_count_limit_from_to.Name = "label_file_count_limit_from_to";
            this.label_file_count_limit_from_to.Size = new System.Drawing.Size(20, 20);
            this.label_file_count_limit_from_to.TabIndex = 35;
            this.label_file_count_limit_from_to.Text = "~";
            // 
            // textBox_file_count_upper_limit
            // 
            this.textBox_file_count_upper_limit.Location = new System.Drawing.Point(314, 196);
            this.textBox_file_count_upper_limit.Name = "textBox_file_count_upper_limit";
            this.textBox_file_count_upper_limit.Size = new System.Drawing.Size(100, 27);
            this.textBox_file_count_upper_limit.TabIndex = 34;
            // 
            // textBox_file_count_lower_limit
            // 
            this.textBox_file_count_lower_limit.Location = new System.Drawing.Point(161, 196);
            this.textBox_file_count_lower_limit.Name = "textBox_file_count_lower_limit";
            this.textBox_file_count_lower_limit.Size = new System.Drawing.Size(100, 27);
            this.textBox_file_count_lower_limit.TabIndex = 32;
            // 
            // label_file_count_limit
            // 
            this.label_file_count_limit.AutoSize = true;
            this.label_file_count_limit.Location = new System.Drawing.Point(7, 199);
            this.label_file_count_limit.Name = "label_file_count_limit";
            this.label_file_count_limit.Size = new System.Drawing.Size(117, 20);
            this.label_file_count_limit.TabIndex = 33;
            this.label_file_count_limit.Text = "file count limit:";
            // 
            // label_folder_size_limit_from_to
            // 
            this.label_folder_size_limit_from_to.AutoSize = true;
            this.label_folder_size_limit_from_to.Location = new System.Drawing.Point(277, 166);
            this.label_folder_size_limit_from_to.Name = "label_folder_size_limit_from_to";
            this.label_folder_size_limit_from_to.Size = new System.Drawing.Size(20, 20);
            this.label_folder_size_limit_from_to.TabIndex = 31;
            this.label_folder_size_limit_from_to.Text = "~";
            // 
            // textBox_folder_size_upper_limit
            // 
            this.textBox_folder_size_upper_limit.Location = new System.Drawing.Point(314, 163);
            this.textBox_folder_size_upper_limit.Name = "textBox_folder_size_upper_limit";
            this.textBox_folder_size_upper_limit.Size = new System.Drawing.Size(100, 27);
            this.textBox_folder_size_upper_limit.TabIndex = 30;
            // 
            // textBox_folder_size_lower_limit
            // 
            this.textBox_folder_size_lower_limit.Location = new System.Drawing.Point(161, 163);
            this.textBox_folder_size_lower_limit.Name = "textBox_folder_size_lower_limit";
            this.textBox_folder_size_lower_limit.Size = new System.Drawing.Size(100, 27);
            this.textBox_folder_size_lower_limit.TabIndex = 28;
            // 
            // label_folder_size_limit
            // 
            this.label_folder_size_limit.AutoSize = true;
            this.label_folder_size_limit.Location = new System.Drawing.Point(7, 166);
            this.label_folder_size_limit.Name = "label_folder_size_limit";
            this.label_folder_size_limit.Size = new System.Drawing.Size(124, 20);
            this.label_folder_size_limit.TabIndex = 29;
            this.label_folder_size_limit.Text = "folder size limit:";
            // 
            // label_file_size_limit_from_to
            // 
            this.label_file_size_limit_from_to.AutoSize = true;
            this.label_file_size_limit_from_to.Location = new System.Drawing.Point(277, 133);
            this.label_file_size_limit_from_to.Name = "label_file_size_limit_from_to";
            this.label_file_size_limit_from_to.Size = new System.Drawing.Size(20, 20);
            this.label_file_size_limit_from_to.TabIndex = 27;
            this.label_file_size_limit_from_to.Text = "~";
            // 
            // textBox_file_size_upper_limit
            // 
            this.textBox_file_size_upper_limit.Location = new System.Drawing.Point(314, 130);
            this.textBox_file_size_upper_limit.Name = "textBox_file_size_upper_limit";
            this.textBox_file_size_upper_limit.Size = new System.Drawing.Size(100, 27);
            this.textBox_file_size_upper_limit.TabIndex = 26;
            // 
            // textBox_file_size_lower_limit
            // 
            this.textBox_file_size_lower_limit.Location = new System.Drawing.Point(161, 130);
            this.textBox_file_size_lower_limit.Name = "textBox_file_size_lower_limit";
            this.textBox_file_size_lower_limit.Size = new System.Drawing.Size(100, 27);
            this.textBox_file_size_lower_limit.TabIndex = 7;
            // 
            // label_file_size_limit
            // 
            this.label_file_size_limit.AutoSize = true;
            this.label_file_size_limit.Location = new System.Drawing.Point(7, 133);
            this.label_file_size_limit.Name = "label_file_size_limit";
            this.label_file_size_limit.Size = new System.Drawing.Size(102, 20);
            this.label_file_size_limit.TabIndex = 25;
            this.label_file_size_limit.Text = "file size limit:";
            // 
            // checkBox_case_sensitive
            // 
            this.checkBox_case_sensitive.AutoSize = true;
            this.checkBox_case_sensitive.Checked = true;
            this.checkBox_case_sensitive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_case_sensitive.Location = new System.Drawing.Point(191, 98);
            this.checkBox_case_sensitive.Name = "checkBox_case_sensitive";
            this.checkBox_case_sensitive.Size = new System.Drawing.Size(130, 24);
            this.checkBox_case_sensitive.TabIndex = 24;
            this.checkBox_case_sensitive.Text = "case sensitive";
            this.checkBox_case_sensitive.UseVisualStyleBackColor = true;
            // 
            // label_search_rule
            // 
            this.label_search_rule.AutoSize = true;
            this.label_search_rule.Location = new System.Drawing.Point(7, 35);
            this.label_search_rule.Name = "label_search_rule";
            this.label_search_rule.Size = new System.Drawing.Size(94, 20);
            this.label_search_rule.TabIndex = 23;
            this.label_search_rule.Text = "Search rule:";
            // 
            // comboBox_search_rule
            // 
            this.comboBox_search_rule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_search_rule.FormattingEnabled = true;
            this.comboBox_search_rule.Items.AddRange(new object[] {
            "include",
            "same",
            "regular"});
            this.comboBox_search_rule.Location = new System.Drawing.Point(107, 31);
            this.comboBox_search_rule.Name = "comboBox_search_rule";
            this.comboBox_search_rule.Size = new System.Drawing.Size(102, 28);
            this.comboBox_search_rule.TabIndex = 22;
            // 
            // checkBox_recursive_search
            // 
            this.checkBox_recursive_search.AutoSize = true;
            this.checkBox_recursive_search.Checked = true;
            this.checkBox_recursive_search.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_recursive_search.Location = new System.Drawing.Point(6, 98);
            this.checkBox_recursive_search.Name = "checkBox_recursive_search";
            this.checkBox_recursive_search.Size = new System.Drawing.Size(148, 24);
            this.checkBox_recursive_search.TabIndex = 21;
            this.checkBox_recursive_search.Text = "recursive search";
            this.checkBox_recursive_search.UseVisualStyleBackColor = true;
            // 
            // checkBox_search_folders
            // 
            this.checkBox_search_folders.AutoSize = true;
            this.checkBox_search_folders.Checked = true;
            this.checkBox_search_folders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_search_folders.Location = new System.Drawing.Point(191, 68);
            this.checkBox_search_folders.Name = "checkBox_search_folders";
            this.checkBox_search_folders.Size = new System.Drawing.Size(133, 24);
            this.checkBox_search_folders.TabIndex = 20;
            this.checkBox_search_folders.Text = "search folders";
            this.checkBox_search_folders.UseVisualStyleBackColor = true;
            // 
            // checkBox_search_files
            // 
            this.checkBox_search_files.AutoSize = true;
            this.checkBox_search_files.Checked = true;
            this.checkBox_search_files.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_search_files.Location = new System.Drawing.Point(7, 68);
            this.checkBox_search_files.Name = "checkBox_search_files";
            this.checkBox_search_files.Size = new System.Drawing.Size(111, 24);
            this.checkBox_search_files.TabIndex = 19;
            this.checkBox_search_files.Text = "search files";
            this.checkBox_search_files.UseVisualStyleBackColor = true;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(98, 462);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(94, 29);
            this.button_ok.TabIndex = 19;
            this.button_ok.Text = "ok";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(280, 462);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(94, 29);
            this.button_cancel.TabIndex = 20;
            this.button_cancel.Text = "cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_default
            // 
            this.button_default.Location = new System.Drawing.Point(368, 12);
            this.button_default.Name = "button_default";
            this.button_default.Size = new System.Drawing.Size(94, 29);
            this.button_default.TabIndex = 21;
            this.button_default.Text = "default";
            this.button_default.UseVisualStyleBackColor = true;
            this.button_default.Click += new System.EventHandler(this.button_default_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 503);
            this.Controls.Add(this.button_default);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.groupBox_search);
            this.Controls.Add(this.comboBox_language);
            this.Controls.Add(this.label_language);
            this.Controls.Add(this.groupBox_batch_load);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            this.groupBox_batch_load.ResumeLayout(false);
            this.groupBox_batch_load.PerformLayout();
            this.groupBox_search.ResumeLayout(false);
            this.groupBox_search.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox_batch_load;
        private Label label_language;
        private CheckBox checkBox_enable_batch_load;
        private Label label_batch_load_threshold;
        private TextBox textBox_batch_size;
        private Label label_batch_size;
        private TextBox textBox_batch_load_threshold;
        private ComboBox comboBox_language;
        private GroupBox groupBox_search;
        private CheckBox checkBox_recursive_search;
        private CheckBox checkBox_search_folders;
        private CheckBox checkBox_search_files;
        private CheckBox checkBox_case_sensitive;
        private Label label_search_rule;
        private ComboBox comboBox_search_rule;
        private Button button_ok;
        private Button button_cancel;
        private TextBox textBox_batch_interval;
        private Label label_batch_interval;
        private Button button_default;
        private Label label_file_count_limit_from_to;
        private TextBox textBox_file_count_upper_limit;
        private TextBox textBox_file_count_lower_limit;
        private Label label_file_count_limit;
        private Label label_folder_size_limit_from_to;
        private TextBox textBox_folder_size_upper_limit;
        private TextBox textBox_folder_size_lower_limit;
        private Label label_folder_size_limit;
        private Label label_file_size_limit_from_to;
        private TextBox textBox_file_size_upper_limit;
        private TextBox textBox_file_size_lower_limit;
        private Label label_file_size_limit;
    }
}