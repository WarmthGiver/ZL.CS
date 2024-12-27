using System.Collections.Concurrent;

namespace ZL.CS.ObjectPooling
{
    public abstract class Pool<T>

        where T : class
    {
        private readonly ConcurrentBag<T> stock = new();

        public void PreGenerate(int count)
        {
            while (--count >= 0)
            {
                stock.Add(Clone());
            }
        }

        public virtual T Generate()
        {
            if (stock.TryTake(out T? stopwatch))
            {
                return stopwatch;
            }

            return Clone();
        }

        public abstract T Clone();

        public void Collect(T stopwatch)
        {
            stock.Add(stopwatch);
        }
    }
}