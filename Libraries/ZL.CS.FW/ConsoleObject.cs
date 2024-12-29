using System.Collections.Generic;

using System.Numerics;

namespace ZL.CS.FW
{
    public sealed class ConsoleObject : BehaviourObject
    {
        public readonly string name;

        public RectTransform? RectTransform { get; private set; }

        public Transform Transform { get; private set; }

        private readonly List<Component> components = new();

        public ConsoleObject(string name, Vector3 position, Transform? parent)
        {
            this.name = name;

            Transform = new(position, parent);
        }

        internal RectTransform AddRectTransform()
        {
            if (RectTransform == null)
            {
                RectTransform = new(Transform);

                Transform = RectTransform;
            }

            return RectTransform;
        }

        public T AddComponent<T>()

            where T : Component, new()
        {
            Component.Container = this;

            T component = new();

            components.Add(component);

            return component;
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
            foreach (var component in components)
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
            foreach (var component in components)
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
            foreach (var component in components)
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
            foreach (var component in components)
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
            foreach (var component in components)
            {
                if (component.IsEnabled == false)
                {
                    continue;
                }

                component.CallDraw();
            }
        }

        public override void Dispose()
        {
            foreach (var component in components)
            {
                component.Dispose();
            }
        }
    }
}