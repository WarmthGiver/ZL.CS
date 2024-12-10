using ZL.CS.Graphics;

namespace ZL.CS.Demo.ANSIBuilder
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ANSI.ShowColorPalette();

            ANSI.BufferBuilder builder = new();

            builder.SetColor(196, 046);

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
    }
}