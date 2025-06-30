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
                float scaleX = 1.0f, scaleY = 1.0f;

                using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                {
                    scaleX = g.DpiX / 96.0f;
                    scaleY = g.DpiY / 96.0f;
                    Console.WriteLine($"DPI X: {g.DpiX}, DPI Y: {g.DpiY}, scaleX: {scaleX}, scaleY: {scaleY}");
                    // Or, for a GUI app:
                    // MessageBox.Show($"DPI X: {g.DpiX}, DPI Y: {g.DpiY}, scaleX: {scaleX}, scaleY: {scaleY}");
                }

                int width = (int)(bounds.Width * scaleX);
                int height = (int)(bounds.Height * scaleY);

                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.CopyFromScreen(
                        (int)(bounds.X * scaleX),
                        (int)(bounds.Y * scaleY),
                        0,
                        0,
                        new Size(width, height),
                        CopyPixelOperation.SourceCopy
                    );
                }
                return bmp;
            }
            catch
            {
                return null;
            }
        }
    }
}
