namespace GetFolderSize
{
    partial class MainForm
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
            this.listView_data = new System.Windows.Forms.ListView();
            this.columnHeader_name = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_type = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_size = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_file_count = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_last_write_time = new System.Windows.Forms.ColumnHeader();
            this.label_path = new System.Windows.Forms.Label();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.label_alert = new System.Windows.Forms.Label();
            this.button_root = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            this.label_now_path = new System.Windows.Forms.Label();
            this.button_export = new System.Windows.Forms.Button();
            this.button_import = new System.Windows.Forms.Button();
            this.button_show_in_explorer = new System.Windows.Forms.Button();
            this.button_search = new System.Windows.Forms.Button();
            this.label_search = new System.Windows.Forms.Label();
            this.textBox_search = new System.Windows.Forms.TextBox();
            this.button_refresh = new System.Windows.Forms.Button();
            this.button_config = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView_data
            // 
            this.listView_data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_data.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_name,
            this.columnHeader_type,
            this.columnHeader_size,
            this.columnHeader_file_count,
            this.columnHeader_last_write_time});
            this.listView_data.FullRowSelect = true;
            this.listView_data.Location = new System.Drawing.Point(40, 159);
            this.listView_data.MultiSelect = false;
            this.listView_data.Name = "listView_data";
            this.listView_data.Size = new System.Drawing.Size(900, 420);
            this.listView_data.TabIndex = 0;
            this.listView_data.UseCompatibleStateImageBehavior = false;
            this.listView_data.View = System.Windows.Forms.View.Details;
            this.listView_data.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_data_ColumnClick);
            this.listView_data.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.listView_data_ItemMouseHover);
            this.listView_data.SizeChanged += new System.EventHandler(this.listView_data_SizeChanged);
            this.listView_data.Click += new System.EventHandler(this.listView_data_Click);
            // 
            // columnHeader_name
            // 
            this.columnHeader_name.Text = "name";
            this.columnHeader_name.Width = 400;
            // 
            // columnHeader_type
            // 
            this.columnHeader_type.Text = "type";
            this.columnHeader_type.Width = 100;
            // 
            // columnHeader_size
            // 
            this.columnHeader_size.Text = "size";
            this.columnHeader_size.Width = 100;
            // 
            // columnHeader_file_count
            // 
            this.columnHeader_file_count.Text = "file count";
            this.columnHeader_file_count.Width = 100;
            // 
            // columnHeader_last_write_time
            // 
            this.columnHeader_last_write_time.Text = "last write time";
            this.columnHeader_last_write_time.Width = 200;
            // 
            // label_path
            // 
            this.label_path.AutoSize = true;
            this.label_path.Location = new System.Drawing.Point(40, 24);
            this.label_path.Name = "label_path";
            this.label_path.Size = new System.Drawing.Size(45, 20);
            this.label_path.TabIndex = 1;
            this.label_path.Text = "Path:";
            // 
            // textBox_path
            // 
            this.textBox_path.Location = new System.Drawing.Point(108, 21);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(318, 27);
            this.textBox_path.TabIndex = 2;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(432, 20);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(100, 29);
            this.button_ok.TabIndex = 3;
            this.button_ok.Text = "ok";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // label_alert
            // 
            this.label_alert.AutoSize = true;
            this.label_alert.Location = new System.Drawing.Point(538, 24);
            this.label_alert.Name = "label_alert";
            this.label_alert.Size = new System.Drawing.Size(0, 20);
            this.label_alert.TabIndex = 4;
            // 
            // button_root
            // 
            this.button_root.Location = new System.Drawing.Point(40, 97);
            this.button_root.Name = "button_root";
            this.button_root.Size = new System.Drawing.Size(94, 29);
            this.button_root.TabIndex = 5;
            this.button_root.Text = "root";
            this.button_root.UseVisualStyleBackColor = true;
            this.button_root.Click += new System.EventHandler(this.button_root_Click);
            // 
            // button_back
            // 
            this.button_back.Location = new System.Drawing.Point(140, 97);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(94, 29);
            this.button_back.TabIndex = 6;
            this.button_back.Text = "back";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            // 
            // label_now_path
            // 
            this.label_now_path.AutoSize = true;
            this.label_now_path.Location = new System.Drawing.Point(40, 130);
            this.label_now_path.Name = "label_now_path";
            this.label_now_path.Size = new System.Drawing.Size(0, 20);
            this.label_now_path.TabIndex = 7;
            // 
            // button_export
            // 
            this.button_export.Location = new System.Drawing.Point(240, 97);
            this.button_export.Name = "button_export";
            this.button_export.Size = new System.Drawing.Size(94, 29);
            this.button_export.TabIndex = 8;
            this.button_export.Text = "export";
            this.button_export.UseVisualStyleBackColor = true;
            this.button_export.Click += new System.EventHandler(this.button_export_Click);
            // 
            // button_import
            // 
            this.button_import.Location = new System.Drawing.Point(338, 97);
            this.button_import.Name = "button_import";
            this.button_import.Size = new System.Drawing.Size(94, 29);
            this.button_import.TabIndex = 9;
            this.button_import.Text = "import";
            this.button_import.UseVisualStyleBackColor = true;
            this.button_import.Click += new System.EventHandler(this.button_import_Click);
            // 
            // button_show_in_explorer
            // 
            this.button_show_in_explorer.Location = new System.Drawing.Point(438, 97);
            this.button_show_in_explorer.Name = "button_show_in_explorer";
            this.button_show_in_explorer.Size = new System.Drawing.Size(154, 29);
            this.button_show_in_explorer.TabIndex = 10;
            this.button_show_in_explorer.Text = "show in explorer";
            this.button_show_in_explorer.UseVisualStyleBackColor = true;
            this.button_show_in_explorer.Click += new System.EventHandler(this.button_show_in_explorer_Click);
            // 
            // button_search
            // 
            this.button_search.Location = new System.Drawing.Point(432, 53);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(100, 29);
            this.button_search.TabIndex = 13;
            this.button_search.Text = "search";
            this.button_search.UseVisualStyleBackColor = true;
            this.button_search.Click += new System.EventHandler(this.button_search_Click);
            // 
            // label_search
            // 
            this.label_search.AutoSize = true;
            this.label_search.Location = new System.Drawing.Point(40, 57);
            this.label_search.Name = "label_search";
            this.label_search.Size = new System.Drawing.Size(62, 20);
            this.label_search.TabIndex = 11;
            this.label_search.Text = "Search:";
            // 
            // textBox_search
            // 
            this.textBox_search.Location = new System.Drawing.Point(108, 53);
            this.textBox_search.Name = "textBox_search";
            this.textBox_search.Size = new System.Drawing.Size(318, 27);
            this.textBox_search.TabIndex = 12;
            // 
            // button_refresh
            // 
            this.button_refresh.Location = new System.Drawing.Point(598, 97);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(94, 29);
            this.button_refresh.TabIndex = 19;
            this.button_refresh.Text = "refresh";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // button_config
            // 
            this.button_config.Location = new System.Drawing.Point(538, 53);
            this.button_config.Name = "button_config";
            this.button_config.Size = new System.Drawing.Size(100, 29);
            this.button_config.TabIndex = 21;
            this.button_config.Text = "config";
            this.button_config.UseVisualStyleBackColor = true;
            this.button_config.Click += new System.EventHandler(this.button_config_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 603);
            this.Controls.Add(this.button_config);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.button_search);
            this.Controls.Add(this.label_search);
            this.Controls.Add(this.textBox_search);
            this.Controls.Add(this.button_show_in_explorer);
            this.Controls.Add(this.button_import);
            this.Controls.Add(this.button_export);
            this.Controls.Add(this.label_now_path);
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_root);
            this.Controls.Add(this.label_alert);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.label_path);
            this.Controls.Add(this.listView_data);
            this.Controls.Add(this.textBox_path);
            this.Name = "MainForm";
            this.Text = "GetFolderSize";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView listView_data;
        private ColumnHeader columnHeader_type;
        private ColumnHeader columnHeader_name;
        private ColumnHeader columnHeader_size;
        private ColumnHeader columnHeader_file_count;
        private Label label_path;
        private TextBox textBox_path;
        private Button button_ok;
        private Label label_alert;
        private Button button_root;
        private Button button_back;
        private Label label_now_path;
        private Button button_export;
        private Button button_import;
        private Button button_show_in_explorer;
        private Button button_search;
        private Label label_search;
        private TextBox textBox_search;
        private Button button_refresh;
        private ColumnHeader columnHeader_last_write_time;
        private Button button_config;
    }
}