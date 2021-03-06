﻿using CalculationEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class DeterministicPath : BaseModel, IStochModel
    {
        private Func<double, double> m_path;

        public DeterministicPath(Func<double, double> path, int nbTimes, double T) : base(path(0), nbTimes, T, 1)
        {
            m_path = path;
        }

        public double[][] Grid => m_grid;

        public double[][] Paths => m_paths;

        public int[][] PathIndices => m_pathIndices;

        public double[] Times => m_times;

        public void RollOutGrid()
        {
            m_grid = new double[NbTimes][];

            for (int iTime = 0; iTime < NbTimes; iTime++)
                m_grid[iTime] = new double[1] { m_path(iTime * m_dt) };
        }

        public void Simulate()
        {
            m_paths = new double[NbSimus][];
            m_paths[0] = new double[NbTimes];
            m_pathIndices = new int[NbSimus][];
            m_pathIndices[0] = new int[NbTimes];

            for (int iTime = 0; iTime < NbTimes; iTime++)
            {
                m_paths[0][iTime] = m_path(iTime * m_dt);
                m_pathIndices[0][iTime] = 0;
            }
        }

        public (double[], int[]) TransitionProb(int iTime, int jS)
        {
            return (new double[] { 1.0 }, new int[] { 0 });
        }
    }
}
