using System;

using System.Runtime.InteropServices;

namespace ZL.CS.ConsoleEngine
{
    public abstract class Application
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]

        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]

        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]

        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]

        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]

        private static extern bool DeleteMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        private const int GWL_STYLE = -16;

        private const int WS_SIZEBOX = 0x00040000; // 창 크기 조정 스타일

        private const int WS_MAXIMIZEBOX = 0x00010000; // 최대화 버튼 스타일

        private int width;

        private int height;

        protected Application(int width, int height)
        {
            this.width = width;

            this.height = height;
        }

        public void Run()
        {
            FixedConsole.SetWindowSize(width, height);

            FixedConsole.SetBufferSize(width, height);

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

            }
        }
    }
}