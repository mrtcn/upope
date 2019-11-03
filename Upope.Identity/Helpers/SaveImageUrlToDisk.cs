using ImageMagick;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace Upope.Identity.Helpers
{
    public static class ImageHelper
    {
        public static string SaveImageUrl(string imageUrl, ImageFormat format, string filename = null)
        {            
            if(string.IsNullOrWhiteSpace(filename))
                filename = Path.GetRandomFileName().Replace(".", "") + "." + format.ToString();

            string folderPath = @"UserImage/";
            string newImageUrl = $"{GlobalSettings.AppSettingsProvider.GatewayUrl}/resources/UserImage/" + filename;
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

                return newImageUrl;
            }                            
        }

        public static string SaveImage(byte[] imageByteArray, string fileName = null)
        {
            if(string.IsNullOrWhiteSpace(fileName))
                fileName = Path.GetRandomFileName().Replace(".", "") + "." + ImageFormat.Png;

            string folderPath = @"UserImage/";
            string newImageUrl = $"{GlobalSettings.AppSettingsProvider.GatewayUrl}/resources/UserImage/" + fileName;
            fileName = folderPath + fileName; // Remove period.

            if (imageByteArray.Length > 0)
            {
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    stream.Write(imageByteArray, 0, imageByteArray.Length);
                    stream.Flush();
                    stream.Close();
                }
            }

            var optimizer = new ImageOptimizer();
            optimizer.Compress(fileName);

            return newImageUrl;
        }
    }
}
