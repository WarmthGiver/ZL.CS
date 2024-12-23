#pragma warning disable CA1416

using System;

using System.Drawing;

namespace ZL.CS.ConsoleEngine
{
    public static class FixedConsole
    {
        public static Size GetWindowSize()
        {
            return new(Console.WindowWidth >> 1, Console.WindowHeight);
        }

        public static void SetWindowSize(Size value)
        {
            Console.SetWindowSize(value.Width << 1, value.Height);
        }

        public static void SetWindowSize(int value)
        {
            Console.SetWindowSize(value << 1, value);
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