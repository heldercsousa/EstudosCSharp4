using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudosCSharp.Generics
{
    /// <summary>
    /// Exemplo de Default Constructor Constraint. Ele descreve a necessidade de ter um construtor default disponivel para um certo tipo de parametro.
    /// Se T não tiver um construtor padrão, o compiler rejeitará o tipo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Factory<T> where T : new()
    {
        public T CreateInstance()
        {
            return new T(); //este new T(), atualmente, faz uma chamada a uma metodo da BCL System.Activator
        }
    }

    /// <summary>
    /// so aceita tipo referenciados, como classes, interfaces e delegates
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class HasRef<T> where T : class
    {
        private T _reference;
    }

    /// <summary>
    /// só aceita Value Types como parâmetro, e não podem ser Nullable. Um lugar onde isso é aplicado dentro do framework é na Struct Nullable
    /// </summary>
    /// <typeparam name="T">Neste caso, não aceita parâmetro que seja Nullable, pois o compiler irá reclamar</typeparam>
    struct HasVal<T> where T : struct
    {
        private T _value;
    }
}
