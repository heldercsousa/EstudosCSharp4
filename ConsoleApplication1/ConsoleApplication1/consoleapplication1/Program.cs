using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EstudosCSharp.Generics;
using EstudosCSharp.Delegates;
using EstudosCSharp.Linq;
using EstudosCSharp.DesignPatterns;
using EstudosCSharp.BCL;

namespace EstudosCSharp
{
    class Program
    {

        static CountDownEvent2 countDown1;

        static void Main(string[] args)
        {

            //construindo um filtro genérico
            /* 
             */




            //testando jeito imperativo de codificar
            /*PrintProductList(Fundamentals.GetProductsLowerThan(25));
            PrintProductList(Fundamentals.GetDBProductsLowerThan25());
            PrintProductList(Fundamentals.GetXMLProductsLowerThan25());
            PrintProductList(Fundamentals.GetLinqProductsLowerThan(25));
            PrintDictionaryList(Fundamentals.RankingByEachTen());
            PrintDictionaryList(Fundamentals.RankingByEach10Linq());
            PrintLinqProductList(Fundamentals.GetLinqToSQLProductsLowerThan(25));
            PrintProductList(Fundamentals.GetLinqToXMLProductsLowerThan(25m));
            PrintStringList(Fundamentals.GetProductNames());
            Fundamentals.GetWindowsProcesses();
            Fundamentals.GetEnumerableMethods();
            Fundamentals.PrintAverageByCategory();
            Fundamentals.PrintByCategoryOrderedByPrice();
            Fundamentals.SquareCubeStars();
            Internals.GetEnumerableStaticMethods();
            Internals.GetOddEvensNumbers();
            Internals.NonGenericListOddNums();
            Internals.InfiniteLoopIterator();
            Internals.IteratorCountDown();
            Internals.AGenericFilter();
            Internals.MultipleOfSix();
            Internals.LazyEvaluation();
            Internals.NotLazillyEvaluated();
            Internals.SourceGenerators();
            Internals.Restrictions();
            Internals.PagingDataSource(10);
            //Internals.SingleElement<string>(new[] { "helder", "sousa", "carvalho", "helder", "carvalho" }, x => x=="helder");//erro pq tem mais que um
            Internals.SingleElement<string>(new[] { "helder", "sousa", "carvalho", "helder", "carvalho" }, x => x == "sousa");//ok, aqui nao gera erro.
            var s = new[] { "helder", "sousa", "carvalho", "helder", "carvalho" }.Single(x => x == "sousa");//ok, aqui nao gera erro.
            Internals.OfTypeExample();
            Internals.NumberedList();
            Internals.IteratorExample();
            Enumerable.Range(5, 6).StartWith(4).StartWith(3).StartWith(2).StartWith(1).PrintCollection();
            Internals.SelectManyEx1();
            Internals.SelectManyEx3();
            Internals.ZipExample();
            Internals.ZipExample2();
            Internals.ZipExample3();
            Internals.GroupByExample();
            Internals.GroupByEx1();
            Internals.OrderingExample();
            Fundamentals.PrintAverageByCategory();*/

            /* ObjAdaptar ob = new ObjAdaptar();
             ob.FazerAlgo();
             ob = new Adapter(new ObjAdaptador());
             ob.FazerAlgo();
             Console.ReadLine();
             */

            /*invocando eventos de dentro de Subtipos*/
            /*var countDown = new CountDownSubtype();
            new Thread(() =>  //faz uma parada de emergência apos quatro segundos.
                {
                    Thread.Sleep(4000);
                    countDown.EmergencyStop();
                }).Start();
            countDown.Start();
            */

            /*detach handlers in countdown usando EventHandlers*/
            /*
            countDown1 = new CountDownEvent2(5);
            countDown1.Tick += countDown1_Tick;
            countDown1.Finished += countDown1_Finished;
            countDown1.Start();
            Console.ReadLine();
            */


            /*detach handlers in countdown -- certo*/
            /*
            var countDown = new CountDownEvent(5);
            Action<uint> tickHandler = currentSecond => Console.WriteLine(currentSecond);
            Action finishedHandler = null;//essa variavel esta pre atribuida
            Action __temp = () =>
            { 
                Console.Beep();
                countDown.Tick -= tickHandler;
                countDown.Finished -= finishedHandler;
            };
            finishedHandler = __temp;
            countDown.Tick += tickHandler;
            countDown.Finished += finishedHandler;
            countDown.Start();
            Console.ReadLine();*/

            /*detach handlers in countdown -- errado*/
            /*var countDown = new CountDownEvent(5);
            Action<uint> tickHandler = currentSecond => Console.WriteLine(currentSecond);
            Action finishedHandler = () => { //recursive lambda expression doesnt compile
                Console.Beep();
                countDown.Tick -= tickHandler;
                countDown.Finished -= finishedHandler;
            }
            countDown.Tick += tickHandler;
            countDown.Finished += finishedHandler;
            countDown.Start();
            Console.ReadLine();*/

            /*Detach handlers*/
            //DetachHandlersWrongly.Do();
            //DetachHandlers.Do();

            /*Events test*/
            //RaisingEvents.WrongWay();
            //RaisingEvents.CorrectWay();


            /*delegates tests 001*/
            /*Console.WriteLine("AddDelegate: " + AnonymousFunctionClosuresExamples.addDelegate(1, 2).ToString());
            Console.WriteLine("Calculte 1+2: " + AnonymousFunctionClosuresExamples.Calculate((x, y) => x + y, 1, 2));
            Console.WriteLine("Calculte 3*5: " + AnonymousFunctionClosuresExamples.Calculate((x, y) => x * y, 3, 5));
            Console.Read();*/

            /*delegates tests 002*/
            /*var countDown = new CountDownDelegate(5);
            //countDown.Tick = currentSecond => Console.WriteLine(currentSecond);//lambda expressions
            //countDown.Finished = () => Console.Beep();//lambda expressions
            countDown.Tick = delegate(uint currentSecond) { Console.WriteLine(currentSecond); };//anonymous function expressions
            countDown.Finished = delegate() { Console.Beep(); };//anonymous function expressions
            countDown.Start();
            Console.ReadLine();*/

            /*delegates tests 003*/
            /* var countDown = new CountDownEvent(5);
             countDown.Tick += currentSecond => Console.WriteLine(currentSecond);//lambda expressions
             countDown.Finished += () => Console.Beep();//lambda expressions
             countDown.Start();
             Console.ReadLine();*/

            //Console.WriteLine("AddExpression: ")  +AnonymousFunctionClosuresExamples.addExpression(1,2));
            //AnonymousFunctionClosuresExamples.ExampleOfNoisedLambda();
            // AnonymousFunctionClosuresExamples.ExampleSpaceLeak();


            /*Generics Tests*/
            // TryOrderedList();

            /*Generics Performance Test*/
            /*MeasureTest01.MeasureArrayList();
            Console.ReadLine();
            MeasureTest01.MeasureGenericList();
            Console.ReadLine();
            MeasureTest01.MeasureArrayIntLisa();
            Console.ReadLine();*/

            /*Generics Default Constructor Constraint test*/
            /* Factory<int> fct = new Factory<int>();
             var zero = fct.CreateInstance(); //aceito pq tem um construtor default
             Console.WriteLine("zero:" + zero);
             //var fctString = new Factory<string>(); //rejeitado pq não tem um default constructor sem parametros
             Console.ReadLine();
             * */


            /* BCL studies */
            //CultureFormatting.TryParseNumbersByCulture();
            //CultureFormatting.PrintTimeZone();
            //CultureFormatting.PrintTimeZoneInfo();
            //CultureFormatting.TestCommandLine(args);
            //CultureFormatting.MyAppRunsFrom();
            //CultureFormatting.ChildProcess();
            //CultureFormatting.FormatingNumberToString();
            CultureFormatting.Sifrao();

        }

