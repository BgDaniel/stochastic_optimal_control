using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculationEngine
{
    public class BinomialTree : BaseModel, IStochModel
    {
        private double m_sigma;
        private double m_r;
        
        private double m_u;
        private double m_d;
        private double m_pU;
        private double m_pD;

        public int NbTimes => m_nbTimes;

        public BinomialTree(double S0, int nbTimes, double T, double sigma, double r) : base(S0, nbTimes, T)
        {
            m_sigma = sigma;
            m_r = r;

            m_u = Math.Exp(+ m_sigma * Math.Sqrt(m_dt));
            m_d = Math.Exp(- m_sigma * Math.Sqrt(m_dt));
            m_pU = (Math.Exp(m_r * m_dt) - Math.Exp(- m_sigma * Math.Sqrt(m_dt))) 
                / (Math.Exp(+ m_sigma * Math.Sqrt(m_dt)) - Math.Exp(- m_sigma * Math.Sqrt(m_dt)));
            m_pD = 1.0 - m_pU;
    }

        public double[][] Paths => m_paths;           

        public double[][] Simulate()
        {
            m_paths[0] = new double[] { m_S0 };

            for(int iTime = 1; iTime < NbTimes; iTime++)
            {
                m_paths[iTime] = new double[iTime + 1];

                for (int jS = 0; jS < iTime + 1; jS++)
                    m_paths[iTime][jS] = m_S0 * m_u.Pow(iTime - jS) * m_d.Pow(jS);
            }

            return m_paths;
        }

        public IEnumerable<int> SNext(int jS, int iTime)
        {
            for (int i = 0; i < 2; i++)
                yield return jS + i;            
        }

        public double TransitionProbability(int iTime, int jS, int kS)
        {
            if (jS == kS)
                return m_pU;
            else if (jS + 1 == kS)
                return m_pD;
            else
                return .0;
        }
    }
}
