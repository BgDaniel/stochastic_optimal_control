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

        (double[], int[]) TransitionProb(int iTime, int jS);

        double[][] Simulate();
    }
}
