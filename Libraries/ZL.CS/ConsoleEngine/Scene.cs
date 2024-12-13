using System;

using System.Collections.Generic;

using System.Drawing;

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

                threadDelay = TimeSpan.FromSeconds(1.0 / frameRate);
            }
        }

        private TimeSpan threadDelay;

        protected readonly Size size;

        protected readonly List<Camera> cameras = new();

        private readonly List<ConsoleObject> consoleObjects = new();

        protected Scene(int framesRate, Size size)
        {
            FrameRate = framesRate;

            this.size = size;

            var consoleObject = CreateConsoleObject("Main Camera");

            var camera = consoleObject.Add<Camera>();

            camera.Size = size;
        }

        protected Camera CreateCamera(string name, Size size)
        {
            var consoleObject = CreateConsoleObject(name);

            var camera = consoleObject.Add<Camera>();

            camera.Size = size;

            return camera;
        }

        protected ConsoleObject CreateConsoleObject(string name)
        {
            ConsoleObject consoleObject = new(name);

            consoleObjects.Add(consoleObject);

            return consoleObject;
        }

        public virtual void Start()
        {
            Fixed.Console.SetWindowSize(size);

            fixedUpdate = Task.Run(FixedUpdate);

            update = Task.Run(Update);

            draw = Task.Run(Draw);
        }

        public void End()
        {

        }

        private Task fixedUpdate;

        private async void FixedUpdate()
        {
            while (true)
            {
                foreach (var consoleObject in consoleObjects)
                {
                    consoleObject.TryFixedUpdate();
                }

                await Task.Delay(threadDelay);
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

        private async void Draw()
        {
            while (true)
            {
                foreach (var subCanvas in cameras)
                {
                    subCanvas.Render();
                }

                Camera.main?.Render();

                await Task.Delay(threadDelay);
            }
        }
    }
}