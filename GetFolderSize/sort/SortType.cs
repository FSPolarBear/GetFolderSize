using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFolderSize
{
    internal enum SortType
    {
        NameAsc,
        NameDesc,
        FolderFirst,
        FileFirst,
        SizeAsc,
        SizeDesc,
        FileCountAsc,
        FileCountDesc,
        LastWriteTimeAsc,
        LastWriteTimeDesc
    }
}
