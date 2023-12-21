using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFolderSize
{
    /// <summary>
    /// 英语本地化
    /// <para>2023.12.14</para>
    /// <para>version 1.4.0</para>
    /// </summary>
    public class LocalizationEN: Localization
    {

        private LocalizationEN() { Name = "EN"; }
        private static Localization? instance;
        public new static Localization GetInstance()
        {
            if (instance == null)
            {
                instance = new LocalizationEN();
                
            }
            return instance;
        }




    }
}
