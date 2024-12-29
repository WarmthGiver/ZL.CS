using System;

namespace ZL.CS.ANSIDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ANSI.ShowColorPalette();

            ANSI.Builder ansiBuilder = new();

            ansiBuilder.SetColor(196, 046);

            ansiBuilder.Append($" {ansiBuilder.BackgroundColor.ToString("000")},{ansiBuilder.ForegroundColor.ToString("000")} ");

            ansiBuilder.AppendLine();

            ansiBuilder.SetColor(046, 021);

            ansiBuilder.Append($" {ansiBuilder.BackgroundColor.ToString("000")},{ansiBuilder.ForegroundColor.ToString("000")} ");

            ansiBuilder.AppendLine();

            ansiBuilder.SetColor(021, 196);

            ansiBuilder.Append($" {ansiBuilder.BackgroundColor.ToString("000")},{ansiBuilder.ForegroundColor.ToString("000")} ");

            ansiBuilder.AppendReset();

            Console.WriteLine(ansiBuilder.ToString());
        }
    }
}