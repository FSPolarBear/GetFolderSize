using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GetFolderSize
{
    /// <summary>
    /// 查询文件夹中文件和子文件夹的大小，默认以大小的降序排列
    /// <para>2023.12.21</para>
    /// <para>version 1.4.0</para>
    /// </summary>
    public partial class MainForm : Form
    {
        Localization localization;

        FolderOrFile root; //查询结果文件夹作为此对象的Children
        FolderOrFile? now; //当前显示的文件夹

        
        Thread? thread = null; //点击ok按钮或确定导入时创建的线程。此变量为未被废弃线程，若某子线程t与此变量不一致则子线程t被废弃
        Thread? thread_batch_add_item = null; //当一页需要展示的项数过多时，使用此线程分批将项添加至listView中。此变量为未被废弃线程，若某子线程t与此变量不一致则子线程t被废弃

        int sort_type = 4;//当前排序方法。0 名字升序；1 名字降序；2 文件夹优先； 3 文件优先；4 大小降序；5 大小升序；6 文件数降序；7 文件数升序；8 最后修改时间降序；9 最后修改时间升序

        private string update_form_flag = "";  // 每次更新界面时生成一个flag，为字段赋值并传递给向listView添加数据的函数。只有函数接收的flag与字段中的flag相同时才向listView添加数据。

        ToolTip? toolTip = null; // 鼠标在项上悬停时使用提示框显示文件/文件夹路径


        public MainForm()
        {
            InitializeComponent();
            Config.LoadFromFile(); // 程序开始时读取配置文件
            localization = Localization.GetInstance(Config.Language);
            root = new FolderOrFile();
            root.IsFolder = true;
            root.Children = Array.Empty<FolderOrFile>();
            root.FullName = "";
            InitializeLocalization();

        }

        /// <summary>
        /// 设置界面中显示的文本
        /// <para>2023.12.14</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        private void InitializeLocalization()
        {
            label_path.Text = localization.Main_Label_Path;
            label_search.Text = localization.Main_Label_Search;
            button_ok.Text = localization.Main_Button_Ok;
            button_search.Text = localization.Main_Button_Search;
            button_root.Text = localization.Main_Button_Root;
            button_back.Text = localization.Main_Button_Back;
            button_export.Text = localization.Main_Button_Export;
            button_import.Text = localization.Main_Button_Import;
            button_show_in_explorer.Text = localization.Main_Button_ShowInExplorer;
            button_refresh.Text = localization.Main_Button_Refresh;
            button_config.Text = localization.Main_Button_Config;

            columnHeader_name.Text = localization.Main_ListViewColumn_Name;
            columnHeader_type.Text = localization.Main_ListViewColumn_Type;
            columnHeader_size.Text = localization.Main_ListViewColumn_Size;
            columnHeader_file_count.Text = localization.Main_ListViewColumn_FileCount;
            columnHeader_last_write_time.Text = localization.Main_ListViewColumn_LastWriteTime;
        }


        /// <summary>
        /// 当窗口拖动时保持列宽的百分比不变
        /// <para>2023.12.13</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_data_SizeChanged(object sender, EventArgs e)
        {
            double[] width_persentage = { 400.0 / 900.0, 100.0 / 900.0, 100.0 / 900.0, 100.0 / 900.0 , 200.0 / 900.0};
            for(int i = 0; i < 5; i++)
            {
                listView_data.Columns[i].Width = (int)(listView_data.Width * width_persentage[i]);
            }
        }

        /// <summary>
        /// 设置查找、导入、搜索会生成线程的按钮是否可以点击
        /// 生成线程后将按钮设置为不可点击，以防止重复生成线程；线程执行结束后将按钮设置为可以点击
        /// <para>2023.12.11</para>
        /// <para>version 1.3.1</para>
        /// </summary>
        /// <param name="enabled">true: 设置按钮为可点击; false: 设置按钮为不可点击</param>
        private void SetThreadButtonEnabled(bool enabled)
        {
            button_ok.Enabled = enabled;
            button_import.Enabled = enabled;
            button_search.Enabled = enabled;
        }

        /// <summary>
        /// 点击ok按钮，查找指定文件夹
        /// <para>2023.12.15</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ok_Click(object sender, EventArgs e)
        {
            //查找比较慢，使用线程可以防止程序卡死，提升用户体验
            label_alert.Text = localization.Main_Alert_Getting;

            SetThreadButtonEnabled(false); //查找期间不能再次查找、导入、搜索

            if (thread != null) //如果按下ok按钮时已有一个查找正在进行，则终止该查找，并创建一个新查找
                try { thread.Abort(); } catch (Exception) {  }    
            thread = new Thread(OkButtonThreadFunction);
            thread.IsBackground = true;//子线程会随着主线程的退出而退出
            thread.Start(textBox_path.Text);
            
        }


        /// <summary>
        /// 查找或导入文件夹成功或搜索成功，invoke主线程更新界面
        /// <para>2023.12.11</para>
        /// <para>version 1.3.1</para>
        /// </summary>
        /// <param name="f">查找到或导入的文件夹</param>
        /// <param name="update_root">是否更新根节点。对于查找和导入，更新根节点；对于搜索，不需要更新根节点</param>
        private void UpdateFormInvokeFunction(FolderOrFile f, bool update_root=true)
        {
            if(InvokeRequired == false)//主线程则直接操作窗体
            {
                SetThreadButtonEnabled(true); //查询结束后ok、导入、搜索按钮恢复可用
                if (update_root)
                {
                    root.Children = new FolderOrFile[] { f };
                    f.Parent = root;
                }
                //UpdateForm(root);
                label_alert.Text = string.Empty;
                UpdateForm(f);
            }
            else//子线程则调用主线程
            {
                if (thread != null && System.Threading.Thread.CurrentThread.ManagedThreadId == thread.ManagedThreadId) //如果该进程未被废弃则调用主线程操作窗体
                    this.Invoke(UpdateFormInvokeFunction, f, update_root);
            }
        }

        /// <summary>
        /// 查找或导入文件夹失败，invoke主线程更新界面
        /// <para>2023.12.11</para>
        /// <para>version 1.3.1</para>
        /// </summary>
        /// <param name="message">失败消息。查询失败为"folder not found"，导入失败为"import failed"</param>
        private void FolderNotFoundInvokeFunction(string message)
        {
            if(InvokeRequired == false)//主线程则直接操作窗体
            {
                SetThreadButtonEnabled(true); //查询结束后ok、导入、搜索按钮恢复可用
                label_alert.Text = message;
                root.Children = Array.Empty<FolderOrFile>();
                listView_data.Items.Clear();
                now = null;
                label_now_path.Text = string.Empty;
            }
            else//子线程则调用主线程
            {
                if (thread != null && System.Threading.Thread.CurrentThread.ManagedThreadId == thread.ManagedThreadId) //如果该进程未被废弃则调用主线程操作窗体
                    this.Invoke(FolderNotFoundInvokeFunction, message);      
            }
        }



        /// <summary>
        /// ok按钮创建的线程执行的函数
        /// <para>2023.12.20</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="path">路径文本框中的内容</param>
        private void OkButtonThreadFunction(object path)
        {
            try//若找到文件夹则显示在界面上
            {
                string str_path = path.ToString() ?? "";
                FolderOrFile f;
                if (str_path.Contains("|"))
                {
                    f = FolderOrFile.GetObjectFromPaths(str_path.Split("|"));
                }
                else
                {
                    f = FolderOrFile.GetObjectFromPath(str_path);
                }
                
                UpdateFormInvokeFunction(f);
            }
            catch (Exception ex)
            {
                FolderNotFoundInvokeFunction(localization.Main_Alert_FolderNotFound);
            }
            
        }


        /// <summary>
        /// 更新界面中展示的内容
        /// <para>2023.12.13</para>
        /// <para>version 1.3.1</para>
        /// </summary>
        /// <param name="f">界面中展示文件夹f的内容</param>
        public void UpdateForm(FolderOrFile f)
        {
            if (thread_batch_add_item != null) //切换界面时终止正在添加项的进程
            {
                try { thread_batch_add_item.Abort(); } catch (Exception) { }
                thread_batch_add_item = null;
            }
            this.update_form_flag = ""; // 清空flag，以停止当前正在进行的批加载
                

            listView_data.Items.Clear();
            if (f == null || f.Children == null)
            {
                now = null;
                return;
            }

            now = f;
            //当项数较少时，或未开启批加载时，一次性向listView中添加所有项
            if (!Config.EnableBatchLoad || f.Children.Length < Config.BatchLoadThreshold)
            {
                string flag = RandomUtil.RandomString();
                this.update_form_flag = flag;
                UpdateFormAddItems(f, 0, f.Children.Length, flag);
            }
            //当项数较多时，一次性向listView中添加所有项过于耗时。为了防止程序在加载期间卡死，可以启用批加载，使用线程分批添加项
            else
            {
                thread_batch_add_item = new Thread(AddItemsThreadFunction);
                thread_batch_add_item.IsBackground = true;//子线程会随着主线程的退出而退出
                thread_batch_add_item.Start(f);
            }
            label_now_path.Text = f.FullName;
        }

        /// <summary>
        /// 向listView中添加项
        /// 对于start <= i < end，将Children[i]加入listView
        /// <para>2023.12.15</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="Children"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void UpdateFormAddItems(FolderOrFile f, int start, int end, string flag)
        {
            FolderOrFile[] Children = f.Children;
            int lenth = Children.Length;
            for(int i = start; i < end && i < lenth; i++)
            {
                FolderOrFile forf = Children[i];
                ListViewItem item = new ListViewItem();
                item.Text = forf.Name;
                if (forf.IsFolder)
                    item.SubItems.Add(localization.FolderOrFile_Folder);
                else
                    item.SubItems.Add(localization.FolderOrFile_File);

                item.SubItems.Add(forf.SizeFormat());
                if (forf.IsFolder)
                    item.SubItems.Add(forf.FileCount.ToString());
                else
                    item.SubItems.Add("");
                if (forf.LastWriteTime != null)
                    item.SubItems.Add(forf.LastWriteTime.Value.ToString(FolderOrFile.DATETIME_FORMAT));
                else
                    item.SubItems.Add("");

                // 在更新页面时会设置this.update_form_flag = flag后调用此函数，因此通常此条件为true
                // 在批量加载时，若恰好在子线程执行此循环时切换页面并改变this.update_form_flag，此时若继续加载则会将属于f的内容加载到属于新now的界面上
                // 因此判断this.update_form_flag != flag时跳出循环
                //if (!object.ReferenceEquals(f, now))
                if (flag != this.update_form_flag)
                {
                    break;
                }
                listView_data.Items.Add(item);
            }
        }

        /// <summary>
        /// 分批项listView中添加项的线程执行的方法
        /// <para>2023.12.12</para>
        /// <para>version 1.3.1</para>
        /// </summary>
        /// <param name="data"></param>
        public void AddItemsThreadFunction(object data)
        {
            string flag = RandomUtil.RandomString();
            this.update_form_flag = flag;
            FolderOrFile f = (FolderOrFile)data;
            FolderOrFile[] Children = f.Children;
            int num_batch = Children.Length / Config.BatchSize;
            for (int i = 0; i < num_batch; i++)
            {
                AddItemsInvokeFunction(f, i * Config.BatchSize, (i + 1) * Config.BatchSize, flag);
                Thread.Sleep(Config.BatchInterval);
            }
            AddItemsInvokeFunction(f, num_batch * Config.BatchSize, Children.Length, flag);
        }

        /// <summary>
        /// 使用线程分批向listView中添加项时，invoke主线程更新
        /// <para>2023.12.12</para>
        /// <para>version 1.3.1</para>
        /// </summary>
        /// <param name="message">失败消息。查询失败为"folder not found"，导入失败为"import failed"</param>
        private void AddItemsInvokeFunction(FolderOrFile f, int start, int end, string flag)
        {
            if (InvokeRequired == false)//主线程则直接操作窗体
            {
                UpdateFormAddItems(f, start, end, flag);
            }
            else//子线程则调用主线程
            {
                if (thread_batch_add_item != null && System.Threading.Thread.CurrentThread.ManagedThreadId == thread_batch_add_item.ManagedThreadId) //如果该进程未被废弃则调用主线程操作窗体
                    this.Invoke(AddItemsInvokeFunction, f, start, end, flag);
            }
        }

        /// <summary>
        /// 点击列表中的文件夹则显示该文件夹内的内容
        /// <para>2022.6.7</para>
        /// <para>version 1.0.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_data_Click(object sender, EventArgs e)
        {
            
            try
            {
                int index = listView_data.SelectedItems[0].Index;
                if (now.Children[index].IsFolder)
                {
                    FolderOrFile child = now.Children[index];
                    RestoreSortType();
                    UpdateForm(child);
                }
                    
            }
            catch(Exception ex)//如果有异常就什么都不做
            {

            }
        }

        /// <summary>
        /// 返回上级
        /// <para>2022.6.9</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_back_Click(object sender, EventArgs e)
        {
            if (now != null && now.Parent != null)
            {
                RestoreSortType();
                UpdateForm(now.Parent);
            }
               
        }

        /// <summary>
        /// 返回根目录
        /// <para>2022.6.9</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_root_Click(object sender, EventArgs e)
        {
            if (root != null)
            {
                RestoreSortType();
                UpdateForm(root);
            }
                
        }

        /// <summary>
        /// 点击列标题时按对应列排序，再次点击则为反序。
        /// <para>2023.12.18</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_data_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (now == null || now.Children == null)
                return;

            int index = e.Column;//点击的列数
            if(index == 0)//按名字排序。点第一下是升序，第二下是降序
            {
                if(sort_type == 0)//如果已经是名字升序，再次点击则反转变为名字降序
                {
                    Array.Reverse(now.Children);
                    sort_type = 1;
                }
                else if(sort_type == 1) //如果已经是名字降序，再次点击则反转变为名字升序
                {
                    Array.Reverse(now.Children);
                    sort_type = 0;
                }
                else//否则按名字升序排序
                {
                    Array.Sort(now.Children, (f1, f2) => { return CompareFunctions.ByNameAsc(f1, f2); });
                    sort_type=0;
                }
            }
            else if(index == 1)//按类型排序。点第一下是文件夹优先，第二下是文件优先
            {
                if(sort_type == 2)//如果已经是文件夹优先，再次点击则按文件优先排序
                {
                    Array.Reverse(now.Children);
                    sort_type = 3;
                }
                else if (sort_type == 3)//如果已经是文件优先，再次点击则按文件夹优先排序
                {
                    Array.Reverse(now.Children);
                    sort_type = 2;
                }
                else//否则按文件夹优先排序
                {
                    Array.Sort(now.Children, (f1, f2) => { return CompareFunctions.ByIsFolderTrueFirst(f1, f2); });
                    sort_type = 2;
                }
            }
            else if(index == 2)//按大小排序。进行过其他排序后点击此列第一下是降序，点第二下或未进行过其他排序时点击此列则是升序
            {
                if (sort_type == 4)//如果已经是大小降序，再次点击则按大小升序
                {
                    Array.Reverse(now.Children);
                    sort_type = 5;
                }
                else if(sort_type == 5)//如果已经是大小升序，再次点击则按大小降序
                {
                    Array.Reverse(now.Children);
                    sort_type = 4;
                }
                else//否则按大小降序排序
                {
                    Array.Sort(now.Children);
                    sort_type = 4;
                }
            }
            else if (index == 3)//按文件数排序。点第一下是降序，第二下是升序
            {
                if (sort_type == 6)//如果已经是文件数降序，再次点击则按文件数升序
                {
                    Array.Reverse(now.Children);
                    sort_type = 7;
                }
                else if (sort_type == 7)//如果已经是文件数升序，再次点击则按文件数降序
                {
                    Array.Reverse(now.Children);
                    sort_type = 6;
                }
                else//否则按文件数降序排序
                {
                    Array.Sort(now.Children, (f1, f2) => { return CompareFunctions.ByFileCountDesc(f1, f2); });
                    sort_type = 6;
                }
            }
            else if(index == 4)// 按最后修改时间排序
            {
                if (sort_type == 8) // 如果已经按最后修改时间降序，再次点击则按最后修改时间升序
                {
                    Array.Reverse(now.Children);
                    sort_type = 9;
                }
                else if (sort_type == 9) // 如果已经按最后修改时间升序，再次点击则按最后修改时间降序
                {
                    Array.Reverse(now.Children); 
                    sort_type = 8;
                }
                else//否则按最后修改时间降序
                {
                    Array.Sort(now.Children, (f1, f2) => { return CompareFunctions.ByLastWriteTimeDesc(f1, f2); });
                    sort_type = 8;
                }
            }

            UpdateForm(now);//更新界面
        }

        /// <summary>
        /// 若修改过排序方法，在切换显示对象前将now.Children的排序方式恢复为默认的大小降序。
        /// <para>2022.12.18</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        private void RestoreSortType()
        {
            if(now != null && sort_type != 4)
            {
                sort_type = 4;
                Array.Sort(now.Children);
            }
        }

        /// <summary>
        /// 将当前查询的内容导出至json文件
        /// <para>2023.12.15</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_export_Click(object sender, EventArgs e)
        {
            if(root.Children!= null && root.Children.Length > 0)//只有在有查询内容时才可以导出
            {
                try
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "json files(*.json)|*.json|All files (*.*)|*.*";
                    sfd.DefaultExt = ".json";


                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string filename = sfd.FileName;

                        //如果自定义过排序，将存储的内容恢复为默认的排序，但不影响当前显示
                        int now_sort_type = sort_type;
                        RestoreSortType();
                        sort_type = now_sort_type;

                        string json = root.Children[0].ToString();
                        File.WriteAllText(filename, json);
                    }
                    label_alert.Text = localization.Main_Alert_ExportSucceed;

                }
                catch(Exception)
                {
                    label_alert.Text = localization.Main_Alert_ExportFailed;
                }
                
            }
        }

        /// <summary>
        /// 从json文件中导入查询内容
        /// <para>2023.12.15</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_import_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "json files(*.json)|*.json|All files (*.*)|*.*";
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    string filename = ofd.FileName;

                    //将字符串转化为对象一步比较慢，使用线程可以防止程序卡死，提升用户体验
                    label_alert.Text = localization.Main_Alert_Importing;
                    string json = File.ReadAllText(filename);
                    SetThreadButtonEnabled(false); //导入期间不能按ok、导入、搜索按钮

                    if (thread != null) //如果确定导入时已有一个查找或导入正在进行，则终止之，并进行导入
                        try { thread.Abort(); } catch (Exception) { }
                    thread = new Thread(ImportButtonThreadFunction);
                    thread.IsBackground = true;//子线程会随着主线程的退出而退出
                    thread.Start(json);
                }
            }
            catch(Exception)
            {
                FolderNotFoundInvokeFunction(localization.Main_Alert_ImportFailed);
            }
        }

        /// <summary>
        /// import按钮创建的线程执行的函数
        /// <para>2023.12.15</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="path">路径文本框中的内容</param>
        private void ImportButtonThreadFunction(object json)
        {
            try//若导入成功则显示在界面上
            {
                FolderOrFile? f = FolderOrFile.FromJson(json.ToString());
                if (f == null)
                    throw new Exception(localization.Main_Alert_ImportFailed);
                UpdateFormInvokeFunction(f);
            }
            catch (Exception ex)
            {
                FolderNotFoundInvokeFunction(ex.Message);
            }

        }

        /// <summary>
        /// 在资源管理器中打开当前文件夹。若选中了文件或文件夹，则在资源管理器中指向被选中的文件或文件夹
        /// <para>2023.12.7</para>
        /// <para>version 1.3.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_show_in_explorer_Click(object sender, EventArgs e)
        {
            // 若当前未显示文件，则什么都不做
            if (now == null || now.Children == null || now.Children.Length == 0) 
                return;

            string? path;  // 选中的文件或文件夹。未选中文件或文件夹时为null
            if (listView_data.SelectedItems.Count > 0)
            {
                path = now.Children[listView_data.SelectedItems[0].Index].FullName;
            }
            else
            {
                path = null;
            }

            if (path != null)  // 打开资源管理器指向选中的文件或文件夹
            {
                System.Diagnostics.Process.Start("explorer", "/select,\"" + path + "\"");
            }
            else if (Directory.Exists(now.FullName))  // 在资源管理器中打开当前文件夹
            {
                System.Diagnostics.Process.Start("explorer", now.FullName);
            }
            
        }

        /// <summary>
        /// 查找当前文件夹下符合条件的文件或文件夹
        /// <para>2023.12.20</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_search_Click(object sender, EventArgs e)
        {
            // 若当前未显示文件，则什么都不做
            if (now == null || now.Children == null || now.Children.Length == 0)
                return;
            string str = textBox_search.Text;
            //string searchRule = SEARCH_RULES[comboBox_search_rule.SelectedIndex];
            SearchRules searchRule = Config.SearchRule;
            bool searchFile = Config.SearchFiles;
            bool searchFolder = Config.SearchFolders;
            bool recursiveSearch = Config.RecursiveSearch;
            bool caseSensitive = Config.CaseSensitive;
            long? fileSizeLowerLimit = Config.FileSizeLowerLimit;
            long? fileSizeUpperLimit = Config.FileSizeUpperLimit;
            long? folderSizeLowerLimit = Config.FolderSizeLowerLimit;
            long? folderSizeUpperLimit = Config.FolderSizeUpperLimit;
            int? fileCountLowerLimit = Config.FileCountLowerLimit;
            int? fileCountUpperLimit = Config.FileCountUpperLimit;
            if (string.IsNullOrEmpty(str))
            {
                label_alert.Text = localization.Main_Alert_SearchTextEmpty;
                return;
            }

            string json = new Newtonsoft.Json.Linq.JObject()
            {
                {"str", str},
                {"searchRule", (int)searchRule },
                {"searchFile", searchFile },
                {"searchFolder", searchFolder },
                {"recursiveSearch", recursiveSearch },
                {"caseSensitive",  caseSensitive},
                {"fileSizeLowerLimit", fileSizeLowerLimit },
                {"fileSizeUpperLimit", fileSizeUpperLimit},
                {"folderSizeLowerLimit", folderSizeLowerLimit},
                {"folderSizeUpperLimit", folderSizeUpperLimit},
                {"fileCountLowerLimit", fileCountLowerLimit },
                {"fileCountUpperLimit", fileCountUpperLimit },
            }.ToString();

            RestoreSortType(); // 切换显示对象前重置排序
            label_alert.Text = localization.Main_Alert_Searching;
            SetThreadButtonEnabled(false); //查找期间不能再次查找、导入、搜索
            if (thread != null) //如果确定导入时已有一个查找或导入正在进行，则终止之，并进行导入
                try { thread.Abort(); } catch (Exception) { }
            thread = new Thread(SearchButtonThreadFunction);
            thread.IsBackground = true;//子线程会随着主线程的退出而退出
            thread.Start(json);

        }

        /// <summary>
        /// search按钮创建的线程执行的函数
        /// <para>2023.12.15</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        private void SearchButtonThreadFunction(object json)
        {
            FolderOrFile found = now.Search(json.ToString());
            found.Name = localization.FolderOrFile_SearchResult;
            UpdateFormInvokeFunction(found, false);
        }



        /// <summary>
        /// 将鼠标放在文件或文件夹上时，显示完整路径
        /// <para>2023.12.21</para>
        /// <para>version 1.4.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_data_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            try
            {
                if (toolTip != null) // 将鼠标放在另一项时首先销毁之前创建的toolTip对象，以避免当本项无完整路径(搜索结果、获取多个文件夹结果)时显示上一个存在完整路径项的完整路径
                    toolTip.Dispose();
                string fullName = now.Children[e.Item.Index].FullName;
                Console.WriteLine("a:" + fullName);
                if (string.IsNullOrEmpty(fullName))
                    return;
                toolTip = new ToolTip();
                //toolTip.ShowAlways = true;
                toolTip.SetToolTip(e.Item.ListView, fullName);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 刷新当前界面
        /// <para>2023.12.12</para>
        /// <para>version 1.3.1</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_refresh_Click(object sender, EventArgs e)
        {
            if (now != null)
            {
                UpdateForm(now);
            }
        }

        private void button_config_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new ConfigForm();
            configForm.ShowDialog();
            if(Config.Language != localization.Name)
            {
                localization = Localization.GetInstance(Config.Language, true);
                InitializeLocalization();
            }
        }
    }
}
