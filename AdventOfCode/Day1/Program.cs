using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            String inputData = File.ReadAllText("input.txt");

            long upFloors = inputData.Count(thisChar => thisChar == '(');
            long downFloors = inputData.Count(thisChar => thisChar == ')');

            Console.WriteLine("Total Floors: {0}", upFloors - downFloors);

            //  part 2
            long floorOn = 0;
            long moveCounter = 0;
            foreach (char thisMove in inputData)
            {
                if (thisMove == '(')
                    floorOn++;
                if (thisMove == ')')
                    floorOn--;
                if (floorOn == -1)
                {
                    Console.WriteLine("Hit Basement on move {0}", moveCounter);
                    break;
                }    
                moveCounter++;
            }
        }
    }
}
