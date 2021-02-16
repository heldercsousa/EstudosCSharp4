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
using System.Collections.Concurrent;
using System.IO;

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
            PrintDictionaryList(Fundamentals.RankingByEach10Linq());*/
            //PrintLinqProductList(Fundamentals.GetLinqToSQLProductsLowerThan(25));
            /*
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


            /****** BCL studies *******/
            //CultureFormatting.TryParseNumbersByCulture();
            //CultureFormatting.PrintTimeZone();
            //CultureFormatting.PrintTimeZoneInfo();
            //CultureFormatting.TestCommandLine(args);
            //CultureFormatting.MyAppRunsFrom();
            //CultureFormatting.ChildProcess();
            //CultureFormatting.FormatingNumberToString();
            //CultureFormatting.Sifrao();
            //CultureFormatting.DateTimeFormats();
            //FormattingByHand.UsingStringFormat();

            ////// PerfomanceCounter
            //BCL.Diagnostics.DevelopmentTime.PerfomanceCounter.MimicBankTransaction();

            ////// Assertions
            //BCL.Diagnostics.DevelopmentTime.Assertions.DebugIndent();
            //BCL.Diagnostics.DevelopmentTime.Assertions.DebugAssert(0, 1);
            //BCL.Diagnostics.DevelopmentTime.Assertions.DebugFail(0, 1);
            //BCL.Diagnostics.DevelopmentTime.Assertions.DebugAssert(2, 1);
            //BCL.Diagnostics.DevelopmentTime.Assertions.DebugFail(2, 1);
            //Console.Read();

            ////// StackTrace
            //BCL.Diagnostics.DevelopmentTime.StackTrace.DecomposeStackTraceFrames();
            //BCL.Diagnostics.DevelopmentTime.StackTrace.CreateStackTraceWithFileInfo();
            //Console.Read();

            ////// EventLogs
            //BCL.Diagnostics.Instrumentation.EventLogs.CreateEventSource();
            //BCL.Diagnostics.Instrumentation.EventLogs.WriteLogToEventSource();

            ////// PROCESS
            //BCL.Diagnostics.Processes.ProcessesInfo.QueryProcessesInfoGreaterThan(50);
            //BCL.Diagnostics.Processes.ProcessesInfo.StartProcess("cmd.exe");
            ////BCL.Diagnostics.Processes.ProcessesInfo.StartProcess("https://www.youtube.com/watch?v=qIikGK0MmBE&list=RDMMC8mS1CVXjys&index=3");
            //BCL.Diagnostics.Processes.ProcessesInfo.StartProcessAsync("cmd.exe");
            //BCL.NamedParams.AllowedNamedPositionalParams();
            //Console.WriteLine("press some key to exit");
            //Console.Read();

            ////// NET
            //NET.WebRequest.RequestCatsDataWebRequest();
            //NET.WebRequest.RequestCatsDataHttpWebRequest();
            //NET.WebRequest.RequestCatsDataHttpWebRequestAndWritesFile();
            //NET.WebClient.RequestCatsData();
            //NET.WebClient.DownloadFile();
            //NET.WebClient.DownloadFileAsync();
            //NET.WebClient.DownloadFileData();
            //NET.HttpClient.RequestCatsData();
            //Console.Read();

            ////// IO - sync read 
            //IO.FileSystemActivity.Watch(Path.Combine(@"c:\", "temp"), "*.*");
            //IO.Sync.FileRead.ReadAllLines(@"..\..\Program.cs");
            //IO.Sync.FileRead.ReadAllText(@"..\..\Program.cs");
            //IO.Sync.FileRead.ReadAllBytes(@"..\..\Program.cs");
            //IO.Sync.FileRead.ReadLines(@"..\..\Program.cs");
            //IO.Sync.FileWrite.WriteAllLines(@"..\..\Program.cs", @"..\..\Program.bak");
            //IO.Sync.FileWrite.AppendAllLines(new string[] { "Helder", "Sousa", "Appended as lines" }, @"..\..\Program.bak");
            //IO.Sync.FileWrite.AppendAllText("Helder Sousa Appended as a single text", @"..\..\Program.bak");
            //IO.Sync.FileWrite.FileCreateText(@"..\..\Log.cs");
            //IO.Sync.FileWrite.BinaryFormatterSerialize();
            //IO.Sync.FileWrite.BinaryFormatterDeserialize();
            //IO.Sync.FileRead.FileOpenText(@"..\..\Log.cs");
            //IO.Sync.FileRead.ReaderRead(@"..\..\Log.cs");
            //IO.Sync.FileRead.ConsoleInOut();
            //IO.Sync.FileRead.ConsoleSetIn(@"..\..\Program.cs");
            //IO.Sync.FileRead.StringReaderAndWriter();
            //IO.Sync.Stream.TwoMemoryStreamsOverSingleDataSync();
            //IO.Sync.Stream.FileStreamFromAnExeSync();
            //IO.Sync.Stream.BinaryReaderGetsFileSignatureSync();
            //IO.Async.Stream.FileStreamAsync();
            //IO.Async.Stream.FileStreamWithStateAsync();
            //// delegate Invocation init
            //var h = IO.Async.Stream.DelegateInvocation();
            //while (!h.IsCompleted)
            //    continue;
            //Thread.Sleep(200);
            //// delegate Invocation fim
            //IO.NamedPipes.Sample(); // creates a server
            //IO.NamedPipes.Sample("server"); // creates a client of the server (instatiate in another console to see them sending message in server to client direction);

            //Security.Crypto.Symmetric.Aes.Encrypt(@"..\..\Log2.cs");
            //Security.Crypto.Symmetric.Aes.Decrypt(@"..\..\Log2.cs");
            //Security.UnhandledException.Sample();

            //Systems.WeakReference.Test();
            //Systems.Dynamic.ExpandoObject.Sample();

            //Assemblyy.Samples.Load();

            //Threads.Samples.CancellationTokenSource();
            //Threads.Samples.TaskCompletionSource();
            //Threads.Samples.TaskFromAsync("EstudosCSharp.exe");
            //Threads.Samples.ContinueWhenAll(new[] { "EstudosCSharp.exe", "EstudosCSharp.pdb" });
            //Threads.Samples.WaitAll();

            //Interfaces.VisibilitySample.Sample();

            Regexs.Samples.Pattern1();
            Regexs.Samples.Pattern2();
            Regexs.Samples.Pattern3();
            Regexs.Samples.Pattern4();
            Regexs.Samples.Pattern5();
            Regexs.Samples.Pattern6();
            Regexs.Samples.Pattern7();
            Regexs.Samples.Pattern8();
            Regexs.Samples.Pattern9();
            Regexs.Samples.Pattern10();
            Regexs.Samples.Pattern11();
            Regexs.Samples.Pattern12();
            Regexs.Samples.Pattern13();
            Regexs.Samples.Pattern14();
            Regexs.Samples.Pattern15();
            Regexs.Samples.Pattern16();
            Regexs.Samples.Pattern17();
            Regexs.Samples.Pattern18();
            Regexs.Samples.Pattern19();
            Regexs.Samples.Pattern20();
            Regexs.Samples.Pattern21();
            Regexs.Samples.Pattern22();
            Regexs.Samples.Pattern23();
            //LinqToXML.Samples.LoadDescendantAtrribute();
            //LinqToXML.Samples.LoadDescendantElementGreaterThan();
            //LinqToXML.Samples.CreateXMLTree();
            //LinqToXML.Samples.ParentPropertyOfChildNodes();
            //LinqToXML.Samples.AddTextCreateOrNotANode();
            //LinqToXML.Samples.EmptyNodeValueDoesntDeleteIt();
            //LinqToXML.Samples.NodeTextSerialization();
            //LinqToXML.Samples.NamespacesAreAttributes();
            //LinqToXML.Samples.XPathAxis();
            //LinqToXML.Samples.DeclarationIsProperty();
            //XMLDOM.Samples.FormerXmlDocument();
            //XMLDOM.Samples.AlternateToFormer();
            //XMLDOM.Samples.XmlAttributeOverriding();

            Console.Write("press any key to exit");
            Console.ReadKey();
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
