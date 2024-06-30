using System.Text.RegularExpressions;
using Microsoft.Win32;
using GetFolderSize.util;


namespace GetFolderSize
{
    /// <summary>
    /// 查询文件夹中文件和子文件夹的大小，默认以大小的降序排列
    /// </summary>
    /// 2024.6.14
    /// version 2.0.0
    public partial class MainForm : Form
    {
        #region 字段

        Localization localization;

        Folder root; //查询结果文件夹作为此对象的Children
        Folder? now; //当前显示的文件夹


        Thread? thread = null; //点击获取按钮或确定导入时创建的线程。此变量为未被废弃线程，若某子线程t与此变量不一致则子线程t被废弃
        Thread? thread_show_file_count = null; //获取过程中显示已获取的文件数量
        Thread? thread_batch_add_item = null; //当一页需要展示的项数过多时，使用此线程分批将项添加至listView中。此变量为未被废弃线程，若某子线程t与此变量不一致则子线程t被废弃

        SortType sort_type = SortType.SizeDesc; //当前排序方法。

        private volatile string update_form_flag = "";  // 每次更新界面时生成一个flag，为字段赋值并传递给向listView添加数据的函数。只有函数接收的flag与字段中的flag相同时才向listView添加数据。

        ToolTip? toolTip = null; // 鼠标在项上悬停时使用提示框显示文件/文件夹路径

        List<FolderOrFileUpdater> updaters = new List<FolderOrFileUpdater>(); // 用于实时更新查询结果

        bool forceClose = false; // 为true时，关闭窗口时无视关闭窗口改为最小化的设置，关闭窗口

        #endregion

        #region 初始化

        public MainForm()
        {
            InitializeComponent();
            localization = Localization.GetInstance(Config.Language);
            root = new Folder();
            root.FullName = "";
            InitializeLocalization();
            Microsoft.Win32.SystemEvents.SessionEnding += SessionEndingEvent; // 添加关机时导出数据事件
            StartFunctions();
        }

        /// <summary>
        /// 设置界面中显示的文本
        /// </summary>
        /// 2023.12.28
        /// version 2.0.0
        private void InitializeLocalization()
        {
            Text = localization.Main_Title;

            label_Path.Text = localization.Main_Label_Path;
            label_Search.Text = localization.Main_Label_Search;
            button_Ok.Text = localization.Main_Button_Ok;
            button_ViewFolder.Text = localization.Main_Button_ViewFolder;
            button_Cancel.Text = localization.Main_Button_Cancel;
            button_Search.Text = localization.Main_Button_Search;
            button_Root.Text = localization.Main_Button_Root;
            button_Back.Text = localization.Main_Button_Back;
            button_Export.Text = localization.Main_Button_Export;
            button_ExportCurrent.Text = localization.Main_Button_ExportCurrent;
            button_Import.Text = localization.Main_Button_Import;
            button_ShowInExplorer.Text = localization.Main_Button_ShowInExplorer;
            button_Refresh.Text = localization.Main_Button_Refresh;
            button_Config.Text = localization.Main_Button_Config;
            button_Exit.Text = localization.Exit;

            columnHeader_Name.Text = localization.Main_ListViewColumn_Name;
            columnHeader_Type.Text = localization.Main_ListViewColumn_Type;
            columnHeader_Size.Text = localization.Main_ListViewColumn_Size;
            columnHeader_FileCount.Text = localization.Main_ListViewColumn_FileCount;
            columnHeader_LastWriteTime.Text = localization.Main_ListViewColumn_LastWriteTime;

            notifyIcon.Text = localization.Main_Title;
            toolStripMenuItem_exit.Text = localization.Exit;
        }

        /// <summary>
        /// 关机时导出数据
        /// </summary>
        /// 2024.5.27
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SessionEndingEvent(object sender, SessionEndingEventArgs e)
        {
            ExportDataWhenExit();
        }

        /// <summary>
        /// 程序启动时需要进行的操作，如启动时导入、获取数据等
        /// </summary>
        /// 2024.5.18
        /// version 2.0.0
        private void StartFunctions()
        {
            // 启动文件夹时导入数据
            if (Config.ImportDataWhenStart && System.IO.File.Exists(Config.ImportDataWhenStartPath))
            {
                Import(Config.ImportDataWhenStartPath);
            }
            // 启动时获取文件夹
            else if (Config.GetDataWhenStart)
            {
                textBox_Path.Text = Config.GetDataWhenStartPath;
                button_ok_Click(button_Ok, new EventArgs());
            }
        }

