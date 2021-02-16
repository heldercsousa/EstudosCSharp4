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
            Console.WriteLine("PATTERN 1");
            var pattern = "cat";
            var str = "my cat jumps everywhere. Funcking cat, brokes everithing.";
            _CheckForPattern(pattern, str);
        }

        /// <summary>
        /// But we can also specify alternative matches using square brackets
        /// </summary>
        public static void Pattern2()
        {
            Console.WriteLine("PATTERN 2");
            var pattern = "ca[tr]";
            var str = "my cat jumps into the car alway I enter it.";
            _CheckForPattern(pattern, str);
        }

        /// <summary>
        // Open-and-close square brackets tell the regex engine to match any one of the characters specified, but only one. The above regex won't -- for example -- do what you might expect with the following setup
        /// When you use square brackets, you're telling the regex engine to match on exactly one of the characters contained within the brackets. If the engine finds a c character, then an a character, but the next character isn't r or t, it's not a match. If it finds ca and then either r or t, it stop
        /// </summary>
        public static void Pattern3()
        {   
            Console.WriteLine("PATTERN 3");
            var pattern = "ca[tr]";
            var str = "The cat was cut when it ran under the cart";
            _CheckForPattern(pattern, str);
        }

        /// <summary>
        /// To match these special characters, we must escape them by preceding them with a backslash character \
        /// </summary>
        public static void Pattern4()
        {
            Console.WriteLine("PATTERN 4");
            var pattern = @"\[\]";
            var str = "my cat [] jumps into the cart alway I enter it.";
            _CheckForPattern(pattern, str);
        }

        /// <summary>
        /// MATCHING CARACTERES DE ESCAPE
        /// </summary>
        public static void Pattern5()
        {
            Console.WriteLine("PATTERN 5");
            var pattern = @"\\";
            var str = @"c:\temp\test";
            _CheckForPattern(pattern, str);
        }

        /// <summary>
        /// Only special characters should be preceded by \ to force a literal match. All other characters are interpreted literally by default. For instance, the regular expression t matches only literal lowercase letter t characters:
        /// </summary>
        public static void Pattern6()
        {
            Console.WriteLine("PATTERN 6");
            var pattern = "t";
            var str = @"c:\temp\test";
            _CheckForPattern(pattern, str);
        }

        /// <summary>
        /// \t matches tab characters -- NÃO CONSEGUI FZ FUNCIONAR
        /// </summary>
        public static void Pattern7()
        {
            Console.WriteLine("PATTERN 7");
            var pattern = "\\t";
            var str = @"c:\ temp\   test    ";
            _CheckForPattern(pattern, str);
        }

        /// <summary>
        /// `\[\]` 
        /// </summary>
        public static void Pattern8()
        {
            Console.WriteLine("PATTERN 8");
            var pattern = @"\\\[\\\]";
            var str = @"...match this regex `\[\]` with a regex?";
            _CheckForPattern(pattern, str);
        }

        /// <summary>
        ///  `\r`, `\t`, and `\n` are all regex escape sequences.
        /// </summary>
        public static void Pattern9()
        {
            Console.WriteLine("PATTERN 9");
            _CheckForPattern(@"\\[ntr]", @"`\r`, `\t`, and `\n` are all regex escape sequences.");
        }

        /// <summary>
        ///  the "any" character '.' - special character which is used to match (nearly) any character, and that's the period / full stop character
        /// </summary>
        public static void Pattern10()
        {
            Console.WriteLine("PATTERN 10");
            _CheckForPattern(@".", @"Helder Carvalho de Sousa");
        }

        /// <summary>
        /// If you want to match only patterns that look like escape sequences, you could do something like this
        /// </summary>
        public static void Pattern11()
        {
            Console.WriteLine("PATTERN 11");
            _CheckForPattern(@"\\.", @"Hi Walmart is my grandson there his name is \n \r \t");
        }

        /// <summary>
        /// And as with all special characters, if you want to match a literal ., you need to precede it with a \ character:
        /// </summary>
        public static void Pattern12()
        {
            Console.WriteLine("PATTERN 12");
            _CheckForPattern(@"\.", @"War is Peace. Freedom is Slavery. Ignorance is Strength.");
        }

        /// <summary>
        ///  every lowercase letter we want to match within the square brackets. An easier way of accomplishing this is to use character ranges to match any lowercase letter:
        /// </summary>
        public static void Pattern13()
        {
            Console.WriteLine("PATTERN 13");
            _CheckForPattern(@"\\[a-z]", @"`\n`, `\r`, `\t`, and `\f` are whitespace characters, `\.`, `\\` and `\[` are not.");
        }

        /// <summary>
        /// If you want to match multiple ranges, just put them back-to-back in the square brackets
        /// </summary>
        public static void Pattern14()
        {
            Console.WriteLine("PATTERN 14");
            _CheckForPattern(@"\\[a-gq-z]", @"`\n`, `\r`, `\t`, and `\f` are whitespace characters, `\.`, `\\` and `\[` are not.");
        }

        /// <summary>
        /// Hexadecimal numbers can contain digits 0-9 as well as letters A-F. When used to specify colours, 
        /// "hex" codes can be as short as three characters. Create a regex to find valid hex codes in the list below:
        /// </summary>
        public static void Pattern15()
        {
            Console.WriteLine("PATTERN 15");
            _CheckForPattern(@"[0-9A-F][0-9A-F][0-9A-F]", @"1H8 4E2 8FF 0P1 T8B 776 42B G12");
        }

        /// <summary>
        /// reate a regex that will select only the lowercase consonants (non-vowel characters, including y) in the sentence below
        /// </summary>
        public static void Pattern16()
        {
            Console.WriteLine("PATTERN 16");
            _CheckForPattern(@"[b-df-hj-np-tv-z]", @"The walls in the mall are totally, totally tall.");
        }


        /// <summary>
        /// the "not" carat ^, an easier way to do the same as Pattern16 method (only lowercase consonants)
        /// </summary>
        public static void Pattern17()
        {
            Console.WriteLine("PATTERN 17");
            _CheckForPattern(@"[^aeiou .,T]", @"The walls in the mall are totally, totally tall.");
        }

        /// <summary>
        /// Note that we don't need to escape the . here. Many special characters within square brackets are treated literally, including the open [ -- but not the close ] bracket character (can you see why?). The backslash \ character is also not treated literally. If you want to match on a literal backslash \ using square brackets, you have to escape it by preceding it with a second backslash \\. This behaviour must be allowed in order for whitespace characters to be matchable within square brackets
        /// </summary>
        public static void Pattern18()
        {
            Console.WriteLine("PATTERN 18");
            _CheckForPattern(@"[\t]", @"t  t   t   t");
        }
        /// <summary>
        /// The carat can be used with ranges, as well. If I wanted to only capture the characters a, b, c, x, y, and z, I could do
        /// </summary>
        public static void Pattern19()
        {
            Console.WriteLine("PATTERN 19");
            _CheckForPattern(@"[abcxyz]", @"abcdefghijklmnopqrstuvwxyz");
        }
        /// <summary>
        /// The carat can be used with ranges, as well. If I wanted to only capture the characters a, b, c, x, y, and z, I could do
        /// </summary>
        public static void Pattern20()
        {
            Console.WriteLine("PATTERN 20");
            _CheckForPattern(@"[^d-w]", @"abcdefghijklmnopqrstuvwxyz");
        }

        /// <summary>
        ///Use the "not" carat ^ within square brackets to match all of the words below that don't end with a y
        /// </summary>
        public static void Pattern21()
        {
            Console.WriteLine("PATTERN 21");
            _CheckForPattern(@"[a-z][a-z][^y ]", @"day dog hog hay bog bay ray rub");
        }


        /// <summary>
        ///Write a regex using a range and a "not" carat ^ to find all the years between 1977 and 1982 (inclusive) below
        /// </summary>
        public static void Pattern22()
        {
            Console.WriteLine("PATTERN 22");
            _CheckForPattern(@"19[78][^3-6]", @"1975 1976 1977 1978 1979 1980 1981 1982 1983 1984");
        }

        /// <summary>
        /// Write a regex to match all characters below that aren't a literal carat ^ character
        /// </summary>
        public static void Pattern23()
        {
            Console.WriteLine("PATTERN 23");
            _CheckForPattern(@"[^^]", @"abc1^23*()");
        }


        ///PAREI NO Step 7: character classes

        private static void _CheckForPattern(string pattern, string str)
        {
            Regex rg = new Regex(pattern);
            Console.WriteLine($"str:{str}");
            Console.WriteLine($"pattern:{pattern} - matched?:{rg.IsMatch(str)}");
            var matches = rg.Matches(str);
            Console.Write("Matches:");
            foreach (var match in matches)
            {
                Console.Write(" " + match);
            }
            Console.WriteLine();
            Console.WriteLine();
        }

    }
}
