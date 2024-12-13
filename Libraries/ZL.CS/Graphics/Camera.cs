using System;

using System.Drawing;

using ZL.CS.ConsoleEngine;

namespace ZL.CS.Graphics
{
    public sealed class Camera : Component
    {
        public static Camera? main { get; private set; } = null;

        private Size size;

        public Size Size
        {
            get => size;

            set
            {
                size = value;

                pivot = size.GetHalf();

                depthMap = new int[size.Height, size.Width];

                backgroundColorMap = new byte[size.Height, size.Width];

                foregroundColorMap = new byte[size.Height, size.Width];

                foregroundTextMap = new char[size.Height, size.Width];

                Clear();
            }
        }

        private Size pivot;

        public int maxDepth { get; set; } = int.MaxValue;

        private int[,] depthMap;

        private byte[,] backgroundColorMap;

        private byte[,] foregroundColorMap;

        private char[,] foregroundTextMap;

        private readonly ANSI.BufferBuilder bufferBuilder = new();

        public void Clear()
        {
            depthMap.Fill(maxDepth);

            backgroundColorMap.Fill(Background.defaultColor);

            foregroundColorMap.Fill(Foreground.defaultColor);

            foregroundTextMap.Fill(' ');
        }

        public void SetMain()
        {
            main = this;
        }

        public void DrawCall(Background graphic, Position position)
        {
            if (graphic.colorMap == null)
            {
                return;
            }

            Point cameraLocation = consoleObject.Transform.Location - pivot;

            Rectangle cameraView = new(cameraLocation, size);

            Point graphicLocation = position.location - graphic.pivot;

            Rectangle graphicRect = graphic.GetRect(graphicLocation, cameraView);

            Point mapPoint = new();

            Point mapLocation = graphicLocation.Sub(cameraLocation);

            for (int y = graphicRect.Y; y < graphicRect.Height; ++y)
            {
                mapPoint.Y = mapLocation.Y + y;

                for (int x = graphicRect.X; x < graphicRect.Width; ++x)
                {
                    mapPoint.X = mapLocation.X + x;

                    if (depthMap.Get(mapPoint) < position.depth)
                    {
                        continue;
                    }

                    if (graphic.colorMap[y, x] == 0)
                    {
                        continue;
                    }

                    depthMap.Set(mapPoint, position.depth);

                    backgroundColorMap.Set(mapPoint, graphic.colorMap[y, x]);

                    foregroundTextMap.Set(mapPoint, ' ');
                }
            }
        }

        public void DrawCall(Foreground graphic, Position position)
        {
            position.location -= graphic.pivot;

            Rectangle cameraView = new(consoleObject.Transform.Location - pivot, size);

            Rectangle graphicRect = graphic.GetRect(position.location, cameraView);

            Point mapPoint = new();

            for (int y = graphicRect.Y; y < graphicRect.Height; ++y)
            {
                mapPoint.Y = position.location.Y + y;

                for (int x = graphicRect.X; x < graphicRect.Width; ++x)
                {
                    mapPoint.X = position.location.X + x;

                    if (depthMap.Get(mapPoint) < position.depth)
                    {
                        continue;
                    }

                    depthMap.Set(mapPoint, position.depth);

                    if (graphic.colorMap != null)
                    {
                        foregroundColorMap.Set(mapPoint, graphic.colorMap[y, x]);
                    }

                    foregroundTextMap.Set(mapPoint, graphic.textMap[y, x]);
                }
            }
        }

        public void Render()
        {
            for (int y = 0; ;)
            {
                for (int x = 0; x < size.Width; ++x)
                {
                    bufferBuilder.SetColor(backgroundColorMap[y, x], foregroundColorMap[y, x]);

                    bufferBuilder.Append(foregroundTextMap[y, x]);
                }

                if (++y >= size.Height)
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