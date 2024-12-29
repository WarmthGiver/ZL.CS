using System;

namespace ZL.CS.FW
{
    public static class Time
    {
        public static float MinDeltaTime { get; internal set; }

        public static float DeltaTime { get; internal set; }

        private static float fixedDeltaTime;

        public static float FixedDeltaTime
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