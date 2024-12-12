using System.Collections.Generic;

using ZL.CS.Graphics;

namespace ZL.CS.ConsoleEngine
{
    public sealed class SceneObject : Object
    {
        public readonly string name;

        public RectTransform? RectTransform { get; private set; }

        public Transform Transform { get; private set; }

        private readonly List<Component> components = new();

        public SceneObject(string name)
        {
            this.name = name;

            Transform = new(this);
        }

        internal RectTransform AddRectTransform()
        {
            RectTransform = new(Transform);

            Transform = RectTransform;

            return RectTransform;
        }

        public void AddComponents(params Component[] components)
        {
            foreach (var component in components)
            {
                AddComponent(component);
            }
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        protected override void Update()
        {
            foreach (Component component in components)
            {
                component.TryUpdate();
            }
        }
    }
}