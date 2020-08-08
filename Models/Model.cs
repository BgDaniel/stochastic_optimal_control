using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class BaseModel
    {
        private double m_T;
        protected int m_nbTimes;
        protected double m_dt;
        protected double m_S0;
        protected double[][] m_grid;
        protected double[][] m_paths;
        protected int[][] m_pathIndices;
        protected int m_nbSimus;
        protected double[] m_times;

        public int NbTimes => m_nbTimes;

        public int NbSimus => m_nbSimus;

        public BaseModel(double S0, int nbTimes, double T, int nbSimus)
        {
            m_S0 = S0;
            
            m_T = T;
            m_nbTimes = nbTimes;
            m_dt = T / nbTimes;

            m_times = Enumerable.Range(0, m_nbTimes).Select(iT => iT * m_dt).ToArray();

            m_nbSimus = nbSimus;
        }
    }
}
