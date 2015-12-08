using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day7
{
    class CircuitProcessor
    {
        public UInt16 GetValue(string input)
        {
            //  is it a number?
            UInt16 retValue = 0;
            if (UInt16.TryParse(input, out retValue))
                return retValue;

            if (wires.ContainsKey(input))
                return wires[input];
            else
            {
                ParseStuff(allLines.First(x => RHS(x).Equals(input)));
                return wires[input];
            }
        }

        private Dictionary<string, UInt16> wires = new Dictionary<string, UInt16>();
        private string[] allLines;

        public void ParseStuff(string thisInstruction)
        {
            string[] bothSides = thisInstruction.Split(new []{"->"}, StringSplitOptions.None);
            if (bothSides.Length != 2)
                throw new ArgumentException();

            string lhs = bothSides[0].Trim();
            string rhs = bothSides[1].Trim();

            string[] lhsParts = lhs.Split(' ');

            //  There are three options here.  Either there is one part, in which case it's an assignment.
            if (lhsParts.Length == 1)
            {

                wires[rhs] = GetValue(lhsParts[0]);
            }
            //  Or there are two parts, and it's a NOT
            else if (lhsParts.Length == 2 && lhsParts[0].Equals("NOT"))
            {
                wires[rhs] = (UInt16) ~GetValue(lhsParts[1]);
            }
            //  Or there are there parts, two operands and one operator
            else if (lhsParts.Length == 3)
            {
                UInt16 op1 = GetValue(lhsParts[0]);
                UInt16 op2 = GetValue(lhsParts[2]);

                switch (lhsParts[1])
                {
                    case "AND":
                        wires[rhs] = (UInt16) (op1 & op2);
                        break;
                    case "OR":
                        wires[rhs] = (UInt16) (op1 | op2);
                        break;
                    case "LSHIFT":
                        wires[rhs] = (UInt16) (op1 << Convert.ToInt16(op2));
                        break;
                    case "RSHIFT":
                        wires[rhs] = (UInt16) (op1 >> Convert.ToInt16(op2));
                        break;
                    default:
                        throw new ArgumentException();
                }

            }
            else
                throw new ArgumentException();
            
        }

        public string RHS(string thisInstruction)
        {
            string[] bothSides = thisInstruction.Split(new[] { "->" }, StringSplitOptions.None);
            if (bothSides.Length != 2)
                throw new ArgumentException();

            return bothSides[1].Trim();
        }

        public void ProcessInput()
        {
            allLines = File.ReadAllLines("input.txt");

            UInt16 result = GetValue("a");
            Console.WriteLine(result);

            wires.Clear();
            wires["b"] = result;

            result = GetValue("a");
            Console.WriteLine(result);

           /* foreach (KeyValuePair<string, UInt16> item in wires)
                Console.WriteLine("{0}: {1}", item.Key, item.Value);
            */
            Console.ReadLine();
        }
    }
}
