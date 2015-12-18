using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day17
{
    class BackPacker
    {

        
        private int successCounter = 0;

        private List<List<Int16>> validMatches = new List<List<Int16>>(5000); 
        private void Count(int startingVolume, List<Int16> theContainers, List<Int16> usedContainers )
        {
            if (startingVolume == 0)
                return;

            for (int i = 0; i < theContainers.Count; i++)
            {
                if (startingVolume < theContainers[i])
                    continue;

                List<Int16> usedContainersCopy = new List<Int16>();
                usedContainersCopy.AddRange(usedContainers);
                usedContainersCopy.Add(theContainers[i]);
                    
                if (startingVolume > theContainers[i])
                    Count(startingVolume - theContainers[i], theContainers.GetRange(i + 1, theContainers.Count - (i + 1)), usedContainersCopy);
                if (startingVolume == theContainers[i])
                {
                    successCounter++;
                    validMatches.Add(usedContainersCopy);
                }
            }
        }

        public void CountCombinations()
        {
            List<Int16> containers = new List<Int16>(20);
            containers.AddRange(File.ReadLines("input.txt").Select(Int16.Parse));

            Count(150, containers, new List<Int16>());

            int leastContainers = validMatches.Min(X => X.Count);
            int numMinMatches = validMatches.Count(x => x.Count == leastContainers);

            Console.WriteLine(numMinMatches);
        }
    }
}
