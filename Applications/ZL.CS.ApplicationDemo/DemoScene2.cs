using System.Numerics;

namespace ZL.CS.API.ApplicationDemo
{
    internal sealed class DemoScene2 : Scene
    {
        public DemoScene2() : base(64, 32)
        {
            var playerObject = CreateConsoleObject("Player", new Vector3(0, 0, 0));

            playerObject.
                
                AddComponent<Player>().
                
                AddComponent<ForegroundDrawer>().
                
                graphic = new Foreground(new byte[,] { { 196 } }, "☆");

            Camera.Main = CreateConsoleObject<Camera>("Main Camera", new Vector3(0, 0, 0), playerObject.Transform);

            CreateConsoleObject<BackgroundDrawer>("BG1", new Vector3(0, 0, 2)).

                graphic = ResourceManager.LoadBackground("BG1");

            CreateConsoleObject<BackgroundDrawer>("BG2", new Vector3(1, 1, 1)).

                graphic = ResourceManager.LoadBackground("BG2");
        }
    }
}