using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace EstudosCSharp.BCL.Diagnostics.DevelopmentTime
{
    /// <summary>
    /// samples to show how to use performance counters
    /// </summary>
    public static class PerfomanceCounter
    {
        const string SOURCE = "EstudosCSharp";
        const string DESCRIPTION = "Sample performance counters";
        const string TXCOUNT = "# of transactions";
        const string PROCRATE = "Processing rate";
        const string PROCRATEBASE = "Processing rate base";

        /// <summary>
        /// mimicing a (bank) process cycle, which includes many transactions incoming and outcoming along the time.
        /// </summary>
        public static void MimicBankTransaction()
        {
            if (!PerformanceCounterCategory.Exists(SOURCE))
            { 
                var counter1 = new CounterCreationData(SOURCE, "Monitora o número de transferências realizadas(NumberOfItems32)", PerformanceCounterType.NumberOfItems32);
                var counter2 = new CounterCreationData(PROCRATE, "Média de requisições de transações(AverageCount64)", PerformanceCounterType.AverageCount64);
                var counter3 = new CounterCreationData(PROCRATEBASE, "Deniminador para requisições de transações(AverageBase)", PerformanceCounterType.AverageBase);

                var dataCollection = new CounterCreationDataCollection();
                dataCollection.Add(counter1);
                dataCollection.Add(counter2);
                dataCollection.Add(counter3);

                var category = PerformanceCounterCategory.Create(
                    SOURCE,
                    DESCRIPTION,
                    PerformanceCounterCategoryType.SingleInstance,
                    dataCollection);
            }

            var txCount = new PerformanceCounter(SOURCE, TXCOUNT, false);
            var procRate = new PerformanceCounter(SOURCE, PROCRATE, false);
            var procRateBase = new PerformanceCounter(SOURCE, PROCRATEBASE, false);

            Console.WriteLine("Pressione ENTER para iniciar...");
            Console.ReadLine();

            var ran = new Random();
            var queue = new ConcurrentQueue<int>();

            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(ran.Next(1000, 5000));
                    int n = 0;
                    int tx;
                    while (queue.TryDequeue(out tx))
                    {
                        Console.WriteLine("Processando transacao {0}", tx);
                        Thread.Sleep(ran.Next(0, 100));
                        txCount.Decrement();
                    }

                    procRate.IncrementBy(n);
                    procRateBase.Increment();
                }
            })
            .Start();

            for (int tx = 0; true; tx++)
            {
                Console.WriteLine("Requesting transaction {0}", tx);
                queue.Enqueue(tx);
                txCount.Increment();
                Thread.Sleep(ran.Next(0, 200));
            }

    }


        /// <summary>
        /// Shows all Peformance Counter Categories available in the machine
        /// </summary>
        public static void AllCategoriesAvailable()
        {
            var categorias = PerformanceCounterCategory.GetCategories().Select(x => x.CategoryName);
            var lst = string.Join(Environment.NewLine, categorias);
            Console.WriteLine(lst);
        }

        /// <summary>
        /// 1º create a category
        /// 2º create one or more counters as part of that category
        /// 3º for each of counters, set CategoryName, CounterName, MachineName and ReadOnly
        /// </summary>
        public static void CreateCategory()
        {
            //    var categoryName = "EstudosCSharpCategoryName";

            //    var category = PerformanceCounterCategory.Create(
            //        categoryName, 
            //        "EstudosCSharpHelp",
            //        PerformanceCounterCategoryType.MultiInstance, 
            //        "EstudosCSharpCounterName", 
            //        "EstudosCSharpCounterHelp");
        }

       

       
    }
}
