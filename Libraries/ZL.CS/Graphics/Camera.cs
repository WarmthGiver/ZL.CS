using System;

using System.Drawing;

using ZL.CS.ConsoleEngine;

namespace ZL.CS.Graphics
{
    public sealed class Camera : Component
    {
        public static Camera? main { get; set; } = null;

        private readonly RectTransform rectTransform;

        public int Depth { get; set; } = int.MaxValue;

        private int[,] depthMap;

        private byte[,] backgroundColorMap;

        private byte[,] foregroundColorMap;

        private char[,] foregroundTextMap;

        private readonly ANSI.BufferBuilder bufferBuilder = new();

        public Camera(SceneObject sceneObject, Size size) : base(sceneObject)
        {
            rectTransform = sceneObject.AddRectTransform();

            Resize(size);
        }

        public void Resize(Size size)
        {
            rectTransform.Size = size;

            var rect = rectTransform.Rect;

            depthMap = new int[rect.Height, rect.Width];

            backgroundColorMap = new byte[rect.Height, rect.Width];

            foregroundColorMap = new byte[rect.Height, rect.Width];

            foregroundTextMap = new char[rect.Height, rect.Width];

            Clear();
        }

        public void Clear()
        {
            depthMap.Fill(Depth);

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

            position.location += rectTransform.Pivot;

            position.location -= graphic.pivot;

            Rectangle graphicRect = graphic.rect.Culling(rectTransform.Rect, position.location);

            Point point = new();

            for (int y = graphicRect.Y; y < graphicRect.Height; ++y)
            {
                point.Y = position.location.Y + y;

                for (int x = graphicRect.X; x < graphicRect.Width; ++x)
                {
                    point.X = position.location.X + x;

                    if (depthMap.Get(point) < position.depth)
                    {
                        continue;
                    }

                    if (graphic.colorMap[y, x] == 0)
                    {
                        continue;
                    }

                    depthMap.Set(point, position.depth);

                    backgroundColorMap.Set(point, graphic.colorMap[y, x]);

                    foregroundTextMap.Set(point, ' ');
                }
            }
        }

        public void DrawRequest(Foreground graphic, Position position)
        {
            position.location += rectTransform.Pivot;

            position.location -= graphic.pivot;

            Rectangle graphicRect = graphic.rect.Culling(rectTransform.Rect, position.location);

            Point point = new();

            for (int y = graphicRect.Y; y < graphicRect.Height; ++y)
            {
                point.Y = position.location.Y + y;

                for (int x = graphicRect.X; x < graphicRect.Width; ++x)
                {
                    point.X = position.location.X + x;

                    if (depthMap.Get(point) < position.depth)
                    {
                        continue;
                    }

                    depthMap.Set(point, position.depth);

                    if (graphic.colorMap != null)
                    {
                        foregroundColorMap.Set(point, graphic.colorMap[y, x]);
                    }

                    foregroundTextMap.Set(point, graphic.textMap[y, x]);
                }
            }
        }

        public void Draw()
        {
            for (int y = 0; ;)
            {
                for (int x = 0; x < rectTransform.Rect.Width; ++x)
                {
                    bufferBuilder.SetColor(backgroundColorMap[y, x], foregroundColorMap[y, x]);

                    bufferBuilder.Append(foregroundTextMap[y, x]);
                }

                if (++y >= rectTransform.Rect.Height)
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