using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    internal class Program
    {
        private static KeyEventHandler keyEventHandler = new();

        private static void Main()
        {
            keyEventHandler.AddEvent(ConsoleKey.W, () => Console.Write("W"));

            while (true)
            {
                keyEventHandler.Check();

                Thread.Sleep(100);
            }
        }
    }

    public sealed class KeyEventHandler
    {
        [DllImport("user32.dll")]

        private static extern short GetAsyncKeyState(int key);

        private readonly Dictionary<ConsoleKey, Action?> events = new();

        public void AddEvent(ConsoleKey key, Action action)
        {
            if (events.ContainsKey(key) == false)
            {
                events.Add(key, null);
            }

            events[key] += action;
        }

        public void RemoveEvent(ConsoleKey key, Action action)
        {
            if (events.ContainsKey(key) == false)
            {
                return;
            }

            events[key] -= action;

            if (events[key] == null)
            {
                events.Remove(key);
            }
        }

        public void ClearEvents()
        {
            events.Clear();
        }

        public void Check()
        {
            foreach (ConsoleKey key in events.Keys)
            {
                if ((GetAsyncKeyState((int)key) & 0x8000) != 0)
                {
                    events[key]?.Invoke();
                }
            }
        }
    }
}