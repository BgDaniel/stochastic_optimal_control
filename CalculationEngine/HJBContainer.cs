using System;
using System.Collections.Generic;
using System.Text;

namespace CalculationEngine
{
    struct HJBContainer
    {
        public double Value;
        public double? QNext;
        public int? QIndexNext;

        public HJBContainer(double value, double? qNext, int? qIndexNext)
        {
            Value = value;
            QNext = qNext;
            QIndexNext = qIndexNext;
        }
    }
}
