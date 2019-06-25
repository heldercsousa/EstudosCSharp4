using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EstudosCSharp.Delegates
{
    /// <summary>
    /// Anonymous Function Expressions and Closures
    /// Closures are the capability to capture the outer scope inside in the anonymous function expressions
    /// </summary>
    public static class AnonymousFunctionClosuresExamples
    {
        /// <summary>
        /// Add two numbers through a Delegate
        /// </summary>
        public static Func<int, int, int> addDelegate = (int a, int b) => a + b;
        /// <summary>
        /// Add two number through a Expression Tree
        /// </summary>
        public static Expression<Func<int, int, int>> addExpression = (int a, int b) => a + b;

        /// <summary>
        /// provide a way to pass the function via parameter, and performs the function over params a and b
        /// </summary>
        /// <param name="op">Func delegate that takes 2 arguments in and 1 argument out</param>
        /// <param name="a">any int number</param>
        /// <param name="b">any int number</param>
        /// <returns></returns>
        public static int Calculate(Func<int, int, int> op, int a, int b)
        {
            return op(a, b);
        }

        /// <summary>
        /// C# 2.0 feature. \rAqui é um exemplo onde um ValueType Int32 é modificado após ser associado a um Delegate que usa um Anonymous Function Expression.
        /// Closure é o nome dado à capacidade da function expression obter o valor de fora do escopo, mantendo assim uma 
        /// espécie de referência a um Type. Nesse caso, deixa de ser um ValueType e passa a ser armazenado no Heap. Dessa forma, uma referência,
        /// similar ao que aconteçe com parametros ref, é criada. Ao analisar o codigo no ILDSAML, uma classe "Display" é gerada contendo a "Anonymous Function Expression" 
        /// justamente para permitir a Closure! Essa classe contém todo os Estados capturados do espaço externo, e dois Anonymous Methods para serem usados pelo delegate;
        /// Devido a isso, enquanto ambos delegates a e b existirem, o GC não irá reivindicar o espaço de memória dos fields i e s. Os delegates tem um Lifetime diferente de outros tipos.
        /// Isso é chamado de Space Leak!(vazamento de espaço, os delegates nao permitem que os tipos que ele referencia sejam removidos da memoria).
        /// </summary>
        public static void ExampleSpaceLeak()
        {
            string s = "Foo";
            int i = 42;
            Action a = delegate() { Console.WriteLine(i); };
            Action b = delegate() { Console.WriteLine(s + "" + i); };
            i = 45;
            s = "aaaa";
            a();
            b();
            Console.WriteLine("enter to terminate");
            Console.Read();
        }

        /// <summary>
        /// C# 3.0 feature. Overloaded noised Lambda expression that comes from use of Anonymous Function Expressions
        /// </summary>
        public static void ExampleOfNoisedLambda()
        {
            int[] numbers = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var evens = numbers.Where(delegate(int i) { return i % 2 == 0; });
            var odds  = numbers.Where(delegate(int i) { return i % 2 != 0; });        
            
            /*Console.WriteLine("evens Type:" + );
            Console.WriteLine("odds:" + odds.ToString());
            Console.Read();*/
        }

        /// <summary>
        /// Overloaded yet noised Lambda expression that comes from use of Anonymous Function Expressions.
        /// </summary>
        public static void ExampleOfNoisedLambda02()
        {
            int[] numbers = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var evens = numbers.Where((int i) => i % 2 == 0);
            var odds = numbers.Where((int i) => i % 2 != 0);

            /*Console.WriteLine("evens Type:" + );
            Console.WriteLine("odds:" + odds.ToString());
            Console.Read();*/
        }

        /// <summary>
        ///Cleaned Lambda expression that comes from use of Anonymous Function Expressions. Compiler inference type from source, that is IEnumerable int generic"/>
        /// </summary>
        public static void ExampleOfNoisedLambda03()
        {
            int[] numbers = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var evens = numbers.Where(i => i % 2 == 0);
            var odds = numbers.Where( i => i % 2 != 0);

            /*Console.WriteLine("evens Type:" + );
            Console.WriteLine("odds:" + odds.ToString());
            Console.Read();*/
        }
    }
}
