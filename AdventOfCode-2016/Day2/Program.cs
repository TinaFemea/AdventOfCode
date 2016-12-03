using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] theInput = File.ReadAllLines("input.txt");

            EvilKeypad theKeypad = new EvilKeypad();
            Tuple<int, int> startingPos = new Tuple<int, int>(3, 1);
            Tuple<int, int> endingPos = startingPos;

            foreach (string line in theInput)
            {

                endingPos = theKeypad.Move(line, endingPos);
                Console.Out.Write(theKeypad.GetHumanReadable(endingPos));
            }
            
            Console.In.ReadLine();
        }
    }
}
