using System;
using System.Collections.Generic;

using System.Drawing;

using System.Threading;
using System.Threading.Tasks;
using ZL.CS.Graphics;

namespace ZL.CS.ConsoleEngine
{
    public abstract class Scene
    {
        private int frameRate;

        public int FrameRate
        {
            get => frameRate;

            set
            {
                frameRate = value;

                threadSleepTime = 1000 / frameRate;
            }
        }

        private int threadSleepTime;

        protected readonly Size consoleSize;

        protected readonly Rectangle rect;

        //protected readonly Point pivot;

        protected readonly Canvas mainCanvas;

        protected readonly Canvas[] subCanvases;

        private readonly List<ConsoleObject> consoleObjects = new();

        protected Scene(int framesRate, Size consoleSize, int subCanvasesCount) : this(framesRate, consoleSize, new Rectangle(new(0, 0), consoleSize), subCanvasesCount) { }

        protected Scene(int framesRate, Size consoleSize, Rectangle rect, int subCanvasesCount)
        {
            FrameRate = framesRate;

            this.consoleSize = consoleSize;

            this.rect = rect;

            //pivot = size.GetPivot();

            mainCanvas = new Canvas(rect);

            subCanvases = new Canvas[subCanvasesCount];
        }

        protected ConsoleObject CreateConsoleObject(string name, Point position, byte depth)
        {
            return CreateConsoleObject(name, mainCanvas, position, depth);
        }

        protected ConsoleObject CreateConsoleObject(string name, Canvas canvas, Point position, byte depth, params Component[] components)
        {
            ConsoleObject consoleObject = new ConsoleObject(name, canvas, position, depth);

            consoleObjects.Add(consoleObject);

            return consoleObject;
        }

        public void Start()
        {
            Fixed.Console.SetWindowSize(consoleSize);

            fixedUpdate = Task.Run(FixedUpdate);

            update = Task.Run(Update);

            draw = Task.Run(Draw);
        }

        public void End()
        {

        }

        private Task fixedUpdate;

        private void FixedUpdate()
        {
            while (true)
            {
                foreach (var consoleObject in consoleObjects)
                {
                    consoleObject.TryFixedUpdate();
                }

                Thread.Sleep(threadSleepTime);
            }
        }

        private Task update;

        private void Update()
        {
            while (true)
            {
                foreach (var consoleObject in consoleObjects)
                {
                    consoleObject.TryUpdate();
                }
            }
        }

        private Task draw;

        private void Draw()
        {
            while (true)
            {
                foreach (var subCanvas in subCanvases)
                {
                    subCanvas.Draw();
                }

                mainCanvas.Draw();

                Thread.Sleep(threadSleepTime);
            }
        }
    }
}