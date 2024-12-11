using ZL.CS.Graphics;

namespace ZL.CS.ConsoleEngine.UI
{
    public sealed class Image : UI<Background>
    {
        public Image(SceneObject sceneObject, Background? background = null) : base(sceneObject, background) { }
    }
}