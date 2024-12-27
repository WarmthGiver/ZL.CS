using System;

using System.Collections.Generic;

using System.Diagnostics;

using System.Numerics;

using System.Threading;

using System.Threading.Tasks;

using ZL.CS.ObjectPooling;

namespace ZL.CS.API
{
    public abstract class Scene : BehaviourObject, IDisposable
    {
        private static Scene? instance = null;

        public static SceneState State { get; private set; } = SceneState.Terminated;

        private static readonly ManualResetEventSlim pauseEvent = new(true);

        private readonly LinkedList<ConsoleObject> consoleObjects = new();

        private readonly int width;

        private readonly int height;

        protected Scene(int width, int height)
        {
            this.width = width;

            this.height = height;
        }

        protected T CreateConsoleObject<T>(string name, Transform? parent = null)

            where T : Component, new()
        {
            return CreateConsoleObject(name, new(), parent).AddComponent<T>();
        }

        protected T CreateConsoleObject<T>(string name, Vector3 position, Transform? parent = null)

            where T : Component, new()
        {
            return CreateConsoleObject(name, position, parent).AddComponent<T>();
        }

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

        public void Dispose()
        {

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

        protected static void Resume()
        {
            State = SceneState.Running;

            pauseEvent.Set();
        }

        internal override void CallStart()
        {
            FixedConsole.SetWindowSize(width, height);

            FixedConsole.SetBufferSize(width, height);

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
        }

        private static Task? callFixedUpdate = null;

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

                await Task.Delay(Time.FixedDelayTime);
            }
        }

        private static Task? callUpdate = null;

        internal override async void CallUpdate()
        {
            Stopwatch stopwatch = ClassPool<Stopwatch>.Clone();

            while (State != SceneState.Unloading)
            {
                pauseEvent.Wait();

                stopwatch.Restart();

                //await Task.Delay(new Random().Next(0, 101));

                foreach (var consoleObject in consoleObjects)
                {
                    if (consoleObject.IsEnabled == false)
                    {
                        continue;
                    }

                    consoleObject.CallUpdate();
                }

                CallLateUpdate();

                if (callDraw != null)
                {
                    await callDraw;
                }
                
                callDraw = Task.Run(CallDraw);

                stopwatch.Stop();

                Time.DeltaTime = stopwatch.Elapsed.TotalSeconds;

                if (Time.DeltaTime < Time.MinDeltaTime)
                {
                    var deltaTime = Time.DeltaTime;

                    Time.DeltaTime = Time.MinDeltaTime;

                    await Task.Delay(TimeSpan.FromSeconds(Time.MinDeltaTime - deltaTime));
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

        private static Task? callDraw = null;

        internal override void CallDraw()
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

            callDraw = null;
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