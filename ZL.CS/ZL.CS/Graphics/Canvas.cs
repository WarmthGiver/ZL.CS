using System;
using System.Drawing;
using System.Text;

namespace ZL.CS.Graphics
{
    public sealed class Canvas
    {
        public readonly Size size;

        private readonly byte maxDepth;
        private readonly byte[,] depthMap;

        private readonly byte[,] backgroundColorMap;

        private readonly byte[,] foregroundColorMap;
        private readonly char[,] foregroundTextMap;

        private readonly StringBuilder stringBuilder = new();

        public Canvas(Size size, byte maxDepth = byte.MaxValue)
        {
            this.size = size;

            this.maxDepth = maxDepth;
            depthMap = new byte[size.Height, size.Width];

            backgroundColorMap = new byte[size.Height, size.Width];

            foregroundColorMap = new byte[size.Height, size.Width];
            foregroundTextMap = new char[size.Height, size.Width];

            Clear();
        }

        public void Clear()
        {
            depthMap.Fill(maxDepth);

            backgroundColorMap.Fill(Background.defaultColor);

            foregroundColorMap.Fill(Foreground.defaultColor);
            foregroundTextMap.Fill(' ');
        }

        public void DrawRequest(Background graphic, Point point, byte depth)
        {
            point = point.Sub(graphic.pivot);

            Rectangle rect = new(new(0, 0), graphic.size);
            rect = rect.Culling(new(new(0, 0), size), point);

            Point startPoint = point;

            for (int y = rect.Y; y < rect.Height; ++y)
            {
                point.Y = startPoint.Y + y;

                for (int x = rect.X; x < rect.Width; ++x)
                {
                    point.X = startPoint.X + x;

                    if (depthMap.Get(point) < depth)
                    {
                        continue;
                    }

                    backgroundColorMap.Set(point, graphic.colorMap[y, x]);

                    foregroundTextMap.Set(point, ' ');
                }
            }
        }

        // 이 부분 수정할 것
        public void DrawRequest(Foreground graphic, Point point, byte depth)
        {
            var colorMap = graphic.colorMap;
            var textMap = graphic.textMap;

            point = point.Sub(graphic.pivot);

            Rectangle rect = new(new(0, 0), graphic.size);
            rect = rect.Culling(new(new(0, 0), size), point);

            Point bufferPoint = point;

            for (int y = rect.Y; y < rect.Height; ++y)
            {
                bufferPoint.Y = point.Y + y;

                for (int x = rect.X; x < textMap[y].Length; ++x)
                {
                    bufferPoint.X = point.X + x;

                    if (depthMap.Get(bufferPoint) < depth)
                    {
                        continue;
                    }

                    if (colorMap != null)
                    {
                        foregroundColorMap.Set(bufferPoint, colorMap[y, x]);
                    }
                    foregroundTextMap.Set(bufferPoint, textMap[y][x]);
                }
            }
        }

        public void Draw()
        {
            for (int y = 0; ;)
            {
                for (int x = 0; x < size.Width; ++x)
                {
                    var bgColor = backgroundColorMap[y, x];
                    var fgColor = foregroundColorMap[y, x];

                    stringBuilder.Append(ANSI.ColorText(bgColor, fgColor, foregroundTextMap[y, x]));
                }

                if (++y >= size.Height)
                {
                    break;
                }

                stringBuilder.AppendLine();
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(stringBuilder.ToString());
            stringBuilder.Clear();
        }
    }
}