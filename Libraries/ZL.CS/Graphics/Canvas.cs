using System;

using System.Drawing;

namespace ZL.CS.Graphics
{
    public sealed class Canvas
    {
        private readonly Rectangle rect;

        private readonly Point pivot;

        public byte maxDepth;

        private readonly int[,] depthMap;

        private readonly byte[,] backgroundColorMap;

        private readonly byte[,] foregroundColorMap;

        private readonly char[,] foregroundTextMap;

        private readonly ANSI.BufferBuilder bufferBuilder = new();

        public Canvas(Size size, byte maxDepth = byte.MaxValue) : this(size.ToRect(), maxDepth) { }

        public Canvas(Size size, Point pivot, byte maxDepth = byte.MaxValue) : this(size.ToRect(), pivot, maxDepth) { }

        public Canvas(Rectangle rect, byte maxDepth = byte.MaxValue) : this(rect, rect.GetPivot(), maxDepth) { }

        public Canvas(Rectangle rect, Point pivot, byte maxDepth = byte.MaxValue)
        {
            this.rect = rect;

            this.pivot = pivot;

            this.maxDepth = maxDepth;

            depthMap = new int[rect.Height, rect.Width];

            backgroundColorMap = new byte[rect.Height, rect.Width];

            foregroundColorMap = new byte[rect.Height, rect.Width];

            foregroundTextMap = new char[rect.Height, rect.Width];

            Clear();
        }

        public void Clear()
        {
            depthMap.Fill(maxDepth);

            backgroundColorMap.Fill(Background.defaultColor);

            foregroundColorMap.Fill(Foreground.defaultColor);

            foregroundTextMap.Fill(' ');
        }

        public void DrawRequest(Background graphic, Position position)
        {
            if (graphic.colorMap == null)
            {
                return;
            }

            position.Location = position.Location.Add(pivot);

            position.Location = position.Location.Sub(graphic.pivot);

            Rectangle graphicRect = graphic.rect.Culling(rect, position.Location);

            Point point = new();

            for (int y = graphicRect.Y; y < graphicRect.Height; ++y)
            {
                point.Y = position.Location.Y + y;

                for (int x = graphicRect.X; x < graphicRect.Width; ++x)
                {
                    point.X = position.Location.X + x;

                    if (depthMap.Get(point) < position.Depth)
                    {
                        continue;
                    }

                    if (graphic.colorMap[y, x] == 0)
                    {
                        continue;
                    }

                    depthMap.Set(point, position.Depth);

                    backgroundColorMap.Set(point, graphic.colorMap[y, x]);

                    foregroundTextMap.Set(point, ' ');
                }
            }
        }

        public void DrawRequest(Foreground graphic, Position position)
        {
            position.Location = position.Location.Sub(graphic.pivot);

            Rectangle graphicRect = graphic.rect.Culling(rect, position.Location);

            Point point = new();

            for (int y = graphicRect.Y; y < graphicRect.Height; ++y)
            {
                point.Y = position.Location.Y + y;

                for (int x = graphicRect.X; x < graphicRect.Width; ++x)
                {
                    point.X = position.Location.X + x;

                    if (depthMap.Get(point) < position.Depth)
                    {
                        continue;
                    }

                    depthMap.Set(point, position.Depth);

                    if (graphic.colorMap != null)
                    {
                        foregroundColorMap.Set(point, graphic.colorMap[y, x]);
                    }

                    foregroundTextMap.Set(point, graphic.textMap[y, x]);
                }
            }
        }

        public void Merge(params Canvas[] canvases)
        {

        }

        public void Draw()
        {
            for (int y = 0; ;)
            {
                for (int x = 0; x < rect.Width; ++x)
                {
                    bufferBuilder.SetColor(backgroundColorMap[y, x], foregroundColorMap[y, x]);

                    bufferBuilder.Append(foregroundTextMap[y, x]);
                }

                if (++y >= rect.Height)
                {
                    break;
                }

                bufferBuilder.AppendLine();
            }

            Console.SetCursorPosition(0, 0);

            Console.Write(bufferBuilder.ToString());

            bufferBuilder.Clear();
        }
    }
}