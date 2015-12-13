using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            OptimizeSeating seater = new OptimizeSeating();

            Console.WriteLine(seater.ComputeHappiness());
            Console.ReadLine();
        }
    }
}
