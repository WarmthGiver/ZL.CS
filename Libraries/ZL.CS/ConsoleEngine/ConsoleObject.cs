using System.Collections.Generic;

namespace ZL.CS.ConsoleEngine
{
    public sealed class ConsoleObject : BehaviourObject
    {
        public readonly string name;

        public RectTransform? RectTransform { get; private set; }

        public Transform Transform { get; private set; }

        private readonly List<Component> components = new();

        public ConsoleObject(string name, Position position, Transform? parent)
        {
            this.name = name;

            Transform = new(position, parent);
        }

        public RectTransform AddRectTransform()
        {
            if (RectTransform == null)
            {
                RectTransform = new RectTransform(Transform);

                Transform = RectTransform;
            }

            return RectTransform;
        }

        public T Add<T>()

            where T : Component, new()
        {
            var component = Component.Instantiate<T>(this);

            Add(component);

            return component;
        }

        public void Add(Component component)
        {
            components.Add(component);
        }

        internal override void Start()
        {
            foreach (Component component in components)
            {
                if (component.IsEnabled == false)
                {
                    continue;
                }

                component.Start();
            }
        }

        internal override void FixedUpdate()
        {
            foreach (Component component in components)
            {
                if (component.IsEnabled == false)
                {
                    continue;
                }

                component.FixedUpdate();
            }
        }

        internal override void Update()
        {
            foreach (Component component in components)
            {
                if (component.IsEnabled == false)
                {
                    continue;
                }

                component.Update();
            }
        }

        internal override void LateUpdate()
        {
            foreach (Component component in components)
            {
                if (component.IsEnabled == false)
                {
                    continue;
                }

                component.LateUpdate();
            }
        }

        internal override void DrawCall()
        {
            foreach (Component component in components)
            {
                if (component.IsEnabled == false)
                {
                    continue;
                }

                component.DrawCall();
            }
        }
    }
}