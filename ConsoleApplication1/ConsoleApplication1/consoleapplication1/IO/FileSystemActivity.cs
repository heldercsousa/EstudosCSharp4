using System;
using System.IO;
using System.Runtime.InteropServices;

namespace EstudosCSharp.IO
{
    public static class FileSystemActivity
    {
        public static void Watch(string path, string fileFilter)
        {
            using (var watcher = new System.IO.FileSystemWatcher(path, fileFilter))
            {
                watcher.Created += (o, e) =>
                {
                    System.Console.WriteLine("Created:" + e.FullPath);
                };
                watcher.Deleted += (o, e) =>
                {
                    System.Console.WriteLine("Deleted:" + e.FullPath);
                };
                watcher.Renamed += (o, e) =>
                {
                    System.Console.WriteLine("Renamed:" + e.FullPath);
                };
                watcher.Changed += (o, e) =>
                {
                    System.Console.WriteLine("Changed:" + e.FullPath);
                };

                watcher.EnableRaisingEvents = true;
                System.Console.WriteLine($"Monitoring {path}");
                System.Console.Read();
            }
        }

    }
}
