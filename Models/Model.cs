using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class BaseModel
    {
        private double m_T;
        protected int m_nbTimes;
        protected double m_dt;
        protected double m_S0;

        public BaseModel(double S0, int nbTimes, double T)
        {
            m_S0 = S0;
            
            m_T = T;
            m_nbTimes = nbTimes;
            m_dt = T / nbTimes;
        }
    }
}
