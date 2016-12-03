using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Day1
{
    class Walker
    {

        enum DirEnum
        {
            N,
            E,
            S,
            W
        }

        long X = 0;
        long Y = 0;

        private void Record()
        {
            Tuple<long, long> whereIAmNow = new Tuple<long, long>(X, Y);
            if (locationsVisited.Contains(whereIAmNow))
                Console.Out.WriteLine("Revisited {0},{1}: {2}", X, Y, Math.Abs(X) + Math.Abs(Y));
            locationsVisited.Add(whereIAmNow);
        }

        private void WalkThisWay(DirEnum facing, long distance)
        {
            for (int i = 0; i < distance; i++)
            {
                switch (facing)
                {
                    case DirEnum.N:
                        Y ++;
                        break;
                    case DirEnum.E:
                        X --;
                        break;
                    case DirEnum.S:
                        Y --;
                        break;
                    case DirEnum.W:
                        X ++;
                        break;
                }
                Record();
            }
        }

        List<Tuple<long, long>> locationsVisited = new List<Tuple<long, long>>();

        public void ComputeDistance(string input)
        {
            locationsVisited = new List<Tuple<long, long>>();

            X = 0;
            Y = 0;

            DirEnum facing = DirEnum.N;

            string[] directions = input.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string thisMove in directions)
            {
                char turn = thisMove[0];
                int count = int.Parse(thisMove.Substring(1));

                int nextDir = turn == 'R' ? (int)facing + 1 : (int)facing - 1;

                if (nextDir > 3)
                    facing = DirEnum.N;
                else if (nextDir < 0)
                    facing = DirEnum.W;
                else
                    facing = (DirEnum)nextDir;

                WalkThisWay(facing, count);

            }

            long total = Math.Abs(X) + Math.Abs(Y);

            Console.Out.WriteLine("{0}: {1}", input, total);

        }
    }

}
