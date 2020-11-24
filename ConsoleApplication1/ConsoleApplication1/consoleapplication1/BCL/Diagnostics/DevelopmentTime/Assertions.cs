using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace EstudosCSharp.BCL.Diagnostics.DevelopmentTime
{
    public static class Assertions
    {
        private static string _Agora
        {
            get
            {
                return DateTime.Now.ToString(new CultureInfo("pt-BR").DateTimeFormat);
            }
        }

        public static void DebugIndent()
        {
            Debug.WriteLine("DebugIndent " + _Agora);
            Debug.WriteLine("Before INDENT");
            Debug.Indent();
            Debug.WriteLine("After INDENT");
            Debug.Unindent();
            Debug.WriteLine("After UNIDENT");
            System.Console.WriteLine("See test indent messages in Debug panel, or in the .log trace file");
        }

        public static void DebugAssert(int from, int to)
        {
            
            Debug.Assert(from <= to, $"DebugAssert - {_Agora} - 'from({from})' must be lower than 'to({to})'");
            System.Console.WriteLine("See DebugAssert messages in the .log trace file");
        }

        /// <summary>
        /// Debug.Fail is much like Debug.Assert(false), but it also prints a message, including the failure point stack trace 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void DebugFail(int from, int to)
        {
            if (from > to)
            {
                Debug.Fail($"DebugFail - {_Agora} - 'from({from})' must be lower than 'to({to})'");
            }
            System.Console.WriteLine("See DebugFail message in the .log trace file");
        }
    }
}
