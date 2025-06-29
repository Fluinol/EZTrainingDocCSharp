using System.Drawing;

namespace EZTrainingDocCSharp.ScreenCapture
{
    public class ScreenshotInfo
    {
        public Bitmap Image { get; set; }
        public Point Coordinates { get; set; }
        public string ClickType { get; set; } // "left click" or "right click"
    }
}