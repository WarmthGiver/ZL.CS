using System;

using System.Numerics;

namespace ZL.CS.API.ApplicationDemo
{
    internal sealed class Player : Component
    {
        private Transform transform;

        private KeyEventHandler keyEventHandler = new();

        private Vector3 position;

        private double speed = 10f;

        protected override void Start()
        {
            transform = Container.Transform;

            keyEventHandler.Add(ConsoleKey.W, () => position.Y -= (float)speed * (float)Time.DeltaTime);

            keyEventHandler.Add(ConsoleKey.S, () => position.Y += (float)speed * (float)Time.DeltaTime);

            keyEventHandler.Add(ConsoleKey.A, () => position.X -= (float)speed * (float)Time.DeltaTime);

            keyEventHandler.Add(ConsoleKey.D, () => position.X += (float)speed * (float)Time.DeltaTime);
        }

        protected override void Update()
        {
            position = transform.Position;

            keyEventHandler.Invoke();

            transform.Position = position;
        }
    }
}