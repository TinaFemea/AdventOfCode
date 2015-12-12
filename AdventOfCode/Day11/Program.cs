using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Day11
{
    class Program
    {
        static bool IsValid(char[] inputString)
        {
            if (inputString.Any(t => t == 'i' ||
                                     t == 'o' ||
                                     t == 'l'))
                return false;

            bool foundStraight = false;
            for (int i = 0; i < inputString.Length - 2; i++)
            {
                if ((inputString[i + 2] == inputString[i + 1] +1) &&
                    (inputString[i + 1] == inputString[i] + 1))
                {
                    foundStraight = true;
                    break;
                }
            }
            if (!foundStraight)
                return false;

            int numPairs = 0;
            char firstPairChar = Char.MinValue;
            for (int i = 0; i < inputString.Length - 1; i++)
            {
                if (inputString[i] != inputString[i + 1]) 
                    continue;
                if (inputString[i] == firstPairChar)
                    continue;
                firstPairChar = inputString[i];
                i++;
                numPairs++;
                if (numPairs >= 2)
                    break;
            }

            return numPairs >= 2;
        }

        static char[] IncrementMe(char[] theString)
        {
            if (theString.Length == 0)
                return new char[0];

            if (theString[theString.Length - 1] == 'z')
            {
                char[] oldArrayBits = theString.Take(theString.Length - 1).ToArray();
                List<char> newBits = new List<char>(oldArrayBits.Length + 1);
                newBits.AddRange(IncrementMe(oldArrayBits));
                newBits.Add('a');
                return newBits.ToArray();
            }

            theString[theString.Length-1]++;
            if (theString[theString.Length - 1] == 'i' ||
                theString[theString.Length - 1] == 'o' ||
                theString[theString.Length - 1] == 'l')
                theString[theString.Length - 1]++;

            return theString;
        }

        static string FindNext(string inputString)
        {
            char[] theString = inputString.ToCharArray();

            while (!IsValid(theString))
                theString = IncrementMe(theString);

            return new string(theString);
        }
        static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

           /* bool isValid = IsValid("hijklmmn".ToCharArray());
            isValid = IsValid("abbceffg".ToCharArray());
            isValid = IsValid("abbcegjkbbj".ToCharArray());

            //string nextValid = FindNext("abcdefgh".ToCharArray());
            //string nextValid = FindNext("ghijklmn".ToCharArray());
          */
            string nextValid = FindNext("cqjxjnds");

            timer.Stop();
            Console.WriteLine("{0} in {1} ms", nextValid, timer.ElapsedMilliseconds);
            timer.Start();

            nextValid = FindNext(new string(IncrementMe(nextValid.ToCharArray())));

            timer.Stop();
            Console.WriteLine("{0} in {1} ms", nextValid, timer.ElapsedMilliseconds);

            Console.ReadLine();
        }
    }
}
