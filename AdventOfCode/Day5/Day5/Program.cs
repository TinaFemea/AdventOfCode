using System;
using System.Collections.Generic;
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
            if (theString.Contains("ab") || theString.Contains("cd") || theString.Contains("pq") ||
                theString.Contains("xy"))
                return false;

            return true;
        }

        static bool IsStringPartTwoNice(String theString)
        {
            // It contains a pair of any two letters that appears at least twice in the string without overlapping, 
            // like xyxy (xy) or aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
            bool matchFound = false;
            for (int i = 0; i < theString.Length - 2; i++)
            {
                String thisPairOfTwoLetters = theString[i].ToString() + theString[i + 1].ToString();
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
            /* Part 1 */
            if (IsStringPartOneNice("ugknbfddgicrmopn") == false)
                throw new ApplicationException("ugknbfddgicrmopn");

            if (IsStringPartOneNice("aaa") == false)
                throw new ApplicationException("aaa");

            if (IsStringPartOneNice("jchzalrnumimnmhp") == true)
                throw new ApplicationException("jchzalrnumimnmhp");

            if (IsStringPartOneNice("haegwjzuvuyypxyu") == true)
                throw new ApplicationException("haegwjzuvuyypxyu");

            if (IsStringPartOneNice("dvszwmarrgswjxmb") == true)
                throw new ApplicationException("dvszwmarrgswjxmb");

            long numLinesNice = File.ReadLines("input.txt").Count(IsStringPartOneNice); // 255
            Console.WriteLine(numLinesNice);

            /* Part 2 */
            if (IsStringPartTwoNice("qjhvhtzxzqqjkmpb") == false)
                throw new ApplicationException("qjhvhtzxzqqjkmpb");

            if (IsStringPartTwoNice("xxyxx") == false)
                throw new ApplicationException("xxyxx");

            if (IsStringPartTwoNice("uurcxstgmygtbstg") == true)
                throw new ApplicationException("uurcxstgmygtbstg");

            if (IsStringPartTwoNice("ieodomkazucvgmuy") == true)
                throw new ApplicationException("ieodomkazucvgmuy");

            long numLines2Nice = File.ReadLines("input.txt").Count(IsStringPartTwoNice); // 55
            Console.WriteLine(numLines2Nice);

        }
    }
}