        static void countDown1_Finished(object sender, EventArgs e)
        {
            Console.Beep();
            countDown1.Tick -= countDown1_Tick;
            countDown1.Finished -= countDown1_Finished;
        }

        static void countDown1_Tick(object sender, CountDownEvent2.TickEventArgs e)
        {
            Console.WriteLine(e.Seconds);
        }


        static internal void TryOrderedList()
        {
            var orderedList = new OrderedList<int>();
            var rand = new Random();
            int z = 0;
            Console.WriteLine("adding");
            for (int i = 0; i < 100; i++)
            {

                z = rand.Next(5000);
                orderedList.Add(z);
                Console.Write(z.ToString() + "/");
            }

            bool faulty = false;
            int prev = -1;

            foreach (var n in orderedList)
            {
                if (prev > n)
                {
                    faulty = true;
                    break;
                }
            }
            Console.WriteLine(!faulty);
            Console.WriteLine("*");
            Console.WriteLine("*");
            Console.WriteLine("*");
            foreach (var n in orderedList)
            {
                Console.Write(n + "/");
            }
            Console.WriteLine("Enter to terminate!");
            Console.Read();
        }

        static void PrintProductList(List<Product> produtos)
        {
            Console.WriteLine("----- produtos ");
            foreach (var item in produtos)
	        {
                Console.WriteLine(string.Format("---------- nome:{0} preço:{1}", item.Name, item.Price));
            }
        }

        static void PrintLinqProductList(List<EstudosCSharp.LinqToSQL.Produto> produtos)
        {
            Console.WriteLine("----- produtos ");
            foreach (var item in produtos)
            {
                Console.WriteLine(string.Format("---------- nome:{0} preço:{1}", item.Name, item.Price));
            }
        }

        static void PrintDictionaryList(Dictionary<int,List<Product>> grupos)
        {
            foreach (var item in grupos)
            {
                Console.WriteLine(string.Format("Id grupo:{0} ", item.Key.ToString()));
                PrintProductList(item.Value);
            }
        }

        static void PrintStringList(IEnumerable<string> l)
        {
            Console.WriteLine("----- lista ");
            foreach (var item in l)
                Console.WriteLine(string.Format("---------- txt:{0}", item.ToString()));
        }


    }

}
