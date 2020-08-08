using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public static class MathHelper
    {
        public static double Pow(this double x, int n)
        {
            for (int i = 0; i < n; i++)
                x *= x;

            return x;
        }
    }
}
