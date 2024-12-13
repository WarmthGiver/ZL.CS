namespace ZL.CS.ConsoleEngine
{
    public abstract class Component : Object
    {
        public ConsoleObject consoleObject { get; private set; }

        internal static T Instantiate<T>(ConsoleObject consoleObject)
            
            where T : Component, new()
        {
            T component = new()
            {
                consoleObject = consoleObject
            };

            return component;
        }
    }
}