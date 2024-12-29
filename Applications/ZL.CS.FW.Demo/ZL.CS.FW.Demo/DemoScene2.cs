using System.Numerics;

namespace ZL.CS.FW.Demo
{
    internal sealed class DemoScene2 : Scene
    {
        public DemoScene2() : base(32, 32)
        {
            var playerObject = CreateConsoleObject("Player", new Vector3(0, 0, 0));

            playerObject.AddComponent<Player>();

            playerObject.AddComponent<ForegroundDrawer>().

                target = new Foreground(new byte[,] { { 021 } }, "★");

            var cameraObject = CreateConsoleObject("Main Camera", new Vector3(0, 0, 0), playerObject.Transform);

            Camera.Main = cameraObject.AddComponent<Camera>();

            var bgObject1 = CreateConsoleObject("BG1", new Vector3(0, 0, 2));

            bgObject1.AddComponent<BackgroundDrawer>().

                target = ResourceManager.LoadBackground("BG1");

            var bgObject2 = CreateConsoleObject("BG2", new Vector3(1, 1, 1));

            bgObject2.AddComponent<BackgroundDrawer>().

                target = ResourceManager.LoadBackground("BG2");
        }
    }
}