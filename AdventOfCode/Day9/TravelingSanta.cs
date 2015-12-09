using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace Day9
{

    class TravelingSanta
    {
        Dictionary<Tuple<string, string>, int> locations = new Dictionary<Tuple<string, string>, int>();
        private List<string> allTowns = new List<string>();

        private void AddToMap(string line)
        {
            string[] sides = line.Split(new []{" = "}, StringSplitOptions.None);
            string lhs = sides[0];
            int rhs = Int16.Parse(sides[1]);
            string[] towns = lhs.Split(new[] { " to " }, StringSplitOptions.None);

            locations[new Tuple<string, string>(towns[0], towns[1])] = rhs;
            locations[new Tuple<string, string>(towns[1], towns[0])] = rhs;

            if (!allTowns.Contains(towns[0]))
                allTowns.Add(towns[0]);

            if (!allTowns.Contains(towns[1]))
                allTowns.Add(towns[1]);

        }

        private void ReadInput()
        {
            string[] allLines = File.ReadAllLines("input.txt");
            foreach (string line in allLines)
            {
                AddToMap(line);
            }
        }

        public static List<List<string>> GetPermutations(List<string> items)
        {
            if (items.Count > 1)
            {
                return items.SelectMany(item => GetPermutations(items.Where(i => !i.Equals(item)).ToList()),
                                       (item, permutation) => new [] { item }.Concat(permutation).ToList()).ToList();
            }

            return new List<List<string>> { items };
        }

        private void ProcessPermutations()
        {
            long minTripLength = long.MaxValue;
            long maxTripLength = 0;

            List<List<string>> allPermutations = GetPermutations(allTowns);
            foreach (List<string> thisPermutation in allPermutations)
            {
                long tripLength = 0;
                for (int i = 0; i < thisPermutation.Count - 1; i++)
                {
                    tripLength += locations[new Tuple<string, string>(thisPermutation[i], thisPermutation[i + 1])];
                }

                minTripLength = Math.Min(tripLength, minTripLength);
                maxTripLength = Math.Max(tripLength, maxTripLength);
            }

            Console.WriteLine("Min: {0}", minTripLength);
            Console.WriteLine("Max: {0}", maxTripLength);
        }
        public void GoSantaGo()
        {
            ReadInput();
            ProcessPermutations();
            Console.ReadLine();
        }

    }
}
