using ZL.CS.Graphics;

namespace ZL.CS.ConsoleEngine.UI
{
    public sealed class Image : UI<Background>
    {
        public Image(ConsoleObject sceneObject, Background? background = null) : base(sceneObject, background) { }
    }
}