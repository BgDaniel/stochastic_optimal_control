using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace CalculationEngine
{
    public interface IStochModel
    {
        int NbTimes { get; }

        (double[], int[]) TransitionProb(int iTime, int jS);

        double[][] Simulate();
    }
}
