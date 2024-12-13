using System;

using System.Drawing;

using ZL.CS.ConsoleEngine;

using ZL.CS.Graphics;

namespace ZL.CS.CanvasDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Size size = new(64, 32);

            Fixed.Console.SetWindowSize(size);

            ConsoleObject player = new("Player");

            Camera camera = player.Add<Camera>();

            camera.Size = new(64, 32);

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

            Position position = new(0, 0, 0);

            while (true)
            {
                player.Transform.Position = position;

                camera.Clear();

                camera.DrawCall(background1, new(-2, -1, 0));

                camera.DrawCall(background2, new(0, 0, 1));

                camera.Render();

                switch (Console.ReadKey(false).Key)
                {
                    case ConsoleKey.UpArrow:

                        position.location.Y -= 1;

                        break;

                    case ConsoleKey.DownArrow:

                        position.location.Y += 1;

                        break;

                    case ConsoleKey.LeftArrow:

                        position.location.X -= 2;

                        break;

                    case ConsoleKey.RightArrow:

                        position.location.X += 2;

                        break;
                }
            }
        }
    }
}