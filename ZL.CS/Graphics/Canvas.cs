using System;

using System.Drawing;

namespace ZL.CS.Graphics
{
    public sealed class Canvas
    {
        private readonly Rectangle rect;

        private readonly Point pivot;

        public byte maxDepth;

        private readonly byte[,] depthMap;

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

            depthMap = new byte[rect.Height, rect.Width];

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

        public void DrawRequest(Background graphic, Point location, byte depth)
        {
            if (graphic.colorMap == null)
            {
                return;
            }

            location = location.Add(pivot);

            location = location.Sub(graphic.pivot);

            Rectangle graphicRect = graphic.rect.Culling(rect, location);

            Point point = new();

            for (int y = graphicRect.Y; y < graphicRect.Height; ++y)
            {
                point.Y = location.Y + y;

                for (int x = graphicRect.X; x < graphicRect.Width; ++x)
                {
                    point.X = location.X + x;

                    if (depthMap.Get(point) < depth)
                    {
                        continue;
                    }

                    if (graphic.colorMap[y, x] == 0)
                    {
                        continue;
                    }

                    depthMap.Set(point, depth);

                    backgroundColorMap.Set(point, graphic.colorMap[y, x]);

                    foregroundTextMap.Set(point, ' ');
                }
            }
        }

        public void DrawRequest(Foreground graphic, Point location, byte depth)
        {
            location = location.Sub(graphic.pivot);

            Rectangle graphicRect = graphic.rect.Culling(rect, location);

            Point point = new();

            for (int y = graphicRect.Y; y < graphicRect.Height; ++y)
            {
                point.Y = location.Y + y;

                for (int x = graphicRect.X; x < graphicRect.Width; ++x)
                {
                    point.X = location.X + x;

                    if (depthMap.Get(point) < depth)
                    {
                        continue;
                    }

                    depthMap.Set(point, depth);

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