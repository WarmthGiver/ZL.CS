using System;
using System.Collections.Generic;

namespace ZL.CS.Graphics
{
    public static class ResourceManager
    {
        public static Dictionary<string, Background> backgrounds = new();

        public static Dictionary<string, Foreground> foregrounds = new();

        public static void AddBackground<TEnum>(TEnum name, Background background)

            where TEnum : Enum
        {
            backgrounds.Add(name, background);
        }

        public static void AddBackground(string name, Background background)
        {
            backgrounds.Add(name, background);
        }

        public static Background LoadBackground<TEnum>(TEnum name)

            where TEnum : Enum
        {
            return backgrounds.Get(name);
        }

        public static Background LoadBackground(string name)
        {
            return backgrounds.Get(name);
        }

        public static void AddForeground<TEnum>(TEnum name, Foreground foreground)

            where TEnum : Enum
        {
            foregrounds.Add(name, foreground);
        }

        public static void AddForeground(string name, Foreground foreground)
        {
            foregrounds.Add(name, foreground);
        }

        public static Foreground LoadForeground<TEnum>(TEnum name)

            where TEnum : Enum
        {
            return foregrounds.Get(name);
        }

        public static Foreground LoadForeground(string name)
        {
            return foregrounds.Get(name);
        }
    }
}