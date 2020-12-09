using System;
using System.IO;
using System.Text;

namespace EstudosCSharp.IO.Sync
{
    /// <summary>
    /// What a stream really represents is a stream of bytes allowing read, write, and seek operations, allowing more general operations than using StreamReader and StreamWriter(.
    /// </summary>
    public static class Stream
    {
        /// <summary>
        /// The following code uses a byte array as an in-memory storage for data that gets surfaced through two memory streams.
        /// Each stream has an offset in the array and can be marked as read-only. Illustration can be found at bart the smet c# unleashed pg 1410
        /// </summary>
        public static void TwoMemoryStreamsOverSingleDataSync()
        {
            var bs = new byte[20];
            var ms1 = new MemoryStream(bs, index: 5, count: 10, writable: true);
            var ms2 = new MemoryStream(bs, index: 10, count: 10, writable: false);
            ms1.Seek(5, SeekOrigin.Begin); // @ relative = 5, absolute = 15
            ms1.Write(new byte[] { 9, 8, 7 }, 0, 3);
            Console.WriteLine(bs[10]); // 0x09 @ absolute = 10
            Console.WriteLine(ms2.ReadByte()); // 0x09 @ relative = 0, absolute = 10
            Console.WriteLine(ms2.ReadByte()); // 0x08 @ relative = 1, absolute = 11
            Console.WriteLine(ms2.ReadByte()); // 0x07 @ relative = 2, absolute = 12
            bs[18] = 1; // 0x01 @ absolute = 18
            ms2.Seek(5, SeekOrigin.Current);
            Console.WriteLine(ms2.ReadByte()); // 0x01 @ relative = 8, absolute = 18
        }

        /// <summary>
        /// This trivial example shows how to obtain a FileStream object that can read from the specified
        /// file.It uses the ReadByte method two times to read the first 2 bytes of an executable
        /// file, which are always MZ, after the creator of the executable file format, Mark Zbikowski.
        /// The OpenRead method is just one of the many Open methods available. It controls the 
        /// operations allowed on the obtained FileStream, in the case of OpenRead being set to read-only.
        /// The most generic method is Open,which can be passed a number of flags, including the file mode, 
        /// file access, and file sharing:
        /// </summary>
        public static void FileStreamFromAnExeSync()
        {
            string path = @"\windows\system32\notepad.exe";
            using (FileStream fs = File.OpenRead(path))
            {
                var m = (char)fs.ReadByte();
                var z = (char)fs.ReadByte();
                Console.WriteLine("{0}{1}", m, z);
            }

            using (FileStream fs = File.Open(path, FileMode.Open,  // indicates the actions implied by opening a file. This includes the capability to create a file with or without overwriting behavior, to append to an existing file, and so on
                FileAccess.Read, // FileAccess is simply read, write, or a combination of both. The flags enum has a convenient ReadWrite combo available
                FileShare.ReadWrite)) // FileShare controls what others can do with the file while the FileStream is used. For example, you can allow others to read the file while it’s in use but disallow simultaneous writes.
            {
                var m = (char)fs.ReadByte();
                var z = (char)fs.ReadByte();
                Console.WriteLine("{0}{1}", m, z);
            }

        }

        /// <summary>
        /// The create BinaryReader is just a view over the underlying file stream object, which can
        /// now be consumed more easily.A binary reader object by itself doesn’t have ways to
        /// change the position in the stream but does expose the underlying stream through the
        /// BaseStream property.  data from the selected position using some primitive type is where the use of a
        /// BinaryReader comes in handy.For example, to get an Int32 value from the current stream’s position, 
        /// we can use ReadInt32.This reads 4 bytes and stuffs them into a signed-bit integer in a little-endian format.
        /// </summary>
        public static void BinaryReaderGetsFileSignatureSync()
        {
            string path = @"\windows\system32\notepad.exe";

            using (var fs = File.OpenRead(path))
            using (var br = new BinaryReader(fs))
            {
                br.BaseStream.Position = 0x3c;
                int offset = br.ReadInt32();
                br.BaseStream.Position = offset;
                char[] signature = br.ReadChars(2);
                Console.WriteLine(new string(signature));
            }
        }

        
    }
}
