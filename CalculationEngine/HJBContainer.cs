using System;
using System.Collections.Generic;
using System.Text;

namespace CalculationEngine
{
    public struct HJBContainer
    {
        public double Value;
        public double? QNext;
        public int? QIndexNext;
        public double? Dq;            

        public HJBContainer(double value, double? qNext, int? qIndexNext, double? dq)
        {
            Value = value;
            QNext = qNext;
            QIndexNext = qIndexNext;
            Dq = dq;
        }
    }
}
