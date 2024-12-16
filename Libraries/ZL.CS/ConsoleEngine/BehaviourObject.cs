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

        public virtual void Start() { }

        public virtual void FixedUpdate() { }

        public virtual void Update() { }

        public virtual void LateUpdate() { }

        public virtual void DrawCall() { }
    }
}