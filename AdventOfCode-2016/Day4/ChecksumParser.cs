using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day4
{
    public class Room
    {
        public string name;
        public string checksum;
        public int value;
    }

    class ChecksumParser
    {
        public static Room Parse(string input)
        {

            Room retValue = new Room();

            int firstBracket = input.IndexOf("[");
            int lastBracket = input.IndexOf("]");
            int lastHyphen = input.LastIndexOf("-");

            retValue.checksum = input.Substring(firstBracket + 1, (lastBracket - firstBracket - 1));
            input = input.Substring(0, firstBracket);
            string[] rest = input.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries);

            retValue.value = int.Parse(rest.Last());

            retValue.name = input.Substring(0, lastHyphen);


            return retValue;

        }
    }
}
