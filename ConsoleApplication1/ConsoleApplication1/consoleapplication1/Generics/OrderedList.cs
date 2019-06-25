using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudosCSharp.Generics
{
    /// <summary>
    /// where T:IComparable é uma interface constraint que permite que façamos uso do metodo CompareTo dentro do código desta classe. Caso contrário, não estaria disponivel.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class OrderedList<T>:IEnumerable<T> where T: IComparable<T>
    {
        private List<T> _elements = new List<T>();

        public int Add(T value)
        {
            int i = 0;
            while (i < _elements.Count)
            { 
                if (_elements[i].CompareTo(value) >= 0)
                    break;
                i++;
            }
            _elements.Insert(i, value);
            return _elements.Count;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    
}
