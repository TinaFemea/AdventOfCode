using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Xml.Serialization;

namespace Day5
{
    class Program
    {
        static bool IsStringPartOneNice(string theString)
        {
           // A nice string is one with all of the following properties:
            if (theString.Length < 3)
                return false;

          //  It contains at least three vowels (aeiou only), like aei, xazegov, or aeiouaeiouaeiou.
            char[] vowels = {'a', 'e', 'i', 'o', 'u'};
            if (theString.Count(x => vowels.Contains(x)) < 3)
                return false;

            //  It contains at least one letter that appears twice in a row, like xx, abcdde (dd), or aabbccdd (aa, bb, cc, or dd).
            if (!theString.Take(theString.Count() - 1).Where((item, index) => theString[index + 1] == item).Any())
                return false;

            //   It does not contain the strings ab, cd, pq, or xy, even if they are part of one of the other requirements.  
            if (theString.Contains("ab") || theString.Contains("cd") || theString.Contains("pq") || theString.Contains("xy"))
                return false;

            return true;
        }

        static bool IsStringPartTwoNice(string theString)
        {
            // It contains a pair of any two letters that appears at least twice in the string without overlapping, 
            // like xyxy (xy) or aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
            bool matchFound = false;
            for (int i = 0; i < theString.Length - 2; i++)
            {
                string thisPairOfTwoLetters = theString[i] + theString[i + 1].ToString();
                if (theString.IndexOf(thisPairOfTwoLetters, i + 2, StringComparison.Ordinal) != -1)
                {
                    matchFound = true;
                    break;
                }
            }

            if (!matchFound)
                return false;

            //It contains at least one letter which repeats with exactly one letter between them, 
            //like xyx, abcdefeghi (efe), or even aaa.
            matchFound = false;
            for (int i = 0; i < theString.Length - 2; i++)
            {
                if (theString[i] == theString[i + 2])
                {
                    matchFound = true;
                    break;
                }
                
            }

            if (!matchFound)
                return false;

            return true;
        }

        static void Main(string[] args)
        {
            Stopwatch watch = Stopwatch.StartNew();

            /* Part 1 */
            Trace.Assert(IsStringPartOneNice("ugknbfddgicrmopn"));
            Trace.Assert(IsStringPartOneNice("aaa"));
            Trace.Assert(IsStringPartOneNice("jchzalrnumimnmhp") == false);
            Trace.Assert(IsStringPartOneNice("haegwjzuvuyypxyu") == false);
            Trace.Assert(IsStringPartOneNice("dvszwmarrgswjxmb") == false);

            long numLinesNice = File.ReadLines("input.txt").Count(IsStringPartOneNice); // 255
            Console.WriteLine(numLinesNice);

            /* Part 2 */
            Trace.Assert(IsStringPartTwoNice("qjhvhtzxzqqjkmpb"));
            Trace.Assert(IsStringPartTwoNice("xxyxx"));
            Trace.Assert(IsStringPartTwoNice("uurcxstgmygtbstg") == false);
            Trace.Assert(IsStringPartTwoNice("ieodomkazucvgmuy") == false);

            long numLines2Nice = File.ReadLines("input.txt").Count(IsStringPartTwoNice); // 55
            Console.WriteLine(numLines2Nice);

            watch.Stop();
            long elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Timing: {0}", elapsedMs);
        }
    }
}
