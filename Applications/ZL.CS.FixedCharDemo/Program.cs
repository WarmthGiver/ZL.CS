namespace ZL.CS.FixedCharDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string text = "a★aa★a aa";

            //Console.WriteLine(text);

            var list = text.ToFixedChar();

            foreach (var item in list)
            {
                Console.Write(item.ToString());
            }
        }
    }
}