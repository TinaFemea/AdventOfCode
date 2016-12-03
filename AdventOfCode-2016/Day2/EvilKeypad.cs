using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day2
{
    class EvilKeypad
    {
        char[,] keyPad = new char[,]{
            { '0', '0', '0', '0', '0', '0', '0'},
            { '0', '0', '0', '1', '0', '0', '0'}, 
            { '0', '0', '2', '3', '4', '0', '0'},   
            { '0', '5', '6', '7', '8', '9', '0'},   
            { '0', '0', 'A', 'B', 'C', '0', '0'},   
            { '0', '0', '0', 'D', '0', '0', '0'},   
            { '0', '0', '0', '0', '0', '0', '0'}
        };

        public char GetHumanReadable(Tuple<int, int> input)
        {
            int Y = input.Item1;
            int X = input.Item2;


            return keyPad[Y, X];
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
            int newY = startPoint.Item1;
            int newX = startPoint.Item2;

            switch (currMove)
            {
                case 'U':
                    newY--;
                    break;
                case 'D':
                    newY++;
                    break;
                case 'L':
                    newX--;
                    break;
                case 'R':
                    newX++;
                    break;
            }
            
            Tuple<int, int> newTuple = new Tuple<int, int>(newY, newX);
            return GetHumanReadable(newTuple) == '0' ? startPoint : newTuple;
        }
    }
}
