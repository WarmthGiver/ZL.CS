namespace ZL.CS.ConsoleEngine
{
    public abstract class Object
    {
        public bool isEnabled = true;

        public virtual void SetEnabled(bool value)
        {
            isEnabled = value;
        }

        public void CallUpdate()
        {
            if (isEnabled)
            {
                Update();
            }
        }

        protected abstract void Update();
    }
}