using System;

using System.Numerics;

namespace ZL.CS.FW.Demo
{
    // Component 클래스를 상속받기만 하면 생명주기에 의해 스스로 작동함.
    internal sealed class Player : Component
    {
        // 컴포넌트가 가질 변수 선언.

        private Transform transform;

        private InputActionHandler inputActionHandler = new();

        private Vector3 position;

        private float speed = 10f;

        // 생명주기에 의해 자동으로 실행되며, 단 한번만 실행됨.
        protected override void Start()
        {
            // 컴포넌트 초기화.
            
            transform = container.Transform;

            inputActionHandler.Add(ConsoleKey.W, () => position.Y -= speed * Time.DeltaTime);

            inputActionHandler.Add(ConsoleKey.S, () => position.Y += speed * Time.DeltaTime);

            inputActionHandler.Add(ConsoleKey.A, () => position.X -= speed * Time.DeltaTime);

            inputActionHandler.Add(ConsoleKey.D, () => position.X += speed * Time.DeltaTime);

            inputActionHandler.Add(ConsoleKey.D1, () => Scene.Load<DemoScene1>());

            inputActionHandler.Add(ConsoleKey.D2, () => Scene.Load<DemoScene2>());

            inputActionHandler.Add(ConsoleKey.D3, () => Scene.Terminate());
        }

        // 생명주기에 의해 매 프레임 호출됨.
        protected override void Update()
        {
            position = transform.Position;

            inputActionHandler.Invoke();

            transform.Position = position;
        }
    }
}