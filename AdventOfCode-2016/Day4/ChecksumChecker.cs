using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day4
{
    public class ChecksumChecker
    {
        public static bool IsValid(Room room)
        {
            Dictionary<char, int> counter = new Dictionary<char, int>(26);

            foreach (char letter in room.name)
            {
                if (letter == '-')
                    continue;

                if (!counter.ContainsKey(letter))
                    counter[letter] = 0;

                counter[letter]++;
            }

            List<KeyValuePair<char, int>> counterAsList = counter.ToList();

            counterAsList.Sort(
                delegate(KeyValuePair<char, int> a, KeyValuePair<char, int> b)
                {
                    if (a.Value != b.Value)
                        return b.Value.CompareTo(a.Value);
                    return a.Key.CompareTo(b.Key);
                }
            );

            string alphabeticalList = String.Join("", counterAsList.Select(x => x.Key));
            return alphabeticalList.StartsWith(room.checksum);

        }

        public static string Decrypt(string input, int counter)
        {
            string retValue = "";
            foreach (char x in input)
            {
                if (x == '-')
                    retValue += ' ';
                else
                {
                    char newValue = (char)(x + (counter % 26));
                    if (newValue > 'z')
                        newValue = (char)('a' + (char)(newValue - ('z' + 1)));
                    retValue += newValue;
                }
            }
            return retValue;
        }
    }
}