        #endregion

        #region 组件

        /// <summary>
        /// 若MinimizeWhenClosing为true，点击叉图标不再退出程序，改为最小化程序。
        /// 若ExportWhenExit为true，退出程序时保存已获取的数据。
        /// </summary>
        /// 2024.5.27
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (Config.MinimizeWhenClosing && !forceClose)
                {
                    e.Cancel = true;
                    Hide();
                }
                else
                    ExportDataWhenExit();
            }
        }

        /// <summary>
        /// 点击ok按钮，获取指定文件夹
        /// </summary>
        /// 2024.5.28
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ok_Click(object sender, EventArgs e)
        {
            //获取比较慢，使用线程可以防止程序卡死，提升用户体验
            label_Alert.Text = localization.Main_Alert_Getting;

            SetThreadButtonEnabled(false); //获取期间不能再次获取、导入、搜索

            if (thread != null) //如果按下ok按钮时已有一个获取正在进行，则终止该获取，并创建一个新获取
                try { thread.Interrupt(); } catch (Exception) { }
            thread = new Thread(OkButtonThreadFunction);
            thread.IsBackground = true;//子线程会随着主线程的退出而退出
            thread.Start(textBox_Path.Text);

            //获取过程中实时显示已获取的文件数
            if (thread_show_file_count != null) try { thread_show_file_count.Interrupt(); } catch (Exception) { }
            if (Config.EnableShowFileCountWhenGetting)
            {
                thread_show_file_count = new Thread(ShowFileCountThreadFunction);
                thread_show_file_count.IsBackground = true;
                thread_show_file_count.Start();
            }
        }

        private void button_ViewFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox_Path.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// 取消正在进行的获取、导入、搜索
        /// </summary>
        /// 2024.5.27
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            if (thread != null)
            {
                try { thread.Interrupt(); } catch { }
                thread = null;
            }
            if (thread_batch_add_item != null)
            {
                try { thread_batch_add_item.Interrupt(); } catch { }
                thread_batch_add_item = null;
            }
            label_Alert.Text = "";
            if (thread_show_file_count != null)
            {
                try { thread_show_file_count.Interrupt(); } catch { }
                thread_show_file_count = null;
            }
            SetThreadButtonEnabled(true);
        }

        /// <summary>
        /// 获取当前文件夹下符合条件的文件或文件夹
        /// </summary>
        /// 2024.6.14
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Search_Click(object sender, EventArgs e)
        {
            // 若当前未显示文件，则什么都不做
            if (now == null || now.Children == null || now.Children.Count == 0)
                return;
            string str = textBox_Search.Text;

            //string searchRule = SEARCH_RULES[comboBox_search_rule.SelectedIndex];
            SearchRules searchRule = Config.SearchRule;
            bool searchFile = Config.SearchFiles;
            bool searchFolder = Config.SearchFolders;
            bool recursiveSearch = Config.RecursiveSearch;
            bool caseSensitive = Config.CaseSensitive;
            bool matchFullName = Config.MatchFullName;
            long? fileSizeLowerLimit = Config.FileSizeLowerLimit;
            long? fileSizeUpperLimit = Config.FileSizeUpperLimit;
            long? folderSizeLowerLimit = Config.FolderSizeLowerLimit;
            long? folderSizeUpperLimit = Config.FolderSizeUpperLimit;
            int? fileCountLowerLimit = Config.FileCountLowerLimit;
            int? fileCountUpperLimit = Config.FileCountUpperLimit;
            string[] localizedSearchRuleNames = localization.Config_SearchRules_LocalizedNames;
            char localizedColon = localization.LocalizedColon;
            if (string.IsNullOrEmpty(str))
            {
                label_Alert.Text = localization.Main_Alert_SearchTextEmpty;
                return;
            }

            if (searchRule == SearchRules.Regular) // 若正则字符串不正确，给出提示并终止搜索
            {
                try
                {
                    Regex _ = new Regex(str);
                }
                catch (RegexParseException ex)
                {
                    label_Alert.Text = localization.Main_Alert_RegularExpressionIncorrect;
                    return;
                }
            }
            else if (searchRule == SearchRules.Wildcard) // 若通配符字符串不正确，给出提示并终止搜索
            {
                if (!WildcardUtil.CheckValidity(str))
                {
                    label_Alert.Text = localization.Main_Alert_WildcardIncorrect;
                    return;
                }
            }

            SearchArgs args = new SearchArgs(new Json.JsonObject()
            {
                {"str", str},
                {"searchRule", (int)searchRule },
                {"searchFile", searchFile },
                {"searchFolder", searchFolder },
                {"recursiveSearch", recursiveSearch },
                {"caseSensitive",  caseSensitive},
                {"matchFullName", matchFullName },
                {"fileSizeLowerLimit", fileSizeLowerLimit },
                {"fileSizeUpperLimit", fileSizeUpperLimit},
                {"folderSizeLowerLimit", folderSizeLowerLimit},
                {"folderSizeUpperLimit", folderSizeUpperLimit},
                {"fileCountLowerLimit", fileCountLowerLimit },
                {"fileCountUpperLimit", fileCountUpperLimit },
                {"localizedSearchRuleNames", localizedSearchRuleNames },
                {"localizedColon", localizedColon },
            });

            label_Alert.Text = localization.Main_Alert_Searching;
            SetThreadButtonEnabled(false); //获取期间不能再次获取、导入、搜索
            if (thread != null) //如果确定导入时已有一个获取或导入正在进行，则终止之，并进行导入
                try { thread.Interrupt(); } catch (Exception) { }
            thread = new Thread(SearchButtonThreadFunction);
            thread.IsBackground = true;//子线程会随着主线程的退出而退出
            thread.Start(args);
        }

        private void button_Config_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new ConfigForm();
            configForm.ShowDialog();
            if (Config.Language != localization.Name)
            {
                localization = Localization.GetInstance(Config.Language, true);
                InitializeLocalization();
            }
        }

        /// <summary>
        /// 返回根目录
        /// </summary>
        /// 2022.6.9
        /// version 1.1.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Root_Click(object sender, EventArgs e)
        {
            if (root != null)
            {
                UpdateForm(root);
            }
        }

        /// <summary>
        /// 返回上级
        /// </summary>
        /// 2022.6.9
        /// version 1.1.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Back_Click(object sender, EventArgs e)
        {
            if (now != null && now.Parent != null)
            {
                UpdateForm(now.Parent);
            }
        }

        /// <summary>
        /// 将当前获取的内容导出至json文件
        /// </summary>
        /// 2024.4.13
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Export_Click(object sender, EventArgs e)
        {
            if (root.Children != null && root.Children.Count > 0)//只有存在查询内容时才可以导出
            {
                try
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "json files(*.json)|*.json|All files (*.*)|*.*";
                    sfd.DefaultExt = ".json";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string filename = sfd.FileName;
                        string json = root.Children[0].ToString();
                        System.IO.File.WriteAllText(filename, json);
                        label_Alert.Text = localization.Main_Alert_ExportSucceed;
                    }
                }
                catch (Exception)
                {
                    label_Alert.Text = localization.Main_Alert_ExportFailed;
                }
            }
        }

        /// <summary>
        /// 将当前页内容导出至json文件
        /// </summary>
        /// 2024.4.13
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ExportCurrent_Click(object sender, EventArgs e)
        {
            if (now != null && now.Parent != null)//只有存在当前页时才可以导出，root和搜索结果的父文件夹不可以导出
            {
                try
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "json files(*.json)|*.json|All files (*.*)|*.*";
                    sfd.DefaultExt = ".json";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string filename = sfd.FileName;
                        string json = now.ToString();
                        System.IO.File.WriteAllText(filename, json);
                        label_Alert.Text = localization.Main_Alert_ExportSucceed;
                    }
                }
                catch (Exception)
                {
                    label_Alert.Text = localization.Main_Alert_ExportFailed;
                }
            }
        }

        /// <summary>
        /// “导入”按钮
        /// </summary>
        /// 2024.5.18
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Import_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "json files(*.json)|*.json|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filename = ofd.FileName;
                    Import(filename);
                }
            }
            catch (Exception)
            {
                FolderNotFoundInvokeFunction(localization.Main_Alert_ImportFailed);
            }
        }

        /// <summary>
        /// 在资源管理器中打开当前文件夹。若选中了文件或文件夹，则在资源管理器中指向被选中的文件或文件夹
        /// </summary>
        /// 2024.4.13
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ShowInExplorer_Click(object sender, EventArgs e)
        {
            // 若当前未显示文件，则什么都不做
            if (now == null || now.Children == null || now.Children.Count == 0)
                return;

            string? path;  // 选中的文件或文件夹。未选中文件或文件夹时为null
            if (listView_Data.SelectedItems.Count > 0)
            {
                path = now.Children[listView_Data.SelectedItems[0].Index].FullName;
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
        /// 刷新当前界面
        /// </summary>
        /// 2023.12.12
        /// version 1.3.1
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Refresh_Click(object sender, EventArgs e)
        {
            if (now != null)
            {
                UpdateForm(now);
            }
        }

        /// <summary>
        /// 退出按钮
        /// </summary>
        /// 2024.5.22
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Exit_Click(object sender, EventArgs e)
        {
            forceClose = true;
            this.Close();
        }

        /// <summary>
        /// 当窗口拖动时保持列宽的百分比不变
        /// </summary>
        /// 2023.12.13
        /// version 1.4.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Data_SizeChanged(object sender, EventArgs e)
        {
            double[] width_persentage = { 400.0 / 900.0, 100.0 / 900.0, 100.0 / 900.0, 100.0 / 900.0, 200.0 / 900.0 };
            for (int i = 0; i < 5; i++)
            {
                listView_Data.Columns[i].Width = (int)(listView_Data.Width * width_persentage[i]);
            }
        }

        /// <summary>
        /// 点击列表中的文件夹则显示该文件夹内的内容
        /// </summary>
        /// 2024.4.13
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_data_Click(object sender, EventArgs e)
        {
            if (now == null)
                return;
            try
            {
                int index = listView_Data.SelectedItems[0].Index;
                if (now.Children[index] is Folder child_folder)
                {
                    UpdateForm(child_folder);
                }
            }
            catch (Exception ex)//如果有异常就什么都不做
            {

            }
        }

        /// <summary>
        /// 点击列标题时按对应列排序，再次点击则为反序。
        /// </summary>
        /// 2024.5.22
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Data_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (now == null || now.Children == null)
                return;

            int NAME = 0, TYPE = 1, SIZE = 2, FILECOUNT = 3, LASTWRITETIME = 4;

            int index = e.Column;//点击的列数
            if (index == NAME)//按名字排序。点第一下是升序，第二下是降序
            {
                if (sort_type == SortType.NameAsc)//如果已经是名字升序，再次点击则反转变为名字降序
                {
                    now.Children.Reverse();
                    sort_type = SortType.NameDesc;
                }
                else if (sort_type == SortType.NameDesc) //如果已经是名字降序，再次点击则反转变为名字升序
                {
                    now.Children.Reverse();
                    sort_type = SortType.NameAsc;
                }
                else//否则按名字升序排序
                {
                    now.Children.Sort((f1, f2) => { return CompareFunctions.ByNameAsc(f1, f2); });
                    sort_type = SortType.NameAsc;
                }
            }
            else if (index == TYPE)//按类型排序。点第一下是文件夹优先，第二下是文件优先
            {
                if (sort_type == SortType.FolderFirst)//如果已经是文件夹优先，再次点击则按文件优先排序
                {
                    now.Children.Sort((f1, f2) => { return CompareFunctions.ByFileFirst(f1, f2); });
                    sort_type = SortType.FileFirst;
                }
                else if (sort_type == SortType.FileFirst)//如果已经是文件优先，再次点击则按文件夹优先排序
                {
                    now.Children.Reverse();
                    sort_type = SortType.FolderFirst;
                }
                else//否则按文件夹优先排序
                {
                    now.Children.Sort((f1, f2) => { return CompareFunctions.ByFolderFirst(f1, f2); });
                    sort_type = SortType.FolderFirst;
                }
            }
            else if (index == SIZE)//按大小排序。进行过其他排序后点击此列第一下是降序，点第二下或未进行过其他排序时点击此列则是升序
            {
                if (sort_type == SortType.SizeDesc)//如果已经是大小降序，再次点击则按大小升序
                {
                    now.Children.Reverse();
                    sort_type = SortType.SizeAsc;
                }
                else if (sort_type == SortType.SizeAsc)//如果已经是大小升序，再次点击则按大小降序
                {
                    now.Children.Reverse();
                    sort_type = SortType.SizeDesc;
                }
                else//否则按大小降序排序
                {
                    now.Children.Sort();
                    sort_type = sort_type = SortType.SizeDesc;
                }
            }
            else if (index == FILECOUNT)//按文件数排序。点第一下是降序，第二下是升序
            {
                if (sort_type == SortType.FileCountDesc)//如果已经是文件数降序，再次点击则按文件数升序
                {
                    now.Children.Reverse();
                    sort_type = SortType.FileCountAsc;
                }
                else if (sort_type == SortType.FileCountAsc)//如果已经是文件数升序，再次点击则按文件数降序
                {
                    now.Children.Reverse();
                    sort_type = SortType.FileCountDesc;
                }
                else//否则按文件数降序排序
                {
                    now.Children.Sort((f1, f2) => { return CompareFunctions.ByFileCountDesc(f1, f2); });
                    sort_type = SortType.FileCountDesc;
                }
            }
            else if (index == LASTWRITETIME)// 按最后修改时间排序
            {
                if (sort_type == SortType.LastWriteTimeDesc) // 如果已经按最后修改时间降序，再次点击则按最后修改时间升序
                {
                    now.Children.Reverse();
                    sort_type = SortType.LastWriteTimeAsc;
                }
                else if (sort_type == SortType.LastWriteTimeAsc) // 如果已经按最后修改时间升序，再次点击则按最后修改时间降序
                {
                    now.Children.Reverse();
                    sort_type = SortType.LastWriteTimeDesc;
                }
                else//否则按最后修改时间降序
                {
                    now.Children.Sort((f1, f2) => { return CompareFunctions.ByLastWriteTimeDesc(f1, f2); });
                    sort_type = SortType.LastWriteTimeDesc;
                }
            }

            // 除默认排序SizeDesc外，其他排序后设置now为非默认排序。
            if (sort_type != SortType.SizeDesc)
                now.Sorted = false;
            UpdateForm(now, need_sort: false);//更新界面，更新时不使用默认排序。
        }

        /// <summary>
        /// 将鼠标放在文件或文件夹上时，显示完整路径
        /// </summary>
        /// 2023.12.21
        /// version 1.4.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Data_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            try
            {
                if (toolTip != null) // 将鼠标放在另一项时首先销毁之前创建的toolTip对象，以避免当本项无完整路径(搜索结果、获取多个文件夹结果)时显示上一个存在完整路径项的完整路径
                    toolTip.Dispose();
                string fullName = now.Children[e.Item.Index].FullName;
                if (string.IsNullOrEmpty(fullName))
                    return;
                toolTip = new ToolTip();
                //toolTip.ShowAlways = true;
                toolTip.SetToolTip(e.Item.ListView, fullName);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 双击托盘图标显示界面
        /// </summary>
        /// 2024.4.26
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    WindowState = FormWindowState.Normal;
                }
                Activate();
                Visible = true;
                ShowInTaskbar = true;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 小图标右键菜单退出程序
        /// </summary>
        /// 2024.5.13
        /// version 2.0.0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            forceClose = true;
            this.Close();
        }

        #endregion

        #region 设置组件是否可用

        /// <summary>
        /// 设置获取、导入、搜索会生成线程的按钮是否可以点击
        /// 生成线程后将按钮设置为不可点击，以防止重复生成线程；线程执行结束后将按钮设置为可以点击
        /// </summary>
        /// 2024.4.28
        /// version 2.0.0
        /// <param name="enabled">true: 设置按钮为可点击; false: 设置按钮为不可点击</param>
        private void SetThreadButtonEnabled(bool enabled)
        {
            button_Ok.Enabled = enabled;
            button_Import.Enabled = enabled;
            button_Search.Enabled = enabled;
            // 获取导入搜索可用时，取消按钮不可用；获取导入搜索不可用时，取消按钮可用
            button_Cancel.Enabled = !enabled;
        }

        #endregion

        #region 线程运行方法

        /// <summary>
        /// ok按钮创建的线程执行的函数
        /// </summary>
        /// 2024.5.26
        /// version 2.0.0
        /// <param name="path">路径文本框中的内容</param>
        private void OkButtonThreadFunction(object path)
        {
            try//若找到文件夹则显示在界面上
            {
                string str_path = path.ToString()?.Trim() ?? "";
                Folder f;
                if (str_path.Contains("|"))
                {
                    string[] paths = str_path.Split("|");
                    for (int i = 0; i < paths.Length; i++)
                        paths[i] = paths[i].Trim();
                    f = FolderOrFile.GetObjectsFromPaths(paths);
                }
                else
                {
                    f = new Folder(str_path);
                }

                // 无论是否成功获取，均停止实时更新显示已获取的文件数
                // 这一段写在finally里可能会导致获取完毕后仍实时更新一次已获取文件数。为了避免这种情况，应将这一段写在调用UpdateFormInvokeFunction之前，在正常流程和catch中分别写一遍
                if (thread_show_file_count != null)
                {
                    try { thread_show_file_count.Interrupt(); } catch { }
                    thread_show_file_count = null;
                }

                UpdateFormInvokeFunction(f, need_update_data: Config.EnableUpdateGottenData);
            }
            catch (Exception ex)
            {
                // 无论是否成功获取，均停止实时更新显示已获取的文件数
                if (thread_show_file_count != null)
                {
                    try { thread_show_file_count.Interrupt(); } catch { }
                    thread_show_file_count = null;
                }
                FolderNotFoundInvokeFunction(localization.Main_Alert_FolderNotFound);
            }
        }

        /// <summary>
        /// 获取时实时显示文件总数的线程执行的函数
        /// </summary>
        /// 2024.5.28
        /// version 2.0.0
        private void ShowFileCountThreadFunction()
        {

            File.ClearTotalFileCount(); // 首先清空文件计数
            try
            {
                while (thread_show_file_count != null && Thread.CurrentThread.ManagedThreadId == thread_show_file_count.ManagedThreadId)
                {
                    Thread.Sleep(Config.ShowFileCountInterval);
                    ShowFileCountInvokeFunction(File.TotalFileCount);
                }
            }
            catch { }
        }

        /// <summary>
        /// 分批项listView中添加项的线程执行的方法
        /// </summary>
        /// 2024.5.13
        /// version 2.0.0
        /// <param name="data"></param>
        private void AddItemsThreadFunction(object data)
        {
            string flag = RandomUtil.RandomString();
            this.update_form_flag = flag;
            Folder f = (Folder)data;
            int num_batch = f.Children.Count / Config.BatchSize;
            for (int i = 0; i < num_batch; i++)
            {
                AddItemsInvokeFunction(f, i * Config.BatchSize, (i + 1) * Config.BatchSize, flag);
                // 此处线程中断时可能会抛出异常导致程序崩溃，因此添加try-catch
                try
                {
                    Thread.Sleep(Config.BatchInterval);
                }
                catch (ThreadInterruptedException)
                {
                    return;
                }
            }
            AddItemsInvokeFunction(f, num_batch * Config.BatchSize, f.Children.Count, flag);
        }

        /// <summary>
        /// search按钮创建的线程执行的函数
        /// </summary>
        /// 2024.5.19
        /// version 2.0.0
        private void SearchButtonThreadFunction(object args)
        {
            Folder? folder_to_search;
            if (Config.SearchFromRoot)
            {
                if (root.Children.Count > 0 && root.Children.First() is Folder f)
                    folder_to_search = f;
                else
                    folder_to_search = null;
            }
            else
                folder_to_search = now;

            if (folder_to_search == null)
                return;
            try
            {
                Folder found = folder_to_search.Search((SearchArgs)args);
                found.Name = localization.FolderOrFile_SearchResult;
                UpdateFormInvokeFunction(found, false);
            }
            catch (Exception ex)
            {
                FolderNotFoundInvokeFunction(localization.Main_Alert_ConditionStringIncorrect);
            }
        }

        /// <summary>
        /// import按钮创建的线程执行的函数
        /// </summary>
        /// 2024.6.28
        /// version 2.0.0
        /// <param name="path">路径文本框中的内容</param>
        private void ImportButtonThreadFunction(object json)
        {
            try//若导入成功则显示在界面上
            {
                Json.JsonObject jobj = Json.JsonObject.Parse((string)json);
                jobj = Version.UpdateImportedData(jobj);
                Folder f = jobj.GetValue<Folder>();
                UpdateFormInvokeFunction(f, need_update_data: Config.EnableUpdateImportedData);
            }
            catch (Exception ex)
            {
                FolderNotFoundInvokeFunction(localization.Main_Alert_ImportFailed);
            }
        }

        #endregion

        #region 线程回调方法

        /// <summary>
        /// 获取或导入文件夹成功或搜索成功，invoke主线程更新界面
        /// </summary>
        /// 2024.5.26
        /// version 2.0.0
        /// <param name="f">获取到或导入的文件夹</param>
        /// <param name="change_root">是否更新根节点。对于获取和导入，更新根节点；对于搜索，不需要更新根节点</param>
        /// <param name="need_update_data">是否需要在文件或文件夹发生变化时实时更新数据</param>
        private void UpdateFormInvokeFunction(Folder f, bool change_root = true, bool need_update_data = false)
        {

            if (InvokeRequired == false)//主线程则直接操作窗体
            {
                SetThreadButtonEnabled(true); //查询结束后ok、导入、搜索按钮恢复可用
                // 对获取的文件夹进行监视，以在文件或文件夹发生变化时修改GetFolderSize程序中的对应数据
                if (need_update_data)
                {
                    foreach (FolderOrFileUpdater updater in updaters)
                    {
                        try
                        {
                            updater.EnableRaisingEvents = false;
                            updater.Dispose();
                        }
                        catch { }
                    }
                    updaters.Clear();

                    // 获取多个文件夹，为每个获取的文件夹创建监视
                    if (f.Name.Contains('|'))
                    {
                        foreach (Folder child in f.Children)
                        {
                            FolderOrFileUpdater new_updater = new FolderOrFileUpdater(child);
                            new_updater.EnableRaisingEvents = true;
                            updaters.Add(new_updater);
                        }
                    }
                    // 获取单个文件
                    else
                    {
                        FolderOrFileUpdater new_updater = new FolderOrFileUpdater(f);
                        new_updater.EnableRaisingEvents = true;
                        updaters.Add(new_updater);
                    }
                }

                if (change_root)
                {
                    root.Children.Clear();
                    root.Children.Add(f);
                    f.Parent = root;
                }
                //UpdateForm(root);
                label_Alert.Text = string.Empty;
                UpdateForm(f);
            }
            else//子线程则调用主线程
            {
                if (thread != null && System.Threading.Thread.CurrentThread.ManagedThreadId == thread.ManagedThreadId) //如果该进程未被废弃则调用主线程操作窗体
                    this.Invoke(UpdateFormInvokeFunction, f, change_root, need_update_data);
            }
        }

        /// <summary>
        /// 获取或导入文件夹失败，invoke主线程更新界面
        /// </summary>
        /// 2024.5.19
        /// version 2.0.0
        /// <param name="message">失败消息。查询失败为"folder not found"，导入失败为"import failed"</param>
        private void FolderNotFoundInvokeFunction(string message)
        {
            if (InvokeRequired == false)//主线程则直接操作窗体
            {
                SetThreadButtonEnabled(true); //查询结束后ok、导入、搜索按钮恢复可用
                label_Alert.Text = message;
            }
            else//子线程则调用主线程
            {
                if (thread != null && System.Threading.Thread.CurrentThread.ManagedThreadId == thread.ManagedThreadId) //如果该进程未被废弃则调用主线程操作窗体
                    this.Invoke(FolderNotFoundInvokeFunction, message);
            }
        }

        /// <summary>
        /// 获取时实时显示文件总数
        /// </summary>
        /// 2024.5.27
        /// version 2.0.0
        /// <param name="count"></param>
        private void ShowFileCountInvokeFunction(int count)
        {
            if (InvokeRequired == false)//主线程则直接操作窗体
                label_Alert.Text = string.Format(localization.Main_Alert_Format_GettingWithFileCount, count);
            else
            {
                if (thread_show_file_count != null && System.Threading.Thread.CurrentThread.ManagedThreadId == thread_show_file_count.ManagedThreadId) //如果该进程未被废弃则调用主线程操作窗体
                    this.Invoke(ShowFileCountInvokeFunction, count);
            }
        }

        #endregion

        #region 功能方法

        /// <summary>
        /// 更新界面中展示的内容
        /// </summary>
        /// 2024.5.12
        /// version 2.0.0
        /// <param name="f">界面中展示文件夹f的内容</param>
        /// <param name="need_sort">为true时对元素排序</param>
        private void UpdateForm(Folder f, bool need_sort = true)
        {
            if (thread_batch_add_item != null) //切换界面时终止正在添加项的进程
            {
                try { thread_batch_add_item.Interrupt(); } catch (Exception) { }
                thread_batch_add_item = null;
            }

            this.update_form_flag = ""; // 清空flag，以停止当前正在进行的批加载


            listView_Data.Items.Clear();
            if (f == null)
            {
                now = null;
                return;
            }

            now = f;

            // 除点击列标题排序后更新内容外，其他情况更新展示数据前先对f.Children进行默认排序。
            // 如果已经有序则不需要再次排序
            if (need_sort && !f.Sorted)
            {
                sort_type = SortType.SizeDesc;
                f.Children.Sort();
                f.Sorted = true;
            }

            //当项数较少时，或未开启批加载时，一次性向listView中添加所有项
            if (!Config.EnableBatchLoad || f.Children.Count < Config.BatchLoadThreshold)
            {
                string flag = RandomUtil.RandomString();
                this.update_form_flag = flag;
                UpdateFormAddItems(f, 0, f.Children.Count, flag);
            }
            //当项数较多时，一次性向listView中添加所有项过于耗时。为了防止程序在加载期间卡死，可以启用批加载，使用线程分批添加项
            else
            {
                thread_batch_add_item = new Thread(AddItemsThreadFunction);
                thread_batch_add_item.IsBackground = true;//子线程会随着主线程的退出而退出
                thread_batch_add_item.Start(f);
            }
            label_NowPath.Text = f.FullName;
        }

        /// <summary>
        /// 向listView中添加项
        /// 对于start <= i < end，将Children[i]加入listView
        /// </summary>
        /// 2023.12.15
        /// version 1.4.0
        private void UpdateFormAddItems(Folder f, int start, int end, string flag)
        {
            int lenth = f.Children.Count;
            for (int i = start; i < end && i < lenth; i++)
            {
                FolderOrFile child = f.Children[i];
                ListViewItem item = new ListViewItem();
                item.Text = child.Name;
                if (child is Folder child_folder)
                {
                    item.SubItems.Add(localization.FolderOrFile_Folder);
                    item.SubItems.Add(child.SizeFormat());
                    item.SubItems.Add(child_folder.FileCount.ToString());
                }
                else
                {
                    item.SubItems.Add(localization.FolderOrFile_File);
                    item.SubItems.Add(child.SizeFormat());
                    item.SubItems.Add("");
                }

                if (child.LastWriteTime != null)
                    item.SubItems.Add(child.LastWriteTime.Value.ToString(FolderOrFile.DATETIME_FORMAT));
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
                listView_Data.Items.Add(item);
            }
        }

        /// <summary>
        /// 使用线程分批向listView中添加项时，invoke主线程更新
        /// </summary>
        /// 2024.6.3
        /// version 2.0.0
        private void AddItemsInvokeFunction(Folder f, int start, int end, string flag)
        {
            if (InvokeRequired == false)//主线程则直接操作窗体
            {
                UpdateFormAddItems(f, start, end, flag);
            }
            else//子线程则调用主线程
            {
                // 此处可能抛出线程中断异常，需要添加try-catch块
                try
                {
                    if (thread_batch_add_item != null && System.Threading.Thread.CurrentThread.ManagedThreadId == thread_batch_add_item.ManagedThreadId) //如果该进程未被废弃则调用主线程操作窗体
                        this.Invoke(AddItemsInvokeFunction, f, start, end, flag);
                }
                catch { }
            }
        }

        /// <summary>
        /// 从json文件中导入查询内容
        /// </summary>
        /// 2024.5.18
        /// version 2.0.0
        /// <param name="path">json文件路径</param>
        private void Import(string path)
        {
            //将字符串转化为对象一步比较慢，使用线程可以防止程序卡死，提升用户体验
            label_Alert.Text = localization.Main_Alert_Importing;
            string json = System.IO.File.ReadAllText(path);
            SetThreadButtonEnabled(false); //导入期间不能按ok、导入、搜索按钮

            if (thread != null) //如果确定导入时已有一个获取或导入正在进行，则终止之，并进行导入
                try { thread.Interrupt(); } catch (Exception) { }
            thread = new Thread(ImportButtonThreadFunction);
            thread.IsBackground = true;//子线程会随着主线程的退出而退出
            thread.Start(json);
        }
        
        /// <summary>
        /// 退出程序或关机时导出数据
        /// </summary>
        /// 2024.5.27
        /// version 2.0.0
        private void ExportDataWhenExit()
        {
            if (Config.ExportWhenExit && root.Children.Count > 0)
            {
                string filename = Config.ExportWhenExitPath;
                try
                {
                    string json = root.Children[0].ToString();
                    System.IO.File.WriteAllText(filename, json);
                }
                catch { }
            }
        }

        #endregion
    }
}
