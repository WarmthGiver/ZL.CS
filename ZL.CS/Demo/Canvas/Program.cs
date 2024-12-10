using System;

using System.Drawing;

using ZL.CS.Graphics;

namespace ZL.CS.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Size size = new(64, 32);

            Fixed.Console.SetWindowSize(size);

            Canvas canvas = new(size, new Point(33, 16));

            Background background1 = new(new byte[7, 14]
            {
                {015,015,000,000,015,015,000,000,015,015,000,000,015,015},


                {000,000,015,015,000,000,015,015,000,000,015,015,000,000},


                {015,015,000,000,015,015,000,000,015,015,000,000,015,015},


                {000,000,015,015,000,000,015,015,000,000,015,015,000,000},


                {015,015,000,000,015,015,000,000,015,015,000,000,015,015},


                {000,000,015,015,000,000,015,015,000,000,015,015,000,000},


                {015,015,000,000,015,015,000,000,015,015,000,000,015,015},
            });

            Background background2 = new(new byte[7, 14]
            {
                {201,201,000,000,201,201,000,000,201,201,000,000,201,201},


                {000,000,201,201,000,000,201,201,000,000,201,201,000,000},


                {201,201,000,000,201,201,000,000,201,201,000,000,201,201},


                {000,000,201,201,000,000,201,201,000,000,201,201,000,000},


                {201,201,000,000,201,201,000,000,201,201,000,000,201,201},


                {000,000,201,201,000,000,201,201,000,000,201,201,000,000},


                {201,201,000,000,201,201,000,000,201,201,000,000,201,201},
            });

            Point position = new(-2, -1);

            while (true)
            {
                canvas.Clear();

                canvas.DrawRequest(background1, position, 0);

                canvas.DrawRequest(background2, new(0, 0), 1);

                canvas.Draw();

                switch (Console.ReadKey(false).Key)
                {
                    case ConsoleKey.UpArrow:

                        position.Y -= 1;

                        break;

                    case ConsoleKey.DownArrow:

                        position.Y += 1;

                        break;

                    case ConsoleKey.LeftArrow:

                        position.X -= 2;

                        break;

                    case ConsoleKey.RightArrow:

                        position.X += 2;

                        break;
                }
            }
        }
    }
}