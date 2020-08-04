using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace StochasticControl
{
    [DelimitedRecord(";")]
    public class OptimalStep
    {
        public double S;
        public double Q;
        public double q;
        public double Value;

        public OptimalStep(double _S, double _Q, double _q, double _Value)
        {
            S = _S;
            Q = _Q;
            q = _q;
            Value = _Value;
        }
    }
}
