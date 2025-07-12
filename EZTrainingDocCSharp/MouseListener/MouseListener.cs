using EZTrainingDocCSharp.ScreenCapture;
using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EZTrainingDocCSharp.Mouse
{
    public static class MouseListener
    {
        private static IKeyboardMouseEvents _globalHook;
        private static List<ScreenshotInfo> _capturedScreenshotsList;

        // Start method only needs the screenshot list
        public static void Start(List<ScreenshotInfo> capturedScreenshotsList)
        {
            _capturedScreenshotsList = capturedScreenshotsList;
            Start();

        }

        public static void Start()
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.MouseDownExt += GlobalHookMouseDownExt;
            Console.WriteLine("Mouse listener started");
        }

        public static void Stop()
        {
            if (_globalHook != null)
            {
                _globalHook.MouseDownExt -= GlobalHookMouseDownExt;
                _globalHook.Dispose();
                _globalHook = null;
                Console.WriteLine("Mouse listener stopped");
            }
        }

        private static void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                OnLeftClick(e.X, e.Y);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                OnRightClick(e.X, e.Y);
            }
        }

        private static void OnLeftClick(int x, int y)
        {
            var screenshot = ScreenshotTaker.CaptureScreen();

            //save screenshot with timestamp into desktop folder for debugging purposes
            //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            //string screenshotPath = Path.Combine(desktopPath, $"screenshot_{timestamp}.png");
            //screenshot?.Save(screenshotPath, System.Drawing.Imaging.ImageFormat.Png);

            if (screenshot != null && _capturedScreenshotsList != null)
            {
                using (Graphics g = Graphics.FromImage(screenshot))
                {
                    // Draw the system cursor at the click location (32x32 size, adjust as needed)
                    Cursors.Default.Draw(g, new Rectangle(x, y, 32, 32));
                }
                _capturedScreenshotsList.Add(new ScreenshotInfo
                {
                    Image = screenshot,
                    Coordinates = new System.Drawing.Point(x, y),
                    ClickType = "left click"
                });
            }
        }

        private static void OnRightClick(int x, int y)
        {
            var screenshot = ScreenshotTaker.CaptureScreen();
            if (screenshot != null && _capturedScreenshotsList != null)
            {
                using (Graphics g = Graphics.FromImage(screenshot))
                {
                    // Draw the system cursor at the click location (32x32 size, adjust as needed)
                    Cursors.Default.Draw(g, new Rectangle(x, y, 32, 32));
                }
                _capturedScreenshotsList.Add(new ScreenshotInfo
                {
                    Image = screenshot,
                    Coordinates = new System.Drawing.Point(x, y),
                    ClickType = "right click"
                });
            }
        }
    }
}
