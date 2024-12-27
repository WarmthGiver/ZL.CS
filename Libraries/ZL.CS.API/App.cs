using System;

using System.Runtime.InteropServices;
using System.Threading;

namespace ZL.CS.API
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

        private const int WS_SIZEBOX = 0x00040000; // 창 크기 조정 스타일

        private const int WS_MAXIMIZEBOX = 0x00010000; // 최대화 버튼 스타일

        private static int targetFrameRate;

        public static int TargetFrameRate
        {
            get => targetFrameRate;

            set
            {
                targetFrameRate = value;

                Time.MinDeltaTime = 1.0 / value;
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
            while (Scene.State != SceneState.Terminated)
            {
                Thread.Sleep(1);
            }
        }
    }
}