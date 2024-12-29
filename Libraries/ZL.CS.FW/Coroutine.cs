using System.Collections;

using System.Collections.Generic;

namespace ZL.CS.FW
{
    public sealed class Coroutine
    {
        private static Dictionary<IEnumerator, IEnumerator> routines = new();

        private static Stack<IEnumerator> finishedRoutines = new();

        internal void Update()
        {
            while (true)
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
            routines.Add(routine, Routine(routine));
        }

        private IEnumerator Routine(IEnumerator routine)
        {
            while (routine.MoveNext() == true)
            {
                yield return null;
            }

            finishedRoutines.Push(routine);
        }
    }
}