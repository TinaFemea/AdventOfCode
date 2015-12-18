using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day18
{
    class GameOfLight
    {
        private int rowLength;
        private int[,] lights;

        private void populateLights(List<string> strings)
        {
            rowLength = strings.Count;
            lights = new int[rowLength + 2, rowLength + 2];

            for (int y = 0; y < strings.Count; y++)
            {
                for (int x = 0; x < strings[y].Length; x++)
                    lights[y + 1, x + 1] = (strings[y][x] == '#') ? 1 : 0;
            }

            TurnOnStuckLights();
        }

        private int CountNeighbors(int x, int y)
        {
            int count = 0;
            count += lights[x - 1, y - 1];
            count += lights[x - 1, y];
            count += lights[x - 1, y + 1];
            count += lights[x, y + 1];
            count += lights[x, y - 1];
            count += lights[x + 1, y + 1];
            count += lights[x + 1, y];
            count += lights[x + 1, y - 1];

            return count;
        }

        private void TurnOnStuckLights()
        {
            lights[1, 1] = 1;
            lights[1, rowLength] = 1;
            lights[rowLength, rowLength] = 1;
            lights[rowLength, 1] = 1;

        }


        private void RunOneIteration()
        {
            int[,] newLights = new int[rowLength + 2, rowLength + 2];
            
            for (int x = 1; x <= rowLength; x++)
                for (int y = 1; y <= rowLength; y++)
                {
                    int neighbors = CountNeighbors(x, y);
                    if (lights[x, y] == 1)
                        newLights[x, y] = (neighbors == 2 || neighbors == 3) ? 1 : 0;
                    if (lights[x, y] == 0)
                        newLights[x, y] = (neighbors == 3) ? 1 : 0;
                }
            lights = newLights;
            TurnOnStuckLights();
        }

        private void PrintLights()
        {
            for (int x = 1; x <= rowLength; x++)
            {
                for (int y = 1; y <= rowLength; y++)
                    Console.Write(lights[x, y] == 1 ? '#' : '.');
                Console.WriteLine();
            }
        }
        public void RunGameOfLights()
        {
            populateLights(File.ReadLines("input.txt").ToList());

            for (int i = 0; i < 100; i++)
                RunOneIteration();
            PrintLights();

            int numOn = lights.Cast<int>().Sum();
        }
    }
}
