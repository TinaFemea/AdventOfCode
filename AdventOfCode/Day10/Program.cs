using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day10
{
    class Program
    {
        static string ExpandInput(string input)
        {
            StringBuilder newString = new StringBuilder();

            int currStartChar = 0;
            int length = input.Length;
            while (currStartChar < length)
            {
                char thisChar = input[currStartChar];
                int count = 1;
                while ((currStartChar + count < length) && (input[currStartChar + count] == thisChar))
                    count++;
                
                newString.Append(count);
                newString.Append(thisChar);
                currStartChar += count;
            }
            return newString.ToString();
        }

        static void Main(string[] args)
        {
            string input = "1321131112";

            for (int i = 0; i < 40; i++)
                input = ExpandInput(input);

            Console.WriteLine("40 iterations: {0}", input.Length);

            for (int i = 0; i < 10; i++)
                input = ExpandInput(input);

            Console.WriteLine("50 iterations: {0}", input.Length);
            Console.ReadLine();
        }
    }
}
