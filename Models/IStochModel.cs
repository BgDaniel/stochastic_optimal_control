using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace CalculationEngine
{
    public interface IStochModel
    {
        int NbTimes { get; }
        double[] Times { get; }

        double[][] Grid { get; }

        double[][] Paths { get; }

        int[][] PathIndices { get; }

        (double[], int[]) TransitionProb(int iTime, int jS);

        void Simulate();
    }
}
