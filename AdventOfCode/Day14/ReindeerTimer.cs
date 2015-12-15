using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day14
{
    class ReindeerTimer
    {
        private class ReindeerStatus
        {
            public string name;
            private int distancePerSec;
            private int flyingTime;
            private int restingTime;

            private List<int> distances;
            public int numPoints;

            public void ParseFromString(string input)
            {
                string[] tokens = input.Split(' ');
                name = tokens[0];
                distancePerSec = Int32.Parse(tokens[3]);
                flyingTime = Int32.Parse(tokens[6]);
                restingTime = Int32.Parse(tokens[13]);
                numPoints = 0;
            }

            public void PopulateDistances(int howLong)
            {
                distances = new List<int>(howLong);

                int thisDistance = 0;
                int timeRemaining = howLong;
                while (timeRemaining > 0)
                {
                    //  flying time first
                    int currFlyingTime = Math.Min(timeRemaining, flyingTime);
                    for (int i = 0; i < currFlyingTime; i++)
                    {
                        thisDistance += distancePerSec;
                        distances.Add(thisDistance);
                        timeRemaining--;
                    }

                    long currRestingTime = Math.Min(timeRemaining, restingTime);
                    distances.AddRange(Enumerable.Repeat(thisDistance, Convert.ToInt32(currRestingTime)));
                    timeRemaining -= restingTime;
                }
            }

            public int HowFarHaveIGone(int howLong)
            {
                return distances[howLong-1];
            }

            public void AddPoints()
            {
                numPoints++;
            }
        }

        List<ReindeerStatus> flyingRatStats = new List<ReindeerStatus>(); 

        private void ReadInReindeer(string input)
        {
            ReindeerStatus status = new ReindeerStatus();
            status.ParseFromString(input);
            flyingRatStats.Add(status);

        }
        public void ProcessAllReindeer()
        {
            foreach (string readLine in File.ReadLines("input.txt"))
                ReadInReindeer(readLine);

            foreach (ReindeerStatus reindeerStatus in flyingRatStats)
                reindeerStatus.PopulateDistances(2503);

            Console.WriteLine("{0} km", flyingRatStats.Max(x => x.HowFarHaveIGone(2503)));
            for (int i = 1; i < 2503; i++)
            {
                int maxDistance = flyingRatStats.Max(x => x.HowFarHaveIGone(i));
                foreach (ReindeerStatus reindeer in flyingRatStats.Where(x => x.HowFarHaveIGone(i) == maxDistance))
                    reindeer.AddPoints();
            }

            foreach (ReindeerStatus reindeerStatus in flyingRatStats.OrderBy(status => status.numPoints ))
            {
                Console.WriteLine("{0}: {1} points", reindeerStatus.name, reindeerStatus.numPoints);
            }


            Console.ReadLine();

        }
    }
}
