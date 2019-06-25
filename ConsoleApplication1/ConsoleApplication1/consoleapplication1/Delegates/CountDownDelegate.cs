using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace EstudosCSharp.Delegates
{
    /// <summary>
    /// este contador tem grande confiança no consumidor do serviço, confiando que o consumidor nunca irá invocar os delegates Tick e Finished manualmente. 
    /// Porém, o consumidor pode acioná-los manualmente, o que provocará ações inesperadas. Além deste problema, com delegates, temos que confiar que o consumidor não irá
    /// sobrepor toda a lista de metodos invocados pelo Delegate por uma nova lista, o que provocaria um comportamento inesperado.
    /// </summary>
    class CountDownDelegate
    {
        private uint _seconds;
        public Action<uint> Tick        {get; set;} //invocar o delegate de fora do contexto não é uma operação segura. Os delegates oferecem essa possibilidade.
        public Action       Finished    {get; set;}

        public CountDownDelegate(uint seconds)
        {
            _seconds = seconds;
        }

        /// <summary>
        /// este metodo precisa ser Assincrono para evitar interrupção no fluxo do programa ao invocar Thread.Sleep. Por isso a presença da Background thread
        /// </summary>
        public void Start()
        {
            //lambda expression
            new Thread(() =>
            {
                uint n = _seconds;
                while (n > 0u)
                {
                    if (Tick != null)
                        Tick(n);
                    Thread.Sleep(1000);
                    n--;
                }
                if (Finished != null)
                    Finished();
            }).Start();

            //anonymous function expression
           /*new Thread(delegate()
            {
                uint n = _seconds;
                while (n > 0u)
                {
                    Thread.Sleep(1000);
                    n--;
                }
            }).Start();*/
            
        }

    }


    /// <summary>
    /// este contador usa .NET EVENTS, um caminho mais seguro! Ele impede que os eventos sejam acionados de fora do Tipo em que está contido. 
    /// Isso resolve o problema de ter que confiar nas ações do consumidor do serviço. Além disso, Events não permitem que 
    /// Metodos sejam adicionados a lista com o operador =. Só é permitido fazer isso com os operadores += e -=, o que 
    /// resolve o problema de o consumidor poder substituir toda a lista de metodos de execução por uma nova
    /// </summary>
    class CountDownEvent
    {
        private uint _seconds;
        public event Action<uint> Tick;
        public event Action Finished;

        public CountDownEvent(uint seconds)
        {
            _seconds = seconds;
        }

        /// <summary>
        /// este metodo precisa ser Assincrono para evitar interrupção no fluxo do programa ao invocar Thread.Sleep. Por isso a presença da Background thread
        /// </summary>
        public void Start()
        {
            //lambda expression
            new Thread(() =>
            {
                uint n = _seconds;
                while (n > 0u)
                {
                    if (Tick != null)
                        Tick(n);
                    Thread.Sleep(1000);
                    n--;
                }
                if (Finished != null)
                    Finished();
            }).Start();
        }
    }

    /// <summary>
    /// o mesmo que a classe CountDownEvent, porém aqui é usado um padrão para EventHandler, de forma que outros programadores se sintam familiarizados com os eventos desde o primeiro contato com o framework.
    /// Além disso, usa-se o delegate generico EventHandle
    /// </summary>
    public class CountDownEvent2
    {
        private uint _seconds;
        public event EventHandler<TickEventArgs> Tick;
        public event EventHandler Finished;

        public CountDownEvent2(uint seconds)
        {
            _seconds = seconds;
        }

        /// <summary>
        /// este metodo precisa ser Assincrono para evitar interrupção no fluxo do programa ao invocar Thread.Sleep. Por isso a presença da Background thread
        /// </summary>
        public void Start()
        {

            //lambda expression
            new Thread(() =>
            {
                uint n = _seconds;
                while (n > 0u)
                {
                    var tick = Tick; //evita problema com remoção de eventos ocorridas entre o if a seguir e a invocação do evento dentre dele
                    if (tick != null)
                        tick(this, new TickEventArgs(n));
                    Thread.Sleep(1000);
                    n--;
                }

                var finished = Finished;
                if (finished != null)
                    finished(this, EventArgs.Empty);
            }).Start();

        }

        public class TickEventArgs : EventArgs
        {
            public uint Seconds { get; private set; }
            public TickEventArgs(uint seconds)
            {
                this.Seconds = seconds;
            }
        }
      
    }



    
}
