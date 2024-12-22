using System;

using ZL.CS.ConsoleEngine;

using ZL.CS.Graphics;

namespace ZL.CS.SceneDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ResourceManager.AddBackground("BG1", new(new byte[,]
            {
                {015,000,015,000,015,000,015},

                {000,015,000,015,000,015,000},

                {015,000,015,000,015,000,015},

                {000,015,000,015,000,015,000},

                {015,000,015,000,015,000,015},

                {000,015,000,015,000,015,000},

                {015,000,015,000,015,000,015},
            }));

            ResourceManager.AddBackground("BG2", new(new byte[,]
            {
                {201,000,201,000,201,000,201},

                {000,201,000,201,000,201,000},

                {201,000,201,000,201,000,201},

                {000,201,000,201,000,201,000},

                {201,000,201,000,201,000,201},

                {000,201,000,201,000,201,000},

                {201,000,201,000,201,000,201},
            }));

            Console.CursorVisible = false;

            FixedConsole.SetWindowSize(32);

            SceneManager.Load<DemoScene>();

            Position position = new(0, 0, 0);

            while (true)
            {
                switch (Console.ReadKey(false).Key)
                {
                    case ConsoleKey.UpArrow:

                        --position.location.Y;

                        break;

                    case ConsoleKey.DownArrow:

                        ++position.location.Y;

                        break;

                    case ConsoleKey.LeftArrow:

                        --position.location.X;

                        break;

                    case ConsoleKey.RightArrow:

                        ++position.location.X;

                        break;
                }
            }
        }
    }
}