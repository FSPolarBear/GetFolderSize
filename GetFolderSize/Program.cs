using GetFolderSize.util;

// 使测试可以访问此项目的internal类
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("GetFolderSizeTest")]

namespace GetFolderSize
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Config.LoadFromFile();
            if (Config.OnlyOneProcessAllowed)
                StartUtil.StartOrSwitch();

            ApplicationConfiguration.Initialize();
            MainForm mainForm = new MainForm();
            Application.AddMessageFilter(new StartUtil(mainForm));
            Application.Run(mainForm);
        }
    }
}