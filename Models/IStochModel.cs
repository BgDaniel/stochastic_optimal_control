using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace CalculationEngine
{
    public interface IStochModel
    {
        int NbTimes { get; }

        IEnumerable<int> SNext(int jS, int iTime);

        double TransitionProbability(int iTime, int jS, int kS);

        double[][] Paths { get; }

        double[][] Simulate();
    }
}
