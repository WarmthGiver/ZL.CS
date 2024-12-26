using System;

using System.Numerics;

using ZL.CS.ConsoleEngine;

namespace ZL.CS.ApplicationDemo
{
    internal sealed class Player : Component
    {
        private Transform? transform;

        private KeyEventHandler keyEventHandler = new();

        private Vector3 position;

        private float speed = 3f;

        protected override void Start()
        {
            transform = Container.Transform;

            keyEventHandler.Add(ConsoleKey.W, () => position.Y -= speed * (float)Scene.DeltaTime);

            keyEventHandler.Add(ConsoleKey.S, () => position.Y += speed * (float)Scene.DeltaTime);

            keyEventHandler.Add(ConsoleKey.A, () => position.X -= speed * (float)Scene.DeltaTime);

            keyEventHandler.Add(ConsoleKey.D, () => position.X += speed * (float)Scene.DeltaTime);
        }

        protected override void Update()
        {
            position = transform.Position;

            keyEventHandler.Check();

            transform.Position = position;
        }
    }
}