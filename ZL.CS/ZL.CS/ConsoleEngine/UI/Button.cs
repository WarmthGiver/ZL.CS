namespace ZL.CS.ConsoleEngine.UI
{
    public sealed class Button : Selectable
    {
        public Image? image = null;
        public Text? text = null;

        public Button(SceneObject sceneObject) : base(sceneObject) { }

        protected override void Update()
        {

        }
    }
}