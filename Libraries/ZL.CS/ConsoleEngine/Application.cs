namespace ZL.CS.ConsoleEngine
{
    public abstract class Application
    {
        public void Run()
        {
            //state = State.Running;

            Start();

            LifeSupport();
        }

        protected abstract void Start();

        private void LifeSupport()
        {
            while (Scene.State != SceneState.Terminated) ;
        }
    }
}