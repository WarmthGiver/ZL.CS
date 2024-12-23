namespace ZL.CS.ConsoleEngine
{
    public abstract class Component : BehaviourObject
    {
        public ConsoleObject ConsoleObject { get; private set; }

        internal static T Instantiate<T>(ConsoleObject consoleObject)
            
            where T : Component, new()
        {
            T component = new()
            {
                ConsoleObject = consoleObject
            };

            return component;
        }

        public T? GetComponent<T>()

            where T : Component
        {
            return ConsoleObject.GetComponent<T>();
        }

        internal override void CallStart() => Start();

        protected virtual void Start() { }

        internal override void CallFixedUpdate() => FixedUpdate();

        protected virtual void FixedUpdate() { }

        internal override void CallUpdate() => Update();

        protected virtual void Update() { }

        internal override void CallLateUpdate() => LateUpdate();

        protected virtual void LateUpdate() { }
    }
}