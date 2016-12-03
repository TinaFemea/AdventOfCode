using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day3
{
    class TriangleChecker
    {
       
        public bool IsGoodTriangle(int a, int b, int c)
        {
            if (a + b <= c)
                return false;
            if (b + c <= a)
                return false;
            if (c + a <= b)
                return false;

            return true;
        }
    }
}
