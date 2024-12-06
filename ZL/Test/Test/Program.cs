using System.Drawing;

using ZL.CS.Graphics;

namespace ZL.CS.Test
{
    internal class Program
    {
        private static Canvas canvas;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Rectangle rect = new(0, 0, 64, 32);

            Fixed.Console.SetWindowSize(rect.Size);

            Background background = new(new int[7,14]
            {
                {15,15,-1,-1,15,15,-1,-1,15,15,-1,-1,15,15},
                {-1,-1,15,15,-1,-1,15,15,-1,-1,15,15,-1,-1},
                {15,15,-1,-1,15,15,-1,-1,15,15,-1,-1,15,15},
                {-1,-1,15,15,-1,-1,15,15,-1,-1,15,15,-1,-1},
                {15,15,-1,-1,15,15,-1,-1,15,15,-1,-1,15,15},
                {-1,-1,15,15,-1,-1,15,15,-1,-1,15,15,-1,-1},
                {15,15,-1,-1,15,15,-1,-1,15,15,-1,-1,15,15},
            });

            Debug.DrawRect(rect, ConsoleColor.Magenta);
            Debug.DrawDot(rect.GetPivot(), ConsoleColor.Cyan);

            canvas = new(rect);
            canvas.Show();
            //canvas.Draw(background, new(0, 0), 0);

            Console.ReadKey(false);
        }
    }
}