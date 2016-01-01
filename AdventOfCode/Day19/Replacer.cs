using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Day19
{
    class Replacer
    {

        List<Tuple<string, string>> replacements = new List<Tuple<String, String>>();
        Dictionary<string, int> results = new Dictionary<String, Int32>(1000000);

        private void Parse(string input)
        {
            string[] tokens = input.Split(new[] { " => " }, StringSplitOptions.None);
            replacements.Add(new Tuple<String, String>(tokens[0], tokens[1]));

        }

        private void Iterate(string input, long maxLength)
        {
            foreach (Tuple<string, string> replacement in replacements)
            {
                int currPosition = input.IndexOf(replacement.Item1);
                while (currPosition != -1)
                {
                    int firstEnd = Math.Max(currPosition, 0);
                    int secondBegin = currPosition + replacement.Item1.Length;
                    int secondLength = Math.Max(input.Length - secondBegin, 0);
                    string newString = input.Substring(0, firstEnd) + replacement.Item2 + input.Substring(secondBegin, secondLength);

                    if (newString.Length <= maxLength)
                        results[newString] = 0;

                    currPosition = input.IndexOf(replacement.Item1, currPosition + 1);
                }
            }
        }

        private int IterateBackwards(string input)
        {
            int totalReplacments = 0;

            Queue<Tuple<string, int>> queue = new Queue<Tuple<string, int>>();
            queue.Enqueue(new Tuple<string, int>(input, 0));

            while(queue.Count > 0)
            {
                Tuple<string, int> next = queue.Dequeue();
                string thisString = next.Item1;
                int thisCount = next.Item2;

               foreach (Tuple<string, string> replacement in replacements)
                {
                    int numReplacments = Regex.Matches(input, replacement.Item2, RegexOptions.Compiled).Count;
                    if (numReplacments > 0)
                    {
                        thisString = input.Replace(replacement.Item2, replacement.Item1);
                        thisCount += numReplacments;

                        if (thisString == "e")
                            return thisCount;
                    }
                }
                queue.Enqueue(new Tuple<string, int>(thisString, thisCount));
            }
/*

            while (!input.Equals("e"))
            {
                foreach (Tuple<string, string> replacement in replacements)
                {
                    int numReplacments = Regex.Matches(input, replacement.Item2, RegexOptions.Compiled).Count;
                    if (numReplacments > 0)
                    {
                        string result = input.Replace(replacement.Item2, replacement.Item1);
                        results[result] 
                        if (result.Equals("e"))
                            return results
                        totalReplacments += numReplacments;
                    }
                }
            }
            */
            return totalReplacments;
        }


        private int SortReplacents(Tuple<string, string> x, Tuple<string, string> y)
        {
            if (x == null && y == null)
                return 0;

            if (x == null)
                return 1;
            if (y == null)
                return -1;
            if ((x.Item2.Length - x.Item1.Length)> (y.Item2.Length - y.Item1.Length))
                return -1;
            if ((x.Item2.Length - x.Item1.Length) < (y.Item2.Length - y.Item1.Length))
                return 1;

            return 0;
        }
        public void Replace()
        {
            string startingString = "CRnCaSiRnBSiRnFArTiBPTiTiBFArPBCaSiThSiRnTiBPBPMgArCaSiRnTiMgArCaSiThCaSiRnFArRnSiRnFArTiTiBFArCaCaSiRnSiThCaCaSiRnMgArFYSiRnFYCaFArSiThCaSiThPBPTiMgArCaPRnSiAlArPBCaCaSiRnFYSiThCaRnFArArCaCaSiRnPBSiRnFArMgYCaCaCaCaSiThCaCaSiAlArCaCaSiRnPBSiAlArBCaCaCaCaSiThCaPBSiThPBPBCaSiRnFYFArSiThCaSiRnFArBCaCaSiRnFYFArSiThCaPBSiThCaSiRnPMgArRnFArPTiBCaPRnFArCaCaCaCaSiRnCaCaSiRnFYFArFArBCaSiThFArThSiThSiRnTiRnPMgArFArCaSiThCaPBCaSiRnBFArCaCaPRnCaCaPMgArSiRnFYFArCaSiThRnPBPMgAr";
            foreach (string input in File.ReadLines("input.txt"))
                Parse(input);

            replacements.Sort(SortReplacents);

            IterateBackwards(startingString);

            //     Iterate(startingString, long.MaxValue);

      //      results.Clear();
            long numLoops = 0;
            results[startingString] = 0;
            while (!results.ContainsKey("e"))
            {
                List<string> newInputs = new List<String>(results.Keys);
                results.Clear();
            
                foreach (string currInput in newInputs)
                    IterateBackwards(currInput);
                numLoops++;
                Console.WriteLine("{0}: results: {1}", numLoops, results.Count);
            }
           
            Console.WriteLine(numLoops);
        }

        public void AnotherTry()
        {
            string str = "CRnCaSiRnBSiRnFArTiBPTiTiBFArPBCaSiThSiRnTiBPBPMgArCaSiRnTiMgArCaSiThCaSiRnFArRnSiRnFArTiTiBFArCaCaSiRnSiThCaCaSiRnMgArFYSiRnFYCaFArSiThCaSiThPBPTiMgArCaPRnSiAlArPBCaCaSiRnFYSiThCaRnFArArCaCaSiRnPBSiRnFArMgYCaCaCaCaSiThCaCaSiAlArCaCaSiRnPBSiAlArBCaCaCaCaSiThCaPBSiThPBPBCaSiRnFYFArSiThCaSiRnFArBCaCaSiRnFYFArSiThCaPBSiThCaSiRnPMgArRnFArPTiBCaPRnFArCaCaCaCaSiRnCaCaSiRnFYFArFArBCaSiThFArThSiThSiRnTiRnPMgArFArCaSiThCaPBCaSiRnBFArCaCaPRnCaCaPMgArSiRnFYFArCaSiThRnPBPMgAr";

            Func<string, int> countStr = x =>
            {
                var count = 0;
                for (var index = str.IndexOf(x); index >= 0; index = str.IndexOf(x, index + 1), ++count) { }
                return count;
            };

            var num = str.Count(char.IsUpper) - countStr("Rn") - countStr("Ar") - 2 * countStr("Y") - 1;
        }
    }
}
