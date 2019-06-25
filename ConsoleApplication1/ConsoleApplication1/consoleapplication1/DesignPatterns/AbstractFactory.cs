using EstudosCSharp.DesignPatterns.AbstractFactoryFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstudosCSharp.DesignPatterns
{
    public class AbstractFactory
    {
        public AbstractFactory()
        {
            var x = TransportFactory.CreateAutomovel();
            var y = TransportFactory.CreateLocomotiva();
            x.Acelerar(Direcao.Adiante);
            y.Acelerar(Direcao.Re);
            var c = new Locomotiva();//sendo permitid so pq a classe esta dentto do mesmo assembly
        }
    }

    
}
