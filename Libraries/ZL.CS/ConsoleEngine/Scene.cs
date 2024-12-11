﻿using System;

using System.Collections.Generic;

using System.Drawing;

using System.Threading.Tasks;

using ZL.CS.Graphics;

namespace ZL.CS.ConsoleEngine
{
    public abstract class Scene
    {
        private int frameRate;

        public int FrameRate
        {
            get => frameRate;

            set
            {
                frameRate = value;

                threadDelay = TimeSpan.FromSeconds(1.0 / frameRate);
            }
        }

        private TimeSpan threadDelay;

        protected readonly Size consoleSize;

        protected readonly Rectangle rect;

        //protected readonly Point pivot;

        protected readonly Canvas mainCanvas;

        protected readonly List<Canvas> subCanvases = new();

        private readonly List<SceneObject> sceneObjects = new();

        protected Scene(int framesRate, Size consoleSize, int subCanvasesCount) : this(framesRate, consoleSize, new Rectangle(new(0, 0), consoleSize)) { }

        protected Scene(int framesRate, Size consoleSize, Rectangle rect)
        {
            FrameRate = framesRate;

            this.consoleSize = consoleSize;

            this.rect = rect;

            //pivot = size.GetPivot();

            mainCanvas = new Canvas(rect);
        }

        protected Canvas CreateCanvas()
        {
            Canvas canvas = new(consoleSize);

            subCanvases.Add(canvas);

            return canvas;
        }

        protected SceneObject CreateSceneObject(string name, Canvas canvas)
        {
            SceneObject sceneObject = new(name, canvas);

            sceneObjects.Add(sceneObject);

            return sceneObject;
        }

        public virtual void Start()
        {
            Fixed.Console.SetWindowSize(consoleSize);

            fixedUpdate = Task.Run(FixedUpdate);

            update = Task.Run(Update);

            draw = Task.Run(Draw);
        }

        public void End()
        {

        }

        private Task fixedUpdate;

        private async void FixedUpdate()
        {
            while (true)
            {
                foreach (var sceneObject in sceneObjects)
                {
                    sceneObject.TryFixedUpdate();
                }

                await Task.Delay(threadDelay);
            }
        }

        private Task update;

        private void Update()
        {
            while (true)
            {
                foreach (var sceneObject in sceneObjects)
                {
                    sceneObject.TryUpdate();
                }
            }
        }

        private Task draw;

        private async void Draw()
        {
            while (true)
            {
                foreach (var subCanvas in subCanvases)
                {
                    subCanvas.Draw();
                }

                mainCanvas.Draw();

                await Task.Delay(threadDelay);
            }
        }
    }
}