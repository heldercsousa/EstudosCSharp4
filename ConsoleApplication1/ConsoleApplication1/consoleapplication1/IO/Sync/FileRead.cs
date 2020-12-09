using System;
using System.IO;
using System.Text;

namespace EstudosCSharp.IO.Sync
{    
     /// <summary>
     ///  Read operations over File class. Sync read operations over system files. Although all of them are helpful methods, they should be used with some care when potentially big files are involved.
     /// </summary>
    public static class FileRead
    {
        /// <summary>
        /// The ReadAllLines method is nothing but a convenience method around lower-level functionality. It reads all the lines, and than returns them. Not efficient for big files.
        /// </summary>
        /// <param name="path">path to the file to be printed</param>
        public static void ReadAllLines(string path)
        {
            string[] lines = File.ReadAllLines(path);
            foreach (var line in lines)
                Console.WriteLine(line);
        }

        /// <summary>
        /// Same as ReadAllLines, but returns a string rather than an array of string standing for the lines. Contains chars like \n and \r
        /// </summary>
        /// <param name="path"></param>
        public static void ReadAllText(string path)
        {
            string entireText = File.ReadAllText(path);
            Console.WriteLine(entireText);
        }

        /// <summary>
        /// Same as ReadAllLines, but returns an array of bytes
        /// </summary>
        /// <param name="path"></param>
        public static void ReadAllBytes(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            foreach (var by in bytes)
            { 
                Console.WriteLine(by);
            }
        }

        /// <summary>
        /// Reads and returns line by line (IEnumerable is the return type, which has an iterator internally), instead of reading all file to finally return the content, like ReadAllLines. That´s why it is more efficient for big files.
        /// </summary>
        /// <param name="path"></param>
        public static void ReadLines(string path)
        {
            foreach (var ln in File.ReadLines(path)) // reads and returns line by line even if file was not entirely red
            {
                Console.WriteLine(ln);
            }
        }

        /// <summary>
        /// In this example, we basically do what ReadAllLines does internally - read line by line
        /// </summary>
        /// <param name="path"></param>
        public static void FileOpenText(string path)
        {
            Console.WriteLine("reading " + path);
            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                    Console.WriteLine(sr.ReadLine());
            }
        }

        /// <summary>
        /// other methods, such as Read, ReadToEnd, and ReadBlock, can be found, all of
        /// which allow reading at different levels of granularity.For example, the Read method has
        /// two overloads, one of which returns a single character, whereas another takes in an array
        /// of characters to be populated.
        /// </summary>
        /// <param name="path"></param>
        public static void ReaderRead(string path)
        {
            Console.WriteLine("reading " + path);
            using (StreamReader sr = File.OpenText(path))
            {
                var buffer = new char[1024];
                int read = sr.Read(buffer, 0, buffer.Length);
                Console.WriteLine("{0} characters read", read);
            }
        }

        /// <summary>
        /// The two classes we briefly encountered in the previous examples are subclasses of the TextReader and TextWriter classes.Using object-oriented programming
        /// at its best, these base classes offer virtual methods for a series of operations that can be applied in more than just the context of file I/O.In fact, on 
        /// our much beloved Console class, you can find static properties called In and Out that expose a TextReader and a TextWriter
        /// </summary>
        public static void ConsoleInOut()
        {
            Console.Write("Type something:");
            string line = Console.In.ReadLine();
            Console.Out.WriteLine(line);
        }

        /// <summary>
        /// Using the SetIn method, you can simply continue writing code against methods like Console.ReadLine while input is being redirected from another reader:
        /// </summary>
        /// <param name="path"></param>
        public static void ConsoleSetIn(string path)
        {
            Console.SetIn(File.OpenText(path));

            string line;
            while ((line = Console.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// Besides StreamReader and StreamWriter deriving from the TextReader and TextWriter
        /// base classes, other derived types exist. StringReader and StringWriter are such classes,
        /// allowing reading from and writing to string objects. Because strings are immutable, this is
        /// a bit of a lie because the StringWriter uses a StringBuilder to write (and append) to. An example of the use of these two types is shown here
        /// </summary>
        public static void StringReaderAndWriter()
        {
            Console.Write("type something:");
            StringBuilder sb = new StringBuilder();
            TextWriter writer = new StringWriter(sb);
            string input = Console.ReadLine();
            writer.WriteLine(input); // writer writes onto sb
            TextReader reader = new StringReader(sb.ToString());
            string line = reader.ReadLine();
            Console.WriteLine(line);
        }

    }
}
