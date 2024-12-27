using System;

using System.Collections.Generic;

namespace ZL.CS.API
{
    /*public abstract class PooledObject<T> : Component

        where T : PooledObject<T>
    {
        public ObjectPool<T> Pool { get; private set; }

        public static T Clone(ObjectPool<T> pool)
        {
            var clone = Instantiate(pool.Original, pool.Parent);

            clone.Pool = pool;

            return clone;
        }

        protected virtual void OnDisable()
        {
            Pool.Collect((T)this);
        }

        public void ReturnToPool()
        {
            Container.IsEnabled = false;
        }
    }

    public class ObjectPool<T> : Pool<T>

        where T : PooledObject<T>
    {
        protected T original;

        public T Original => original;

        private Transform parent;

        public Transform Parent => parent;

        public override T Generate()
        {
            var clone = base.Generate();

            return clone;
        }

        public override T Clone()
        {
            return PooledGameObject<T>.Clone(this);
        }
    }

    [Serializable]

    public sealed class ManagedGameObjectPool<T> : ObjectPool<T>

        where T : PooledGameObject<T>
    {
        public readonly HashSet<T> clones = new();

        public override T Generate()
        {
            var clone = base.Generate();

            clones.Add(clone);

            return clone;
        }

        public void Recall()
        {
            foreach (var clone in clones)
            {
                clone.ReturnToPool();
            }
        }
    }*/
}