using System;

namespace EstudosCSharp.BCL
{
    public static class FormattingByHand
    {
        public static void UsingStringFormat()
        {
            Console.WriteLine("Using custom String.Format");
            var phone = new PhoneNumber();
            phone.Digits = "6134847680";
            IFormatProvider formatter = new PhoneNumberFormatter();
            var phoneFormatted = String.Format(formatter, "{0}", phone);
            Console.WriteLine(phoneFormatted);
        }

        public class PhoneNumber
        {
            private string _digits { get; set; }
            public string Digits { 
                get 
                {
                    return this._digits;
                }
                set 
                {
                    if (value.Length != 10) throw new ArgumentException();
                    this._digits = value;
                }
            }

        }

        public class PhoneNumberFormatter : IFormatProvider, ICustomFormatter
        {
            public string Format(string format, object arg, IFormatProvider formatProvider)
            {
                if (arg is PhoneNumber)
                {
                    var num = (PhoneNumber)arg;
                    return "(" + num.Digits.Substring(0, 3) + ")" + num.Digits.Substring(3, 3) + "-" + num.Digits.Substring(6, 4);
                }
                else if (string.IsNullOrEmpty(format))
                {
                    return arg.ToString();
                }
                else
                {
                    return string.Format("{0:" + format + "}", arg);
                }
            }

            public object GetFormat(Type formatType)
            {
                if (formatType == typeof(ICustomFormatter))
                {
                    return this;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
