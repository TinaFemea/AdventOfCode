using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Day6
{
    class FastLights
    {
        private int[,] lights = new int[1000,1000];
        private Operation operation;
        private int minX;
        private int maxX;
        private int minY;
        private int maxY;

        enum Operation
        {
            TurnOn,
            TurnOff,
            Toggle
        }

        void ParseDirections(string directions)
        {
            List<string> parts = directions.Split(' ').ToList();
            if (parts.Count < 4)
                throw new ArgumentException("bad direction string");

            if (parts[0].Equals("turn"))
            {
                operation = parts[1].Equals("off") ? Operation.TurnOff : Operation.TurnOn;
                parts.RemoveRange(0, 2);
            }
            else
            {
                operation = Operation.Toggle;
                parts.RemoveRange(0, 1);
            }

            short[] ints1 = parts[0].Split(',').Select(Int16.Parse).ToArray();
            short[] ints2 = parts[2].Split(',').Select(Int16.Parse).ToArray();

            minX = Math.Min(ints1[0], ints2[0]);
            maxX = Math.Max(ints1[0], ints2[0]);
            minY = Math.Min(ints1[1], ints2[1]);
            maxY = Math.Max(ints1[1], ints2[1]);
        }

        void PerformOperationPart1(int x, int y)
        {
            switch (operation)
            {
                case Operation.Toggle:
                    lights[x,y] = lights[x,y] == 0 ? 1 : 0;
                    break;
                case Operation.TurnOn:
                    lights[x,y] = 1;
                    break;
                case Operation.TurnOff:
                    lights[x,y] = 0;
                    break;
            }
        }

        void PerformOperationPart2(int x, int y)
        {
            switch (operation)
            {
                case Operation.Toggle:
                    lights[x,y] += 2;
                    break;
                case Operation.TurnOn:
                    lights[x,y] += 1;
                    break;
                case Operation.TurnOff:
                    lights[x,y] = Math.Max(lights[x,y] - 1, 0);
                    break;
            }
        }

        void ManipulateLights(string directions, Action<int, int> method)
        {
            ParseDirections(directions);

            for (int x = minX; x <= maxX; x++)
                for (int y = minY; y <= maxY; y++)
                    method(x, y);
        }

        void ZeroLights()
        {
            for (int x = 0; x < 1000; x++)
                for (int y = 0; y < 1000; y++)
                    lights[x,y] = 0;
        }

        long CountLights()
        {
            long howMany = 0;
            for (int x = 0; x < 1000; x++)
                for (int y = 0; y < 1000; y++)
                    howMany += lights[x, y];
            
            return howMany;


        }
        public void ProcessLightsFaster()
        {
            Stopwatch watch = Stopwatch.StartNew();

            ZeroLights();

            foreach (var line in File.ReadLines("input.txt"))
                ManipulateLights(line, PerformOperationPart1);

            long numLightsOn = CountLights();
            Console.WriteLine(numLightsOn);

            ZeroLights();
            foreach (var line in File.ReadLines("input.txt"))
                ManipulateLights(line, PerformOperationPart2);

            long totalBrightness = CountLights();
;
            Console.WriteLine(totalBrightness);

            watch.Stop();
            long timing = watch.ElapsedMilliseconds;
            Console.WriteLine("fast method: {0} ms", timing);
        }
    }
}
