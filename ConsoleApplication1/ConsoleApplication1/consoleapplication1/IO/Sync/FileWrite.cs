using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

        /// <summary>
        /// serializes a class instance data to an array of bytes to a FileStream
        /// </summary>
        public static void BinaryFormatterSerialize()
        {
            // Create a hashtable of values that will eventually be serialized.
            Hashtable addresses = new Hashtable();
            addresses.Add("Jeff", "123 Main Street, Redmond, WA 98052");
            addresses.Add("Fred", "987 Pine Road, Phila., PA 19116");
            addresses.Add("Mary", "PO Box 112233, Palo Alto, CA 94301");

            // To serialize the hashtable and its key/value pairs,
            // you must first open a stream for writing.
            // In this case, use a file stream.
            FileStream fs = new FileStream("DataFile.dat", FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, addresses);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        public static void BinaryFormatterDeserialize()
        {
            // Declare the hashtable reference.
            Hashtable addresses = null;

            // Open the file containing the data that you want to deserialize.
            FileStream fs = new FileStream("DataFile.dat", FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and
                // assign the reference to the local variable.
                addresses = (Hashtable)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            // To prove that the table deserialized correctly,
            // display the key/value pairs.
            foreach (DictionaryEntry de in addresses)
            {
                Console.WriteLine("{0} lives at {1}.", de.Key, de.Value);
            }
        }
    }
}
