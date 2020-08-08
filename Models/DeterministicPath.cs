using CalculationEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class DeterministicPath : BaseModel, IStochModel
    {
        private Func<double, double> m_path;

        public DeterministicPath(Func<double, double> path, int nbTimes, double T) : base(path(0), nbTimes, T)
        {
            m_path = path;
        }

        public int NbTimes => m_nbTimes;

        public double[][] Simulate()
        {
            var paths = new double[NbTimes][];

            for (int iTime = 0; iTime < NbTimes; iTime++)
            {
                paths[iTime] = new double[1] { m_path(iTime * m_dt) };
            }

            return paths;
        }

        public (double[], int[]) TransitionProb(int iTime, int jS)
        {
            return (new double[] { 1.0 }, new int[] { 0 });
        }
    }
}
