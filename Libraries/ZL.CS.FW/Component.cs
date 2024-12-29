namespace ZL.CS.FW
{
    public abstract class Component : BehaviourObject
    {
        public static ConsoleObject? Container = null;

        public readonly ConsoleObject container;

        public Component()
        {
            container = Container;

            Container = null;
        }

        public T AddComponent<T>()

            where T : Component, new()
        {
            return container.AddComponent<T>();
        }

        public T? GetComponent<T>()

            where T : Component
        {
            return container.GetComponent<T>();
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