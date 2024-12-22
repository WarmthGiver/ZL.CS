namespace ZL.CS.ConsoleEngine
{
    public sealed class SceneManager
    {
        public static void Load<T>()

            where T : Scene, new()
        {
            new T().Load();
        }
    }
}