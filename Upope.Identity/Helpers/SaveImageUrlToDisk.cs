using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Reflection;

namespace Upope.Identity.Helpers
{
    public static class SaveImageUrlToDisk
    {
        public static string SaveImage(string imageUrl, string projectPath, ImageFormat format)
        {            
            string filename = Path.GetRandomFileName().Replace(".", "") + "." + format.ToString();
            string folderPath = projectPath + @"\UserImage\";
            filename = folderPath  + filename; // Remove period.

            using (WebClient client = new WebClient())
            {
                new FileInfo(folderPath).Directory.Create();

                Stream stream = client.OpenRead(imageUrl);
                Bitmap bitmap; bitmap = new Bitmap(stream);

                if (bitmap != null)
                    bitmap.Save(filename, format);

                stream.Flush();
                stream.Close();
                client.Dispose();

                return filename;
            }                            
        }
    }
}
