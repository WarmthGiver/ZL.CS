using System;
using System.Drawing;

namespace ZL.CS
{
    public static class Debug
    {
        public static void DrawDot(Point point, ConsoleColor color)
        {
            Console.BackgroundColor = color;

            Console.SetCursorPosition(point.X, point.Y);
            Console.Write(' ');

            Console.ResetColor();
        }

        public static void DrawRect(Rectangle rect, ConsoleColor color)
        {
            Console.BackgroundColor = color;

            for (int y = rect.Y; y < rect.Bottom; ++y)
            {
                for (int x = rect.X; x < rect.Right; ++x)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(' ');
                }
            }

            Console.ResetColor();
        }
    }
}