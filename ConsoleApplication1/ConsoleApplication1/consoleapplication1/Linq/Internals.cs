using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace EstudosCSharp.Linq
{
    /// <summary>
    /// charpter 20 - Linq internals
    /// </summary>
    static class Internals
    {
        /// <summary>
        /// Because most nongeneric enumerable sequences are homogenous concerning the objects they yield, a 
        /// common operation is to cast all elements to a specific type. LINQ provides such an operator,
        /// called Cast
        /// </summary>
        public static void FromArrayListToStrongType()
        {
            var lst = new ArrayList { 1, 2, 3 }; // Old-fashioned List<int>

            // Two equivalent ways to provide stronger typing, using Cast<T>.
            IEnumerable<int> lstInts1 = lst.Cast<int>();
            IEnumerable<int> lstInts2 = from int i in lst select i;

            /*
            foreach (int num in lstInts1)
            {
                var i = 0;
                // Do something
            }

            //The preceding foreach is turned into the following equivalent form.
            //remember foreach is just a sintatical sugar to that following:
            using (var e = lstInts1.GetEnumerator()) 
            {
                while (e.MoveNext()) 
                {
                    int num = e.Current;
                    // Do something
                }
            }
            */
        }

        /// <summary>
        /// display all the Enumerable methods together with the overload count. Adding a 
        /// few more clauses to only show static methods (because extension methods are always static) 
        /// and to order operators alphabetically. Há um metodo parecido no namespace Fundamentals
        /// </summary>
        public static void GetEnumerableStaticMethods()
        {
            var queryOperators = from m in typeof(Enumerable).GetMethods()
                                 where m.IsStatic
                                 group m by m.Name into queryOperator
                                 orderby queryOperator.Key /* Key is method name m.Name */
                                 select new
                                 {
                                     Name = queryOperator.Key,
                                     Overloads = queryOperator.Count()
                                 };

            foreach (var queryOperator in queryOperators)
            {
                Console.WriteLine("{0} has {1} overload(s)", queryOperator.Name, queryOperator.Overloads);
            }

        }

        public static void GetOddEvensNumbers()
        {
            ///numeros pares e impares com Linq e equivalente em Lambda
            var nums = new List<int> { 1, 2, 3, 4, 5 };
            // Without query expression syntax
            var evens = nums.Where(n => n % 2 == 0);
            // With query expression syntax
            var odd = from n in nums
                      where n % 2 != 0
                      select n;

            Console.WriteLine("Evens");
            foreach (var item in evens)
            {
                Console.Write(String.Format("{0},", item.ToString()));
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Odds");
            foreach (var item in odd)
            {
                Console.Write(String.Format("{0},", item.ToString()));
            }
        }

        public static void NonGenericListOddNums()
        {
            ///numeros impares com Linq em uma lista não generica
            var nums = new ArrayList { 1, 2, 3, 4, 5 };
            var odd = from int n in nums
                      where n % 2 != 0
                      select n;

            Console.WriteLine("Odds in Nongeneric list");
            foreach (var item in odd)
            {
                Console.Write(String.Format("{0},", item.ToString()));
            }
        }

        public static void InfiniteLoopIterator()
        {
            //loop infinito em um Iterator
            var nums = GetIntegers();
            foreach (var item in nums)
            {
                Console.WriteLine(item);
            }
        }

        static IEnumerable<int> GetIntegers()
        {
            int n = 0;
            while (true)
                yield return n++;
        }

        public static void IteratorCountDown()
        {
            //CountDown com Iterator
            var countDown = CountDown();
            while (countDown.MoveNext())
                Console.WriteLine(countDown.Current);

        }

        static IEnumerator<int> CountDown()
        {
            Console.WriteLine("Before three");
            yield return 3;
            Console.WriteLine("After three");
            Console.WriteLine("Before two");
            yield return 2;
            Console.WriteLine("After two");
            Console.WriteLine("Before one");
            yield return 1;
            Console.WriteLine("After one");
            Console.WriteLine("Before zero");
            yield return 0;
        }

        public static void AGenericFilter()
        {
            var x = new[] { 2, 5, 7, 4, 1, 9, 5, 8 };
            //var evens = Filter(x, n => n % 2 == 0);
            var evens = Filter(new[] { 2, 5, 7, 4, 1, 9, 5, 8 }, n =>
            {
                Console.WriteLine("Filtering " + n);
                return n % 2 == 0;
            });
            foreach (var item in evens)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// semelhante ao extension method Where do Linq, presente na classe statica Enumerable
        /// Perceba que o Iterator foi removido de dentro deste metodo generico para permitir que as 
        /// exceções sejam validadas no momento da declaração da Query Expression que usa o metodo Filter.
        /// Caso contrário, a validação seria feita apenas no momento em que inicia-se a enumerar sobre o iterator.
        /// A remoção do Yield tornou este metodo em um método regular
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        static IEnumerable<T> Filter<T>(IEnumerable<T> source, Func<T, bool> filter)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (filter == null)
                throw new ArgumentNullException("filter");
            

            // A hypothetical anonymous iterator (hence it won’t compile!)
            /*return new IEnumerable<T> {
                foreach (T item in source)
                    if (filter(item))
                        yield return item;
            };*/

            return FilterInternal(source, filter);
        }

        /// <summary>
        /// metodo do tipo iterator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static IEnumerable<T> FilterInternal<T>(IEnumerable<T> source,Func<T,bool> filter)
        {
 	        foreach (T item in source)
                if (filter(item))
                    yield return item;
        }

        /// <summary>
        /// demonstração de como fica mais natural codificar com os Extension Methods, ao oposto de fazer Nesting de metodos
        /// </summary>
        public static void MultipleOfSix()
        {
            var multiplesOfSix = Filter(
                Filter(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, n => n % 2 == 0),
                n => n % 3 != 0
            );

            var multiplesOfSix2 = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Where(n => n % 2 == 0).Where(n => n % 3 != 0);
            /*
             * Internamente, essa cadeia de metodos se transforma no codigo a seguir (Engenharia reversa do Linq(Query Operators) uysando Iterators, Lambda Expressions e Extension Methods.
               var multiplesOfSix = 
                      Enumerable.Where(
                          Enumerable.Where(new [] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, n => n % 2 == 0),
                          n => n % 3 != 0
                      );
             * */

            Console.WriteLine("Filter generico:");
            foreach (var item in multiplesOfSix)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Filter Where do Linq:");
            foreach (var item in multiplesOfSix2)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Devido às Query Expressions serem implementadas usando Iterators, elas são executadas apenas On-Demand(na chamada ao MoveNext do iterator, como visto antes neste namespace).
        /// Este comportamento tardio (Lazy Evaluation)é muito desejado porque os dados que serão analisado podem mudar entre o momento da declaração da Query Expression com o momento em que eles são de fato consumidos.
        /// Portanto, Query Expression não é nada mais do que uma "declaração de intenção".
        /// </summary>
        public static void LazyEvaluation()
        {
            var nums = new List<int> { 2, 5, 7, 4, 1, 9 };
            var evens = from n in nums
                        where n % 2 == 0
                        select n;

            // Shows 2, 4
            Console.WriteLine("Before changing the Nums list");
            foreach (var n in evens)
                Console.WriteLine(n);

            // Somehow the collection changes, e.g. because the user edits the data.
            Console.WriteLine("After adding to the Nums list 5 and 8, showing the lazy behavior");
            nums.Add(5);
            nums.Add(8);

            // Shows 2, 4, 8 - no need to redeclare the query
            foreach (var n in evens)
                Console.WriteLine(n);
        }

        /// <summary>
        /// Um exemplo que mostra uma ocasião onde a expressão é avaliada na mesma hora, e onde a validação tardia não ocorre.
        /// </summary>
        public static void NotLazillyEvaluated()
        {
            var nums = new List<int> { 2, 5, 7, 4, 1, 9 };
            var evens = (from n in nums
                        where n % 2 == 0
                         select n).ToList();

            // Shows 2, 4
            Console.WriteLine("Before changing the Nums list");
            foreach (var n in evens)
                Console.WriteLine(n);

            // Somehow the collection changes, e.g. because the user edits the data.
            Console.WriteLine("After adding to the Nums list 5 and 8, dont show 5 and 8 beacuse ToList makes query expressions be evaluated imediatelly");
            nums.Add(5);
            nums.Add(8);

            // Still shows 2, 4 since ToList caused the query to execute on the spot,
            // conceptually creating a snapshot of query results at that point in time.
            foreach (var n in evens)
                Console.WriteLine(n);
        }


        /// <summary>
        /// demonstração de Source Generator. Range oferece uma forma de criar uma sequencia facilmente
        /// </summary>
        public static void SourceGenerators()
        {
            IEnumerable<int> firstTen = Enumerable.Range(0, 10);
            foreach (var item in firstTen)
            {
                Console.Write(item + ",");
            }

            Console.WriteLine();
            IEnumerable<char> aToz = Enumerable.Range(0,26).Select(i=> (char)('a' + i));
            foreach (var item in aToz)
            {
                Console.Write(item + ",");
            }

            Console.WriteLine();
            IEnumerable<string> heyHeyHey = Enumerable.Repeat("Hey", 3);
            foreach (var item in heyHeyHey)
            {
                Console.Write(item + ",");
            }

            Console.WriteLine();
            var nothing = Enumerable.Empty<int>(); //retorna uma sequencia vazia
            foreach (var item in nothing)
            {
                Console.Write(item + ",");
            }

            Console.WriteLine();
            var res1 = nothing.DefaultIfEmpty();
            var res2 = nothing.DefaultIfEmpty(99);
            var res3 = Enumerable.Range(0, 10).DefaultIfEmpty();
            Console.Write("DefaultIfEmpty() :");
            foreach (var item in res1)
            {
                Console.Write(item + ",");
            }
            
            Console.WriteLine();
            Console.Write("DefaultIfEmpty(99) :");
            foreach (var item in res2)
            {
                Console.Write(item + ",");
            }

            Console.WriteLine();
            Console.Write("Enumerable.Range(0, 10).DefaultIfEmpty() :");
            foreach (var item in res3)
            {
                Console.Write(item + ",");
            }
        }


        public static void Restrictions()
        {
            // { "Bart”, “Lisa” }
            var res = new [] { "Bart", "Homer", "Lisa" }.Where(s => s.Length == 4);

            // { “Zero”, “Two” }
            var res2 = new [] { "Zero", "One", "Two", "Three" }.Where((s, i) => i % 2 == 0);
            /*Here, s stands for the string objects in the input sequence, while i stands for the index of
            the object in the input sequence. We’re not using s (though we obviously could) to do the
            index-based filtering here.*/


            var nums = new[] { 5, 2, 1, 3, 1, 4, 5 }.Distinct(); // { 5, 2, 1, 3, 4 }
            //static IEnumerable<T> Distinct<T>(this IEnumerable<T> source,IEqualityComparer<T> comparer); //olhar Collection Types (capitulo 16) para entender como usar o IEqualityComparer

            var res3 = Enumerable.Range(0, 10).Skip(5); // { 5, 6, 7, 8, 9 }
            var res4 = Enumerable.Range(0, 10).SkipWhile(n => n < 8); // { 8, 9 }
            //Notice SkipWhile is obviously not the same as a simple Where
            var res5 = new [] { 1, 2, 3, 4, 3, 2, 1 }.SkipWhile(n => n < 4);// { 4, 3, 2, 1 } 
            var res6 = new[] { 1, 2, 3, 4, 3, 2, 1 }.Where(n => n >= 4); // { 4 }
            //The opposite of “to skip” is “to take”
            var res7 = Enumerable.Range(0, 10).Take(5); // { 0, 1, 2, 3, 4 }
            var res8 = Enumerable.Range(0, 10).TakeWhile(n => n < 8);// { 0, 1, 2, 3, 4, 5, 6, 7 }
        }

        /// <summary>
        /// demonstra como paginar um dataSource com Take e Skip
        /// </summary>
        public static void PagingDataSource(int elementsPerPage)
        {
            // For example, page 3 = Skip(30).Take(10)
            var source = Enumerable.Range(0, 100);
            var page = 0;
            
            while (page <= source.Count()/elementsPerPage)
            {
                Console.Clear();
                Console.WriteLine("Showing page " + page);
                var onePageOfData = source.Skip(page * elementsPerPage).Take(elementsPerPage);
                foreach (var item in onePageOfData)
                {
                    Console.WriteLine("" + item);
                }
                Console.ReadKey(false);             
                page++;
            }
        }

        /// <summary>
        /// Single example
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static T SingleElement<T>(IEnumerable<T> elements, Func<T, bool> filter)
        {
            return elements.Single(filter);
            //return elements.Single();
        }

        /// <summary>
        /// OfType using example 
        /// </summary>
        public static void OfTypeExample()
        {
            var src = new Object[] { "helder", 2, "sousa", 3, 5 };
            //IEnumerable<object> res = src.Where(x => x is int);//works fine but has the disavantage of not performing a cast over all the elements.
            //IEnumerable<int> res = src.Where(x => x is int).Select(x=>(int)x);//we´d prefer a way to get all the integer values back typed as such.
            var res = src.OfType<int>();//this is not exactly a Restriction operator, because it transforms objs in the input sequence by applying a Cast.

            foreach (var item in res)
                Console.Write(item.ToString()+",");
        }

        /// <summary>
        /// provides an example of how to use the overload method of Select that provides an index number of the item
        /// </summary>
        public static void NumberedList()
        {
            var lst = new[] { "Zero", "Um", "Dois", "Tres", "Quatro" };
            IEnumerable<string> numbers = lst.Select((x, i) =>
            {
                var t = " " + i.ToString() + " é " + x.ToString();
                Console.Write(t);
                return t;
            });

            var z = numbers.Count();//force the execuction;
        }

        /// <summary>
        /// sequence operator, which doesn’t come in LINQ out of the box. This operator enables you to prepend an existing sequence with a single object, much like a cons-cell in LISP does
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tail"></param>
        /// <param name="head"></param>
        /// <returns></returns>
        public static IEnumerable<T> StartWith<T>(this IEnumerable<T> tail, T head)
        {
            yield return head;
            foreach (T item in tail)
                yield return item;

        }

        public static void IteratorExample()
        {

            Enumerable.Range(2, 8).StartWith(1).StartWith(0).PrintCollection();

            //No worries if you don’t know LISP; here is a concrete example of what this does:
            // { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }
            /*StartWith(
                // { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
                StartWith(
                // { 2, 3, 4, 5, 6, 7, 8, 9 }
                        Enumerable.Range(2, 8),
                    1),
                0);*/
        }

        public static void PrintCollection<T>(this IEnumerable<T> collection)
        {
            foreach (var item in collection)
                Console.WriteLine(item.ToString() + ",");
        }

        class PetOwner
        {
            public string Name { get; set; }
            public List<String> Pets { get; set; }
        }

        class Pet
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        /// <summary>
        /// SelectMany collapses many elements into a single collection. The resulting collection is of another element type. We specify how an element is transformed into a collection of other elements.
        /// </summary>
        public static void SelectManyEx1()
        {
            PetOwner[] petOwners = 
                    { new PetOwner { Name="Higa, Sidney", 
                          Pets = new List<string>{ "Scruffy", "Sam" } },
                      new PetOwner { Name="Ashkenazi, Ronen", 
                          Pets = new List<string>{ "Walker", "Sugar" } },
                      new PetOwner { Name="Price, Vernette", 
                          Pets = new List<string>{ "Scratches", "Diesel" } } };

            // Query using SelectMany().
            IEnumerable<string> query1 = petOwners.SelectMany(petOwner => petOwner.Pets);

            Console.WriteLine("Using SelectMany():");

            // Only one foreach loop is required to iterate  
            // through the results since it is a 
            // one-dimensional collection. 
            foreach (string pet in query1)
            {
                Console.WriteLine(pet);
            }

            // This code shows how to use Select()  
            // instead of SelectMany().
            IEnumerable<List<String>> query2 =
                petOwners.Select(petOwner => petOwner.Pets);

            Console.WriteLine("\nUsing Select():");

            // Notice that two foreach loops are required to  
            // iterate through the results 
            // because the query returns a collection of arrays. 
            foreach (List<String> petList in query2)
            {
                foreach (string pet in petList)
                {
                    Console.WriteLine(pet);
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// SelectMany Overload example
        /// </summary>
        public static void SelectManyEx3()
        {
            PetOwner[] petOwners =
                    { new PetOwner { Name="Higa", 
                          Pets = new List<string>{ "Scruffy", "Sam" } },
                      new PetOwner { Name="Ashkenazi", 
                          Pets = new List<string>{ "Walker", "Sugar" } },
                      new PetOwner { Name="Price", 
                          Pets = new List<string>{ "Scratches", "Diesel" } },
                      new PetOwner { Name="Hines", 
                          Pets = new List<string>{ "Dusty" } } };

            // Project the pet owner's name and the pet's name. 
            var query =
                petOwners
                
                .SelectMany(p => p.Pets, (pOw, petNm) => new { petOwnerNm = pOw.Name, petNm })
                .Where(ownerAndPet => ownerAndPet.petNm.StartsWith("S"));

                /*.Select(ownerAndPet =>
                        new
                        {
                            Owner = ownerAndPet.pOw.Name,
                            Pet = ownerAndPet.petNm
                        }
                );*/

            query.PrintCollection();

        }

        public static void ZipExample()
        {
            var result = Enumerable.Range(0, 26).Zip(
                from i in Enumerable.Range(0, 26) select (char)(i + 'a'),
                (n, c) => n + " is " + c
            );

            result.PrintCollection();
        }

        enum Color : uint
        {
            Red = 0xFF0000,
            Green = 0x00FF00,
            Blue = 0x0000FF,
        }

        public static void ZipExample2()
        {
            var res = Enum.GetNames(typeof(Color)).Zip(
                        Enum.GetValues(typeof(Color)).Cast<uint>(),
                        (name, value) => new { Name = name, Value = value }
                    );
            res.PrintCollection();
        }

        public static void ZipExample3()
        {
            var nomes = new [] {"helder", "naiara", "francisca", "ribamar"};
            var idades = new[] { 35, 24, 67, 60 };
            var final = nomes.Zip(idades, (nm, ida) => new { Nome = nm, Idade = ida });
            final.PrintCollection();
        }

        public static void GroupByExample()
        {
            // Same as from x in new[] { 5, 2, 3, 5, 1, 1 }
            // group x by x % 2 == 0
            // ...
            var coll = new[] { 9,8,7,6,5,4,3,2,1, 0 }.GroupBy(x => x % 2 == 0, x => x ); //agrupa por Bool (Even or Odd), e seleciona o Número
            foreach (var group in coll)
            {
                Console.WriteLine(group.Key ? "Even" : "Odd");
                group.PrintCollection();
            }
        }

        // Uses method-based query syntax.
        public static void GroupByEx1()
        {
            // Create a list of pets.
            List<Pet> pets =
                new List<Pet>{ new Pet { Name="Barley"  , Age=8 },
                               new Pet { Name="Boots"   , Age=4 },
                               new Pet { Name="Whiskers", Age=1 },
                               new Pet { Name="Daisy"   , Age=4 } 
                                };

            // Group the pets using Age as the key value 
            // and selecting only the pet's Name for each value.
            IEnumerable<IGrouping<int, string>> query =
                pets.GroupBy(pet => pet.Age, pet => pet.Name);

            // Iterate over each IGrouping in the collection.
            foreach (IGrouping<int, string> petGroup in query)
            {
                // Print the key value of the IGrouping.
                Console.WriteLine(petGroup.Key);
                // Iterate over each value in the 
                // IGrouping and print the value.
                foreach (string name in petGroup)
                    Console.WriteLine("  {0}", name);
            }
        }

        public static void OrderingExample()
        {
            var memoryHungry = from process in Process.GetProcesses()
                               orderby process.ProcessName, process.WorkingSet64 descending
                               select process;
            //The preceding query expression translates into the use of two operators, OrderBy and ThenByDescending. Their respective roles will become apparent in just a moment:
            //var memoryHungry = Process.GetProcesses().OrderBy(process => process.ProcessName).ThenByDescending(process => process.WorkingSet64);
            memoryHungry.PrintCollection();
        }
    }
}
