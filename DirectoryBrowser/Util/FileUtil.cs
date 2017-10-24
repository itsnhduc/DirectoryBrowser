using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DirectoryBrowser.Util
{
    class FileUtil
    {
        public static string StringifyShort(long fileSize)
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
        public static ImageSource ToImageSource(Bitmap bitmap)
        {
            ImageSource result;
            
            MemoryStream memStream = new MemoryStream();

            bitmap.Save(memStream, System.Drawing.Imaging.ImageFormat.Png);
            result = BitmapFrame.Create(memStream);

            return result;
        }
        public static ImageSource ToImageSource(Icon icon)
        {
            return ToImageSource(icon.ToBitmap());
        }
    }
}
