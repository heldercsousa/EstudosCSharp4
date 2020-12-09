using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace EstudosCSharp.IO.Async
{
    /// <summary>
    /// The essence of Async feature is found in Begin/End method pairs, such as BeginRead and 
    /// EndRead. Where Read is a blocking operation waiting for data to be read and bubbled up to
    /// the application, the BeginRead method immediately returns. However, it doesn’t have the
    /// requested data available right away. Instead, it will call you back after it has completed.To
    /// do so, a callback procedure is passed to the BeginRead method.As we all know, passing
    /// around a reference to a method is accomplished with a delegate
    /// </summary>
    public static class Stream
    {
        /// <summary>
        /// An example of calling the Begin method is shown here, using a lambda expression to
        /// declare the callback procedure.Notice we pass null for the stateObject for a reason
        /// explained later
        /// </summary>
        public static void FileStreamAsync()
        {
            string path = @"\windows\system32\notepad.exe";
            var iterator = _Indicator().GetEnumerator();

            using (var fs = File.OpenRead(path))
            {
                var buffer = new byte[1024 * 1024 * 1024]; // 1 GB buffer
                IAsyncResult iar = fs.BeginRead(buffer, 0, buffer.Length, result =>
                {
                    // This code will be called upon completion.
                    int count = fs.EndRead(result); // It basically accepts the asynchronous result object and returns the operation’s result, which in the case of file read is the number of bytes read and written to the buffer.
                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine("{0} bytes read", count);
                }, null);

                Console.Write("Read operation started");
                while (!iar.IsCompleted)
                {
                    Console.SetCursorPosition(0, 1);
                    iterator.MoveNext();
                    Console.Write(iterator.Current);
                }                
            }
        }

        private static FileStream _fs;

        /// <summary>
        /// In this case, state parameter `buffer` is passed to BeginRead, meaning it will be available right inside _OnReadCompleted method.
        /// It avoids declaring a property specifically to share this variable among BeginRead and EndRead moments.
        /// </summary>
        public static void FileStreamWithStateAsync()
        {
            string path = @"\windows\system32\notepad.exe";
            _fs = File.OpenRead(path);

            var iterator = _Indicator().GetEnumerator();

            var buffer = new byte[1024 * 1024 * 1024]; // 1 GB buffer
            IAsyncResult iar = _fs.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(_OnReadCompleted), buffer);

            Console.Write("Read operation started");
            while (!iar.IsCompleted)
            {
                Console.SetCursorPosition(0, 3);
                iterator.MoveNext();
                Console.Write(iterator.Current);
            }
        }

        private static void _OnReadCompleted(IAsyncResult result)
        {
            int n = _fs.EndRead(result);
            byte[] buffer = (byte[])result.AsyncState; // access the state parameter `buffer`
            
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("{0} bytes read --> {1}", n, string.Join("",buffer.Take(10)));

            _fs.Dispose();
        }

        /// <summary>
        /// it’s possible to invoke any delegate asynchronously using the
        /// BeginInvoke method.To retrieve the result of the computation, you use the corresponding
        /// EndInvoke method, which obviously takes in an IAsyncResult.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<string> _Indicator()
        {
            while (true)
            { 
                yield return "|";
                yield return "/";
                yield return "-";
                yield return @"\";
                yield return "|";
                yield return "/";
                yield return "-";
                yield return @"\";
            }
        }

        public static IAsyncResult DelegateInvocation()
        {
            Func<int, int> twice = x => x * 2;
            int n = 21;
            Console.WriteLine("Please wait while we`re computing({0} * 2)...", n);
            return twice.BeginInvoke(n, iar =>
            {
                int res = twice.EndInvoke(iar);
                Console.WriteLine(@"And the answer to({0} * 2) is {1}!", n, res);
            }, null /* the dreaded state parameter */);

        }
    }
}
