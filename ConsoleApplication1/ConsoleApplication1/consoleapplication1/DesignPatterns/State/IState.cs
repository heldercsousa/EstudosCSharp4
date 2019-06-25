using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstudosCSharp.DesignPatterns.State
{
    public abstract class State<TStateControlled>
    {
        public TStateControlled _contextObject;
        public State(TStateControlled contextObject)
        {
            _contextObject = contextObject;
        }

        public abstract State<TStateControlled> Evaluate();
    }
}
