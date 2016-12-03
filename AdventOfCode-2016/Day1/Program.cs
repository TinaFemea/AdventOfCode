using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>() { "R2, L3", "R2, R2, R2", "R5, L5, R5, R3", "R8, R4, R4, R8" };
            Walker theWalker = new Walker();
            foreach (string currInput in inputs)
                theWalker.ComputeDistance(currInput);

            string theInput = File.ReadAllText("input.txt");
            theWalker.ComputeDistance(theInput);

            Console.In.ReadLine();
        }

    }
}
