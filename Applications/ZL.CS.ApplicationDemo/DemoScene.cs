using ZL.CS.ConsoleEngine;

using ZL.CS.ConsoleEngine.UI;

using ZL.CS.Graphics;

namespace ZL.CS.ApplicationDemo
{
    internal sealed class DemoScene : Scene
    {
        public DemoScene()
        {
            var playerObject = CreateConsoleObject("Player", new Position(0, 0, 0));

            playerObject.AddComponent<Player>();

            var text = playerObject.AddComponent<Text>();

            text.graphic = new Foreground(new byte[,] { { 196 } }, "♥");

            var mainCameraObject = CreateConsoleObject("Main Camera", new Position(0, 0, 0));

            Camera.Main = mainCameraObject.AddComponent<Camera>();

            mainCameraObject.Transform.Parent = playerObject.Transform;

            var bg1 = CreateConsoleObject("BG1", new Position(0, 0, 1));

            var bg1Image = bg1.AddComponent<Image>();

            bg1Image.graphic = ResourceManager.LoadBackground("BG1");

            var bg2 = CreateConsoleObject("BG2", new Position(1, 1, 2));

            var bg2Image = bg2.AddComponent<Image>();

            bg2Image.graphic = ResourceManager.LoadBackground("BG2");
        }
    }
}