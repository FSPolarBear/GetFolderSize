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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            listView_Data = new ListView();
            columnHeader_Name = new ColumnHeader();
            columnHeader_Type = new ColumnHeader();
            columnHeader_Size = new ColumnHeader();
            columnHeader_FileCount = new ColumnHeader();
            columnHeader_LastWriteTime = new ColumnHeader();
            label_Path = new Label();
            textBox_Path = new TextBox();
            button_Ok = new Button();
            label_Alert = new Label();
            button_Root = new Button();
            button_Back = new Button();
            label_NowPath = new Label();
            button_Export = new Button();
            button_Import = new Button();
            button_ShowInExplorer = new Button();
            button_Search = new Button();
            label_Search = new Label();
            textBox_Search = new TextBox();
            button_Refresh = new Button();
            button_Config = new Button();
            notifyIcon = new NotifyIcon(components);
            contextMenuStrip = new ContextMenuStrip(components);
            toolStripMenuItem_exit = new ToolStripMenuItem();
            button_Exit = new Button();
            button_Cancel = new Button();
            button_ViewFolder = new Button();
            button_ExportCurrent = new Button();
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // listView_Data
            // 
            listView_Data.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView_Data.Columns.AddRange(new ColumnHeader[] { columnHeader_Name, columnHeader_Type, columnHeader_Size, columnHeader_FileCount, columnHeader_LastWriteTime });
            listView_Data.FullRowSelect = true;
            listView_Data.Location = new Point(49, 191);
            listView_Data.Margin = new Padding(4);
            listView_Data.MultiSelect = false;
            listView_Data.Name = "listView_Data";
            listView_Data.Size = new Size(1099, 503);
            listView_Data.TabIndex = 0;
            listView_Data.UseCompatibleStateImageBehavior = false;
            listView_Data.View = View.Details;
            listView_Data.ColumnClick += listView_Data_ColumnClick;
            listView_Data.ItemMouseHover += listView_Data_ItemMouseHover;
            listView_Data.SizeChanged += listView_Data_SizeChanged;
            listView_Data.Click += listView_data_Click;
            // 
            // columnHeader_Name
            // 
            columnHeader_Name.Text = "name";
            columnHeader_Name.Width = 400;
            // 
            // columnHeader_Type
            // 
            columnHeader_Type.Text = "type";
            columnHeader_Type.Width = 100;
            // 
            // columnHeader_Size
            // 
            columnHeader_Size.Text = "size";
            columnHeader_Size.Width = 100;
            // 
            // columnHeader_FileCount
            // 
            columnHeader_FileCount.Text = "file count";
            columnHeader_FileCount.Width = 100;
            // 
            // columnHeader_LastWriteTime
            // 
            columnHeader_LastWriteTime.Text = "last write time";
            columnHeader_LastWriteTime.Width = 200;
            // 
            // label_Path
            // 
            label_Path.AutoSize = true;
            label_Path.Location = new Point(49, 25);
            label_Path.Margin = new Padding(4, 0, 4, 0);
            label_Path.Name = "label_Path";
            label_Path.Size = new Size(53, 24);
            label_Path.TabIndex = 1;
            label_Path.Text = "Path:";
            // 
            // textBox_Path
            // 
            textBox_Path.Location = new Point(132, 25);
            textBox_Path.Margin = new Padding(4);
            textBox_Path.Name = "textBox_Path";
            textBox_Path.Size = new Size(388, 30);
            textBox_Path.TabIndex = 2;
            // 
            // button_Ok
            // 
            button_Ok.Location = new Point(528, 25);
            button_Ok.Margin = new Padding(4);
            button_Ok.Name = "button_Ok";
            button_Ok.Size = new Size(122, 35);
            button_Ok.TabIndex = 3;
            button_Ok.Text = "ok";
            button_Ok.UseVisualStyleBackColor = true;
            button_Ok.Click += button_ok_Click;
            // 
            // label_Alert
            // 
            label_Alert.AutoSize = true;
            label_Alert.Location = new Point(788, 64);
            label_Alert.Margin = new Padding(4, 0, 4, 0);
            label_Alert.Name = "label_Alert";
            label_Alert.Size = new Size(0, 24);
            label_Alert.TabIndex = 4;
            // 
            // button_Root
            // 
            button_Root.Location = new Point(49, 116);
            button_Root.Margin = new Padding(4);
            button_Root.Name = "button_Root";
            button_Root.Size = new Size(115, 35);
            button_Root.TabIndex = 5;
            button_Root.Text = "root";
            button_Root.UseVisualStyleBackColor = true;
            button_Root.Click += button_Root_Click;
            // 
            // button_Back
            // 
            button_Back.Location = new Point(171, 116);
            button_Back.Margin = new Padding(4);
            button_Back.Name = "button_Back";
            button_Back.Size = new Size(115, 35);
            button_Back.TabIndex = 6;
            button_Back.Text = "back";
            button_Back.UseVisualStyleBackColor = true;
            button_Back.Click += button_Back_Click;
            // 
            // label_NowPath
            // 
            label_NowPath.AutoSize = true;
            label_NowPath.Location = new Point(49, 156);
            label_NowPath.Margin = new Padding(4, 0, 4, 0);
            label_NowPath.Name = "label_NowPath";
            label_NowPath.Size = new Size(0, 24);
            label_NowPath.TabIndex = 7;
            // 
            // button_Export
            // 
            button_Export.Location = new Point(293, 116);
            button_Export.Margin = new Padding(4);
            button_Export.Name = "button_Export";
            button_Export.Size = new Size(115, 35);
            button_Export.TabIndex = 8;
            button_Export.Text = "export";
            button_Export.UseVisualStyleBackColor = true;
            button_Export.Click += button_Export_Click;
            // 
            // button_Import
            // 
            button_Import.Location = new Point(592, 116);
            button_Import.Margin = new Padding(4);
            button_Import.Name = "button_Import";
            button_Import.Size = new Size(115, 35);
            button_Import.TabIndex = 9;
            button_Import.Text = "import";
            button_Import.UseVisualStyleBackColor = true;
            button_Import.Click += button_Import_Click;
            // 
            // button_ShowInExplorer
            // 
            button_ShowInExplorer.Location = new Point(714, 116);
            button_ShowInExplorer.Margin = new Padding(4);
            button_ShowInExplorer.Name = "button_ShowInExplorer";
            button_ShowInExplorer.Size = new Size(188, 35);
            button_ShowInExplorer.TabIndex = 10;
            button_ShowInExplorer.Text = "show in explorer";
            button_ShowInExplorer.UseVisualStyleBackColor = true;
            button_ShowInExplorer.Click += button_ShowInExplorer_Click;
            // 
            // button_Search
            // 
            button_Search.Location = new Point(528, 64);
            button_Search.Margin = new Padding(4);
            button_Search.Name = "button_Search";
            button_Search.Size = new Size(122, 35);
            button_Search.TabIndex = 13;
            button_Search.Text = "search";
            button_Search.UseVisualStyleBackColor = true;
            button_Search.Click += button_Search_Click;
            // 
            // label_Search
            // 
            label_Search.AutoSize = true;
            label_Search.Location = new Point(49, 64);
            label_Search.Margin = new Padding(4, 0, 4, 0);
            label_Search.Name = "label_Search";
            label_Search.Size = new Size(71, 24);
            label_Search.TabIndex = 11;
            label_Search.Text = "Search:";
            // 
            // textBox_Search
            // 
            textBox_Search.Location = new Point(132, 64);
            textBox_Search.Margin = new Padding(4);
            textBox_Search.Name = "textBox_Search";
            textBox_Search.Size = new Size(388, 30);
            textBox_Search.TabIndex = 12;
            // 
            // button_Refresh
            // 
            button_Refresh.Location = new Point(910, 116);
            button_Refresh.Margin = new Padding(4);
            button_Refresh.Name = "button_Refresh";
            button_Refresh.Size = new Size(115, 35);
            button_Refresh.TabIndex = 19;
            button_Refresh.Text = "refresh";
            button_Refresh.UseVisualStyleBackColor = true;
            button_Refresh.Click += button_Refresh_Click;
            // 
            // button_Config
            // 
            button_Config.Location = new Point(658, 64);
            button_Config.Margin = new Padding(4);
            button_Config.Name = "button_Config";
            button_Config.Size = new Size(122, 35);
            button_Config.TabIndex = 21;
            button_Config.Text = "config";
            button_Config.UseVisualStyleBackColor = true;
            button_Config.Click += button_Config_Click;
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "GetFolderSize";
            notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.ImageScalingSize = new Size(24, 24);
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { toolStripMenuItem_exit });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(112, 34);
            // 
            // toolStripMenuItem_exit
            // 
            toolStripMenuItem_exit.Name = "toolStripMenuItem_exit";
            toolStripMenuItem_exit.Size = new Size(111, 30);
            toolStripMenuItem_exit.Text = "exit";
            toolStripMenuItem_exit.Click += toolStripMenuItem_Exit_Click;
            // 
            // button_Exit
            // 
            button_Exit.Location = new Point(1033, 116);
            button_Exit.Margin = new Padding(4);
            button_Exit.Name = "button_Exit";
            button_Exit.Size = new Size(115, 35);
            button_Exit.TabIndex = 22;
            button_Exit.Text = "exit";
            button_Exit.UseVisualStyleBackColor = true;
            button_Exit.Click += button_Exit_Click;
            // 
            // button_Cancel
            // 
            button_Cancel.Enabled = false;
            button_Cancel.Location = new Point(788, 25);
            button_Cancel.Margin = new Padding(4);
            button_Cancel.Name = "button_Cancel";
            button_Cancel.Size = new Size(122, 35);
            button_Cancel.TabIndex = 23;
            button_Cancel.Text = "cancel";
            button_Cancel.UseVisualStyleBackColor = true;
            button_Cancel.Click += button_Cancel_Click;
            // 
            // button_ViewFolder
            // 
            button_ViewFolder.Location = new Point(658, 25);
            button_ViewFolder.Margin = new Padding(4);
            button_ViewFolder.Name = "button_ViewFolder";
            button_ViewFolder.Size = new Size(122, 35);
            button_ViewFolder.TabIndex = 24;
            button_ViewFolder.Text = "view";
            button_ViewFolder.UseVisualStyleBackColor = true;
            button_ViewFolder.Click += button_ViewFolder_Click;
            // 
            // button_ExportCurrent
            // 
            button_ExportCurrent.Location = new Point(416, 116);
            button_ExportCurrent.Margin = new Padding(4);
            button_ExportCurrent.Name = "button_ExportCurrent";
            button_ExportCurrent.Size = new Size(168, 35);
            button_ExportCurrent.TabIndex = 25;
            button_ExportCurrent.Text = "export current";
            button_ExportCurrent.UseVisualStyleBackColor = true;
            button_ExportCurrent.Click += button_ExportCurrent_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 724);
            Controls.Add(button_ExportCurrent);
            Controls.Add(button_ViewFolder);
            Controls.Add(button_Cancel);
            Controls.Add(button_Exit);
            Controls.Add(button_Config);
            Controls.Add(button_Refresh);
            Controls.Add(button_Search);
            Controls.Add(label_Search);
            Controls.Add(textBox_Search);
            Controls.Add(button_ShowInExplorer);
            Controls.Add(button_Import);
            Controls.Add(button_Export);
            Controls.Add(label_NowPath);
            Controls.Add(button_Back);
            Controls.Add(button_Root);
            Controls.Add(label_Alert);
            Controls.Add(button_Ok);
            Controls.Add(label_Path);
            Controls.Add(listView_Data);
            Controls.Add(textBox_Path);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "MainForm";
            Text = " GetFolderSize";
            FormClosing += MainForm_FormClosing;
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView_Data;
        private ColumnHeader columnHeader_Type;
        private ColumnHeader columnHeader_Name;
        private ColumnHeader columnHeader_Size;
        private ColumnHeader columnHeader_FileCount;
        private Label label_Path;
        private TextBox textBox_Path;
        private Button button_Ok;
        private Label label_Alert;
        private Button button_Root;
        private Button button_Back;
        private Label label_NowPath;
        private Button button_Export;
        private Button button_Import;
        private Button button_ShowInExplorer;
        private Button button_Search;
        private Label label_Search;
        private TextBox textBox_Search;
        private Button button_Refresh;
        private ColumnHeader columnHeader_LastWriteTime;
        private Button button_Config;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem toolStripMenuItem_exit;
        private Button button_Exit;
        private Button button_Cancel;
        private Button button_ViewFolder;
        private Button button_ExportCurrent;
    }
}