using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudosCSharp.Generics
{
    /// <summary>
    /// contem metodos que demonstram CoVariance e ContraVariance. Antes do .NET 4, o compilador rejeitaria uma coleção IEnumerable de Apple onde uma colecao de IEnumerable de Fruit é esperada.
    /// Ou seja, antes do .NET 4, IEnumerable de T era tratado como Invariant
    /// </summary>
    public static class Variance
    {
        /// <summary>
        /// array covariance accept the CAST bellow. Isso é um problema, porque não permite Segurança de Tipo. Exemplo: pode-se tentar invocar um metodo de String que nao existe em Object, 
        /// e pode-se adicionar qualquer Tipo, o que não garante que um metodo contido em String existirá em Object ou outro tipo.
        /// </summary>
        public static void ArrayCovariance()
        {
            string[] arrayCovariance = new string[] { "aaa", "bbb", "ccc" };
            object[] destiny = arrayCovariance; 
            destiny[3] = new int(); //durante o runtime, será emitida uma exceção aki, pois destiny já tem um tipo mais especifico que Object, que é String
        }

        /// <summary>
        /// este metodo tem que ser testado numa versao anterior à versão 4 do .net, pois Invariance só existia neste caso
        /// </summary>
        public static void DotNet3_5Invariance()
        {

            //este metodo não foi finalizado para fazer o que pretende-se
            var poli01 = new Politician();
            var poli02 = new Politician();
            var poli03 = new Politician();

            IEnumerable<Politician> politicians = new Politician[3] { poli01, poli02, poli03 };

            var person01 = new Person();

            //politicians. = person01; //válido
                        
        }

    }

    public class Person
    {
        static int PersonCount;
        public int PersonId { get; private set; }
        int Age { get; set; }
        string Name { get; set; }
        char Genre { get; set; }
        public Person()
        {
            Person.PersonCount++;
            PersonId = Person.PersonCount;
        }
    }

    public class Politician: Person
    {
        string partido { get; set; }
        public Politician()
        { }
    }

    public class Assistant: Politician
    {
        Politician boss { get; set; }
        public Assistant()
        { }
    }

}
