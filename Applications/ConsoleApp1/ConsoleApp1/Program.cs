using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    internal class Program
    {
        private AsyncKeyEventHandler inputManager = new();

        private static void Main()
        {

        }
    }

    public sealed class AsyncKeyEventHandler
    {
        private readonly Dictionary<ConsoleKey, Action?> events = new();

        [DllImport("user32.dll")]

        private static extern short GetAsyncKeyState(int key);

        public void AddEvent(ConsoleKey key, Action action)
        {
            events[key] += action;
        }

        public void RemoveEvent(ConsoleKey key, Action action)
        {
            if (events.ContainsKey(key) == true)
            {
                events[key] -= action;
            }
        }

        public void ClearEvents()
        {
            events.Clear();
        }
    }
}