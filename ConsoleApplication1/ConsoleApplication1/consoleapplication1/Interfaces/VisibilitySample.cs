using System;

namespace EstudosCSharp.Interfaces
{
    public static class VisibilitySample
    {
        static Visibility vsample;
        public static void Sample()
        {
            vsample = new Visibility();
            var t = ((IVisibility)vsample).doSomething("helder");
            Console.WriteLine(t);
        }

    }

    public interface IVisibility    {
        string doSomething(string someText);
    }

    public class Visibility : IVisibility
    {
        string IVisibility.doSomething(string someText)
        {
            return someText + " - concat - carvalho de sousa";
        }
    }
}
