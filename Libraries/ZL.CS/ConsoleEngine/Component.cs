namespace ZL.CS.ConsoleEngine
{
    public abstract class Component : Object
    {
        public readonly SceneObject sceneObject;

        protected Component(SceneObject sceneObject)
        {
            this.sceneObject = sceneObject;
        }
    }
}