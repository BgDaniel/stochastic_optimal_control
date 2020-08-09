using Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CalculationEngine
{
    public class BinomialTreeWithDrift : BaseModel, IStochModel
    {
        private double m_sigma;
        private double m_r;

        private double m_u;
        private double m_d;
        private double[] m_pU;
        private double[] m_pD;

        private Func<double, double> m_drift;

        public BinomialTreeWithDrift(double S0, int nbTimes, double T, double sigma, double r, int nbSimu,
            Func<double, double> drift) : base(S0, nbTimes, T, nbSimu)
        {
            m_sigma = sigma;
            m_r = r;
            m_drift = drift;

            m_u = Math.Exp(+m_sigma * Math.Sqrt(m_dt));
            m_d = Math.Exp(-m_sigma * Math.Sqrt(m_dt));
            m_pU = new double[nbTimes];
            m_pD = new double[nbTimes];

            for (int iTime = 0; iTime < nbTimes; iTime++)
            {
                m_pU[iTime] = (Math.Exp((m_r + m_drift(iTime * m_dt)) * m_dt) - Math.Exp(-m_sigma * Math.Sqrt(m_dt)))
                    / (Math.Exp(+m_sigma * Math.Sqrt(m_dt)) - Math.Exp(-m_sigma * Math.Sqrt(m_dt)));
                m_pU[iTime] = 1.0 - m_pU[iTime];
            }
        }

        public double[][] Grid => m_grid;

        public double[][] Paths => m_paths;

        public int[][] PathIndices => m_pathIndices;

        public double[] Times => m_times;

        public void Simulate()
        {
            m_grid = new double[NbTimes][];
            m_grid[0] = new double[] { m_S0 };

            m_paths = new double[NbSimus][];
            m_pathIndices = new int[NbSimus][];

            var rnd = new Random();

            for (int iTime = 1; iTime < NbTimes; iTime++)
            {
                m_grid[iTime] = new double[iTime + 1];

                for (int jS = 0; jS < iTime + 1; jS++)
                    m_grid[iTime][jS] = m_S0 * m_u.Pow(iTime - jS) * m_d.Pow(jS);
            }

            for (int iSimu = 0; iSimu < NbSimus; iSimu++)
            {
                m_paths[iSimu] = new double[NbTimes];
                m_paths[iSimu][0] = m_S0;
                m_pathIndices[iSimu] = new int[NbTimes];
                m_pathIndices[iSimu][0] = 0;

                for (int iTime = 1; iTime < NbTimes; iTime++)
                {
                    var k = 0;

                    if (rnd.NextDouble() <= m_pD[iTime])
                        k = 1;

                    m_pathIndices[iSimu][iTime] = m_pathIndices[iSimu][iTime - 1] + k;
                    m_paths[iSimu][iTime] = m_grid[iTime][m_pathIndices[iSimu][iTime]];
                }
            }
        }

        public IEnumerable<int> SNext(int jS, int iTime)
        {
            for (int i = 0; i < 2; i++)
                yield return jS + i;
        }

        public (double[], int[]) TransitionProb(int iTime, int jS)
        {
            return (new double[] { m_pU[iTime], m_pD[iTime] }, new int[] { jS, jS + 1 });
        }
    }
}
