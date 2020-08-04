using CalculationEngine;
using FileHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StochasticControl
{
    public class RollOut
    {
        private HJBContainer[][][] m_J;
        private int[][] m_pathIndices;
        private double[][] m_paths;
        private GeometricBrownianMotion m_gbm;
        private double m_Q0;

        public RollOut(HJBContainer[][][] J, double[][] paths, int[][] pathIndices, GeometricBrownianMotion gbm, double Q0)
        {
            m_J = J;
            m_paths = paths;
            m_pathIndices = pathIndices;
            m_gbm = gbm;
            m_Q0 = Q0;
        }

        public void WriteToFile(string pathToFolder, List<int> simus)
        {
            int nbSteps = m_gbm.NbSteps;            

            foreach (var i_simu in simus)
            {
                var pathRolledOut = new List<OptimalStep>();

                var _Q = m_Q0;
                var _iQ = 0;

                for (int j_time = 0; j_time < nbSteps; j_time++)
                {
                    var _iS = m_pathIndices[j_time][i_simu];

                    pathRolledOut.Add(new OptimalStep(m_paths[i_simu][j_time], 
                        _Q, m_J[j_time][_iQ][_iS].Dq.Value, 
                        m_J[j_time][_iQ][_iS].Value));
                    _Q = m_J[j_time][_iQ][_iS].QNext.Value;
                    _iQ = m_J[j_time][_iQ][_iS].QIndexNext.Value;
                }

                var engine = new FileHelperEngine(typeof(OptimalStep));
                engine.HeaderText = engine.GetFileHeader();
                engine.WriteFile(path, pathRolledOut);
            }
        }

        private void WriteToFile(string path)
        {

        }
    }
}
