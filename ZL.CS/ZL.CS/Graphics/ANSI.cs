using System;

namespace ZL.CS.Graphics
{
    public static class ANSI
    {
        public static void ColorPalette()
        {
            Console.WriteLine();
            for (short i = 0; i < 16;)
            {
                for (short j = 0; j < 8; ++i, ++j)
                {
                    Console.Write($"\x1b[48;5;{i}m {i.ToString("000")} ");
                }
                Console.WriteLine("\x1b[0m");
            }
            Console.WriteLine();
            for (short i = 16; i < 232;)
            {
                for (short j = 0; j < 6; ++i, ++j)
                {
                    Console.Write($"\x1b[48;5;{i}m {i.ToString("000")} ");
                }
                Console.WriteLine("\x1b[0m");
            }
            Console.WriteLine();
            for (short i = 232; i < 256;)
            {
                for (short j = 0; j < 6; ++i, ++j)
                {
                    Console.Write($"\x1b[48;5;{i}m {i.ToString("000")} ");
                }
                Console.WriteLine("\x1b[0m");
            }
            Console.WriteLine();
        }

        public static string ColorText(byte bgColor, byte fgColor, char text)
        {
            return $"\x1b[48;5;{bgColor};38;5;{fgColor}m{text}\x1b[0m";
        }
        public static string ColorText(byte bgColor, byte fgColor, string text)
        {
            return $"\x1b[48;5;{bgColor};38;5;{fgColor}m{text}\x1b[0m";
        }
    }
}