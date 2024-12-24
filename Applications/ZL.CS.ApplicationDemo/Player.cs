using System;

using ZL.CS.ConsoleEngine;

namespace ZL.CS.ApplicationDemo
{
    internal sealed class Player : Component
    {
        private Transform? transform;

        private KeyEventHandler keyEventHandler = new();

        protected override void Start()
        {
            transform = Container.Transform;

            keyEventHandler.Add(ConsoleKey.W, () => transform.Move(new Position(0, -1, 0)));

            keyEventHandler.Add(ConsoleKey.S, () => transform.Move(new Position(0, 1, 0)));

            keyEventHandler.Add(ConsoleKey.A, () => transform.Move(new Position(-1, 0, 0)));

            keyEventHandler.Add(ConsoleKey.D, () => transform.Move(new Position(1, 0, 0)));
        }

        protected override void FixedUpdate()
        {
            keyEventHandler.Check();
        }
    }
}