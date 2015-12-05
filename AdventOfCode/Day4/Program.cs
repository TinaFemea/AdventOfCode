using System;
using System.Diagnostics;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            MD5Hasher hasher = new MD5Hasher();

 /*         long testcase1 = hasher.FindLowestHashThatStartsWithNZeros("abcdef",5);
            if (testcase1 != 609043)
                throw new Exception("test case failed");

            long testcase2 = hasher.FindLowestHashThatStartsWithNZeros("pqrstuv",5);
            if (testcase2 != 1048970)
                throw new Exception("test case failed");
*/
            string key = "ckczppom";
            Stopwatch watch = Stopwatch.StartNew();
            long fiveZerosResult = hasher.FindLowestHashThatStartsWithNZeros(key, 5);
            Console.WriteLine("5 zeros: {0}", fiveZerosResult);

            long sixZerosResult = hasher.FindLowestHashThatStartsWithNZeros(key, 6);
            Console.WriteLine("6 zeros: {0}", sixZerosResult);

            if (sixZerosResult <= fiveZerosResult)
                throw new Exception("test case failed");

            long totalIterations = fiveZerosResult + sixZerosResult;
            watch.Stop();
            double numMilliseconds = watch.ElapsedMilliseconds;

            Console.WriteLine("{0} iterations in {1} milliseconds", totalIterations, numMilliseconds );
            Console.ReadLine();
        }
    }
}
