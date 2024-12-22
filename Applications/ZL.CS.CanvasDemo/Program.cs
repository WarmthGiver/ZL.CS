using System;

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

            FixedConsole.SetWindowSize(32);

            Background background1 = new(new byte[,]
            {
                {015,000,015,000,015,000,015},

                {000,015,000,015,000,015,000},

                {015,000,015,000,015,000,015},

                {000,015,000,015,000,015,000},

                {015,000,015,000,015,000,015},

                {000,015,000,015,000,015,000},

                {015,000,015,000,015,000,015},
            });

            Background background2 = new(new byte[,]
            {
                {201,000,201,000,201,000,201},

                {000,201,000,201,000,201,000},

                {201,000,201,000,201,000,201},

                {000,201,000,201,000,201,000},

                {201,000,201,000,201,000,201},

                {000,201,000,201,000,201,000},

                {201,000,201,000,201,000,201},
            });

            Foreground foreground1 = new("★");

            ConsoleObject player = new("Player");

            //Camera.WillClear = false;

            //Camera.WillDrawOutline = true;

            //Camera.OutlineColor = 201;

            Camera.WillDrawCrosshair = true;

            Camera.CrosshairColor = 201;

            Camera.Main = player.Add<Camera>();

            Text text = player.Add<Text>();

            text.graphic = foreground1;

            Position position = new(0, 0, -1);

            player.Start();

            while (true)
            {
                player.Transform.Position = position;

                Camera.Main?.Clear();

                player.DrawCall();

                Camera.Main?.DrawCall(background1, new(0, 0, 0));

                Camera.Main?.DrawCall(background2, new(1, 1, 1));

                Camera.Main?.Render();

                
            }
        }
    }
}