using System;

using System.Drawing;

using System.Numerics;

namespace ZL.CS.FW
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

        private float[,] backgroundDepthMap;

        private float[,] foregroundDepthMap;

        private byte[,] backgroundColorMap;
        
        private byte[,] foregroundColorMap;

        private Char2[,] foregroundCharMap;

        private readonly ANSI.Builder bufferBuilder = new();

        public Camera()
        {
            container.AddRectTransform();
        }

        protected override void Start()
        {
            size = FixedConsole.GetWindowSize();

            container.RectTransform.Size = size;

            backgroundDepthMap = new float[size.Height, size.Width];

            foregroundDepthMap = new float[size.Height, size.Width];

            backgroundColorMap = new byte[size.Height, size.Width];

            foregroundColorMap = new byte[size.Height, size.Width];

            foregroundCharMap = new Char2[size.Height, size.Width];

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

                foregroundCharMap.Fill(Char2.Blank);
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

        private void DrawOutline()
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

        internal void Draw(Graphic graphic, Vector3 position)
        {
            RectangleF cameraRect = container.RectTransform.Rect;

            PointF cameraLocation = cameraRect.Location;

            PointF graphicLocation = position.ToPointF() - graphic.pivot;

            Rectangle graphicRect = graphic.GetCulledRect(graphicLocation, cameraRect);

            Point mapPoint = new();

            Point mapLocation = graphicLocation.Sub(cameraLocation).Round();

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

                        foregroundCharMap.Set(mapPoint, Char2.Blank);
                    }
                }
            }

            else if (graphic is Foreground foreground)
            {
                for (int y = graphicRect.Y; y < graphicRect.Height; ++y)
                {
                    mapPoint.Y = mapLocation.Y + y;

                    for (int x = graphicRect.X; x < graphicRect.Width; ++x)
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

                        foregroundCharMap.Set(mapPoint, foreground.char2Map[y, x]);
                    }
                }
            }
        }

        internal void Draw(GUI gui, Vector3 position)
        {

        }

        internal void Render()
        {
            for (int y = 0; ;)
            {
                for (int x = 0; x < size.Width; ++x)
                {
                    bufferBuilder.SetColor(backgroundColorMap[y, x], foregroundColorMap[y, x]);

                    bufferBuilder.Append(foregroundCharMap[y, x].ToString());
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

        public override void Dispose()
        {
            //Clear();
        }
    }
}