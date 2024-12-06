using System;
using System.Drawing;

namespace ZL.CS.Graphics
{
    public sealed class Canvas
    {
        public readonly Rectangle rect;
        public readonly Point pivot;

        private readonly sbyte maxDepth;
        private readonly sbyte[,] depthMap;

        private readonly int[,] backgroundColorMap;

        private readonly int[,] foregroundColorMap;
        private readonly char[,] foregroundTextMap;

        public Canvas(Rectangle rect, sbyte maxDepth = sbyte.MaxValue)
        {
            this.rect = rect;
            pivot = rect.GetPivot();

            this.maxDepth = maxDepth;
            depthMap = new sbyte[rect.Height, rect.Width];

            backgroundColorMap = new int[rect.Height, rect.Width];

            foregroundColorMap = new int[rect.Height, rect.Width];
            foregroundTextMap = new char[rect.Height, rect.Width];

            Clear();
        }

        public void Clear()
        {
            depthMap.Fill(maxDepth);

            backgroundColorMap.Fill(-1);

            foregroundColorMap.Fill(15);
            foregroundTextMap.Fill(' ');
        }

        public void Draw(Background background, Point point, sbyte depth)
        {
            var colorMap = background.colorMap;

            point.Add(pivot);
            point.Sub(background.pivot);

            Point minPoint = new(0, 0);
            Size maxPoint = background.size;

            Point drawPoint = point;

            for (int y = minPoint.Y; y < maxPoint.Height; ++y)
            {
                if (drawPoint.Y < 0)
                {
                    continue;
                }
                if (drawPoint.Y > rect.Bottom)
                {
                    break;
                }

                drawPoint.X = point.X;

                for (int x = minPoint.X; x < maxPoint.Width; ++x)
                {
                    if (drawPoint.X < 0)
                    {
                        continue;
                    }
                    if (drawPoint.Y > rect.Right)
                    {
                        break;
                    }

                    if (depthMap.Get(drawPoint) < depth)
                    {
                        continue;
                    }

                    if (colorMap[y, x] != -1)
                    {
                        backgroundColorMap.Set(drawPoint, colorMap[y, x]);

                        foregroundTextMap.Set(drawPoint, ' ');
                    }

                    ++drawPoint.X;
                }

                ++drawPoint.Y;
            }
        }

        public void Draw(Foreground foreground, Point position, sbyte depth)
        {
            var colorMap = foreground.colorMap;
            var textMap = foreground.textMap;

            Point minPoint = new(0, 0);
            Size maxPoint = foreground.size;

            Point drawPoint = position;

            for (int Y = minPoint.Y; Y < foreground.size.Height; ++Y)
            {
                if (drawPoint.Y < 0)
                {
                    continue;
                }
                if (drawPoint.Y > rect.Bottom)
                {
                    break;
                }

                drawPoint.X = position.X;

                for (int X = minPoint.X; X < textMap[Y].Length; ++X)
                {
                    if (drawPoint.X < 0)
                    {
                        continue;
                    }
                    if (drawPoint.Y > rect.Right)
                    {
                        break;
                    }

                    if (depthMap.Get(drawPoint) < depth)
                    {
                        continue;
                    }

                    if (colorMap != null)
                    {
                        foregroundColorMap.Set(drawPoint, colorMap[Y, X]);
                    }
                    foregroundTextMap.Set(drawPoint, textMap[Y][X]);

                    ++drawPoint.X;
                }

                ++drawPoint.Y;
            }
        }

        public void Show()
        {
            for (int y = 0; y < rect.Height; ++y)
            {
                for (int x = 0; x < rect.Width; ++x)
                {
                    Console.SetCursorPosition(rect.X + x, rect.Y + y);

                    if (backgroundColorMap[y, x] != -1)
                    {
                        Console.BackgroundColor = (ConsoleColor)backgroundColorMap[y, x];
                    }

                    Console.ForegroundColor = (ConsoleColor)foregroundColorMap[y, x];
                    Console.Write(foregroundTextMap[y, x]);

                    Console.ResetColor();
                }
            }
        }
    }
}