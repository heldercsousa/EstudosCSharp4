using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstudosCSharp.Delegates
{
    /// <summary>
    /// para cada instancia de Bar, neste caso, o compilador irá auto gerar "Fields" para as Actions 1 a 8, para armazenar "Handlers" pra estes eventos.
    /// Quando nenhum "handler" está anexado aos eventos, temos 8 referências perdidas, ou seja, memoria alocada sem necessidade. Para 10 instâncias de Bar,
    /// teriamos 80 fields pra se referenciar aos 8 Actions de cada instancia.
    /// </summary>
    class Bar
    {
        public event Action A1, A2, A3, A4, A5, A6, A7, A8;
    }
    
    /// <summary>
    /// Dictionary Driven Bar class. Save memory with a diferent approach of Bar class.
    /// Para cada instância de Bar2, e caso não haja nenhum Handler associado, apenas o custo
    /// do Dictionary é considerado em cada instância, mais as 8 referências estáticas, que 
    /// são alocadas apenas uma vez na memória. Uma grande economia de memória! Este código é 
    /// inseguro num contexto de multithread. Este esquema é conceitualmente equivalente ao 
    /// que é implementado em Windows Forms e WPF.
    /// </summary>
    class Bar2
    {
        static readonly object s_KeyA1 = new Object();//funciona como "Key" do dictionary
        static readonly object s_KeyA2 = new Object();
        static readonly object s_KeyA3 = new Object();
        static readonly object s_KeyA4 = new Object();
        static readonly object s_KeyA5 = new Object();
        static readonly object s_KeyA6 = new Object();
        static readonly object s_KeyA7 = new Object();
        static readonly object s_KeyA8 = new Object();

        private Dictionary<object, Delegate> _handlers;

        public event Action A1
        {
            add { AddHandler(s_KeyA1, value); }
            remove { RemoveHandler(s_KeyA1, value); }
        }

        public event Action A2
        {
            add { AddHandler(s_KeyA2, value); }
            remove { RemoveHandler(s_KeyA2, value); }
        }

        public event Action A3
        {
            add { AddHandler(s_KeyA3, value); }
            remove { RemoveHandler(s_KeyA3, value); }
        }

        public event Action A4
        {
            add { AddHandler(s_KeyA4, value); }
            remove { RemoveHandler(s_KeyA4, value); }
        }

        public event Action A5
        {
            add { AddHandler(s_KeyA5, value); }
            remove { RemoveHandler(s_KeyA5, value); }
        }

        public event Action A6
        {
            add { AddHandler(s_KeyA6, value); }
            remove { RemoveHandler(s_KeyA6, value); }
        }

        public event Action A7
        {
            add { AddHandler(s_KeyA7, value); }
            remove { RemoveHandler(s_KeyA7, value); }
        }

        public event Action A8
        {
            add { AddHandler(s_KeyA8, value); }
            remove { RemoveHandler(s_KeyA8, value); }
        }

        /// <summary>
        /// devido ao uso de Assessores customizados, o compilador não sabe da existência de A1;
        /// Porque o compilador não emite mais um Private Field para os delegates dos eventos, ele não
        /// sabe mais como ganhar acesso ao delegate
        /// </summary>
        public void RaiseA1Wrongly()
        {
            /*Action a1;// = A1;
            if (a1 != null)
                a1();
             */
        }

        public void RaiseA1()
        {
            Action a1 = (Action)GetHandler(s_KeyA1);
            if (a1 != null)
                a1();
        }

        private void AddHandler(object evt, Delegate handler)
        {
            _handlers[evt] = Delegate.Combine(GetHandler(evt), handler);
        }

        private void RemoveHandler(object evt, Delegate handler)
        {
            var newHandler = Delegate.Remove(GetHandler(evt), handler);
            if (newHandler == null && _handlers.ContainsKey(evt))
                _handlers.Remove(evt);
            else
                _handlers[evt] = newHandler;
        }

        private Delegate GetHandler(object evt)
        {
            Delegate handler = null;
            _handlers.TryGetValue(evt, out handler);
            return handler;
        }

    }
}
