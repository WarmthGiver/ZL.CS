﻿using ZL.CS.Graphics;

namespace ZL.CS.ConsoleEngine.UI
{
    public abstract class UI<TGraphic> : Component

        where TGraphic : Graphic
    {
        public TGraphic? graphic = null;

        protected UI(SceneObject sceneObject, TGraphic? graphic) : base(sceneObject)
        {
            this.graphic = graphic;
        }

        protected override void Update()
        {
            graphic?.Draw(sceneObject.canvas, sceneObject.transform.Position);
        }
    }
}