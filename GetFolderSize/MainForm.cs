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
    /// <para>2022.6.10</para>
    /// <para>version 1.2.0</para>
    /// </summary>
    public partial class MainForm : Form
    {
        FolderOrFile root; //查询结果文件夹作为此对象的Children
        FolderOrFile? now; //当前显示的文件夹

        
        Thread? thread = null; //点击ok按钮或确定导入时创建的线程。此变量为未被废弃线程，若某子线程t与此变量不一致则子线程t被废弃

        int sort_type = 4;//当前排序方法。0 名字升序；1 名字降序；2 文件夹优先； 3 文件优先；4 大小降序；5 大小升序；6 文件数降序；7 文件数升序

        public MainForm()
        {
            InitializeComponent();
            root = new FolderOrFile();
            root.IsFolder = true;
            root.Children = Array.Empty<FolderOrFile>();
            root.FullName = "";
            
        }



        /// <summary>
        /// 当窗口拖动时保持列宽的百分比不变
        /// <para>2022.6.7</para>
        /// <para>version 1.0.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_data_SizeChanged(object sender, EventArgs e)
        {
            double[] width_persentage = { 400.0 / 700.0, 100.0 / 700.0, 100.0 / 700.0, 100.0 / 700.0 };
            for(int i = 0; i < 4; i++)
            {
                listView_data.Columns[i].Width = (int)(listView_data.Width * width_persentage[i]);
            }
        }

        /// <summary>
        /// 点击ok按钮，查找指定文件夹
        /// <para>2022.6.10</para>
        /// <para>version 1.2.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ok_Click(object sender, EventArgs e)
        {
            //查找比较慢，使用线程可以防止程序卡死，提升用户体验
            label_status.Text = "loading...";

            button_ok.Enabled = false; //查找期间不能再次按ok按钮
            button_import.Enabled = false;//查找期间不能导入

            if (thread != null) //如果按下ok按钮时已有一个查找正在进行，则终止该查找，并创建一个新查找
                try { thread.Abort(); } catch (Exception) {  }    
            thread = new Thread(OkButtonThreadFunction);
            thread.IsBackground = true;//子线程会随着主线程的退出而退出
            thread.Start(textBox_path.Text);
            
        }


        /// <summary>
        /// 查找或导入文件夹成功，invoke主线程更新界面
        /// <para>2022.6.10</para>
        /// <para>version 1.2.0</para>
        /// </summary>
        /// <param name="f">查找到或导入的文件夹</param>
        private void UpdateFormInvokeFunction(FolderOrFile f)
        {
            if(InvokeRequired == false)//主线程则直接操作窗体
            {
                button_ok.Enabled = true; //查询结束后ok按钮恢复可用
                button_import.Enabled = true;
                root.Children = new FolderOrFile[] { f };
                f.Parent = root;
                UpdateForm(root);
                label_status.Text = "";
                UpdateForm(f);
            }
            else//子线程则调用主线程
            {
                if (thread != null && System.Threading.Thread.CurrentThread.ManagedThreadId == thread.ManagedThreadId) //如果该进程未被废弃则调用主线程操作窗体
                    this.Invoke(UpdateFormInvokeFunction, f);
            }
        }

        /// <summary>
        /// 查找或导入文件夹失败，invoke主线程更新界面
        /// <para>2022.6.10</para>
        /// <para>version 1.2.0</para>
        /// </summary>
        /// <param name="message">失败消息。查询失败为"folder not found"，导入失败为"import failed"</param>
        private void FolderNotFoundInvokeFunction(string message)
        {
            if(InvokeRequired == false)//主线程则直接操作窗体
            {
                button_ok.Enabled = true; //查询结束后ok按钮恢复可用
                button_import.Enabled = true;
                label_status.Text = message;
                root.Children = Array.Empty<FolderOrFile>();
                listView_data.Items.Clear();
                now = null;
                label_now_path.Text = "";
            }
            else//子线程则调用主线程
            {
                if (thread != null && System.Threading.Thread.CurrentThread.ManagedThreadId == thread.ManagedThreadId) //如果该进程未被废弃则调用主线程操作窗体
                    this.Invoke(FolderNotFoundInvokeFunction, message);      
            }
        }



        /// <summary>
        /// ok按钮创建的线程执行的函数
        /// <para>2022.6.7</para>
        /// <para>version 1.0.0</para>
        /// </summary>
        /// <param name="path">路径文本框中的内容</param>
        private void OkButtonThreadFunction(object path)
        {
            try//若找到文件夹则显示在界面上
            {
                FolderOrFile f = FolderOrFile.GetObjectFromPath(path.ToString());
                UpdateFormInvokeFunction(f);
            }
            catch (Exception ex)
            {
                FolderNotFoundInvokeFunction(ex.Message);
            }
            
        }


        /// <summary>
        /// 更新界面中展示的内容
        /// <para>2022.6.9</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <param name="f">界面中展示文件夹f的内容</param>
        public void UpdateForm(FolderOrFile f)
        {
            listView_data.Items.Clear();
            if (f == null)
            {
                now = null;
                return;
            }
                
            foreach(FolderOrFile forf in f.Children)
            {
                ListViewItem item = new ListViewItem();

                item.Text = forf.Name;
                if (forf.IsFolder)
                    item.SubItems.Add(FolderOrFile.FOLDER);
                else
                    item.SubItems.Add(FolderOrFile.FILE);

                item.SubItems.Add(forf.SizeFormat());
                if (forf.IsFolder)
                    item.SubItems.Add(forf.FileCount.ToString());
                else
                    item.SubItems.Add("");
                listView_data.Items.Add(item);
            }
            now = f;
            label_now_path.Text = f.FullName;
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
        /// <para>2022.6.9</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_data_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (now == null)
                return;
            int index = e.Column;//点击的列数
            if(index == 0)//按名字排序。点第一下是升序，第二下是降序
            {
                if(sort_type == 0)//如果已经是名字升序，再次点击则按名字降序排序
                {
                    QuickSort.Sort<FolderOrFile>(now.Children, CompareFunctions.ByNameDesc);
                    sort_type = 1;
                }
                else//否则按名字升序排序
                {
                    QuickSort.Sort<FolderOrFile>(now.Children, CompareFunctions.ByNameAsc);
                    sort_type=0;
                }
            }
            else if(index == 1)//按类型排序。点第一下是文件夹优先，第二下是文件优先
            {
                if(sort_type == 2)//如果已经是文件夹优先，再次点击则按文件优先排序
                {
                    QuickSort.Sort<FolderOrFile>(now.Children, CompareFunctions.ByIsFolderFalseFirst);
                    sort_type = 3;
                }
                else//否则按文件夹优先排序
                {
                    QuickSort.Sort<FolderOrFile>(now.Children, CompareFunctions.ByIsFolderTrueFirst);
                    sort_type = 2;
                }
            }
            else if(index == 2)//按大小排序。进行过其他排序后点击此列第一下是降序，点第二下或未进行过其他排序时点击此列则是升序
            {
                if (sort_type == 4)//如果已经是大小降序，再次点击则按大小升序
                {
                    QuickSort.Sort<FolderOrFile>(now.Children, CompareFunctions.BySizeAsc);
                    sort_type = 5;
                }
                else//否则按大小降序排序
                {
                    QuickSort.Sort<FolderOrFile>(now.Children);//大小降序是默认排序方法
                    sort_type = 4;
                }
            }
            else if (index == 3)//按文件数排序。点第一下是降序，第二下是升序
            {
                if (sort_type == 6)//如果已经是文件数降序，再次点击则按大小升序
                {
                    QuickSort.Sort<FolderOrFile>(now.Children, CompareFunctions.ByFileCountAsc);
                    sort_type = 7;
                }
                else//否则按文件数降序排序
                {
                    QuickSort.Sort<FolderOrFile>(now.Children, CompareFunctions.ByFileCountDesc);
                    sort_type = 6;
                }
            }
            UpdateForm(now);//更新界面
        }

        /// <summary>
        /// 若修改过排序方法，在切换显示对象前将now.Children的排序方式恢复为默认的大小降序。
        /// <para>2022.6.9</para>
        /// <para>version 1.1.0</para>
        /// </summary>
        private void RestoreSortType()
        {
            if(now != null && sort_type != 4)
            {
                sort_type = 4;
                QuickSort.Sort<FolderOrFile>(now.Children);
            }
        }

        /// <summary>
        /// 将当前查询的内容导出至json文件
        /// <para>2022.6.10</para>
        /// <para>version 1.2.0</para>
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
                    label_status.Text = "export succeed";

                }catch(Exception)
                {
                    label_status.Text = "export failed";
                }
                
            }
        }

        /// <summary>
        /// 从json文件中导入查询内容
        /// <para>2022.6.10</para>
        /// <para>version 1.2.0</para>
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
                    label_status.Text = "loading...";
                    string json = File.ReadAllText(filename);
                    button_ok.Enabled = false; //导入期间不能按ok按钮
                    button_import.Enabled = false;//导入期间不能再次导入

                    if (thread != null) //如果确定导入时已有一个查找或导入正在进行，则终止之，并进行导入
                        try { thread.Abort(); } catch (Exception) { }
                    thread = new Thread(ImportButtonThreadFunction);
                    thread.IsBackground = true;//子线程会随着主线程的退出而退出
                    thread.Start(json);
                }
            }
            catch(Exception)
            {
                FolderNotFoundInvokeFunction("import failed");
            }
        }

        /// <summary>
        /// import按钮创建的线程执行的函数
        /// <para>2022.6.10</para>
        /// <para>version 1.2.0</para>
        /// </summary>
        /// <param name="path">路径文本框中的内容</param>
        private void ImportButtonThreadFunction(object json)
        {
            try//若导入成功则显示在界面上
            {
                FolderOrFile? f = FolderOrFile.FromJson(json.ToString());
                if (f == null)
                    throw new Exception("import failed");
                UpdateFormInvokeFunction(f);
            }
            catch (Exception ex)
            {
                FolderNotFoundInvokeFunction(ex.Message);
            }

        }
    }
}
