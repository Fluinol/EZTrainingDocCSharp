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
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.CopyFromScreen(
                        bounds.X,
                        bounds.Y,
                        0,
                        0,
                        new Size(bounds.Width, bounds.Height),
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
