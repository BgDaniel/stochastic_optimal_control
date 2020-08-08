﻿using Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

        public BinomialTree(double S0, int nbTimes, double T, double sigma, double r, int nbSimu) : base(S0, nbTimes, T, nbSimu)
        {
            m_sigma = sigma;
            m_r = r;

            m_u = Math.Exp(+ m_sigma * Math.Sqrt(m_dt));
            m_d = Math.Exp(- m_sigma * Math.Sqrt(m_dt));
            m_pU = (Math.Exp(m_r * m_dt) - Math.Exp(- m_sigma * Math.Sqrt(m_dt))) 
                / (Math.Exp(+ m_sigma * Math.Sqrt(m_dt)) - Math.Exp(- m_sigma * Math.Sqrt(m_dt)));
            m_pD = 1.0 - m_pU;
        }

        public double[][] Grid => m_grid;

        public double[][] Paths => m_paths;

        public int[][] PathIndices => m_pathIndices;

        public void Simulate()
        {
            m_grid = new double[NbTimes][];
            m_grid[0] = new double[] { m_S0 };

            m_paths = new double[NbTimes][];
            m_pathIndices = new int[NbTimes][];

            var rnd = new Random();

            for(int iTime = 1; iTime < NbTimes; iTime++)
            {
                m_grid[iTime] = new double[iTime + 1];

                for (int jS = 0; jS < iTime + 1; jS++)
                    m_grid[iTime][jS] = m_S0 * m_u.Pow(iTime - jS) * m_d.Pow(jS);

                m_paths[iTime] = new double[m_nbSimus];
                m_pathIndices[iTime] = new int[m_nbSimus];

                for (int iSimu = 0; iSimu < m_nbSimus; iSimu++)
                {
                    m_pathIndices[iTime][iSimu] = m_pathIndices[iTime-1][iSimu] + rnd.Next(0, 2);
                    m_paths[iTime][iSimu] = m_grid[iTime][m_pathIndices[iTime][iSimu]];
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
            return (new double[] { m_pU, m_pD }, new int[] { jS, jS + 1});
        }
    }
}
