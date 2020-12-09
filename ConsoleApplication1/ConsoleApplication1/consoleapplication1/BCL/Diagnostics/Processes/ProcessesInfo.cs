using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EstudosCSharp.BCL.Diagnostics.Processes
{
    public static class ProcessesInfo
    {
        /// <summary>
        /// The Process class is the central abstraction to deal with processes. A series of static methods enables you to obtain Process instances by name, process ID, or unfiltered
        /// </summary>
        /// <param name="memoryUsage">which memory usage is greater than this param</param>
        public static void QueryProcessesInfoGreaterThan(int memoryUsage)
        {
            var memoryHogs = from p in Process.GetProcesses()
                             where p.WorkingSet64 > memoryUsage * 1024 * 1024 /* 50 MB */ // The WorkingSet64 property exposes the memory working set size as an Int64 (long) value (and hence the 64 suffix)
                             select p;
            foreach (var memoryHog in memoryHogs)
                Console.WriteLine(memoryHog);
        }

        /// <summary>
        /// Spawning another process is simple using the Process.Start method. The easiest way to achieve the task at hand is to specify an executable filename. Overloads exist that take in
        /// more information, such as arguments and/or user credentials, to perform a “run as” execution. Notice that we don’t specify a full path and rely on the environment settings to resolve the path automatically:
        /// </summary>
        public static void StartProcess(string processStr)
        {
            var process = Process.Start(processStr);
            Console.WriteLine($"{processStr} is running...it´s an async operation. Waiting for this process closing");
            process.WaitForExit();
            Console.WriteLine($"{processStr}.ExitCode:{process.ExitCode}");
        }

        /// <summary>
        /// sample of how to wait async for a process. Here we’re using the ProcessStartInfo object in conjunction with the Process class’s constructor.
        /// </summary>
        /// <param name="processStr">process executable to be waited</param>
        public static void StartProcessAsync(string processStr)
        {
            var cmd = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo(processStr)
            };

            var exited = false;
            cmd.Exited += (o, e) => {
                Console.WriteLine($"{processStr} exited with code {cmd.ExitCode}");
                exited = true;
            };

            cmd.Start();

            Console.WriteLine($"Waiting for {processStr} terminate");
            while (!exited)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
        }
    }
}
