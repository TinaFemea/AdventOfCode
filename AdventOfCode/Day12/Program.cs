using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Day12
{
    class Program
    {
        static int NextNumber(string input, int index, out long nextNumber)
        {
            StringBuilder nextNumberString = new StringBuilder();
            for (int i = index; i < input.Length; i++)
            {
                if (input[i] == '-' || (input[i] >= '0' && input[i] <= '9'))
                    nextNumberString.Append(input[i]);
                else
                {
                    if (nextNumberString.Length > 0)
                    {   //  we were numbering, but we're done now.
                        nextNumber = Int32.Parse(nextNumberString.ToString());
                        return i + 1;
                    }
                }
            }

            nextNumber = 0;
            return -1;
        }

        static int FindPrevUnmatchedOpenBracket(string input, int index)
        {
            int numOpens = 0;
            for (int i = index; i >= 0; i--)
            {
                if (input[i] == '}')
                    numOpens++;
                if (input[i] == '{')
                    numOpens--;

                if (numOpens == -1)
                    return i;
            }
            return -1;
        }
        static int FindNextUnmatchedCloseBracket(string input, int index)
        {
            int numCloses = 0;
            for (int i = index; i < input.Length; i++)
            {
                if (input[i] == '{')
                    numCloses++;
                if (input[i] == '}')
                    numCloses--;

                if (numCloses == -1)
                    return i;
            }
            return -1;

        }
        static string RemoveRedObjects(string input)
        {
            int firstRed = input.IndexOf(":\"red\"");
            while (firstRed != -1)
            {
                int findPreviousCurlyOpenBracket = FindPrevUnmatchedOpenBracket(input, firstRed);
                int findNextCurlyCloseBracket = FindNextUnmatchedCloseBracket(input, firstRed + 6);

                StringBuilder myBuilder = new StringBuilder(input);
                int length = findNextCurlyCloseBracket - findPreviousCurlyOpenBracket;
                myBuilder.Remove(findPreviousCurlyOpenBracket, length + 1);
                input = myBuilder.ToString();
                firstRed = input.IndexOf(":\"red\"");
            }

            return input;
        }
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");
            input = RemoveRedObjects(input);
            long nextNumber;
            int nextIndex = NextNumber(input, 0, out nextNumber);
            if (nextIndex != -1)
            {
                long total = nextNumber;
                do
                {
                    nextIndex = NextNumber(input, nextIndex, out nextNumber);
                    total += nextNumber;
                } while (nextIndex != -1);
                Console.WriteLine("{0}", total);
            }
        }
    }
}
