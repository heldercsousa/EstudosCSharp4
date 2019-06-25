using EstudosCSharp.DesignPatterns.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstudosCSharp.DesignPatterns.AbstractFactoryFiles
{
    public enum Direcao
    {
        Adiante,
        Re
    }
    public interface IVeiculo
    {
        void Frear();
        void Acelerar(Direcao direcao);
    }

    public interface IManobravel
    {
        void VirarDireita();
        void VirarEsquerda();
    }


    public interface IAutomovel : IVeiculo, IManobravel
    { }

    public interface ILocomotiva : IVeiculo
    { }

    public interface ITransportFactory
    {
        ILocomotiva CreateLocomotiva();
        IAutomovel CreateAutomovel();
    }

    public class Locomotiva : ILocomotiva
    {
        internal Locomotiva()
        {

        }

        public void Frear()
        {
            throw new NotImplementedException();
        }

        public void Acelerar(Direcao direcao)
        {
            throw new NotImplementedException();
        }

       
    }

    public class Automovel : IAutomovel, IStateControlled<Automovel>
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float Velocity { get; set; }

        internal Automovel()
        {
            _stateList = new List<State.State<Automovel>>()
            {
                { new AndandoParaFrente(this)},
                { new AndandoDeRe(this) },
                { new Parado(this) }
            };
        }

        public void Frear()
        {
            throw new NotImplementedException();
        }

        public void Acelerar(Direcao direcao)
        {
            throw new NotImplementedException();
        }

        public void VirarDireita()
        {
            throw new NotImplementedException();
        }

        public void VirarEsquerda()
        {
            throw new NotImplementedException();
        }

        public State.State<Automovel> GetCurrentState
        {
            get 
            {
                _currentState = _currentState.Evaluate();
                return _currentState;
            }
        }

        public IList<State.State<Automovel>> StateList
        {
            get { return _stateList; }
        }

        private State.State<Automovel> _currentState;
        private IList<State.State<Automovel>> _stateList;
    }

    public class AndandoParaFrente : State.State<Automovel>
    {
        public AndandoParaFrente(Automovel contextObject) : base(contextObject)
        {
            
        }

        public override State.State<Automovel> Evaluate()
        {
            State.State<Automovel> st = null;
            if (_contextObject.Velocity == 0f)
            {
                st = _contextObject.StateList.OfType<Parado>().Single();
            }

            return st;
        }
    }

    public class Parado : State.State<Automovel>
    {
        public Parado(Automovel contextObject)
            : base(contextObject)
        {
            
        }

        public override State<Automovel> Evaluate()
        {
            throw new NotImplementedException();
        }
    }

    public class AndandoDeRe : State.State<Automovel>
    {
        public AndandoDeRe(Automovel contextObject)
            : base(contextObject)
        {
            
        }

        public override State<Automovel> Evaluate()
        {
            throw new NotImplementedException();
        }

    }

    public static class TransportFactory 
    {
        static TransportFactory()
        {
        }

        public static ILocomotiva CreateLocomotiva()
        {
            return new Locomotiva();
        }

        public static IAutomovel CreateAutomovel()
        {
            return new Automovel();
        }
    }
}
