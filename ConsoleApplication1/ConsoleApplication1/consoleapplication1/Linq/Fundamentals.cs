using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

namespace EstudosCSharp.Linq
{
    /// <summary>
    /// Noções fundamentias de Linq
    /// </summary>
    public static class Fundamentals
    {
        public static List<Product> InMemoryListOfProducts;

        /// <summary>
        /// build a standard list of in-memory products
        /// </summary>
        public static void PopulateInMemoryList()
        {
            InMemoryListOfProducts = new List<Product> {
                new Product { Name = "Chai", Price = 18.00m },
                new Product { Name = "Chang", Price = 19.00m },
                new Product { Name = "Aniseed Syrup", Price = 10.00m },
                new Product { Name = "Chef Anton’s Cajun Seasoning", Price = 22.00m },
                new Product { Name = "Chef Anton’s Gumbo Mix", Price = 21.35m },
                new Product { Name = "Grandma’s Boysenberry Spread", Price = 25.00m },
                new Product { Name = "Uncle Bob’s Organic Dried Pears", Price = 30.00m },
                new Product { Name = "Northwoods Cranberry Sauce", Price = 40.00m },
                new Product { Name = "Mishi Kobe Niku", Price = 97.00m },
                new Product { Name = "Ikura", Price = 31.00m }
            };
        }

        /// <summary>
        /// metodo com codigo estilo imperativo. Note no codigo que as coisas começam a não ficar claras quando se quer entender o que a segunda parte do codigo faz, que é ranquear os produtos por multiplos de 10. A primeira parte é mais fácil de entender que é para filtrar preços menor que o parametro passado.
        /// ---- Desvantagens: 
        /// ---- 1) Imperative nature of the code, not revealing the intent of the user clearly 
        /// ---- 2)Lots of manual plumbing needed for a seemingly simple task  
        /// ---- 3)Not composable out of simple query operator primitives
        /// </summary>
        /// <param name="price"></param>
        public static List<Product> GetProductsLowerThan(decimal price)
        {
            if (InMemoryListOfProducts == null)
                PopulateInMemoryList();

            //até que é nitido que estamos filtrando precos menores que o parametro
            var cheap = new List<Product>();
            foreach (var item in InMemoryListOfProducts)
            {
                if (item.Price < price)
                    cheap.Add(item);
            }

            return cheap;
        }

        /// <summary>
        /// metodo com codigo estilo imperativo. Note no codigo que as coisas começam a não ficar claras quando se quer entender o que a segunda parte do codigo faz, que é ranquear os produtos por multiplos de 10. A primeira parte é mais fácil de entender que é para filtrar preços menor que o parametro passado.
        /// ---- Desvantagens: 
        /// ---- 1) Imperative nature of the code, not revealing the intent of the user clearly 
        /// ---- 2)Lots of manual plumbing needed for a seemingly simple task  
        /// ---- 3)Not composable out of simple query operator primitives
        /// </summary>
        public static Dictionary<int, List<Product>> RankingByEachTen()
        {
            if (InMemoryListOfProducts == null)
                PopulateInMemoryList();

            //ranking não é nitido neste codigo
            var priceGroups = new Dictionary<int, List<Product>>();
            foreach (var product in InMemoryListOfProducts)
            {
                int group = 10 * (int)(product.Price / 10);
                List<Product> productsInGroup;
                if (!priceGroups.TryGetValue(group, out productsInGroup))
                    priceGroups[group] = productsInGroup = new List<Product>();
                productsInGroup.Add(product);
            }

            return priceGroups;
        }

