using System;
using System.IO;

namespace AsciiDotNet
{
    class Program
    {
        private const int DEFAULT_SCALE = 50;

        static void Main(string[] args)
        {
            String filename;
            string scale;

            if(args.Length == 0)
            {
                Console.Write("Input File Name: ");
                filename = Console.ReadLine();
                Console.Write("Input scale: ");
                scale = Console.ReadLine();
            }
            else
            {
                filename = args[0];
                scale = args.Length > 1 ? args[1] : DEFAULT_SCALE.ToString();
            }

            // make sure "scale" is alright
            int intscale = DEFAULT_SCALE;
            try
            {
                intscale = int.Parse(scale);
            }
            catch(FormatException)
            {
                Console.WriteLine($"Error: {scale} is not an integer.");
                return;
            }

            try
            {
                Ascii ascii = new Ascii(filename, intscale);
                ascii.Export();
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine($"Error: File {filename} does not exist.");
            }
        }
    }
}
