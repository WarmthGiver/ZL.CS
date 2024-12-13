using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZL.CS.ConsoleEngine;

namespace ZL.CS.Graphics.Demo
{
    public sealed class ANSIDemoScene : Scene
    {
        ANSI.BufferBuilder builder = new();

        public ANSIDemoScene(int framesRate, Size consoleSize) : base(framesRate, consoleSize)
        {
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

        public override void Start()
        {
            base.Start();

            ANSI.ShowColorPalette();
        }
    }
}