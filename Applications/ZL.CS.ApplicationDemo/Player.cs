using System;

using ZL.CS.ConsoleEngine;

namespace ZL.CS.ApplicationDemo
{
    internal sealed class Player : Component
    {
        private Transform? transform;

        protected override void Start()
        {
            transform = ConsoleObject.Transform;
        }

        protected override void FixedUpdate()
        {
            if (Console.KeyAvailable == true)
            {
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.W:

                        transform.Move(new Position(0, -1, 0));

                        break;

                    case ConsoleKey.S:

                        transform.Move(new Position(0, 1, 0));

                        break;

                    case ConsoleKey.A:

                        transform.Move(new Position(-1, 0, 0));

                        break;

                    case ConsoleKey.D:

                        transform.Move(new Position(1, 0, 0));

                        break;
                }
            }
        }
    }
}