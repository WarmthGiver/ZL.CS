using System;

using System.Collections.Generic;

using System.Runtime.InteropServices;

namespace ZL.CS
{
    public class Trigger
    {
        protected bool isReloaded = false;

        public void Reload()
        {
            isReloaded = true;
        }

        public virtual bool Fire()
        {
            if (isReloaded == true)
            {
                isReloaded = false;

                return true;
            }

            return false;
        }
    }

    public sealed class ActionTrigger : Trigger
    {
        public Action? onFire = null;

        public static ActionTrigger operator +(ActionTrigger left, Action rigth)
        {
            left.onFire += rigth;

            return left;
        }

        public static ActionTrigger operator -(ActionTrigger left, Action rigth)
        {
            left.onFire -= rigth;

            return left;
        }


        public override bool Fire()
        {
            onFire?.Invoke();

            return false;
        }
    }

    public sealed class KeyEventHandler
    {
        private readonly Dictionary<ConsoleKey, Action?> actions = new();

        public void Add(ConsoleKey key, Action action)
        {
            if (actions.ContainsKey(key) == false)
            {
                actions.Add(key, null);
            }

            actions[key] += action;
        }

        public void Remove(ConsoleKey key, ActionTrigger action)
        {
            if (actions.ContainsKey(key) == true)
            {
                actions.Remove(key);
            }
        }

        public void Clear()
        {
            actions.Clear();
        }

        public void Check()
        {
            foreach (var key in actions.Keys)
            {
                if (Input.GetKeyDown(key) == true)
                {
                    actions[key].Fire();
                }

                if (Input.GetKeyUp(key) == true)
                {
                    actions[key].Reload();
                }
            }
        }
    }

    public static class Input
    {
        [DllImport("user32.dll")]

        private static extern short GetAsyncKeyState(int key);

        public static bool GetKeyDown(ConsoleKey key)
        {
            return (GetAsyncKeyState((int)key) & 0x8000) != 0;
        }

        public static bool GetKeyUp(ConsoleKey key)
        {
            return (GetAsyncKeyState((int)key) & 0x0001) != 0;
        }
    }
}