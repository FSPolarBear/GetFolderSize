
using IWshRuntimeLibrary;

namespace GetFolderSize
{
    /// <summary>
    /// 自启动相关功能
    /// </summary>
    /// 2024.5.19
    /// version 2.0.0
    internal static class AutostartUtil
    {
        private static readonly string path_exe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        private static readonly string dir_GetFolderSize = Path.GetDirectoryName(path_exe);
        private static readonly string path_lnk = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\GetFolderSize.exe.lnk";

        /// <summary>
        /// 设置开启或关闭开机自启动
        /// </summary>
        /// 2024.5.19
        /// version 2.0.0
        /// <param name="enabled">true表示开启，false表示关闭</param>
        public static void SetAutostart(bool enabled)
        {
            if (enabled)
                AutostartUtil.EnableAutostart();
            else
                AutostartUtil.DisableAutostart();
        }

        /// <summary>
        /// 开启自启动
        /// </summary>
        /// 2024.4.28
        /// version 2.0.0
        public static void EnableAutostart()
        {
            
            if (System.IO.File.Exists(path_lnk))
                return;
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(path_lnk);
            shortcut.TargetPath = path_exe;
            shortcut.WorkingDirectory = dir_GetFolderSize;
            shortcut.WindowStyle = 1;
            shortcut.Description = "GetFolderSize.exe";
            shortcut.IconLocation = path_exe;
            shortcut.Save();
        }

        /// <summary>
        /// 关闭自启动
        /// </summary>
        /// 2024.4.28
        /// version 2.0.0
        public static void DisableAutostart()
        {
            if (System.IO.File.Exists(path_lnk))
                System.IO.File.Delete(path_lnk);
        }

    }
}
