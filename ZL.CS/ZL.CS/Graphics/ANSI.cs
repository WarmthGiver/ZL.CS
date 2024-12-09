using System;

using System.Text;

namespace ZL.CS.Graphics
{
    public static class ANSI
    {
        public static void ShowColorPalette()
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

        public static string ColoredText(byte bgColor, byte fgColor, char text)
        {
            return $"\x1b[48;5;{bgColor};38;5;{fgColor}m{text}\x1b[0m";
        }

        public static string ColoredText(byte bgColor, byte fgColor, string text)
        {
            return $"\x1b[48;5;{bgColor};38;5;{fgColor}m{text}\x1b[0m";
        }

        public class Builder
        {
            private StringBuilder escapeCodeBuilder = new StringBuilder();

            private bool isEscapeCodeOpened = false;

            private byte ackgroundColor = Background.defaultColor;

            public byte BackgroundColor { get; set; }

            private byte foregroundColor = Foreground.defaultColor;

            public byte ForegroundColor { get; set; }

            public void SetColor(byte  backgroundColor, byte foregroundColor)
            {
                isEscapeCodeOpened = false;

                BackgroundColor = backgroundColor;

                ForegroundColor = foregroundColor;
            }

            public void Append(char value)
            {
                AppentColorCode();

                escapeCodeBuilder.Append(value);
            }

            public void Append(string value)
            {
                AppentColorCode();

                escapeCodeBuilder.Append(value);
            }

            private void AppentColorCode()
            {
                if (isEscapeCodeOpened == false)
                {
                    isEscapeCodeOpened = true;

                    escapeCodeBuilder.Append("\x1b[");

                    if (BackgroundColor != Background.defaultColor)
                    {
                        escapeCodeBuilder.Append($";48;5;{BackgroundColor}");
                    }

                    if (ForegroundColor != Foreground.defaultColor)
                    {
                        escapeCodeBuilder.Append($";38;5;{ForegroundColor}");
                    }

                    escapeCodeBuilder.Append('m');
                }
            }

            public void AppendLine()
            {
                escapeCodeBuilder.AppendLine("\x1b[0m");

                isEscapeCodeOpened = false;
            }

            public void ClearColor()
            {
                escapeCodeBuilder.Append("\x1b[0m");

                isEscapeCodeOpened = false;

                BackgroundColor = Background.defaultColor;

                ForegroundColor = Foreground.defaultColor;
            }

            public override string ToString()
            {
                return escapeCodeBuilder.ToString();
            }

            public void Clear()
            {
                escapeCodeBuilder.Clear();

                isEscapeCodeOpened = false;

                BackgroundColor = Background.defaultColor;

                ForegroundColor = Foreground.defaultColor;
            }
        }
    }
}