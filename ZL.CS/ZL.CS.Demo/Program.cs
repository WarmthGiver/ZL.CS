using System;
using System.Drawing;
using System.Threading;

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
            Canvas canvas = new(size);

            Background background = new(new byte[7, 14]
            {
                {015,015,000,000,015,015,000,000,015,015,000,000,015,015},


                {000,000,015,015,000,000,015,015,000,000,015,015,000,000},


                {015,015,000,000,015,015,000,000,015,015,000,000,015,015},


                {000,000,015,015,000,000,015,015,000,000,015,015,000,000},


                {015,015,000,000,015,015,000,000,015,015,000,000,015,015},


                {000,000,015,015,000,000,015,015,000,000,015,015,000,000},


                {015,015,000,000,015,015,000,000,015,015,000,000,015,015},
            });

            Point position = new(1, 0);

            while (true)
            {
                canvas.Clear();
                canvas.DrawRequest(background, position, 0);
                canvas.Draw();

                var key = Console.ReadKey(false).Key;

                switch (key)
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

                //Thread.Sleep(16);
            }
        }
    }
}