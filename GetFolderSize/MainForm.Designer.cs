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
            this.label_path = new System.Windows.Forms.Label();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.label_not_found = new System.Windows.Forms.Label();
            this.button_root = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            this.label_now_path = new System.Windows.Forms.Label();
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
            this.columnHeader_file_count});
            this.listView_data.FullRowSelect = true;
            this.listView_data.Location = new System.Drawing.Point(42, 121);
            this.listView_data.MultiSelect = false;
            this.listView_data.Name = "listView_data";
            this.listView_data.Size = new System.Drawing.Size(700, 400);
            this.listView_data.TabIndex = 0;
            this.listView_data.UseCompatibleStateImageBehavior = false;
            this.listView_data.View = System.Windows.Forms.View.Details;
            this.listView_data.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_data_ColumnClick);
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
            // label_path
            // 
            this.label_path.AutoSize = true;
            this.label_path.Location = new System.Drawing.Point(40, 51);
            this.label_path.Name = "label_path";
            this.label_path.Size = new System.Drawing.Size(45, 20);
            this.label_path.TabIndex = 1;
            this.label_path.Text = "Path:";
            // 
            // textBox_path
            // 
            this.textBox_path.Location = new System.Drawing.Point(91, 48);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(335, 27);
            this.textBox_path.TabIndex = 2;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(432, 47);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(100, 29);
            this.button_ok.TabIndex = 3;
            this.button_ok.Text = "ok";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // label_not_found
            // 
            this.label_not_found.AutoSize = true;
            this.label_not_found.Location = new System.Drawing.Point(538, 51);
            this.label_not_found.Name = "label_not_found";
            this.label_not_found.Size = new System.Drawing.Size(0, 20);
            this.label_not_found.TabIndex = 4;
            // 
            // button_root
            // 
            this.button_root.Location = new System.Drawing.Point(40, 86);
            this.button_root.Name = "button_root";
            this.button_root.Size = new System.Drawing.Size(94, 29);
            this.button_root.TabIndex = 5;
            this.button_root.Text = "root";
            this.button_root.UseVisualStyleBackColor = true;
            this.button_root.Click += new System.EventHandler(this.button_root_Click);
            // 
            // button_back
            // 
            this.button_back.Location = new System.Drawing.Point(140, 86);
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
            this.label_now_path.Location = new System.Drawing.Point(240, 90);
            this.label_now_path.Name = "label_now_path";
            this.label_now_path.Size = new System.Drawing.Size(0, 20);
            this.label_now_path.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.label_now_path);
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_root);
            this.Controls.Add(this.label_not_found);
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
        private Label label_not_found;
        private Button button_root;
        private Button button_back;
        private Label label_now_path;
    }
}