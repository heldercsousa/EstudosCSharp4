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
            Console.WriteLine("Format Number to String");
            Console.WriteLine(123.45.ToString(new CultureInfo("en-US")));
            Console.WriteLine(123.45.ToString(new CultureInfo("nl-BE")));
            Console.WriteLine("{0:N2}", 29.32f);  // c 2 casas decimais 23,55 por ex
            Console.WriteLine("{0:C1}", 29.32f);  // c 1 casa decimal 29.32 com cifrão da cultura corrente
            Console.WriteLine(29.99f.ToString("0000.###"));
            Console.WriteLine(10.ToString("00000"));
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

        /// <summary>
        /// using standard format specifiers for datetime
        /// </summary>
        public static void DateTimeFormats()
        {
            Console.WriteLine("Format DateTime to String");
            DateTime dt = DateTime.Now;
            Console.WriteLine("{0:t}", dt);  // "4:05 PM"                         ShortTime
            Console.WriteLine("{0:d}", dt);  // "3/9/2008"                        ShortDate
            Console.WriteLine("{0:T}", dt);  // "4:05:07 PM"                      LongTime
            Console.WriteLine("{0:D}", dt);  // "Sunday, March 09, 2008"          LongDate
            Console.WriteLine("{0:f}", dt);  // "Sunday, March 09, 2008 4:05 PM"  LongDate+ShortTime
            Console.WriteLine("{0:F}", dt);  // "Sunday, March 09, 2008 4:05:07 PM" FullDateTime
            Console.WriteLine("{0:g}", dt);  // "3/9/2008 4:05 PM"                ShortDate+ShortTime
            Console.WriteLine("{0:G}", dt);  // "3/9/2008 4:05:07 PM"             ShortDate+LongTime
            Console.WriteLine("{0:m}", dt);  // "March 09"                        MonthDay
            Console.WriteLine("{0:y}", dt);  // "March, 2008"                     YearMonth
            Console.WriteLine("{0:r}", dt);  // "Sun, 09 Mar 2008 16:05:07 GMT"   RFC1123
            Console.WriteLine("{0:s}", dt);  // "2008-03-09T16:05:07"             SortableDateTime
            Console.WriteLine("{0:u}", dt);  // "2008-03-09 16:05:07Z"            UniversalSortableDateTime
            Console.WriteLine("{0:MM/dd/yyy}", dt);

            Console.Read();
        }
    }
}
