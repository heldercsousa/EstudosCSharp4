using System;
using System.Linq;

namespace EstudosCSharp.BCL.Diagnostics.DevelopmentTime
{
    public static class StackTrace
    {
        public static System.Diagnostics.StackTrace _StackTrace { get; set; }
        /// <summary>
        /// The creation of a new instance of this class captures the stack trace of the current thread
        /// </summary>
        public static void CreateStackTrace()
        {
            if (_StackTrace == null)
            {
                _StackTrace = new System.Diagnostics.StackTrace();
                Console.WriteLine(_StackTrace);//The StackTrace object can be printed by means of its ToString method
                Console.WriteLine("StackTrace created!");
            }
        }

        /// <summary>
        /// a StackTrace instance can be decomposed into individual stack frames by using the GetFrame and GetFrames methods.
        /// </summary>
        public static void DecomposeStackTraceFrames()
        {
            CreateStackTrace();
            Console.WriteLine("Decomposed StackTrace frames:");
            var frames = _StackTrace.GetFrames();
            var decomposed = string.Join(Environment.NewLine, frames.Select((x,y) => $"{y.ToString()} - {x.ToString()}"));
            Console.WriteLine(decomposed);
        }

        /// <summary>
        /// By passing true to the constructor, file info can be obtained as well, given a symbol file
        /// (with extension .pdb) is found.Note that this process is rather slow, so use of stack trace
        /// logging is better avoided in performance-critical code paths.
        /// </summary>
        public static void CreateStackTraceWithFileInfo()
        {
            var trace = new System.Diagnostics.StackTrace(true);
            Console.WriteLine("StackTrace with loggingFileInfo true");
            Console.WriteLine(trace); // to string calls implicitly
        }
    }
}
