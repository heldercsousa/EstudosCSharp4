using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EstudosCSharp.Regexs
{
    /// <summary>
    /// Regex training got from https://dev.to/awwsmm/20-small-steps-to-become-a-regex-master-mpc
    /// </summary>
    public static class Samples
    {
        /// <summary>
        /// The easiest regular expressions to understand are those that simply look for a character-to-character match between the regex pattern and the target string
        /// </summary>
        public static void Pattern1()
        {
            var pattern = "cat";
            Regex rg = new Regex(pattern);
            var str = "my cat jumps everywhere. Funcking cat, brokes everithing.";
            Console.WriteLine("isMatch:{0}",rg.IsMatch(str));
            var matches = rg.Matches(str);
            Console.WriteLine("Matches:");
            foreach (var match in matches)
            {
                Console.WriteLine("Match:{0}", match);
            }
        }

        /// <summary>
        /// But we can also specify alternative matches using square brackets
        /// </summary>
        public static void Pattern2()
        {
            var pattern = "ca[tr]";
            Regex rg = new Regex(pattern);
            var str = "my cat jumps into the car alway I enter it.";
            Console.WriteLine("isMatch:{0}", rg.IsMatch(str));
            var matches = rg.Matches(str);
            Console.WriteLine("Matches:");
            foreach (var match in matches)
            {
                Console.WriteLine("Match:{0}", match);
            }
        }

        /// <summary>
        // Open-and-close square brackets tell the regex engine to match any one of the characters specified, but only one. The above regex won't -- for example -- do what you might expect with the following setup
        /// When you use square brackets, you're telling the regex engine to match on exactly one of the characters contained within the brackets. If the engine finds a c character, then an a character, but the next character isn't r or t, it's not a match. If it finds ca and then either r or t, it stop
        /// </summary>
        public static void Pattern3()
        {
            var pattern = "ca[tr]";
            Regex rg = new Regex(pattern);
            var str = "my cat jumps into the cart alway I enter it.";
            Console.WriteLine("isMatch:{0}", rg.IsMatch(str));
            var matches = rg.Matches(str);
            Console.WriteLine("Matches:");
            foreach (var match in matches)
            {
                Console.WriteLine("Match:{0}", match);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Pattern4()
        {
            var pattern = @"\[\]";
            Regex rg = new Regex(pattern);
            var str = "my cat [] jumps into the cart alway I enter it.";
            Console.WriteLine("isMatch:{0}", rg.IsMatch(str));
            var matches = rg.Matches(str);
            Console.WriteLine("Matches:");
            foreach (var match in matches)
            {
                Console.WriteLine("Match:{0}", match);
            }
        }
        /// <summary>
        /// MATCHING CARACTERES DE ESCAPE
        /// </summary>
        public static void Pattern5()
        {
            var pattern = @"\\";
            Regex rg = new Regex(pattern);
            var str = @"c:\temp\test";
            Console.WriteLine("isMatch:{0}", rg.IsMatch(str));
            var matches = rg.Matches(str);
            Console.WriteLine("Matches:");
            foreach (var match in matches)
            {
                Console.WriteLine("Match:{0}", match);
            }
        }

        /// <summary>
        /// t
        /// </summary>
        public static void Pattern6()
        {
            var pattern = "t";
            Regex rg = new Regex(pattern);
            var str = @"c:\temp\test";
            Console.WriteLine("isMatch:{0}", rg.IsMatch(str));
            var matches = rg.Matches(str);
            Console.WriteLine("Matches:");
            foreach (var match in matches)
            {
                Console.WriteLine("Match:{0}", match);
            }
        }

        /// <summary>
        /// \t matches tab characters -- NÃO CONSEGUI FZ FUNCIONAR
        /// </summary>
        public static void Pattern7()
        {
            var pattern = "\\t";
            Regex rg = new Regex(pattern);
            var str = @"c:\ temp\   test    ";
            Console.WriteLine("isMatch:{0}", rg.IsMatch(str));
            var matches = rg.Matches(str);
            Console.WriteLine("Matches:");
            foreach (var match in matches)
            {
                Console.WriteLine("Match:{0}", match);
            }
        }

        /// <summary>
        /// `\[\]` 
        /// </summary>
        public static void Pattern8()
        {
            var pattern = @"\\\[\\\]";
            Regex rg = new Regex(pattern);
            var str = @"...match this regex `\[\]` with a regex?";
            Console.WriteLine("isMatch:{0}", rg.IsMatch(str));
            var matches = rg.Matches(str);
            Console.WriteLine("Matches:");
            foreach (var match in matches)
            {
                Console.WriteLine("Match:{0}", match);
            }
        }

        /// <summary>
        ///  `\r`, `\t`, and `\n` are all regex escape sequences.
        /// </summary>
        public static void Pattern9()
        {
            _CheckForPattern(@"\\[ntr]", @"`\r`, `\t`, and `\n` are all regex escape sequences.");
        }

        /// <summary>
        ///  '.'- special character which is used to match (nearly) any character, and that's the period / full stop character
        /// </summary>
        public static void Pattern10()
        {
            _CheckForPattern(@".", @"Helder Carvalho de Sousa");
        }

        /// <summary>
        /// If you want to match only patterns that look like escape sequences, you could do something like this
        /// </summary>
        public static void Pattern11()
        {
            _CheckForPattern(@"\\.", @"Hi Walmart is my grandson there his name is \n \r \t");
        }


        private static void _CheckForPattern(string pattern, string str)
        {
            Regex rg = new Regex(pattern);
            Console.WriteLine($"str:{str} - pattern:{pattern} - matched?:{rg.IsMatch(str)}");
            var matches = rg.Matches(str);
            Console.Write("Matches:");
            foreach (var match in matches)
            {
                Console.Write(match);
            }
            Console.WriteLine();
        }

    }
}
