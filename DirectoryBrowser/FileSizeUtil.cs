using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryBrowser
{
    class FileSizeUtil
    {
        public static string ConvertToString(long fileSize)
        {
            if (fileSize > Math.Pow(1024, 2))
            {
                return (int)Math.Round(fileSize / Math.Pow(1024, 2)) + "MB";
            }
            else if (fileSize > 1024f)
            {
                return (int)Math.Round(fileSize / 1024f) + "MB";
            }
            else
            {
                return fileSize + "B";
            }
        }
    }
}
