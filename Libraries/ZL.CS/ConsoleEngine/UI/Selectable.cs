using System;

namespace ZL.CS.ConsoleEngine.UI
{
    public abstract class Selectable : Component
    {
        public readonly Navigation navigation = new();

        protected State state = State.Enabled;

        protected Selectable(SceneObject ConsoleObject) : base(ConsoleObject) { }

        public void Select(bool value)
        {
            if (value == true)
            {
                Navigation.selected?.Select(false);

                Navigation.selected = this;
            }

            UpdateState();
        }

        public override void SetEnabled(bool value)
        {
            base.SetEnabled(value);

            UpdateState();
        }

        private void UpdateState()
        {
            if (Navigation.selected == this)
            {
                state = isEnabled ? State.Enabled_Selected : State.Disabled_Selected;
            }
            else
            {
                state = isEnabled ? State.Enabled : State.Disabled;
            }
        }

        public sealed class Navigation
        {
            public static Selectable? selected = null;

            public Selectable? Up { get; set; }

            public Selectable? Down { get; set; }

            public Selectable? Left { get; set; }

            public Selectable? Right { get; set; }

            public static void Move(Selectable selected, ConsoleKey key)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:

                        selected?.navigation?.Up?.Select(true);

                        break;

                    case ConsoleKey.DownArrow:

                        selected?.navigation?.Down?.Select(true);

                        break;

                    case ConsoleKey.LeftArrow:

                        selected?.navigation?.Left?.Select(true);

                        break;

                    case ConsoleKey.RightArrow:

                        selected?.navigation?.Right?.Select(true);

                        break;
                }
            }
        }

        protected enum State
        {
            Enabled,

            Enabled_Selected,

            Disabled,

            Disabled_Selected,
        }
    }
}