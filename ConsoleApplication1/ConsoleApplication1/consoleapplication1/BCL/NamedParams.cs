
using System;

namespace EstudosCSharp.BCL
{
    public static class NamedParams
    {
        public static void AllowedNamedPositionalParams()
        {
            Console.WriteLine(CalcArea(lenght: 20, width: 30));
            Console.WriteLine(CalcArea(width: 30, lenght: 20));
            Console.WriteLine(CalcArea(20, width: 30));
            Console.WriteLine(CalcArea(lenght: 20, 30));
        }

        static int CalcArea(int lenght, int width)
        {
            return lenght * width;
        }
    }
}
