#pragma warning disable CA1416

using System;

using System.Drawing;

namespace ZL.CS.FW
{
    public static class FixedConsole
    {
        public static void SetBufferSize(Size value)
        {
            SetBufferSize(value.Width, value.Height);
        }

        public static void SetBufferSize(int width, int height)
        {
            Console.SetBufferSize(width << 1, height);
        }

        public static Size GetWindowSize()
        {
            return new(Console.WindowWidth >> 1, Console.WindowHeight);
        }
        
        public static void SetWindowSize(Size value)
        {
            SetWindowSize(value.Width, value.Height);
        }

        public static void SetWindowSize(int width, int height)
        {
            Console.SetWindowSize(width << 1, height);
        }

        public static Point GetCursorPosition()
        {
            return new(Console.CursorLeft, Console.CursorTop);
        }

        public static void SetCursorPosition(Point value)
        {
            Console.SetCursorPosition(value.X, value.Y);
        }
    }
}