        /// <summary>
        /// usando uma conexão SQL de System.Data.SqlClient para fazer operações sobre dados do SQLServer
        /// </summary>
        public static List<Product> GetDBProductsLowerThan25()
        {
            //string connectionString = //ConsoleApplication1.Properties.Settings.Default.ConnectionString;
            string connectionString = ConfigurationManager.ConnectionStrings["SQLServerLocal"].ConnectionString;

            var products = new List<Product>();
            using (var conn = new SqlConnection(connectionString)) 
            //using (var conn = new SqlConnection()) 
            {
                string sql = "SELECT * FROM Produtos WHERE Price < 25";
                using (var cmd = new SqlCommand(sql, conn)) {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader()) 
                    {
                        while (reader.Read()) {
                        string name = (string)reader["Name"];
                        decimal price = (decimal)reader["Price"];
                        products.Add(new Product { Name = name, Price = price });
                        }
                    }
                }
            }

            return products;
        }

        /// <summary>
        /// usando um arquivo XML como fonte de informação para Produtos. System.Xml is an XML Document Object Model (DOM) API that isn’t very easy to use. 
        /// To create XML documents, you must write very imperative-style code that calls the methods
        /// CreateElement, InsertAfter, and so on. Although we’ll be loading a document from a
        /// file in the following example, be aware of the bigger picture of the API being used. As
        /// you’ll see later, the new System.Xml.Linq namespace makes the task of creating XML
        /// documents (or fragments) easier, too.
        /// </summary>
        public static List<Product> GetXMLProductsLowerThan25()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load( "XMLs/Produtos.xml");
            
            
            XmlNodeList res = doc.SelectNodes("//Product[@Price<25]");
            var products = new List<Product>();
            foreach (XmlNode node in res)
            {
                products.Add(new Product { 
                    Name = node.Attributes["Name"].Value, 
                    Price = decimal.Parse(node.Attributes["Price"].Value) }  
                );
            }
            return products;
        }

        /// <summary>
        /// pega os produtosd com preço menor que o parametro. LINQ query expressions. What we’ve shown here is that any of those operators can be
        /// glued together easily, facilitating a declarative programming style where you simply say what
        /// you want as opposed to how it needs to be done. By using a declarative approach to
        /// querying, you also give the runtime more control over the precise mechanics of evaluating
        /// your query, potentially taking benefit of multicore processors and whatnot.
        /// </summary>
        /// <param name="price">preco maximo, não inclusivo</param>
        /// <returns></returns>
        public static List<Product> GetLinqProductsLowerThan(decimal price)
        {
            if (InMemoryListOfProducts == null)
                PopulateInMemoryList();

            var cheap = (from Product in InMemoryListOfProducts
                        where Product.Price < price
                        select Product).Take(3); //exemplo de uso do Take para definir upperbound

            //lazy behavior of linq. O parametro price foi pego por closure! Depois que alterar seu valor, Linq irá reconhece-lo ao inves do valor passado por parametro.
           // price = 19;
            return new List<Product>(cheap);
        }
        
        public static Dictionary<int, List<Product>> RankingByEach10Linq()
        {
            if (InMemoryListOfProducts == null)
                PopulateInMemoryList();

            var priceGroups = from Product in InMemoryListOfProducts
                              group Product by 10m * (int)(Product.Price / 10);

            Dictionary<int, List<Product>> ret = new Dictionary<int, List<Product>>();

            foreach (var group in priceGroups)
            {
                ret[(int)group.Key] = new List<Product>(group);
            }

            return ret;
        }

        public static List<EstudosCSharp.LinqToSQL.Produto> GetLinqToSQLProductsLowerThan(decimal price)
        {
            using (var ctx = new EstudosCSharp.LinqToSQL.ProdutosDataContext())
            {
                ctx.Log = Console.Out;//turn on logging on the data context object:
               /* var cheap = from product in ctx.Produtos
                            where product.Price < price
                            select product;*/

                /*
                var cheap = from p in ctx.Produtos
                            where p.Price < price
                            orderby p.Name, p.Price
                            select p;
                 */

                var cheap = ctx.Produtos.Select(p => p).Where(p=>p.Price<price).OrderBy(p => p.Name).ThenBy(p => p.Price); //equivalente ao Linq expression anterior

                return new List<EstudosCSharp.LinqToSQL.Produto>(cheap);
            }            
        }

        public static List<Product> GetLinqToXMLProductsLowerThan(decimal paramprice)
        {
            var doc = XDocument.Load("XMLs/Produtos.xml");
            var products = doc.Element("Products").Elements();
            var cheap = from product in products
                        let name = product.Attribute("Name").Value
                        let price = decimal.Parse(product.Attribute("Price").Value)
                        where price < paramprice //não funciona, não sei porque
                        select new Product { Name = name, Price = price };

            return new List<Product>(cheap);
        }

        /// <summary>
        /// Linq to SQL 
        /// </summary>
        public static List<string> GetProductNames ()
        {
            using (var context = new EstudosCSharp.LinqToSQL.ProdutosDataContext())
            {
                context.Log = Console.Out;
                /*
                var lst = from p in context.Produtos
                          select p.Name;
                var lst = context.Produtos.Select(p => p.Name);//equivalente em lambda
                */

                /*
                var lst = from p in context.Produtos
                          orderby p.Price
                          select p.Name;
                
                var lst = context.Produtos.OrderBy(p => p.Price).Select(i => i.Name);//equivalente em lambda
                */

                var lst = context.Produtos.OrderByDescending(p => p.Price).Select(p => p.Name);


                return new List<string>(lst);
            }

            //return new List<string>();
        }

        /// <summary>
        /// using JOIN in LINQ
        /// </summary>
        public static void GetWindowsProcesses()
        {
            var descriptions = new[] {
                new { Name = "WDExpress", Description = "Visual Studio Express Desktop" },
                new { Name = "firefox", Description = "Firefox" },
                new { Name = "Ssms", Description = "SQL Server Management Studio" },
                new { Name = "AcroRd32", Description = "Acrobat Reader" }
            };

            const int MB = 1024 * 1024;
           /* var heavyMemoryApps = (
                                  from p in Process.GetProcesses()
                                  join f in descriptions on p.ProcessName equals f.Name
                                  where p.WorkingSet64 > 100 * MB
                                  orderby p.WorkingSet64 descending
                                  select new
                                  {
                                      Name = p.ProcessName,
                                      Description = f.Description,
                                      Memory = p.WorkingSet64/MB
                                  }
                                  ).Take(10);*/

            var heavyMemoryApps = (
                                 from p in Process.GetProcesses()
                                 where p.WorkingSet64 > 100 * MB
                                 orderby p.WorkingSet64 descending
                                 join f in descriptions on p.ProcessName equals f.Name //o join foi mudado de posição por questões de perfomance. É melhor primeiro filtrar(where) e depois odernar, para então fazer o JOIN
                                 select new
                                 {
                                     Name = p.ProcessName,
                                     Description = f.Description,
                                     Memory = p.WorkingSet64 / MB
                                 }
                                 ).Take(10);

            foreach (var item in heavyMemoryApps)
            {
                //Console.WriteLine(string.Format("Nome: {0}    Memoria: {1}", item.Name, item.Memory.ToString()));
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// using GroupBy em Linq
        /// </summary>
        public static void GetEnumerableMethods()
        {
            /*
            //forma imperativa 01
             var queryOperators = from m in typeof(Enumerable).GetMethods()
                                 group m by m.Name;
              
            foreach (var queryOperator in queryOperators) 
            {
                int n = 0;
                foreach (var overload in queryOperator)
                n++;
                Console.WriteLine("{0} has {1} overload(s)", queryOperator.Key, n);
            }
             */

            /*
            //forma imperativa 02
            var queryOperators = from m in typeof(Enumerable).GetMethods()
                                 group m by m.Name;
             
            foreach (var queryOperator in queryOperators) {
                int n = queryOperator.Count();
                Console.WriteLine("{0} has {1} overload(s)", queryOperator.Key, n);
            }
             */
 
            //forma declarativa
            var queryOperators = from m in typeof(Enumerable).GetMethods()
                                group m by m.Name into queryOperator
                                select new 
                                { 
                                    Name = queryOperator.Key,
                                    Overloads = queryOperator.Count() 
                                };
                                
            foreach (var queryOperator in queryOperators) {
                Console.WriteLine("{0} has {1} overload(s)", queryOperator.Name, queryOperator.Overloads);
            }

        }

        /// <summary>
        /// Using GroupBy from Linq to SQL to get the average products per category. 
        /// </summary>
        public static void PrintAverageByCategory()
        {
            using (var context = new EstudosCSharp.LinqToSQL.ProdutosDataContext())
            {
                context.Log = Console.Out;

                var categoryStats = from p in context.Produtos
                                    group p by p.Category into category
                                    select new
                                    {
                                        Category = category.Key,
                                        AvgPrice = category.Average(pr => pr.Price)
                                    };

                Console.WriteLine("Category----------  AvgPrice-----");
                foreach (var item in categoryStats)
                {
                    Console.WriteLine(String.Format("{0}{1}{2}", item.Category.ToString(), new String(' ',22-"{0}".Length), item.AvgPrice.ToString()));
                }
            }
        }

        /// <summary>
        /// This produces a sequence of anonymous typed objects with a Category as well as a Products property of type IEnumerable<Product>.
        /// </summary>
        public static void PrintByCategoryOrderedByPrice()
        {
            using (var context = new EstudosCSharp.LinqToSQL.ProdutosDataContext())
            {
                //context.Log = Console.Out;

                var perCategory = from product in context.Produtos
                                  group product by product.Category into category
                                  select new
                                  {
                                      Category = category.Key,
                                      Products = from p in category
                                                 orderby p.Price descending
                                                 select new 
                                                 { 
                                                     p.Name,
                                                     p.Price 
                                                 }
                                  };

                Console.WriteLine("Category----------  Name------------------------------------------- Price-------");
                foreach (var category in perCategory)
                {
                    foreach (var prod in category.Products)
                    {
                        Console.WriteLine(String.Format("{0}{1}{2}{3}{4}", category.Category.ToString(), new String(' ', 22-"{0}".Length), prod.Name, new String(' ',50-"{2}".Length), prod.Price.ToString().Trim() ));    
                    }
                    
                }
            }
        }

        /// <summary>
        /// Often, you’ll find it handy to carry out intermediate computations within a query expression, binding the result 
        /// to an identifier within the query expression. An example is worth a thousand words
        /// </summary>
        public static void SquareCubeStars()
        {
            var res = from i in Enumerable.Range(0, 10) //get a sequence of integral numbers 0 through 9 (the parameter 10 indicates the count)
                      let square = i * i
                      where square % 2 == 0
                      let cube = i * square
                      let stars = new string('*', i)
                      select new { idx = i, Stars = stars, Cube = cube, Square = square };
            /*
            //This is precisely what the compiler does on your behalf. It rewrites the query to fit in a pure pipeline-based model of chained query operators, along the following lines
            var res = from i in Enumerable.Range(0, 10)
                      select new { i, square = i * i } into __x1 //__x1 is a compiler-generated name known as transparent identifier.
                      where __x1.square % 2 == 0
                      select new { __x1, cube = __x1.i * __x1.square } into __x2  //__x2 is a compiler-generated name known as transparent identifier.
                      select new { __x2, stars = new string(‘*’, __x2.__x1.i) } into __x3  //__x3 is a compiler-generated name known as transparent identifier.
                      select new { Stars = __x3.stars, Cube = __x3.__x2.cube };
            */

            //trecho de codigo acima, porem agora no estilo imperativo
            /*
            foreach (int i in Enumerable.Range(0, 10)) 
            {
                int square = i * i;
                if (square % 2 == 0) 
                {
                    int cube = i * square;
                    string stars = new string('*', i);
                    yield return new { Stars = stars, Cube = cube };
                }
            }*/

            

            foreach (var item in res)
            {
                Console.WriteLine(String.Format("idx:{0} square:{1}, cube:{2}, stars{3}", item.idx, item.Square, item.Cube, item.Stars));
            }
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
