namespace ZL.CS.ConsoleEngine
{
    public abstract class Object
    {
        public bool isEnabled = true;

        public virtual void SetEnabled(bool value)
        {
            isEnabled = value;
        }

        public void TryFixedUpdate()
        {
            if (isEnabled)
            {
                FixedUpdate();
            }
        }

        protected virtual void FixedUpdate() { }

        public void TryUpdate()
        {
            if (isEnabled)
            {
                Update();
            }
        }

        protected virtual void Update() { }
    }
}