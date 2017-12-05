using System;
using System.Text;
using System.Drawing;
using System.IO;
using SixLabors.ImageSharp;

namespace AsciiDotNet
{
    internal class Ascii
    {
        private const int scale = 70;
        private readonly int xscale = 30;
        private readonly int yscale = 60;
        private StringBuilder output;

        internal Ascii(string filename)
        {
            output = new StringBuilder();

            using (Image<Rgba32> img = Image.Load(filename))
            {
                /*
                xscale = img.Width / scale;
                yscale = (int)((((float)img.Height / img.Width) * xscale) * 1.5);
                */
                Console.WriteLine($"xscale: {xscale}, yscale: {yscale}");
                Process(img);
            }
        }

        internal void Export()
        {
            Console.WriteLine(output.ToString());
        }

        private void Process(Image<Rgba32> img)
        {
            int width = img.Width;
            int height = img.Height;

            for (int y = 0; y < height; y += yscale)
            {
                for (int x = 0; x < width; x += xscale)
                {
                    Rgba32 pixel = img[x, y];

                    int avg = (pixel.R + pixel.G + pixel.B) / 3;
                    char symbol = Ascii.Symbol(avg, pixel.A);

                    output.Append(symbol);
                }

                output.Append(Environment.NewLine);
            }
        }

        private static char Symbol(int avg, int alpha)
        {
            if(alpha == 0)
                return ' ';

            if(avg < 25)
                return '.';
            if(avg < 50)
                return '^';
            if(avg < 75)
                return '*';
            if(avg < 100)
                return '!';
            if(avg < 125)
                return 'T';
            if(avg < 150)
                return 'H';
            if(avg < 175)
                return '&';
            if(avg < 200)
                return '%';
            if(avg < 225)
                return 'W';

            return '#';
        }
    }
}
