namespace ZL.CS.ConsoleEngine
{
    public abstract class Component : Object
    {
        protected ConsoleObject sceneObject;

        protected Component(ConsoleObject sceneObject)
        {
            this.sceneObject = sceneObject;
        }
    }
}