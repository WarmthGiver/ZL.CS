using System;

using System.Drawing;

using System.Numerics;

using ZL.CS.Graphics;

namespace ZL.CS.ConsoleEngine
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

        private static float[,] backgroundDepthMap;

        private static float[,] foregroundDepthMap;

        private static byte[,] backgroundColorMap;

        private static byte[,] foregroundColorMap;

        private static FixedChar[,] foregroundTextMap;

        private static readonly ANSI.BufferBuilder bufferBuilder = new();

        protected override void Start()
        {
            size = FixedConsole.GetWindowSize();

            pivot = size.GetPivot();

            backgroundDepthMap = new float[size.Height, size.Width];

            foregroundDepthMap = new float[size.Height, size.Width];

            backgroundColorMap = new byte[size.Height, size.Width];

            foregroundColorMap = new byte[size.Height, size.Width];

            foregroundTextMap = new FixedChar[size.Height, size.Width];

            Clear();
        }

        internal void Clear()
        {
            backgroundDepthMap.Fill(MaxDepth);

            foregroundDepthMap.Fill(MaxDepth);

            if (WillClear == true)
            {
                backgroundColorMap.Fill(Background.defaultColor);

                foregroundColorMap.Fill(Foreground.defaultColor);

                foregroundTextMap.Fill(new FixedChar(' ', ' '));
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

                backgroundColorMap[y, maxIndex.X] = OutlineColor;
            }
        }

        private static void DrawCrosshair()
        {

        }

        internal void Draw<TGraphic>(TGraphic graphic, Vector3 position)

            where TGraphic : Graphic
        {
            Point cameraLocation = Container.Transform.Position.ToPoint() - pivot;

            Rectangle cameraView = new(cameraLocation, size);

            Point graphicLocation = position.ToPoint() - graphic.pivot;

            Rectangle graphicRect = graphic.GetRect(graphicLocation, cameraView);

            Point mapPoint = new();

            Point mapLocation = graphicLocation.Sub(cameraLocation);

            if (graphic is Background background)
            {
                for (int y = graphicRect.Y; y < graphicRect.Height; ++y)
                {
                    mapPoint.Y = mapLocation.Y + y;

                    for (int x = graphicRect.X; x < graphicRect.Width; ++x)
                    {
                        mapPoint.X = mapLocation.X + x;

                        if (position.Z > backgroundDepthMap.Get(mapPoint))
                        {
                            continue;
                        }

                        if (background.colorMap[y, x] == 0)
                        {
                            continue;
                        }

                        backgroundDepthMap.Set(mapPoint, position.Z);

                        backgroundColorMap.Set(mapPoint, background.colorMap[y, x]);

                        if (position.Z > foregroundDepthMap.Get(mapPoint))
                        {
                            continue;
                        }

                        foregroundTextMap.Set(mapPoint, new(' ', ' '));
                    }
                }
            }

            else if (graphic is Foreground foreground)
            {
                for (int y = graphicRect.Y; y < graphicRect.Height; ++y)
                {
                    mapPoint.Y = mapLocation.Y + y;

                    for (int x = graphicRect.X; x < graphicRect.Width && x < foreground.textMap[y].Count; ++x)
                    {
                        mapPoint.X = mapLocation.X + x;

                        if (foregroundDepthMap.Get(mapPoint) < position.Z)
                        {
                            continue;
                        }

                        foregroundDepthMap.Set(mapPoint, position.Z);

                        if (foreground.colorMap != null)
                        {
                            foregroundColorMap.Set(mapPoint, foreground.colorMap[y, x]);
                        }

                        foregroundTextMap.Set(mapPoint, foreground.textMap[y][x]);
                    }
                }
            }
        }

        internal void Render()
        {
            for (int y = 0; ;)
            {
                for (int x = 0; x < size.Width; ++x)
                {
                    bufferBuilder.SetColor(backgroundColorMap[y, x], foregroundColorMap[y, x]);

                    bufferBuilder.Append(foregroundTextMap[y, x].ToString());
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