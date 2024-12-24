using System;

using ZL.CS.ConsoleEngine;

using ZL.CS.Graphics;

namespace ZL.CS.ApplicationDemo
{
    internal sealed class DemoApplication : Application
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

        public DemoApplication() : base(32, 32) { }

        protected override void Start()
        {
            Scene.FrameRate = 30;

            Scene.Load<DemoScene>();
        }
    }
}