using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day2
{
    class Keypad
    {
        int limitX = 2;
        int limitY = 2;

        public char GetHumanReadable(Tuple<int, int> input)
        {
            int X = input.Item1;
            int Y = input.Item2;

            int retValue = (Y*(limitX+1)) + X + 1;
            return retValue.ToString()[0];
        }
        public Tuple<int, int> Move(string line, Tuple<int, int> startPoint)
        {
            Tuple<int, int> currPoint = startPoint;
            foreach (char currMove in line)
            {
                currPoint = HandleOneMove(currMove, currPoint);
            }

            return currPoint;
        }

        private Tuple<int, int> HandleOneMove(char currMove, Tuple<int, int> startPoint)
        {
            int newX = startPoint.Item1;
            int newY = startPoint.Item2;

            switch (currMove)
            {
                case 'U':
                    newY = Math.Max(newY - 1, 0);
                    break;
                case 'D':
                    newY = Math.Min(newY + 1, limitY);
                    break;
                case 'L':
                    newX = Math.Max(newX - 1, 0);
                    break;
                case 'R':
                    newX = Math.Min(newX + 1, limitX);
                    break;
            }

            return new Tuple<int, int>(newX, newY);
        }
    }
}
