using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace EstudosCSharp.BCL
{
    public static class CultureFormatting
    {
        public static string PORTUGUESEBR = "pt-BR";
        public static string USENGLISH = "en-US";
        public static string BELGIUM = "nl-BE";

        public static void TryParseNumbersByCulture()
        {
            var curr = System.Globalization.NumberFormatInfo.CurrentInfo;
            Console.WriteLine("Your culture is {0}", curr);

            Console.Write("Enter your culture: ");
            string culture = Console.ReadLine();
            string salaryString=string.Empty;
            decimal salary;
            do
            {
                Console.Write("Enter your salary: ");
                salaryString = Console.ReadLine();
            } while (!decimal.TryParse(salaryString, NumberStyles.Currency, new CultureInfo(culture), out salary));

            Console.WriteLine("Your salary is {0}", salary);
            Console.ReadKey();
        }

        /// <summary>
        /// uses the classic TimeZone class to retrieve the computer’s current time zone and print a few pieces of information about that zone.
        /// </summary>
        public static void PrintTimeZone()
        {
            var here = TimeZone.CurrentTimeZone;
            Console.WriteLine(here.StandardName); 
            Console.WriteLine(here.GetUtcOffset(DateTime.Now)); 
            Console.WriteLine(here.IsDaylightSavingTime(DateTime.Now));
            Console.ReadKey();
        }


        /// <summary>
        /// Using the TimeZoneInfo class, we get more flexibility with regard to adjustment rules, and
        ///for that reason it’s the recommended way to deal with time zones whenever your scenario
        ///permits.Here I show how to enumerate all the time zones that deal with daylight savings
        ///time and print some information associated with them
        /// </summary>
        public static void PrintTimeZoneInfo()
        {
            var res = from zone in TimeZoneInfo.GetSystemTimeZones()
                      where zone.SupportsDaylightSavingTime
                      select new
                      {
                          zone.Id,
                          zone.DisplayName,
                          Adjustments = zone.GetAdjustmentRules().Length
                      };
            foreach (var zoneInfo in res)
                Console.WriteLine(zoneInfo);

            Console.ReadKey();
        }

        public static void TestCommandLine(string[] args)
        {
            Console.WriteLine("CommandLine:");
            Console.WriteLine(" " +Environment.CommandLine);
            Console.WriteLine("Main:");
            foreach (var arg in args)
                Console.WriteLine(" " +arg);
            Console.WriteLine("CommandLineArgs:");
            foreach (var arg in Environment.GetCommandLineArgs())
                Console.WriteLine(" " +arg);
            Console.ReadLine();
        }

        public static void MyAppRunsFrom()
        {
            var from = new Uri(Assembly.GetEntryAssembly().CodeBase);
            Console.WriteLine(from.LocalPath);
            Console.ReadLine();
        }

        /// <summary>
        /// shows how the cmd.exe child process (which can outlive its parent but still
        /// have the inherited environment block) sees the MSG variable that has been set.
        /// </summary>
        public static void ChildProcess()
        {
            Console.WriteLine("Type echo %MSG% to figure out child process sees the MSG variable set here");
            Environment.SetEnvironmentVariable("MSG", "Hello");
            System.Diagnostics.Process.Start("cmd.exe");
            Console.ReadLine();
        }

        public static void FormatingNumberToString()
        {
            Console.WriteLine(123.45.ToString(new CultureInfo("en-US")));
            Console.WriteLine(123.45.ToString(new CultureInfo("nl-BE")));
            Console.ReadKey();
        }

        public static void Sifrao()
        {
            var enUS = new CultureInfo("en-US");
            var nlBE = new CultureInfo("nl-BE");
            var jaJP = new CultureInfo("ja-JP");
            double d = 123.456;
            int i = 1234;
            long l = 123456789;
            // Currency, usable for all numeric values // Number of decimal digits determined from culture or explicitly 
            Console.WriteLine(d.ToString("C", enUS));  // $ 123.46 
            Console.WriteLine(d.ToString("C", nlBE));  // &euro; 123,46 
            Console.WriteLine(d.ToString("C", jaJP));  // ¥ 123 
            Console.WriteLine(d.ToString("C3", enUS)); // $ 123.456 
            Console.WriteLine(d.ToString("C0", nlBE)); // &euro; 123 
            Console.WriteLine(d.ToString("C2", jaJP)); // ¥ 123.46
                                                       // Decimal, usable for integral values only 
            // Number of digits controls left padding with zeros 
            Console.WriteLine(i.ToString("D", enUS));  // 1234 
            Console.WriteLine(i.ToString("D5", nlBE)); // 01234
                                                       // Scientific notation with exponent 
            // Default decimal precision is 6 but can be controlled 
            Console.WriteLine(l.ToString("E", enUS));  // 1.234568E+008 
            Console.WriteLine(l.ToString("E3", enUS)); // 1.235E+008


            // Digit placeholders 
            // Doesn’t insert zeros if no digit appears at the given position 
            Console.WriteLine(i.ToString("#####", enUS)); // 1234

            
            // Zero placeholders 
            // Inserts zeros if no digit appears at the given position 
            Console.WriteLine(i.ToString("00000", enUS)); // 01234
            
            // Decimal point 
            // Here mixed used with trailing zero padding 
            Console.WriteLine(d.ToString("0###.##", enUS)); // 0123.46
    
            Console.Read();
        }
    }
}
