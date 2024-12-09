using System;

using System.Drawing;

using ZL.CS.Graphics;

namespace ZL.CS.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ANSIDemo();

            //CanvasDemo();
        }

        private static void ANSIDemo()
        {
            ANSI.ShowColorPalette();

            byte defaultBackgroundColor = Background.defaultColor;

            byte defaultForegroundColor = Foreground.defaultColor;

            Console.WriteLine($"Default Background Color: {defaultBackgroundColor.ToString("000")}");

            Console.WriteLine($"Default Foreground Color: {defaultForegroundColor.ToString("000")}");

            Console.WriteLine();

            Console.Write(ANSI.ColoredText(000, 015, " 000 "));

            Console.Write(" != ");

            Console.Write(ANSI.ColoredText(016, 015, " 016 "));

            Console.Write(" != ");

            Console.Write(ANSI.ColoredText(232, 015, " 232 "));

            Console.WriteLine();

            Console.WriteLine();

            ANSI.Builder builder = new();

            builder.SetColor(196, 046);

            builder.Append($" {builder.BackgroundColor.ToString("000")},{builder.ForegroundColor.ToString("000")} ");

            builder.SetColor(000, 007);

            builder.Append($" {builder.BackgroundColor.ToString("000")},{builder.ForegroundColor.ToString("000")} ");

            builder.AppendLine();

            builder.SetColor(046, 021);

            builder.Append($" {builder.BackgroundColor.ToString("000")},{builder.ForegroundColor.ToString("000")} ");

            builder.AppendLine();

            builder.SetColor(021, 196);

            builder.Append($" {builder.BackgroundColor.ToString("000")},{builder.ForegroundColor.ToString("000")} ");

            builder.ClearColor();

            Console.WriteLine(builder.ToString());
        }

        private static void CanvasDemo()
        {
            Console.CursorVisible = false;

            Size size = new(64, 32);

            Fixed.Console.SetWindowSize(size);

            Canvas canvas = new(size);

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

            Point position = new(1, 0);

            while (true)
            {
                canvas.Clear();

                canvas.DrawRequest(background1, position, 0);

                canvas.DrawRequest(background2, new(31, 16), 1);

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
            }
        }
    }
}