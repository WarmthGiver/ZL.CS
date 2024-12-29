using System;

using System.Collections.Generic;

using System.Diagnostics;

using System.Numerics;

using System.Threading;

using System.Threading.Tasks;

using ZL.CS.ObjectPooling;

namespace ZL.CS.FW
{
    public abstract class Scene : BehaviourObject
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

        public static async void Terminate()
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
            if (State == SceneState.Loading || State == SceneState.Unloading)
            {
                return;
            }

            if (instance != null)
            {
                await Unload();
            }
            
            State = SceneState.Loading;

            instance = new T();

            instance.Run();
        }

        private static async Task Unload()
        {
            State = SceneState.Unloading;

            await Task.WhenAll(fixedUpdate, update);

            instance.Dispose();
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

            CallFixedUpdate();

            CallUpdate();
        }

        internal override void CallFixedUpdate()
        {
            fixedUpdate = Task.Run(FixedUpdate);
        }

        private static Task? fixedUpdate = null;

        private async Task FixedUpdate()
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

        internal override void CallUpdate()
        {
            update = Task.Run(Update);
        }

        private static Task? update = null;

        private async Task Update()
        {
            var stopwatch = ClassPool<Stopwatch>.Clone();

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

                CallDraw();

                Console.SetCursorPosition(0, 0);

                Console.Write($"Delta Time: {Time.DeltaTime}");

                Console.SetCursorPosition(0, 1);

                Console.Write($"{(1.0 / Time.DeltaTime).ToString($"F{1}")}fps");

                stopwatch.Stop();

                Time.DeltaTime = (float)stopwatch.Elapsed.TotalSeconds;

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

        internal override void CallDraw()
        {
            Camera.Main.Clear();

            foreach (var consoleObject in consoleObjects)
            {
                if (consoleObject.IsEnabled == false)
                {
                    continue;
                }

                consoleObject.CallDraw();
            }

            Camera.Main.Render();
        }

        public override void Dispose()
        {
            foreach (var consoleObject in consoleObjects)
            {
                consoleObject.Dispose();
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