using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {


            string[] theInput = File.ReadAllLines("input.txt");
            TriangleChecker checker = new TriangleChecker();
            int counter = 0;

            foreach (string line in theInput)
            {
                string[] numbers = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                int a = int.Parse(numbers[0]);
                int b = int.Parse(numbers[1]);
                int c = int.Parse(numbers[2]);

                if (checker.IsGoodTriangle(a, b, c))
                    counter++;
            }

            Console.Out.WriteLine(counter);

            counter = 0;
            int[,] matrix = new int[3,3];
            for (int i = 0; i < theInput.Length; i+=3)
            {
                for (int k = 0; k < 3; k++)
                {
                    string[] numbers = theInput[i + k].Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < 3; j++)
                        matrix[k, j] = int.Parse(numbers[j]);
                }

                for (int j = 0; j < 3; j++)
                {
                    if (checker.IsGoodTriangle(matrix[0,j], matrix[1,j], matrix[2,j]))
                        counter++;
                }
            }

            Console.Out.WriteLine(counter);
            Console.In.ReadLine();
        }
    }
}
