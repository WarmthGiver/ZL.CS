using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using ZL.CS.Graphics;

namespace ZL.CS.ConsoleEngine
{
    public abstract class Scene
    {
        protected readonly Size size;
        protected readonly Point pivot;

        protected readonly Canvas[] canvases;

        protected readonly List<SceneObject> sceneObjects;

        protected Scene(Size size, int canvasesCount)
        {
            this.size = size;
            pivot = size.GetPivot();

            canvases = new Canvas[canvasesCount];

            sceneObjects = new();
        }

        internal void Start<TApplication>(TApplication application)

            where TApplication : Application<TApplication>
        {
            Fixed.Console.SetWindowSize(size);

            application.StartRoutine(Loop());
        }

        protected IEnumerator Loop()
        {
            while (true)
            {
                CallUpdate();

                foreach (var canvas in canvases)
                {
                    canvas.Show();
                }
                
                yield return null;
            }
        }

        protected void CallUpdate()
        {
            foreach (var sceneObject in sceneObjects)
            {
                sceneObject.CallUpdate();
            }
        }
    }
}