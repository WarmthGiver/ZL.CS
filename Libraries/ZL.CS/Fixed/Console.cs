#pragma warning disable CA1416

using System.Drawing;

namespace ZL.CS.Fixed
{
    public static class Console
    {
        public static Size GetWindowSize()
        {
            return new(System.Console.WindowWidth, System.Console.WindowHeight);
        }

        public static void SetWindowSize(Size value)
        {
            System.Console.SetWindowSize(value.Width, value.Height);
        }

        public static Point GetCursorPosition()
        {
            return new(System.Console.CursorLeft, System.Console.CursorTop);
        }

        public static void SetCursorPosition(Point value)
        {
            System.Console.SetCursorPosition(value.X, value.Y);
        }
    }
}