namespace ZL.CS.ConsoleEngine
{
    public abstract class Component : BehaviourObject
    {
        public ConsoleObject? ConsoleObject { get; private set; } = null;

        internal static T Instantiate<T>(ConsoleObject consoleObject)
            
            where T : Component, new()
        {
            T component = new()
            {
                ConsoleObject = consoleObject
            };

            return component;
        }


    }
}