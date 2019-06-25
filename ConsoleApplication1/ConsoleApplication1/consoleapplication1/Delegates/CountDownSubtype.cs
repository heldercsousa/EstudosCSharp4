using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace EstudosCSharp.Delegates
{
    /// <summary>
    /// essa classe mostra como invocar eventos de um tipo base de dentro de um subtipo
    /// </summary>
    public class CountDownSubtype: CountDownEvent3
    {
        public CountDownSubtype() : this(10) { }
        public CountDownSubtype(uint seconds) : base (seconds) { }

        /// <summary>
        /// permite parar o contador a qualquer momento
        /// </summary>
        public void EmergencyStop()
        {
            OnFinished(EventArgs.Empty); //metodo disponivel para invocar evento da subclasse
        }
    }
    /// <summary>
    /// o mesmo que a classe CountDownEvent2, porém é criada uma estrutura que permite que subtipos invoquem eventos
    /// </summary>
    public class CountDownEvent3
    {
        protected uint _seconds;
        event EventHandler<TickEventArgs> Tick;
        event EventHandler Finished;

        public CountDownEvent3(uint seconds)
        {
            _seconds = seconds;
        }

        /// <summary>
        /// este metodo precisa ser Assincrono para evitar interrupção no fluxo do programa ao invocar Thread.Sleep. Por isso a presença da Background thread
        /// </summary>
        public void Start()
        {
            Tick += TickHandler;
            Finished += FinishedHandler;

            //lambda expression
            new Thread(() =>
            {
                uint n = _seconds;
                while (n > 0u)
                {
                   
                    OnTick(new TickEventArgs(n));
                    Thread.Sleep(1000);
                    n--;
                }

                OnFinished(EventArgs.Empty);
              
            }).Start();

            Console.ReadLine();
        }

        void TickHandler(object sender, TickEventArgs e)
        {
            Console.WriteLine(e.Seconds);
        }

        void FinishedHandler(object sender, EventArgs e)
        {
            Console.Beep();
            Tick -= TickHandler;
            Finished -= FinishedHandler;
        }

        protected virtual void OnTick(TickEventArgs e)
        {
            var tick = Tick; //evita problema com remoção de eventos ocorridas entre o if a seguir e a invocação do evento dentre dele
            if (tick != null)
                tick(this, e);
        }

        protected virtual void OnFinished(EventArgs e)
        {
            var finished = Finished;
            if (finished != null)
                finished(this, e);
        }

        protected class TickEventArgs : EventArgs
        {
            public uint Seconds { get; private set; }
            public TickEventArgs(uint seconds)
            {
                this.Seconds = seconds;
            }
        }

        

    }


}
