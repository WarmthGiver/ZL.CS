namespace ZL.CS.ConsoleEngine
{
    public abstract class Component : Object
    {
        protected SceneObject sceneObject;

        protected Component(SceneObject sceneObject)
        {
            this.sceneObject = sceneObject;
        }
    }
}