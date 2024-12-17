using System;

using System.Drawing;

using ZL.CS.ConsoleEngine;

namespace ZL.CS.Graphics
{
    public sealed class Camera : Component
    {
        private static Camera? main = null;

        public static Camera? Main
        {
            get => main;
            
            set
            {
                main = value;
            }
        }

        public static bool WillClear { get; set; } = true;

        public static bool WillDrawOutline { get; set; } = false;

        public static byte OutlineColor { get; set; } = 233;

        public static bool WillDrawCrosshair { get; set; } = false;

        public static byte CrosshairColor { get; set; } = 233;

        public static int MaxDepth { get; set; } = int.MaxValue;

        private static Size size;

        private static Size pivot;

        private static int[,] backgroundDepthMap;

        private static int[,] foregroundDepthMap;

        private static byte[,] backgroundColorMap;

        private static byte[,] foregroundColorMap;

        private static char[,] foregroundTextMap;

        private static readonly ANSI.BufferBuilder bufferBuilder = new();

        public override void Start()
        {
            size = FixedConsole.GetWindowSize();

            pivot = size.GetPivot();

            backgroundDepthMap = new int[size.Height, size.Width];

            foregroundDepthMap = new int[size.Height, size.Width];

            backgroundColorMap = new byte[size.Height, size.Width];

            foregroundColorMap = new byte[size.Height, size.Width];

            foregroundTextMap = new char[size.Height, size.Width];

            Clear();
        }

        public void Clear()
        {
            backgroundDepthMap.Fill(MaxDepth);

            foregroundDepthMap.Fill(MaxDepth);

            if (WillClear == true)
            {
                backgroundColorMap.Fill(Background.defaultColor);

                foregroundColorMap.Fill(Foreground.defaultColor);

                foregroundTextMap.Fill(' ');
            }

            if (WillDrawOutline == true)
            {
                DrawOutline();
            }

            if (WillDrawCrosshair == true)
            {
                DrawCrosshair();
            }
        }

        private static void DrawOutline()
        {
            Point maxIndex = backgroundColorMap.GetMaxIndex();

            for (int x = maxIndex.X; x >= 0; --x)
            {
                backgroundColorMap[0, x] = OutlineColor;

                backgroundColorMap[maxIndex.Y, x] = OutlineColor;
            }

            for (int y = maxIndex.Y - 1; y >= 1; --y)
            {
                backgroundColorMap[y, 0] = OutlineColor;

                backgroundColorMap[y, 1] = OutlineColor;

                backgroundColorMap[y, maxIndex.X] = OutlineColor;

                backgroundColorMap[y, maxIndex.X - 1] = OutlineColor;
            }
        }

        private static void DrawCrosshair()
        {

        }

        public void DrawCall(Background graphic, Position position)
        {
            if (graphic.colorMap == null)
            {
                return;
            }

            Point cameraLocation = ConsoleObject.Transform.Location - pivot;

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

                    if (position.depth > backgroundDepthMap.Get(mapPoint))
                    {
                        continue;
                    }

                    if (graphic.colorMap[y, x] == 0)
                    {
                        continue;
                    }

                    backgroundDepthMap.Set(mapPoint, position.depth);

                    backgroundColorMap.Set(mapPoint, graphic.colorMap[y, x]);

                    if (position.depth > foregroundDepthMap.Get(mapPoint))
                    {
                        continue;
                    }

                    foregroundTextMap.Set(mapPoint, ' ');
                }
            }
        }

        public void DrawCall(Foreground graphic, Position position)
        {
            Point cameraLocation = ConsoleObject.Transform.Location - pivot;

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

                    if (foregroundDepthMap.Get(mapPoint) < position.depth)
                    {
                        continue;
                    }

                    foregroundDepthMap.Set(mapPoint, position.depth);

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
                bool isHalfWidth = true;

                for (int x = 0; x < size.Width; ++x)
                {
                    if (isHalfWidth == false && foregroundTextMap[y, x] == ' ')
                    {
                        isHalfWidth = true;

                        continue;
                    }

                    bufferBuilder.SetColor(backgroundColorMap[y, x], foregroundColorMap[y, x]);

                    bufferBuilder.Append(foregroundTextMap[y, x]);

                    if (foregroundTextMap[y, x].IsHalfWidth() == false)
                    {
                        isHalfWidth = false;
                    }
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