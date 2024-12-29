using System;

using System.Text;

namespace ZL.CS
{
    public static class ANSI
    {
        public const string open = "\x1b[";

        public const string reset = "\x1b[0m";

        public const byte bgDefaultColor = 000;

        public const byte fgDefaultColor = 007;

        public static void ShowColorPalette()
        {
            Console.WriteLine();

            for (short i = 0; i < 16;)
            {
                for (short j = 0; j < 8; ++i, ++j)
                {
                    Console.Write($"{open}48;5;{i}m {i.ToString("000")} ");
                }

                Console.WriteLine(reset);
            }

            Console.WriteLine();

            for (short i = 16; i < 232;)
            {
                for (short j = 0; j < 6; ++i, ++j)
                {
                    Console.Write($"{open}48;5;{i}m {i.ToString("000")} ");
                }

                Console.WriteLine(reset);
            }

            Console.WriteLine();

            for (short i = 232; i < 256;)
            {
                for (short j = 0; j < 6; ++i, ++j)
                {
                    Console.Write($"{open}48;5;{i}m {i.ToString("000")} ");
                }

                Console.WriteLine(reset);
            }

            Console.WriteLine();
        }

        public static string ColoredText(byte bgColor, byte fgColor, char text)
        {
            return $"{open}48;5;{bgColor};38;5;{fgColor}m{text}{reset}";
        }

        public static string ColoredText(byte bgColor, byte fgColor, string text)
        {
            return $"{open}48;5;{bgColor};38;5;{fgColor}m{text}{reset}";
        }

        public sealed class Builder
        {
            private readonly StringBuilder escapeCodeBuilder = new();

            private bool isEscapeCodeOpened = false;

            private byte backgroundColor = bgDefaultColor;

            public byte BackgroundColor
            {
                get => backgroundColor;

                set
                {
                    if (backgroundColor != value)
                    {
                        backgroundColor = value;

                        isEscapeCodeOpened = false;
                    }
                }
            }

            private byte foregroundColor = fgDefaultColor;

            public byte ForegroundColor
            {
                get => foregroundColor;

                set
                {
                    if (foregroundColor != value)
                    {
                        foregroundColor = value;

                        isEscapeCodeOpened = false;
                    }
                }
            }

            public void SetColor(byte  backgroundColor, byte foregroundColor)
            {
                BackgroundColor = backgroundColor;

                ForegroundColor = foregroundColor;
            }

            public void Append(char value)
            {
                AppentColor();

                escapeCodeBuilder.Append(value);
            }

            public void Append(string value)
            {
                AppentColor();

                escapeCodeBuilder.Append(value);
            }

            private void AppentColor()
            {
                if (isEscapeCodeOpened == false)
                {
                    isEscapeCodeOpened = true;

                    escapeCodeBuilder.Append($"{open}48;5;{BackgroundColor};38;5;{ForegroundColor}m");
                }
            }

            public void AppendLine()
            {
                escapeCodeBuilder.AppendLine(reset);

                isEscapeCodeOpened = false;
            }

            public void AppendReset()
            {
                escapeCodeBuilder.Append(reset);

                isEscapeCodeOpened = false;

                BackgroundColor = bgDefaultColor;

                ForegroundColor = fgDefaultColor;
            }

            public void Clear()
            {
                escapeCodeBuilder.Clear();

                isEscapeCodeOpened = false;

                BackgroundColor = bgDefaultColor;

                ForegroundColor = fgDefaultColor;
            }

            public override string ToString()
            {
                if (escapeCodeBuilder.Length == 0)
                {
                    return string.Empty;
                }

                return escapeCodeBuilder.ToString();
            }
        }
    }
}