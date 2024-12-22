using System;

using System.Collections.Generic;

using System.Drawing;

using System.Threading.Tasks;

namespace ZL.CS.ConsoleEngine
{
    public abstract class Scene : BehaviourObject
    {
        private static Scene? instance = null;

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

        private readonly List<ConsoleObject> consoleObjects = new();

        protected ConsoleObject CreateConsoleObject(string name, Transform? parent = null)
        {
            return CreateConsoleObject(name, new(), parent);
        }

        protected ConsoleObject CreateConsoleObject(string name, Position position, Transform? parent = null)
        {
            ConsoleObject consoleObject = new(name, position, parent);

            consoleObjects.Add(consoleObject);

            return consoleObject;
        }

        internal void Load()
        {
            instance?.End();

            instance = this;

            instance.Start();
        }

        internal override void Start()
        {
            FrameRate = 60;

            foreach (var consoleObject in consoleObjects)
            {
                if (consoleObject.IsEnabled == true)
                {
                    consoleObject.Start();
                }
            }

            fixedUpdate = Task.Run(FixedUpdate);

            update = Task.Run(Update);

            drawCall = Task.Run(DrawCall);
        }

        private void End()
        {

        }

        private Task fixedUpdate;

        internal override async void FixedUpdate()
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

        internal override void Update()
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

        internal override void LateUpdate()
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

        internal override async void DrawCall()
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