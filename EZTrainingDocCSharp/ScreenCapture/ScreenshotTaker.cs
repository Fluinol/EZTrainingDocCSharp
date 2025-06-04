using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace EZTrainingDocCSharp.ScreenCapture
{
    public static class ScreenshotTaker
    {
        public static Bitmap CaptureScreen()
        {
            try
            {
                Rectangle bounds = Screen.PrimaryScreen.Bounds;
                Bitmap bmp = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
                Graphics graphics = Graphics.FromImage(bmp);
                //using Graphics g = graphics;
                graphics.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
                return bmp;
            }
            catch
            {
                return null;
            }
        }
    }
}
