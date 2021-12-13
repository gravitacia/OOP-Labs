using System;
using System.IO;
using Backups.Entities;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            string path = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}repository";
        }
    }
}
