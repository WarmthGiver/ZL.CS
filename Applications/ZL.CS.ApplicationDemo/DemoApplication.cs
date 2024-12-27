using System;

namespace ZL.CS.API.ApplicationDemo
{
    internal sealed class DemoApplication : App
    {
        static DemoApplication()
        {
            ResourceManager.AddBackground("BG1", new(new byte[,]
            {
                {015,000,015,000,015,000,015},

                {000,015,000,015,000,015,000},

                {015,000,015,000,015,000,015},

                {000,015,000,015,000,015,000},

                {015,000,015,000,015,000,015},

                {000,015,000,015,000,015,000},

                {015,000,015,000,015,000,015},
            }));

            ResourceManager.AddBackground("BG2", new(new byte[,]
            {
                {201,000,201,000,201,000,201},

                {000,201,000,201,000,201,000},

                {201,000,201,000,201,000,201},

                {000,201,000,201,000,201,000},

                {201,000,201,000,201,000,201},

                {000,201,000,201,000,201,000},

                {201,000,201,000,201,000,201},
            }));

            Console.CursorVisible = false;
        }

        protected override void Start()
        {
            Scene.Load<DemoScene1>();

            TargetFrameRate = 60;
        }
    }
}