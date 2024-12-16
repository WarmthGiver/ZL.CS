using System.Collections.Generic;

namespace ZL.CS.ConsoleEngine
{
    public sealed class ConsoleObject : BehaviourObject
    {
        public readonly string name;

        public RectTransform? RectTransform { get; private set; }

        public Transform Transform { get; private set; }

        private readonly List<Component> components = new();

        public ConsoleObject(string name)
        {
            this.name = name;

            Transform = new();
        }

        public RectTransform AddRectTransform()
        {
            if (RectTransform == null)
            {
                RectTransform = Component.Instantiate<RectTransform>(this);

                RectTransform.Set(Transform);

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

        public override void Start()
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

        public override void FixedUpdate()
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

        public override void Update()
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

        public override void LateUpdate()
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

        public override void DrawCall()
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