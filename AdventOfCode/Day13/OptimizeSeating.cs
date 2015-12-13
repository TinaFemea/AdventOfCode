using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    class OptimizeSeating
    {
        private Dictionary<Tuple<string, string>, int> happinessMap = new Dictionary<Tuple<string, string>, int>();
        private List<string> allPeople = new List<string>();

        private void AddToHappinessMap(string thisString)
        {
            string[] tokens = thisString.Split(' ');
            string firstPerson = tokens.First();
            string lastPerson = tokens.Last().TrimEnd('.');
            bool isPostive = tokens.Contains("gain");
            int amount = Int32.Parse(tokens[3]);

            if (!isPostive)
                amount = amount*-1;

            happinessMap[new Tuple<string, string>(firstPerson, lastPerson)] = amount;

            if (!allPeople.Contains(firstPerson))
                allPeople.Add(firstPerson);
            if (!allPeople.Contains(lastPerson))
                allPeople.Add(lastPerson);
        }


        public static List<List<string>> BuildPermutations(List<string> items)
        {
            if (items.Count > 1)
            {
                return items.SelectMany(item => BuildPermutations(items.Where(i => !i.Equals(item)).ToList()),
                                       (item, permutation) => new[] { item }.Concat(permutation).ToList()).ToList();
            }

            return new List<List<string>> { items };
        }

        private void ProcessInput()
        {
            foreach (string theLine in File.ReadLines("input.txt"))
                AddToHappinessMap(theLine);

            //  Add myself
            foreach (string person in allPeople)
            {
                happinessMap[new Tuple<string, string>("me", person)] = 0;
                happinessMap[new Tuple<string, string>(person, "me")] = 0;
            }
            allPeople.Add("me");
        }

        public long ComputeHappiness()
        {
            ProcessInput();

            List<List<string>> possibilities = BuildPermutations(allPeople);

            long maxHappiness = long.MinValue;
            foreach (List<string> possibility in possibilities)
            {
                long thisPossibilityHappiness = 0;

                for (int i = 0; i < possibility.Count; i++)
                {
                    //  get the people on either side.
                    string firstPerson = possibility[i];
                    string leftPerson = (i == 0) ? possibility[possibility.Count - 1] : possibility[i - 1];
                    string rightPerson = (i == possibility.Count - 1) ? possibility[0] : possibility[i + 1];

                    thisPossibilityHappiness += happinessMap[new Tuple<string, string>(firstPerson, leftPerson)];
                    thisPossibilityHappiness += happinessMap[new Tuple<string, string>(firstPerson, rightPerson)];
                }

                maxHappiness = Math.Max(maxHappiness, thisPossibilityHappiness);
            }

            return maxHappiness;
        }
    }
}
