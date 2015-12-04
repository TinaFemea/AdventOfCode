using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Day4
{
    class Program
    {
        static void CountHouses(IEnumerable<char> directions, Dictionary<Tuple<long, long>, bool> houses)
        {
            long x = 0;
            long y = 0;

            houses[new Tuple<long, long>(x, y)] = true;

            foreach (char currChar in directions)
            {
                switch (currChar)
                {
                    case '^':
                        y++;
                        break;
                    case 'v':
                        y--;
                        break;
                    case '>':
                        x++;
                        break;
                    case '<':
                        x--;
                        break;
                    default:
                        continue;
                }

                Tuple<long, long> thisHouse = new Tuple<long, long>(x, y);

                houses[thisHouse] = true;
            }

        }

        static void Main(string[] args)
        {
            String inputText = File.ReadAllText("input.txt");

            Dictionary<Tuple<long, long>, bool> houses = new Dictionary<Tuple<long, long>, bool>();
            CountHouses(inputText, houses);
            Console.WriteLine("Santa alone: {0}", houses.Count);
            
            houses.Clear();
            CountHouses(inputText.Where((thisChar, i) => i % 2 == 0), houses);
            CountHouses(inputText.Where((thisChar, i) => i % 2 == 1), houses);
            Console.WriteLine("Santa and Robot: {0}", houses.Count);
        }
    }
}
