using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace CalculationEngine
{
    public interface IStochModel
    {
        int NbSteps { get; }

        double[][] GeneratePaths();

        IEnumerable<double> S(int iTme);

        IEnumerable<int> SNext(int iTme);

        double TransitionProbability(int iTime, int jS, int kS);

        double[][] GridS { get; }
    }
}
