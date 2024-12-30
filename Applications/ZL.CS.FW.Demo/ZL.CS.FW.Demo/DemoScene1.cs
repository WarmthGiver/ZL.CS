using System.Numerics;

namespace ZL.CS.FW.Demo
{
    // Scene 클래스를 상속받기 하면 생명주기가 작동함.
    internal sealed class DemoScene1 : Scene
    {
        public DemoScene1() : base(32, 32)
        {
            // 플레이어 오브젝트 생성.
            var playerObject = CreateConsoleObject("Player", new Vector3(0, 0, 0));

            // 플레이어 오브젝트에 Player 컴포넌트 부착.
            playerObject.AddComponent<Player>();

            // 플레이어 오브젝트에 ForegroundDrawer 컴포넌트 부착.
            playerObject.AddComponent<ForegroundDrawer>().

                //ForegroundDrawer 컴포넌트가 그릴 target에 새로운 Foreground 생성후 대입.
                target = new Foreground(new byte[,] { { 196 } }, "♥");

            // 카메라 오브젝트 생성, 부모 오브젝트를 플레이어 오브젝트로 설정.
            var cameraObject = CreateConsoleObject("Main Camera", new Vector3(0, 0, 0), playerObject.Transform);

            // 카메라 오브젝트에 Camera 컴포넌트 부착.
            Camera.Main = cameraObject.AddComponent<Camera>();

            // 배경 오브젝트 1 생성
            var bgObject1 = CreateConsoleObject("BG1", new Vector3(0, 0, 1));

            // 배경 오브젝트 1에 BackgroundDrawer 컴포넌트 부착.
            bgObject1.AddComponent<BackgroundDrawer>().

                // BackgroundDrawer 컴포넌트가 그릴 target에 미리 생성한 배경 'BG1' 로드후 대입.
                target = ResourceManager.LoadBackground("BG1");

            // 배경 오브젝트 2 생성
            var bgObject2 = CreateConsoleObject("BG2", new Vector3(1, 1, 2));

            // 배경 오브젝트 2에 BackgroundDrawer 컴포넌트 부착.
            bgObject2.AddComponent<BackgroundDrawer>().

                // BackgroundDrawer 컴포넌트가 그릴 target에 미리 생성한 배경 'BG2' 로드후 대입.
                target = ResourceManager.LoadBackground("BG2");
        }
    }
}