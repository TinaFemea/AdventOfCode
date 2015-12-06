using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Day6
{
    class Program
    {
        static Tuple<int, int> ParseNumbers(string numbers)
        {
            short[] ints = numbers.Split(',').Select(Int16.Parse).ToArray();
            if (ints.Length != 2)
                throw new ArgumentException("bad numbers");

            return new Tuple<int, int>(ints[0], ints[1]);
        }

        enum Operation
        {
            TurnOn,
            TurnOff,
            Toggle
        }

        static void ParseDirections(string directions, out Operation operation, List<Tuple<int,int>> corners  )
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

            corners.Add(ParseNumbers(parts[0]));
            corners.Add(ParseNumbers(parts[2]));

        }

        static void PerformOperationPart1(int x, int y, Operation operation, Dictionary<Tuple<int, int>, int> lights)
        {
            Tuple<int, int> location = new Tuple<int, int>(x, y);

            switch (operation)
            {
                case Operation.Toggle:
                    if (!lights.ContainsKey(location))
                        lights[location] = 0;
                    lights[location] = lights[location] == 0 ? 1 : 0;
                    break;
                case Operation.TurnOn:
                    lights[location] = 1;
                    break;
                case Operation.TurnOff:
                    lights[location] = 0;
                    break;
            }
        }

        static void PerformOperationPart2(int x, int y, Operation operation, Dictionary<Tuple<int, int>, int> lights)
        {
            Tuple<int, int> location = new Tuple<int, int>(x, y);

            if (!lights.ContainsKey(location))
                lights[location] = 0;

            switch (operation)
            {
                case Operation.Toggle:
                    lights[location] += 2;
                    break;
                case Operation.TurnOn:
                    lights[location] += 1;
                    break;
                case Operation.TurnOff:
                    lights[location] = Math.Max(lights[location] - 1, 0);
                    break;
            }
        }

        static void ManipulateLights(string directions, Dictionary<Tuple<int, int>, int> lights, Action<int, int, Operation, Dictionary<Tuple<int, int>, int>> method)
        {
            Operation operation;
            List<Tuple<int, int>> corners = new List<Tuple<int, int>>(2);
            
            ParseDirections(directions, out operation, corners);

            int minX = Math.Min(corners[0].Item1, corners[1].Item1);
            int maxX = Math.Max(corners[0].Item1, corners[1].Item1);
            int minY = Math.Min(corners[0].Item2, corners[1].Item2);
            int maxY = Math.Max(corners[0].Item2, corners[1].Item2);

            for (int x = minX; x <= maxX; x++)
                for (int y = minY; y <= maxY; y++)
                    method(x, y, operation, lights);
        }

        static void Main(string[] args)
        {
            Dictionary<Tuple<int, int>, int> lights = new Dictionary<Tuple<int, int>, int>();

            foreach (var line in File.ReadLines("input.txt"))
                ManipulateLights(line, lights, PerformOperationPart1);

            long numLightsOn = lights.Count(pair => pair.Value == 1);
            Console.WriteLine(numLightsOn);

            lights.Clear();
            foreach (var line in File.ReadLines("input.txt"))
                ManipulateLights(line, lights, PerformOperationPart2);

            long totalBrightness = lights.Sum(pair => pair.Value);
            Console.WriteLine(totalBrightness);


        }
    }
}
