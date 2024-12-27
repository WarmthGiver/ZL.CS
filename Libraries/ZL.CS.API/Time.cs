using System;

namespace ZL.CS.API
{
    public static class Time
    {
        public static double MinDeltaTime { get; internal set; }

        public static double DeltaTime { get; internal set; }

        private static double fixedDeltaTime;

        public static double FixedDeltaTime
        {
            get => fixedDeltaTime;

            set
            {
                fixedDeltaTime = value;

                FixedDelayTime = TimeSpan.FromSeconds(value);
            }
        }

        public static TimeSpan FixedDelayTime { get; private set; }
    }
}