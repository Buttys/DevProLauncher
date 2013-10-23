using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace DevProLauncher.Helpers
{
    public static class ImageResizer
    {
        public static void SaveImage(string imagePath, string savedName, int width = 0, int height = 0)
        {
            Image originalImage = Image.FromFile(imagePath);
            string filePath = AppDomain.CurrentDomain.BaseDirectory + savedName;

            if (width > 0 && height > 0)
            {
                Image.GetThumbnailImageAbort myCallback = ThumbnailCallback;
                Image imageToSave = originalImage.GetThumbnailImage(width, height, myCallback, IntPtr.Zero);
                imageToSave.Save(filePath, ImageFormat.Jpeg);
            }
            else
                originalImage.Save(filePath, ImageFormat.Jpeg);
        }

        public static void SaveImage(string imagePath, string savedName, ImageFormat format, Size size)
        {
            Image originalImage = Image.FromFile(imagePath);
            string filePath = AppDomain.CurrentDomain.BaseDirectory + savedName;

            if (size.Width > 0 && size.Height > 0)
            {
                Image.GetThumbnailImageAbort myCallback = ThumbnailCallback;
                Image imageToSave = originalImage.GetThumbnailImage(size.Width, size.Height, myCallback, IntPtr.Zero);
                imageToSave.Save(filePath, format);
            }
            else
                originalImage.Save(filePath, ImageFormat.Jpeg);
        }

        public static void SaveImage(Image image, string savedName, int width = 0, int height = 0)
        {
            Image originalImage = image;
            string filePath = AppDomain.CurrentDomain.BaseDirectory + savedName;

            if (width > 0 && height > 0)
            {
                Image.GetThumbnailImageAbort myCallback = ThumbnailCallback;
                Image imageToSave = originalImage.GetThumbnailImage(width, height, myCallback, IntPtr.Zero);
                imageToSave.Save(filePath, ImageFormat.Jpeg);
            }
            else
                originalImage.Save(filePath, ImageFormat.Jpeg);
        }

        public static byte[] ImageToBinary(string imagePath)
        {
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            Byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, (int)fileStream.Length);
            fileStream.Close();
            return buffer;
        }

        public static string OpenFileWindow(string title, string startpath, string filefilter)
        {
            OpenFileDialog dialog = new OpenFileDialog
                {
                    InitialDirectory = startpath,
                    Title = title,
                    Filter = filefilter,
                    Multiselect = true
                };

            return (dialog.ShowDialog() == DialogResult.OK) ? dialog.FileName : null;
        }

        private static bool ThumbnailCallback()
        {
            return false;
        }
    }
}
