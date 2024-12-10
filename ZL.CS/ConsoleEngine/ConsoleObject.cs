using System.Collections.Generic;

using System.Drawing;

using ZL.CS.Graphics;

namespace ZL.CS.ConsoleEngine
{
    public sealed class ConsoleObject : Object
    {
        public readonly string name;

        public readonly Canvas canvas;

        public Point position;

        public byte depth;

        private List<Component> components;

        private ConsoleObject? parnet = null;

        private List<ConsoleObject> children = new();

        public ConsoleObject(string name, Canvas canvas, Point position, byte depth) : this(name, canvas, position, depth, new List<Component>()) { }

        public ConsoleObject(string name, Canvas canvas, Point position, byte depth, params Component[] components) : this(name, canvas, position, depth, new List<Component>(components)) { }

        private ConsoleObject(string name, Canvas canvas, Point position, byte depth, List<Component> components)
        {
            this.name = name;

            this.canvas = canvas;

            this.position = position;

            this.depth = depth;

            this.components = components;
        }

        protected override void Update()
        {
            foreach (Component component in components)
            {
                component.TryUpdate();
            }
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }
    }
}