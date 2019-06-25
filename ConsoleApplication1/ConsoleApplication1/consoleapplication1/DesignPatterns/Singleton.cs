using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstudosCSharp.DesignPatterns
{
    public sealed class Singleton
    {
        private static volatile Singleton _instance;
        private static object sync = new Object();

        private Singleton()
        {
        }

        public Singleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (sync)
                    {
                        if (_instance == null)
                            _instance = new Singleton();
                 
                    }
                }
                return _instance;
            }
        }
    }
}
