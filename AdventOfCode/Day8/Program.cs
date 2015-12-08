using System;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        static long NumCharsDelta(string thisString)
        {
            long rawCount = thisString.Length;
            long actualCount = rawCount - 2; // for the starting and ending quotes

            for (int i = 0; i < rawCount; i++)
            {
                if ((thisString[i] =='\\') && (i+1 < rawCount && (thisString[i+1] == '\\' || thisString[i+1] == '"')))
                {    //  we went from two characters to one.
                    actualCount--;
                    i++;
                }
                else if ((thisString[i] == '\\') && (i + 3 < rawCount && (thisString[i + 1] == 'x')))
                {   // we went from 4 chars to one.
                    actualCount -= 3;
                    i+=3;
                }
            }

            return rawCount - actualCount;
        }

        static long IncreaseChars(string thisString)
        {
            long rawCount = thisString.Length;
            long actualCount = rawCount + 2; // for the starting and ending quotes

            for (int i = 0; i < rawCount; i++)
            {
                if ((thisString[i] == '"') || (thisString[i] == '\\'))
                {    //  we went from one character to two
                    actualCount++;
                }
            }

            return actualCount - rawCount;
        }
        static void Main(string[] args)
        {
            long result = File.ReadAllLines("input.txt").Sum((Func<string, long>) NumCharsDelta);
            Console.WriteLine(result);

            result = File.ReadAllLines("input.txt").Sum((Func<string, long>)IncreaseChars);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
