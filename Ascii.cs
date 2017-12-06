using System;
using System.Text;
using System.Drawing;
using System.IO;
using SixLabors.ImageSharp;

namespace AsciiDotNet
{
    internal class Ascii
    {
        private const int scale = 30;
        private readonly int xscale;
        private readonly int yscale;
        private readonly int width;
        private readonly int height;

        private StringBuilder output;

        internal Ascii(string filename)
        {
            output = new StringBuilder();

            using (Image<Rgba32> img = Image.Load(filename))
            {
                xscale = img.Width / scale;
                yscale = (int)((img.Width / scale) * 1.7);
                width = img.Width;
                height = img.Height;
                Process(img);
            }
        }

        internal void Export()
        {
            Console.WriteLine(output.ToString());
        }

        private void Process(Image<Rgba32> img)
        {

            for (int y = 0; y < height; y += yscale)
            {
                for (int x = 0; x < width; x += xscale)
                {
                    int gray = ProcessChunk(img, x, y);
                    char symbol = gray == -1 ? ' ' : Ascii.Symbol(gray);

                    output.Append(symbol);
                }

                output.Append(Environment.NewLine);
            }
        }

        private int ProcessChunk(Image<Rgba32> image, int xx, int yy)
        {
            int gray = 0;
            int transparent = 0;
            for (int y = yy; y < yy + yscale; ++y)
            {
                for (int x = xx; x < xx + xscale; ++x)
                {
                    Rgba32 pixel = image[x, y];
                    if(pixel.A == 0)
                        ++transparent;

                    gray += (pixel.R + pixel.G + pixel.B) / 3;
                }
            }

            if(transparent > (xscale * yscale) / 2)
                return -1;
            else
                return gray / (xscale * yscale);
        }

        private static char Symbol(int avg)
        {
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
