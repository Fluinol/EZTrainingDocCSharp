using Gma.System.MouseKeyHook;
using System;
using EZTrainingDocCSharp.ScreenCapture;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using System.Drawing;

namespace EZTrainingDocCSharp.Mouse
{
    public static class MouseListener
    {
        private static IKeyboardMouseEvents _globalHook;
        private static List<Bitmap> _capturedScreenshotsList;

        // Start method only needs the screenshot list
        public static void Start(List<Bitmap> capturedScreenshotsList)
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
                OnLeftClick();
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                OnRightClick();
            }
        }

        private static void OnLeftClick()
        {
            Console.WriteLine("Left click");
            var screenshot = ScreenshotTaker.CaptureScreen();
            if (screenshot != null && _capturedScreenshotsList != null)
            {
                _capturedScreenshotsList.Add(screenshot);
            }
        }

        private static void OnRightClick()
        {
            Console.WriteLine("Right click");
            var screenshot = ScreenshotTaker.CaptureScreen();
            if (screenshot != null && _capturedScreenshotsList != null)
            {
                _capturedScreenshotsList.Add(screenshot);
            }
        }
    }
}
