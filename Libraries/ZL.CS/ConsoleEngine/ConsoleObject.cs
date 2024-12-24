using System.Collections.Generic;

namespace ZL.CS.ConsoleEngine
{
    public sealed class ConsoleObject : BehaviourObject
    {
        public readonly string name;

        //public RectTransform? RectTransform { get; private set; }

        public Transform Transform { get; private set; }

        private readonly List<Component> components = new();

        public ConsoleObject(string name, Position position, Transform? parent)
        {
            this.name = name;

            Transform = new(position, parent);
        }

        /*internal RectTransform AddRectTransform()
        {
            if (RectTransform == null)
            {
                RectTransform = new(Transform);

                Transform = RectTransform;
            }

            return RectTransform;
        }*/

        public T AddComponent<T>()

            where T : Component, new()
        {
            var component = Component.Instantiate<T>(this);

            AddComponent(component);

            return component;
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        public T? GetComponent<T>()

            where T : Component
        {
            foreach (var component in components)
            {
                if (component is T result)
                {
                    return result;
                }
            }

            return null;
        }

        internal override void CallStart()
        {
            foreach (Component component in components)
            {
                if (component.IsEnabled == false)
                {
                    continue;
                }

                component.CallStart();
            }
        }

        internal override void CallFixedUpdate()
        {
            foreach (Component component in components)
            {
                if (component.IsEnabled == false)
                {
                    continue;
                }

                component.CallFixedUpdate();
            }
        }

        internal override void CallUpdate()
        {
            foreach (Component component in components)
            {
                if (component.IsEnabled == false)
                {
                    continue;
                }

                component.CallUpdate();
            }
        }

        internal override void CallLateUpdate()
        {
            foreach (Component component in components)
            {
                if (component.IsEnabled == false)
                {
                    continue;
                }

                component.CallLateUpdate();
            }
        }

        internal override void CallDraw()
        {
            foreach (Component component in components)
            {
                if (component.IsEnabled == false)
                {
                    continue;
                }

                component.CallDraw();
            }
        }
    }
}