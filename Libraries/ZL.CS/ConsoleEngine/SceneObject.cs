using System.Collections.Generic;

using ZL.CS.Graphics;

namespace ZL.CS.ConsoleEngine
{
    public sealed class SceneObject : Object
    {
        public readonly string name;

        public readonly Canvas canvas;

        public readonly Transform transform;

        private readonly List<Component> components = new();

        public SceneObject(string name, Canvas canvas)
        {
            this.name = name;

            this.canvas = canvas;

            transform = new(this);
        }

        protected override void Update()
        {
            foreach (Component component in components)
            {
                component.TryUpdate();
            }
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
    }
}