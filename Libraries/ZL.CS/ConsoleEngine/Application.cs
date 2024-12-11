using System;

using System.Collections;

using System.Collections.Generic;

using System.Threading;

namespace ZL.CS.ConsoleEngine
{
    public abstract class Application<TApplication>

        where TApplication : Application<TApplication>
    {
        private static State state = State.Terminated;

        private static int timeScale = 16;

        private static Dictionary<IEnumerator, IEnumerator> routines = new();

        private static Stack<IEnumerator> finishedRoutines = new();

        public void Run()
        {
            state = State.Running;

            Start();

            //Update();
        }

        public void Pause()
        {
            state = State.Paused;
        }

        public void Quit()
        {
            state = State.Terminated;
        }

        protected abstract void Start();

        protected void Update()
        {
            while (state != State.Terminated)
            {
                if (state == State.Running)
                {
                    if (routines.Count > 0)
                    {
                        foreach (var routine in routines.Values)
                        {
                            routine.MoveNext();
                        }

                        RemoveFinisedRoutines();
                    }
                }

                Thread.Sleep(timeScale);
            }

            Terminate();
        }

        private void RemoveFinisedRoutines()
        {
            foreach (IEnumerator routine in finishedRoutines)
            {
                routines.Remove(routine);
            }

            finishedRoutines.Clear();
        }

        public void StartRoutine(IEnumerator routine)
        {
            var _routnie = Routine(routine);

            routines.Add(routine, _routnie);
        }

        private IEnumerator Routine(IEnumerator routine)
        {
            while (routine.MoveNext())
            {
                yield return null;
            }

            finishedRoutines.Push(routine);
        }

        protected virtual void Terminate() { }

        protected enum State
        {
            Terminated,

            Running,

            Paused,
        }
    }
}