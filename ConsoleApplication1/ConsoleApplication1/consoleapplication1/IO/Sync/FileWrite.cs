using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace EstudosCSharp.IO.Sync
{
    /// <summary>
    /// Write operations over File class
    /// </summary>
    public static class FileWrite
    {
        public static readonly List<string> SampleText = new List<string> {
            "This is a sample text!",
            "Have fun!"
        };

        /// <summary>
        /// Creates or overwrites a file with a content acquired from another file. Sytem.IO.File.WriteAllLines receives an IEnumerable param, and is what differs it from WriteAllText
        /// </summary>
        /// <param name="sourcePath">source of the file which shall have content copied</param>
        /// <param name="targetPath">path further files name where it has to be saved</param>
        public static void WriteAllLines(string sourcePath, string targetPath)
        {
            File.WriteAllLines(
                targetPath,
                from line in File.ReadLines(sourcePath).Take(10) // ReadLines is an iterator, so only 10 first lines will be read. It´s an advantage over ReadAllLInes which reads all the file
                where !string.IsNullOrWhiteSpace(line)
                select line.TrimStart()
            );
            Console.WriteLine($"File was wrote at {targetPath}.");
        }

        /// <summary>
        /// Creates or overwrites a file using a content passed by param. System.IO.File.WriteAllText receives a string as param for the content
        /// </summary>
        /// <param name="content">content of the file</param>
        /// <param name="targetPath">path where file will be saved</param>
        public static void WriteAllText(string content, string targetPath)
        {
            File.WriteAllText(
                targetPath,
                content
            );
            Console.WriteLine($"File was wrote at {targetPath}.");
        }

        public static void AppendAllLines(IEnumerable<string> contents, string targetFilePath)
        {
            File.AppendAllLines(targetFilePath, contents);
            Console.WriteLine($"All content lines was appended to {targetFilePath}");
        }

        public static void AppendAllText(string content, string targetFilePath)
        {
            File.AppendAllText(targetFilePath, content);
            Console.WriteLine($"content was appended to {targetFilePath}");
        }

        public static void FileCreateText(string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.AutoFlush = true;
                sw.WriteLine("{0}-Started", DateTime.Now);
                Thread.Sleep(1000);
                sw.WriteLine("{0}-Stopped", DateTime.Now);
            }
        }
    }
}
