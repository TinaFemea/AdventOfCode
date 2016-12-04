using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] theInput = File.ReadAllLines("input.txt");

            int sum = 0;

            foreach (string line in theInput)
            {
                Room parsed = ChecksumParser.Parse(line);
                if (ChecksumChecker.IsValid(parsed))
                {
                    sum += parsed.value;
                    string decrypted = ChecksumChecker.Decrypt(parsed.name, parsed.value);
                    if(decrypted.Equals("northpole object storage"))
                        Console.Out.WriteLine(parsed.value);
                }
            }

            
            Console.Out.WriteLine(sum);
            Console.In.ReadLine();
            //  ChecksumChecker.Decrypt("qzmt-zixmtkozy-ivhz", 343);
        }
    }
}
