using System;

using System.Drawing;

using ZL.CS.ConsoleEngine;

using ZL.CS.ConsoleEngine.UI;

using ZL.CS.Graphics;

namespace ZL.CS.CameraDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Fixed.Console.SetWindowSize(new(64, 32));

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

            Foreground foreground1 = new("★");

            ConsoleObject player = new("Player");

            //Camera.WillClear = false;

            Camera.WillDrawOutline = true;

            //Camera.OutlineColor = 201;

            Camera.WillDrawCrosshair = true;

            Camera.CrosshairColor = 201;

            Camera.Main = player.Add<Camera>();

            Text text = player.Add<Text>();

            text.graphic = foreground1;

            Position position = new(1, 0, -1);

            player.Start();

            while (true)
            {
                player.Transform.Position = position;

                Camera.Main?.Clear();

                player.DrawCall();

                Camera.Main?.DrawCall(background1, new(0, 0, 0));

                Camera.Main?.DrawCall(background2, new(2, 1, 1));

                Camera.Main?.Render();

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