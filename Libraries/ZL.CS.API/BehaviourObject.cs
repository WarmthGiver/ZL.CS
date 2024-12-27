namespace ZL.CS.API
{
    public abstract class BehaviourObject
    {
        protected bool isEnabled = true;

        public virtual bool IsEnabled
        {
            get => isEnabled;

            set => isEnabled = value;
        }

        internal virtual void CallStart() { }

        internal virtual void CallFixedUpdate() { }

        internal virtual void CallUpdate() { }

        internal virtual void CallLateUpdate() { }

        internal virtual void CallDraw() { }
    }
}