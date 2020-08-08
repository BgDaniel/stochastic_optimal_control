using System;
using System.Collections.Generic;
using System.Text;

namespace CalculationEngine
{
    public class BinomialTree : IStochModel
    {
        public BinomialTree()
        {

        }

        public int NbSteps => throw new NotImplementedException();

        public double[][] GridS => throw new NotImplementedException();

        public double[][] GeneratePaths()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<double> S(int iTme)
        {
            throw new NotImplementedException();
        }

        public double[][] Simulate()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> SNext(int iTme)
        {
            throw new NotImplementedException();
        }

        public double TransitionProbability(int iTime, int jS, int kS)
        {
            throw new NotImplementedException();
        }
    }
}
