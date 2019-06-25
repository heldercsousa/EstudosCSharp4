using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EstudosCSharp.Generics
{
    internal static class PerformanceMeasure
    {
        internal static void Measure(string message, Action a)
        {
            var sw = new Stopwatch();
            sw.Start();
            a();
            sw.Stop();
            Console.WriteLine(message, sw.Elapsed);
        }
    }

    public static class MeasureTest01
    {
        const int N = 10000000;
        static int[] arrIntLisa = new int[N];
        static ArrayList arrList = new ArrayList(N);
        static List<int> genList = new List<int>(N);

        public static void MeasureArrayList()
        {
            PerformanceMeasure.Measure("Write ArrayList: {0}", () =>
            {
                for (int i=0; i < N; i++)
                    arrList.Add(1); //boxing esperado
            });
            PerformanceMeasure.Measure("Read ArrayList: {0}", () =>
            {
                int sum = 0;
                for (int i = 0; i < N; i++)
                    sum += (int)arrList[i]; //unboxing esperado
            });
            PrintGCStats();
            arrList.Clear();
        }

        /// <summary>
        /// armazenamento no array é fortemente tipado, portanto espera-se melhor resultado.
        /// </summary>
        public static void MeasureGenericList()
        {
            PerformanceMeasure.Measure("Write Generic List: {0}", () =>
            {
                for (int i = 0; i < N; i++)
                    genList.Add(1); //esperado uma checagem na capacidade da lista
            });
            PerformanceMeasure.Measure("Read Generic List: {0}", () =>
            {
                int sum = 0;
                for (int i = 0; i < N; i++)
                    sum += genList[i]; 
            });
            PrintGCStats();
            genList.Clear();
        }

        /// <summary>
        /// Mensura perfomance de um array int liso. Espera-se deste melhor perfomance que outros.
        /// </summary>
        public static void MeasureArrayIntLisa()
        {
            PerformanceMeasure.Measure("Write Array Liso de Int: {0}", () =>
            {
                for (int i = 0; i < N; i++)
                    arrIntLisa[i] = 1;
            });
            PerformanceMeasure.Measure("Read Array Liso de Int: {0}", () =>
            {
                int sum = 0;
                for (int i = 0; i < N; i++)
                    sum += arrIntLisa[i];
            });
            PrintGCStats();
            genList.Clear();
        }


        /// <summary>
        /// inspeciona o GC em busca de estatisticas que demonstram o numero de coletas realizadas até agora pelo GC
        /// </summary>
        static void PrintGCStats()
        {
            Console.WriteLine("Gen 0: {0}, Gen 1: {1}, Gen 2: {2}\n", GC.CollectionCount(1), GC.CollectionCount(2), GC.CollectionCount(3));
        }
        
    }
}
