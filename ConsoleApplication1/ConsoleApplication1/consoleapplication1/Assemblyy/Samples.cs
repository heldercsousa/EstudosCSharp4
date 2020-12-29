using System;
using System.Reflection;

namespace EstudosCSharp.Assemblyy
{
    public static class Samples
    {
        public static void Load()
        {
            string longName = "system, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
            Assembly assem = Assembly.Load(longName);
            if (assem == null)
                Console.WriteLine("Unable to load assembly...");
            else
            {
                Console.WriteLine(assem.FullName);
                foreach (Type oType in assem.GetTypes())
                {
                    Console.WriteLine(oType.Name);
                }
            }
        }
    }
}
