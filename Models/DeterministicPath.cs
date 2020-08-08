using CalculationEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class DeterministicPath : BaseModel, IStochModel
    {
        private Func<double, double> m_path;

        public DeterministicPath(Func<double, double> path, int nbTimes, double T) : base(path(0), nbTimes, T, 0)
        {
            m_path = path;
        }

        public double[][] Grid => m_grid;

        public double[][] Paths => m_paths;

        public int[][] PathIndices => m_pathIndices;

        public void Simulate()
        {
            m_grid = new double[NbTimes][];
            m_paths = new double[NbTimes][];
            m_pathIndices = new int[NbTimes][];

            for (int iTime = 0; iTime < NbTimes; iTime++)
            {
                m_grid[iTime] = new double[1] { m_path(iTime * m_dt) };
                m_paths[iTime] = new double[1] { m_path(iTime * m_dt) };
                m_pathIndices[iTime] = new int[1] { 0 };
            }
        }

        public (double[], int[]) TransitionProb(int iTime, int jS)
        {
            return (new double[] { 1.0 }, new int[] { 0 });
        }
    }
}
