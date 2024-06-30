

using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace GetFolderSize.util
{
    /// <summary>
    /// 启动程序时判断是否存在一个已启动的此程序。若存在，则改为切换至已启动的此程序
    /// </summary>
    /// 2024.5.26
    /// version 2.0.0
    internal class StartUtil: IMessageFilter
    {

        private static readonly uint message = 0x8001;
        private MainForm mainForm;
        public StartUtil(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }


        [DllImport("user32.dll")]
        public static extern bool PostThreadMessage(int threadId, uint msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 启动程序时判断是否存在一个已启动的此程序。若存在，则退出此程序，改为切换至已启动的此程序；若不存在，则正常启动程序。
        /// </summary>
        /// 2024.5.26
        /// version 2.0.0
        public static void StartOrSwitch()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id && process.Threads.Count > 0)
                {
                    // 向已启动的程序发送消息，通知其激活窗口
                    PostThreadMessage(process.Threads[0].Id, message, IntPtr.Zero, IntPtr.Zero);
                    Environment.Exit(0);
                }
            }
        }

        /// <summary>
        /// 收到另一个此程序启动的消息，激活并切换至已启动的此程序窗口
        /// </summary>
        /// 2024.5.22
        /// version 2.0.0
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == message)
            {
                if (mainForm.WindowState == FormWindowState.Minimized)
                {
                    mainForm.WindowState = FormWindowState.Normal;
                }
                mainForm.Activate();
                mainForm.Visible = true;
                mainForm.ShowInTaskbar = true; 
                return true;
            } 
            return false;
        }
        



    }
}
