using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstudosCSharp.DesignPatterns
{
    public class ObjAdaptar
    {
        public virtual void FazerAlgo()
        {
            Console.WriteLine("ObjAdaptar method");
        }
    }

    public class Adapter : ObjAdaptar
    {
        IAdaptador adaptador;
        public Adapter(IAdaptador objAdaptador)
        {
            adaptador = objAdaptador;
        }

        public override void FazerAlgo()
        {
            adaptador.FazerAlgo();
        }
    }

    class ObjAdaptador : IAdaptador
    {
        public void FazerAlgo()
        {
            Console.WriteLine("IAdaptador method");
        }
    }

    public interface IAdaptador
    {
        void FazerAlgo();
    }
}
