using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstudosCSharp.Delegates
{
    static class SystemEvents
    {
        public static event Action ShuttingDown;
    }

    class Handlers
    {
        public void OnShutDown()
        {

        }

        ///finalizer para provar ou desprovar se o Handlers foi reivindicado pelo GC
        ~Handlers() { 
            Console.WriteLine("Handlers got collected");
        }
    }

    /// <summary>
    /// Event handlers tendem a ser fontes comuns de vazamento de memoria quando nao sao mantido corretamente.
    /// </summary>
    public static class DetachHandlersWrongly
    {
        public static void Do()
        {
            var handlers = new Handlers();
            SystemEvents.ShuttingDown += handlers.OnShutDown;//aqui, está sendo feita uma referência para a Instância de Handlers, o que impede que o GC colete este Tipo. Esta linha é traduzida para algo parecido com System.add_ShuttingDown(new Action(&handlers, OnShutDown));, onde o nome da instancia e o nome do metodo sao usados como ref

            handlers = null; /// nesse ponto, parece que ninguem mais faz referencia para o objeto Handlers. Entao, ele estaria disponivel para o GC. Porem, uma referencia a ele existe no SystemEvents.ShuttingDown

            GC.Collect();
            Console.WriteLine("Waiting For Pending Finalizers");
            GC.WaitForPendingFinalizers();
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Forma correta de remover Event handlers 
    /// </summary>
    public static class DetachHandlers
    {
        public static void Do()
        {
            var handlers = new Handlers();
            SystemEvents.ShuttingDown += handlers.OnShutDown;//aqui, está sendo feita uma referência para a Instância de Handlers, o que impede que o GC colete este Tipo. Esta linha é traduzida para algo parecido com System.add_ShuttingDown(new Action(&handlers, OnShutDown));, onde o nome da instancia e o nome do metodo sao usados como ref

            SystemEvents.ShuttingDown -= handlers.OnShutDown;//nao ha mais interesse no event handler. Deve ser removido antes de setar handlers para null, ou irá acontecer uma exceção
            handlers = null;
                  
            GC.Collect();
            Console.WriteLine("Waiting For Pending Finalizers");
            GC.WaitForPendingFinalizers();
            Console.ReadLine();
        }
    }

    /// <summary>
    /// aqui, parece que estamos removendo o mesmo anonimous method, porém nao estamos. Anonimous Methods sao compilados para metodos distintos, apesar de serem semanticamente identicos.
    /// </summary>
    public static class DetachAnonymousHandlersWrongly
    {
        public static void Do()
        {
            Action evt = null;
            evt += () => Console.WriteLine("Hi"); /// isso compila para evt+=new Action(<>__AnonymousMethod1);
            if (evt != null)
                evt();
            evt -= () => Console.WriteLine("Hi");/// isso compila para evt+=new Action(<>__AnonymousMethod2);
            if (evt != null)
                evt();
        }
    }

    /// <summary>
    /// usando uma variavel intermediaria para fazer referencia a um metodo em comum
    /// </summary>
    public static class DetachAnonymousHandlers
    {
        public static void Do()
        {
            Action evt = null;
            Action handler = () => Console.WriteLine("Hi");
            evt += handler;
            if (evt != null)
                evt();
            evt -= handler;
            if (evt != null)
                evt();
        }
    }


}
