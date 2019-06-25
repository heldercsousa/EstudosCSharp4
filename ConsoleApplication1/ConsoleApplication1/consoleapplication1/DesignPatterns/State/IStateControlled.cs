using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstudosCSharp.DesignPatterns.State
{
    public interface IStateControlled<TStateControlled>
    {
        State<TStateControlled> GetCurrentState { get; }
        IList<State<TStateControlled>> StateList { get; }
    }
}
