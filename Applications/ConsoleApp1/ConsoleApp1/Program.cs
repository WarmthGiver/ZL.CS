using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    internal class Program
    {
        private static readonly KeyEventHandler keyEventHandler = new();

        private static void Main()
        {
            var spf = TimeSpan.FromSeconds(1.0 / 60);

            keyEventHandler.Add(ConsoleKey.W, () => Console.Write("W"));

            while (true)
            {
                keyEventHandler.Check();

                Thread.Sleep(spf);
            }
        }
    }

    public sealed class KeyEventHandler
    {
        [DllImport("user32.dll")]

        private static extern short GetAsyncKeyState(int key);

        private readonly Dictionary<ConsoleKey, Action?> actions = new();

        public void Add(ConsoleKey key, Action action)
        {
            if (actions.ContainsKey(key) == false)
            {
                actions.Add(key, null);
            }

            actions[key] += action;
        }

        public void Remove(ConsoleKey key, Action action)
        {
            if (actions.ContainsKey(key) == false)
            {
                return;
            }

            actions[key] -= action;

            if (actions[key] == null)
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
                if (key.IsPressed() == true)
                {
                    actions[key]?.Invoke();
                }
            }
        }
    }

    public static class Input
    {
        [DllImport("user32.dll")]

        private static extern short GetAsyncKeyState(int key);

        public static bool IsPressed(this ConsoleKey key)
        {
            return (GetAsyncKeyState((int)key) & 0x8000) != 0;
        }

        public static bool GetKeyUp(ConsoleKey key)
        {
            return (GetAsyncKeyState((int)key) & 0x0001) != 0;
        }
    }
}