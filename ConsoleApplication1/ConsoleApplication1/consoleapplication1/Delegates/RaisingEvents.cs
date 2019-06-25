using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EstudosCSharp.Delegates
{
    /// <summary>
    /// demonstração da forma correta de invocar um evento
    /// </summary>
    public static class RaisingEvents
    {
        static event Action myEvent;

        /// <summary>
        /// demonstra a forma errada de se invocar eventos, onde um erro de runtime, devido a concorrência 
        /// entre threads, pode ocorrer a qualquer momento. Não é possível prever quando exatamente ocorre.
        /// Com sorte, esse codigo irá imprimir Hi! algumas vezes antes de dar erro, ou haverá erro quase que imediato.
        /// </summary>
        public static void WrongWay()
        { 
            //Thread mimicking a client attaching e detaching handlers
            new Thread(() =>
            {
                while (true)
                {
                    Action a = () => Console.WriteLine("Hi!");
                    myEvent += a;
                    myEvent -= a;
                }
            }).Start();

            //main thread raises the event with too little protection
            while (true)
            {
                if (myEvent != null)
                    myEvent();
            }
        }

        /// <summary>
        /// forma correta de se invocar Eventos sem haver problema com concorrência. Trata-se do uso de uma variavel intermediaria para receber o conteúdo do myEvent. 
        /// Isso é possível porque ao usar o operadores += e -=, o CLR cria um novo MulticastDelegate e o retorna contendo o novo conteúdo do delegate, ou seja, é gerado um novo Multicast delegate.
        /// Por isso, é possivel criar uma var intermediaria para armazenar o conteudo corrente do multcastDelegate pois -= ou += feito por outra Thread não afetara essa var intermediaria.
        /// </summary>
        public static void CorrectWay()
        {
            new Thread(() =>
            {
                while (true)
                {
                    Action a = () => Console.WriteLine("Hi without erros!");
                    myEvent += a;
                    myEvent -= a;
                }
            }).Start();
                
            while(true)
            {
                var intermediateVariable = myEvent; //antes de invocar o evento, atribui-se seu valor a outra variavel, protegendo-se de remoção (-=) feita por outra Thread, pois ela pode ser feita exatamente entre o IF (event!=null) e a execução do event em si, o que resultaria num event==null.
                if (intermediateVariable != null)
                    intermediateVariable();
            }
        }

    }
}
