using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Day16
{
    class SueSearcher
    {
        Dictionary<int, Dictionary<string, int>> ListOfSues = new Dictionary<Int32, Dictionary<String, Int32>>();

        private void Parse(string input)
        {
            string[] tokens = input.Split(' ');
            
            for (int i = 0; i < tokens.Length; i++)
                tokens[i] = tokens[i].TrimEnd(':').TrimEnd(',');

            int sueNumber = Int32.Parse(tokens[1]);

            Dictionary<string, int> theAttributes = new Dictionary<String, Int32>((tokens.Length - 2) / 2);
            for (int i = 2; i < tokens.Length; i += 2)
                theAttributes[tokens[i]] = Int32.Parse(tokens[i+1]);

            ListOfSues[sueNumber] = theAttributes;
        }

        public int FindSue()
        {
            foreach (string line in File.ReadLines("input.txt"))
            {
                Parse(line);
            }

            Dictionary<string, int> requirements = new Dictionary<String, Int32>(10);

            requirements["children"] = 3;
            requirements["cats"] = 7;
            requirements["samoyeds"] = 2;
            requirements["pomeranians"] = 3;
            requirements["akitas"] = 0;
            requirements["vizslas"] = 0;
            requirements["goldfish"] = 5;
            requirements["trees"] = 3;
            requirements["cars"] = 2;
            requirements["perfumes"] = 1;
            

            //  Find anyone with 3 children or no 
            List<int> stillValidSues = new List<int>(ListOfSues.Keys);
            foreach (KeyValuePair<string, int> requirement in requirements)
                stillValidSues = ListOfSues.Where(X => !X.Value.ContainsKey(requirement.Key) || X.Value[requirement.Key] == requirement.Value).Select(Y => Y.Key).Where(X => stillValidSues.Contains(X)).ToList();
            
            return 0;
        }
    }
}
