using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    /// <summary>
    /// Config of json.
    /// </summary>
    /// 2024.4.23
    /// version 1.0.2
    public static class JsonConfig
    {

        /// <summary>
        /// If true, all non-ascii characters will be escaped by \u.
        /// </summary>
        /// 2024.4.23
        /// version 1.0.3
        public static bool EnsureAscii = false;
    }
}
