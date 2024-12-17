using System;

using System.Collections.Generic;

using System.Drawing;

using System.Threading.Tasks;

using ZL.CS.Graphics;

namespace ZL.CS.ConsoleEngine
{
    public abstract class Scene : BehaviourObject
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
        }

        protected ConsoleObject CreateConsoleObject(string name)
        {
            ConsoleObject consoleObject = new(name);

            consoleObjects.Add(consoleObject);

            return consoleObject;
        }

        public override void Start()
        {
            foreach (var consoleObject in consoleObjects)
            {
                if (consoleObject.IsEnabled == true)
                {
                    consoleObject.Start();
                }
            }

            FixedConsole.SetWindowSize(size);

            fixedUpdate = Task.Run(FixedUpdate);

            update = Task.Run(Update);

            drawCall = Task.Run(DrawCall);
        }

        public void End()
        {

        }

        private Task fixedUpdate;

        public override async void FixedUpdate()
        {
            while (true)
            {
                foreach (var consoleObject in consoleObjects)
                {
                    if (consoleObject.IsEnabled == false)
                    {
                        continue;
                    }

                    consoleObject.FixedUpdate();
                }

                await Task.Delay(threadDelay);
            }
        }

        private Task update;

        public override void Update()
        {
            while (true)
            {
                foreach (var consoleObject in consoleObjects)
                {
                    if (consoleObject.IsEnabled == false)
                    {
                        continue;
                    }

                    consoleObject.Update();
                }

                LateUpdate();
            }
        }

        public override void LateUpdate()
        {
            foreach (var consoleObject in consoleObjects)
            {
                if (consoleObject.IsEnabled == false)
                {
                    continue;
                }

                consoleObject.LateUpdate();
            }
        }

        private Task drawCall;

        public override async void DrawCall()
        {
            while (true)
            {
                Camera.Main?.Clear();

                foreach (var consoleObject in consoleObjects)
                {
                    if (consoleObject.IsEnabled == false)
                    {
                        continue;
                    }

                    consoleObject.DrawCall();
                }

                Camera.Main?.Render();

                await Task.Delay(threadDelay);
            }
        }
    }
}