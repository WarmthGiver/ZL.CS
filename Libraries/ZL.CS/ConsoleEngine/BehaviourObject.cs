namespace ZL.CS.ConsoleEngine
{
    public abstract class BehaviourObject
    {
        protected bool isEnabled = true;

        public virtual bool IsEnabled
        {
            get => isEnabled;

            set => isEnabled = value;
        }

        internal virtual void Start() { }

        internal virtual void FixedUpdate() { }

        internal virtual void Update() { }

        internal virtual void LateUpdate() { }

        internal virtual void DrawCall() { }
    }
}