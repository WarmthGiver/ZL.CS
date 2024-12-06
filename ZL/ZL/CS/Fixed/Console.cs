using System.Drawing;

namespace ZL.CS.Fixed
{
    public static class Console
    {
        public static void SetWindowSize(Size size)
        {
            System.Console.SetWindowSize(size.Width, size.Height);
        }
    }
}