using System;

using System.Numerics;

namespace ZL.CS.FW.Demo
{
    internal sealed class Player : Component
    {
        private Transform transform;

        private InputActionHandler inputActionHandler = new();

        private Vector3 position;

        private float speed = 10f;

        protected override void Start()
        {
            transform = container.Transform;

            inputActionHandler.Add(ConsoleKey.W, () => position.Y -= speed * Time.DeltaTime);

            inputActionHandler.Add(ConsoleKey.S, () => position.Y += speed * Time.DeltaTime);

            inputActionHandler.Add(ConsoleKey.A, () => position.X -= speed * Time.DeltaTime);

            inputActionHandler.Add(ConsoleKey.D, () => position.X += speed * Time.DeltaTime);

            inputActionHandler.Add(ConsoleKey.D1, () => Scene.Load<DemoScene1>());

            inputActionHandler.Add(ConsoleKey.D2, () => Scene.Load<DemoScene2>());

            inputActionHandler.Add(ConsoleKey.D3, () => Scene.Terminate());
        }

        protected override void Update()
        {
            position = transform.Position;

            inputActionHandler.Invoke();

            transform.Position = position;
        }
    }
}