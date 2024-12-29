using System;

namespace ZL.CS.FW.Demo
{
    internal sealed class DemoApplication : App
    {
        static DemoApplication()
        {
            ResourceManager.AddBackground("BG1",

                new Background(new byte[,]
                {
                    {015,000,015,000,015,000,015},

                    {000,015,000,015,000,015,000},

                    {015,000,015,000,015,000,015},

                    {000,015,000,015,000,015,000},

                    {015,000,015,000,015,000,015},

                    {000,015,000,015,000,015,000},

                    {015,000,015,000,015,000,015},
                })
            );

            ResourceManager.AddBackground("BG2",

                new Background(new byte[,]
                {
                    {201,000,201,000,201,000,201},

                    {000,201,000,201,000,201,000},

                    {201,000,201,000,201,000,201},

                    {000,201,000,201,000,201,000},

                    {201,000,201,000,201,000,201},

                    {000,201,000,201,000,201,000},

                    {201,000,201,000,201,000,201},
                })
            );

            Console.CursorVisible = false;

            TargetFrameRate = 60;
        }

        protected override void Start()
        {
            Scene.Load<DemoScene1>();
        }
    }
}