using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EstudosCSharp.Threads
{
    public static class Samples
    {
        public static void CancellationTokenSource()
        {
            var cTokenSource = new CancellationTokenSource();
            var cToken = cTokenSource.Token;
            Task t = Task.Factory.StartNew(() =>
            {
                while (!cToken.IsCancellationRequested)
                {
                    Thread.Sleep(1000);
                    Console.Write('.');
                }
                Console.WriteLine("cancelation requested!");
            }, cToken);
            
            Console.WriteLine("press a key to stop!");
            Console.ReadKey();
            cTokenSource.Cancel();
        }

        public static void TaskCompletionSource()
        {
            TaskCompletionSource<string> tcs1 = new TaskCompletionSource<string>();
            Task<string> t1 = tcs1.Task;

            var cTokenSource = new CancellationTokenSource();
            var cToken = cTokenSource.Token;
            Task.Factory.StartNew(() =>
            {
                while (!cToken.IsCancellationRequested)
                {
                    Thread.Sleep(1000);
                    Console.Write('.');
                }
                tcs1.SetResult("cancelation requested, msg returned by TaskCompletionSource!");
            }, cToken);

            Console.WriteLine("press a key to stop!");
            Console.ReadKey();
            cTokenSource.Cancel();
            Console.WriteLine(t1.Result);
        }

        public static void TaskFromAsync(string path)
        {
            FileInfo fi = new FileInfo(path);
            byte[] data = null;
            data = new byte[fi.Length];

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, data.Length, true);

            //Task<int> returns the number of bytes read
            Task<int> task = Task<int>.Factory.FromAsync(
                    fs.BeginRead, fs.EndRead, data, 0, data.Length, null);

            // It is possible to do other work here while waiting
            // for the antecedent task to complete.
            // ...

            // Add the continuation, which returns a Task<string>.
            var tCont = task.ContinueWith((antecedent) =>
            {
                fs.Close();

                // Result = "number of bytes read" (if we need it.)
                if (antecedent.Result < 100)
                {
                    return "Data is too small to bother with.";
                }
                else
                {
                    // If we did not receive the entire file, the end of the
                    // data buffer will contain garbage.
                    if (antecedent.Result < data.Length)
                        Array.Resize(ref data, antecedent.Result);

                    // Will be returned in the Result property of the Task<string>
                    // at some future point after the asynchronous file I/O operation completes.
                    return new UTF8Encoding().GetString(data);
                }
            });

            Console.WriteLine(tCont.Result);
        }
        public static void ContinueWhenAll(string[] filesToRead)
        {
            FileStream fs;
            Task<string>[] tasks = new Task<string>[filesToRead.Length];
            byte[] fileData = null;
            for (int i = 0; i < filesToRead.Length; i++)
            {
                fileData = new byte[0x1000];
                fs = new FileStream(filesToRead[i], FileMode.Open, FileAccess.Read, FileShare.Read, fileData.Length, true);

                // By adding the continuation here, the
                // Result of each task will be a string.
                tasks[i] = Task<int>.Factory.FromAsync(
                         fs.BeginRead, fs.EndRead, fileData, 0, fileData.Length, null)
                         .ContinueWith((antecedent) =>
                         {
                             fs.Close();

                             // If we did not receive the entire file, the end of the
                             // data buffer will contain garbage.
                             if (antecedent.Result < fileData.Length)
                                 Array.Resize(ref fileData, antecedent.Result);

                             // Will be returned in the Result property of the Task<string>
                             // at some future point after the asynchronous file I/O operation completes.
                             return new UTF8Encoding().GetString(fileData);
                         });
            }

            // Wait for all tasks to complete.
            var t2 = Task<string>.Factory.ContinueWhenAll(tasks, (data) =>
            {
                // Propagate all exceptions and mark all faulted tasks as observed.
                Task.WaitAll(data);

                // Combine the results from all tasks.
                StringBuilder sb = new StringBuilder();
                foreach (var t in data)
                {
                    sb.Append(t.Result);
                }
                // Final result to be returned eventually on the calling thread.
                return sb.ToString();
            });

            Console.WriteLine(t2.Result);
        }

        public static void WaitAll()
        {
            var tasks = new List<Task<int>>();

            // Define a delegate that prints and returns the system tick count
            Func<object, int> action = (object obj) =>
            {
                int i = (int)obj;

                // Make each thread sleep a different time in order to return a different tick count
                Thread.Sleep(i * 100);

                // The tasks that receive an argument between 2 and 5 throw exceptions
                if (2 <= i && i <= 5)
                {
                    throw new InvalidOperationException("SIMULATED EXCEPTION");
                }

                int tickCount = Environment.TickCount;
                Console.WriteLine("Task={0}, i={1}, TickCount={2}, Thread={3}", Task.CurrentId, i, tickCount, Thread.CurrentThread.ManagedThreadId);

                return tickCount;
            };

            // Construct started tasks
            for (int i = 0; i < 10; i++)
            {
                int index = i;
                tasks.Add(Task<int>.Factory.StartNew(action, index));
            }

            try
            {
                // Wait for all the tasks to finish.
                Task.WaitAll(tasks.ToArray());

                // We should never get to this point
                Console.WriteLine("WaitAll() has not thrown exceptions. THIS WAS NOT EXPECTED.");
            }
            catch (AggregateException e)
            {
                Console.WriteLine("\nThe following exceptions have been thrown by WaitAll(): (THIS WAS EXPECTED)");
                for (int j = 0; j < e.InnerExceptions.Count; j++)
                {
                    Console.WriteLine("\n-------------------------------------------------\n{0}", e.InnerExceptions[j].ToString());
                }
            }
        }

    }
    
}
