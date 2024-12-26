using System;

using System.Collections.Generic;

using System.Diagnostics;

using System.Numerics;

using System.Threading;

using System.Threading.Tasks;

namespace ZL.CS.ConsoleEngine
{
    public abstract class Scene : BehaviourObject
    {
        private static Scene? instance = null;

        public static SceneState State { get; private set; } = SceneState.Terminated;

        private static readonly ManualResetEventSlim pauseEvent = new(true);

        private static int fps;

        public static int FPS
        {
            get => fps;

            set
            {
                fps = value;

                spf = 1.0 / fps;
            }
        }

        private static double spf;

        public static double DeltaTime { get; private set; }

        private static double fixedDeltaTime;

        public static double FixedDeltaTime
        {
            get => fixedDeltaTime;

            set
            {
                fixedDeltaTime = value;

                fixedDelayTime = TimeSpan.FromSeconds(value);
            }
        }

        private static TimeSpan fixedDelayTime;

        private readonly LinkedList<ConsoleObject> consoleObjects = new();

        protected ConsoleObject CreateConsoleObject(string name, Transform? parent = null)
        {
            return CreateConsoleObject(name, new(), parent);
        }

        protected ConsoleObject CreateConsoleObject(string name, Vector3 position, Transform? parent = null)
        {
            ConsoleObject consoleObject = new(name, position, parent);

            consoleObjects.AddLast(consoleObject);

            return consoleObject;
        }

        internal static async void Terminate()
        {
            if (instance != null)
            {
                await Unload();
            }

            State = SceneState.Terminated;
        }

        public static async void Load<T>()

            where T : Scene, new()
        {
            if (instance != null)
            {
                await Unload();
            }

            State = SceneState.Loading;

            instance = new T();

            instance.Run();
        }

        private static Task Unload()
        {
            State = SceneState.Unloading;

            return Task.WhenAll(callFixedUpdate, callUpdate);
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
            while (State != SceneState.Unloading)
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

                await Task.Delay(fixedDelayTime);
            }
        }

        private static Task callUpdate;

        internal override async void CallUpdate()
        {
            Stopwatch stopwatch = new();

            while (State != SceneState.Unloading)
            {
                pauseEvent.Wait();

                stopwatch.Restart();

                foreach (var consoleObject in consoleObjects)
                {
                    if (consoleObject.IsEnabled == false)
                    {
                        continue;
                    }

                    consoleObject.CallUpdate();
                }

                CallLateUpdate();

                stopwatch.Stop();

                DeltaTime = stopwatch.Elapsed.TotalSeconds;

                if (DeltaTime < spf)
                {
                    DeltaTime = spf - DeltaTime;

                    await Task.Delay(TimeSpan.FromSeconds(DeltaTime));
                }
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
            while (State != SceneState.Unloading)
            {
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

                await Task.Delay(TimeSpan.FromSeconds(spf));
            }
        }
    }

    public enum SceneState
    {
        Terminated,

        Loading,

        Unloading,

        Running,

        Paused,
    }
}