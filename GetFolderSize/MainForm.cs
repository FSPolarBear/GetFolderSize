using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetFolderSize
{
    /// <summary>
    /// 查询文件夹中文件和子文件夹的大小，以大小的降序排列
    /// <para>2022.6.8</para>
    /// <para>version 1.0.0</para>
    /// </summary>
    public partial class MainForm : Form
    {
        FolderOrFile root; //查询结果文件夹作为此对象的Children
        FolderOrFile now; //当前显示的文件夹

        //public static MainForm Instance;
        Thread thread_ok = null; //点击ok按钮时创建的线程。此变量为未被废弃线程，若某子线程t与此变量不一致则子线程t被废弃

        public MainForm()
        {
            InitializeComponent();
            root = new FolderOrFile();
            root.Children = Array.Empty<FolderOrFile>();
            root.FullName = "";
            //Instance = this;
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
        /// <para>2022.6.8</para>
        /// <para>version 1.0.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ok_Click(object sender, EventArgs e)
        {
            label_not_found.Text = "loading...";

            button_ok.Enabled = false; //查找期间不能再次按ok按钮

            if (thread_ok != null) //如果按下ok按钮时已有一个查找正在进行，则终止该查找，并创建一个新查找
                try { thread_ok.Abort(); } catch (Exception) {  }    
            thread_ok = new Thread(OkButtonThreadFunction);
            thread_ok.Start(textBox_path.Text);
            
        }


        /// <summary>
        /// 查找文件夹成功，invoke主线程更新界面
        /// <para>2022.6.8</para>
        /// <para>version 1.0.0</para>
        /// </summary>
        /// <param name="f">查找到的文件夹</param>
        private void UpdateFormInvokeFunction(FolderOrFile f)
        {
            if(InvokeRequired == false)//主线程则直接操作窗体
            {
                button_ok.Enabled = true; //查询结束后ok按钮恢复可用
                root.Children = new FolderOrFile[] { f };
                f.Parent = root;
                UpdateForm(root);
                label_not_found.Text = "";
                UpdateForm(f);
            }
            else//子线程则调用主线程
            {
                if (thread_ok != null && System.Threading.Thread.CurrentThread.ManagedThreadId == thread_ok.ManagedThreadId) //如果该进程未被废弃则调用主线程操作窗体
                    this.Invoke(UpdateFormInvokeFunction, f);
            }
        }

        /// <summary>
        /// 查找文件夹失败，invoke主线程更新界面
        /// <para>2022.6.8</para>
        /// <para>version 1.0.0</para>
        /// </summary>
        /// <param name="message">失败消息。一般为"folder not found"</param>
        private void FolderNotFoundInvokeFunction(string message)
        {
            if(InvokeRequired == false)//主线程则直接操作窗体
            {
                button_ok.Enabled = true; //查询结束后ok按钮恢复可用
                label_not_found.Text = message;
                root.Children = Array.Empty<FolderOrFile>();
                listView_data.Items.Clear();
            }
            else//子线程则调用主线程
            {
                if (thread_ok != null && System.Threading.Thread.CurrentThread.ManagedThreadId == thread_ok.ManagedThreadId) //如果该进程未被废弃则调用主线程操作窗体
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
        /// <para>2022.6.7</para>
        /// <para>version 1.0.0</para>
        /// </summary>
        /// <param name="f">界面中展示文件夹f的内容</param>
        public void UpdateForm(FolderOrFile f)
        {
            listView_data.Items.Clear();
            if (f == null)
                return;
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
                    UpdateForm(now.Children[index]);
            }
            catch(Exception ex)//如果有异常就什么都不做
            {

            }
        }

        /// <summary>
        /// 返回上级
        /// <para>2022.6.7</para>
        /// <para>version 1.0.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_back_Click(object sender, EventArgs e)
        {
            if (now != null && now.Parent != null)
                UpdateForm(now.Parent);
        }

        /// <summary>
        /// 返回根目录
        /// <para>2022.6.7</para>
        /// <para>version 1.0.0</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_root_Click(object sender, EventArgs e)
        {
            if (root != null)
                UpdateForm(root);
        }
    }
}
