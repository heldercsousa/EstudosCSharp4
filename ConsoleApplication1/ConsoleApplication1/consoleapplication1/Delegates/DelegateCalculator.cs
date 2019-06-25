using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudosCSharp.Delegates
{
    class DelegateCalculator
    {
        private Dictionary<string, Func<int, int, int>> _ops = new Dictionary<string, Func<int, int, int>> {
            {"Add", (a,b) => a+b }, 
            {"Sub", (a,b) => a-b }, 
            {"Mul", (a,b) => a*b }, 
            {"Div", (a,b) => a/b }, 
            {"Mod", (a,b) => a%b }
        };
        /// <summary>
        /// permite que a calculadora seja complementada com novas funções além das padrões, ou sobreposição das funções existentes
        /// </summary>
        /// <param name="name">Nome da função</param>
        /// <param name="function">Delegate</param>
        public void AddOperator(string name, Func<int, int, int> function)
        {
            _ops[name] = function;
        }
        /// <summary>
        /// realiza calculo da função passada por string nos dois operandos
        /// </summary>
        /// <param name="lhs">operando 1</param>
        /// <param name="op">nome da função desejada</param>
        /// <param name="rhs">operando 2</param>
        /// <returns></returns>
        public int Eval (int lhs, string op, int rhs)
        {
            return _ops[op](lhs, rhs);
        }
    }
}
