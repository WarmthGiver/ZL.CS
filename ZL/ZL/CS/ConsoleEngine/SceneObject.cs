using System.Collections.Generic;
using System.Drawing;

using ZL.CS.Graphics;

namespace ZL.CS.ConsoleEngine
{
    public sealed class SceneObject : Object
    {
        public readonly string name;

        public readonly Canvas canvas;

        public Point position;
        public sbyte depth;

        private List<Component> components = new();

        private SceneObject? parnet = null;
        private List<SceneObject> children = new();

        public SceneObject(string name, Canvas canvas, Point position, sbyte depth)
        {
            this.name = name;

            this.canvas = canvas;

            this.position = position;
            this.depth = depth;
        }

        protected override void Update()
        {
            foreach (Component component in components)
            {
                component.CallUpdate();
            }
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }
    }
}