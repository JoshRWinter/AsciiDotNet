using System;
using System.IO;

namespace AsciiDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            String filename;
            if(args.Length == 0)
            {
                Console.Write("Input File Name: ");
                filename = Console.ReadLine();
            }
            else
            {
                filename = args[0];
            }

            try
            {
                Ascii ascii = new Ascii(filename);
                ascii.Export();
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine($"Error: File {filename} does not exist.");
            }
        }
    }
}
