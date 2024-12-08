using System.Drawing;

namespace ZL.CS
{
    public static class StringExtension
    {
        public static Size GetSize(this string[] instance)
        {
            return new(instance.GetMaxWidth(), instance.Length);
        }

        public static int GetMaxWidth(this string[] instance)
        {
            int width = instance[0].Length;

            for (int i = 1; i < instance.Length; ++i)
            {
                if (width < instance[i].Length)
                {
                    width = instance[i].Length;
                }
            }

            return width;
        }
    }
}