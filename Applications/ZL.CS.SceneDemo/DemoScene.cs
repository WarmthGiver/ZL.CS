using System.Drawing;

using ZL.CS.ConsoleEngine;

using ZL.CS.ConsoleEngine.UI;

using ZL.CS.Graphics;

namespace ZL.CS.SceneDemo
{
    internal sealed class DemoScene : Scene
    {
        public DemoScene()
        {
            var mainCamera = CreateConsoleObject("Main Camera");

            Camera.Main = mainCamera.Add<Camera>();

            var player = CreateConsoleObject("Player");

            var text = player.Add<Text>();

            text.graphic = new Foreground("★");
        }
    }
}