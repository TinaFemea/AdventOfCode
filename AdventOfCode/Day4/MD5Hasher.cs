using System;
using System.Text;
using System.Security.Cryptography;

namespace Day4
{
    class MD5Hasher
    {
        private bool doesMD5HashStartWithNZeros(MD5 md5Hash, string input, long howMany)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            if (howMany > data.Length)  // technically, it could go up to double the length, but let's be reasonable
                throw new ArgumentException();

            long half = howMany / 2;
            //  first, check the whole bytes.
            for (int i = 0; i < half; i++)
                if (data[i] != 0)
                    return false;

            // do we need another half a byte?
            if (howMany % 2 == 1)
            {
                if (data[half] > 0x0f)
                    return false;
            }
            return true;
        }

        public long FindLowestHashThatStartsWithNZeros(string key, long howMany)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                long counter = 0;
                string currentString = key + counter;

                while (!doesMD5HashStartWithNZeros(md5Hash, currentString, howMany))
                {
                    counter++;
                    currentString = key + counter;
                }
                return counter;
            }

        }
    }
}
