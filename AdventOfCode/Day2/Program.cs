using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day2
{
    class Program
    {
        static List<int> GetDimensions(string oneLine)
        {
            List<int> numberDimensions = oneLine.Split('x').Select(Int32.Parse).ToList();
            if (numberDimensions.Count != 3)
                throw new ArgumentException("need 3 dimensions");
            numberDimensions.Sort();
            return numberDimensions;
        }

        static long CalcVolume(string oneLine)
        {
            List<int> dimensions = GetDimensions(oneLine);
            int l = dimensions[0];
            int w = dimensions[1];
            int h = dimensions[2];

            long necessaryPaper = 2*l*w + 2*w*h + 2*h*l;
            long slop = l*w;
            return necessaryPaper + slop;
        }

        static long CalcRibbon(string oneLine)
        {
            List<int> dimensions = GetDimensions(oneLine);
            int l = dimensions[0];
            int w = dimensions[1];
            int h = dimensions[2];

            long ribbon = 2 * l + 2 * w;
            long bow = l*w*h;
            return ribbon + bow;
        }
            
        static void Main(string[] args)
        {
            long totalPaper = File.ReadLines("input.txt").Sum(oneLine => CalcVolume(oneLine));
            long totalRibbon = File.ReadLines("input.txt").Sum(oneLine => CalcRibbon(oneLine)); 

            Console.WriteLine("Paper: {0}, Ribbon {1}", totalPaper, totalRibbon);
        }
    }
}
