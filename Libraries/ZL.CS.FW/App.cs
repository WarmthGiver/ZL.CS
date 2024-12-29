using System;

using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ZL.CS.FW
{
    public abstract class App
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]

        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]

        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]

        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_STYLE = -16;

        private const int WS_SIZEBOX = 0x00040000;

        private const int WS_MAXIMIZEBOX = 0x00010000;

        private static int targetFrameRate;

        public static int TargetFrameRate
        {
            get => targetFrameRate;

            set
            {
                targetFrameRate = value;

                Time.MinDeltaTime = 1.0f / value;
            }
        }

        public void Run()
        {
            IntPtr consoleWindow = GetConsoleWindow();

            int style = GetWindowLong(consoleWindow, GWL_STYLE);

            style &= ~WS_MAXIMIZEBOX;

            style &= ~WS_SIZEBOX;

            SetWindowLong(consoleWindow, GWL_STYLE, style);

            Start();

            LifeSupport();
        }

        protected abstract void Start();

        private void LifeSupport()
        {
            while (Scene.State != SceneState.Terminated) { }
        }
    }
}