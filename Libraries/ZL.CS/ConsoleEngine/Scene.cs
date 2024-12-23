using System;

using System.Collections.Generic;

using System.Threading;

using System.Threading.Tasks;

namespace ZL.CS.ConsoleEngine
{
    public abstract class Scene : BehaviourObject
    {
        private static Scene? instance = null;

        public static SceneState State { get; private set; } = SceneState.Ended;

        private static ManualResetEventSlim pauseEvent = new(true);

        private static int frameRate;

        public static int FrameRate
        {
            get => frameRate;

            set
            {
                frameRate = value;

                taskDelay = TimeSpan.FromSeconds(1.0 / frameRate);
            }
        }

        private static TimeSpan taskDelay;

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

        public static async void Load<T>()

            where T : Scene, new()
        {
            if (instance != null)
            {
                await instance.End();
            }

            instance = new T();

            instance.Run();
        }

        internal static async void Terminate()
        {
            if (instance != null)
            {
                await instance.End();
            }

            State = SceneState.Terminated;
        }

        private Task End()
        {
            State = SceneState.Ended;

            return Task.WhenAll(callFixedUpdate, callUpdate, callDraw);
        }

        private void Run()
        {
            State = SceneState.Running;

            CallStart();
        }

        protected static void Pause()
        {
            State = SceneState.Paused;

            pauseEvent.Reset();
        }

        protected void Resume()
        {
            State = SceneState.Running;

            pauseEvent.Set();
        }

        internal override void CallStart()
        {
            foreach (var consoleObject in consoleObjects)
            {
                if (consoleObject.IsEnabled == false)
                {
                    continue;
                }

                consoleObject.CallStart();
            }

            callFixedUpdate = Task.Run(CallFixedUpdate);

            callUpdate = Task.Run(CallUpdate);

            callDraw = Task.Run(CallDraw);
        }

        private static Task callFixedUpdate;

        internal override async void CallFixedUpdate()
        {
            while (State != SceneState.Ended)
            {
                pauseEvent.Wait();

                foreach (var consoleObject in consoleObjects)
                {
                    if (consoleObject.IsEnabled == false)
                    {
                        continue;
                    }

                    consoleObject.CallFixedUpdate();
                }

                await Task.Delay(taskDelay);
            }
        }

        private static Task callUpdate;

        internal override void CallUpdate()
        {
            while (State != SceneState.Ended)
            {
                pauseEvent.Wait();

                foreach (var consoleObject in consoleObjects)
                {
                    if (consoleObject.IsEnabled == false)
                    {
                        continue;
                    }

                    consoleObject.CallUpdate();
                }

                CallLateUpdate();
            }
        }

        internal override void CallLateUpdate()
        {
            foreach (var consoleObject in consoleObjects)
            {
                if (consoleObject.IsEnabled == false)
                {
                    continue;
                }

                consoleObject.CallLateUpdate();
            }
        }

        private static Task callDraw;

        internal override async void CallDraw()
        {
            while (State != SceneState.Ended)
            {
                pauseEvent.Wait();

                Camera.Main?.Clear();

                foreach (var consoleObject in consoleObjects)
                {
                    if (consoleObject.IsEnabled == false)
                    {
                        continue;
                    }

                    consoleObject.CallDraw();
                }

                Camera.Main?.Render();

                await Task.Delay(taskDelay);
            }
        }
    }
}