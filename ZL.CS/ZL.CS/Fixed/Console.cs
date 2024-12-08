using System;
using System.Drawing;

namespace ZL.CS.Fixed
{
    public static class Console
    {
        public static void SetWindowSize(Size size)
        {
            System.Console.SetWindowSize(size.Width, size.Height);
        }

        public static void SetCursorPosition(ValueTuple<int, int> position)
        {
            System.Console.SetCursorPosition(position.Item1, position.Item2);
        }
    }
}