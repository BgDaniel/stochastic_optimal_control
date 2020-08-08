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
        private CalculationEngine.OptimalValues[][][] m_J;
        private int[][] m_pathIndices;
        private double[][] m_paths;
        private GeometricBrownianMotion m_gbm;
        private int m_iQ0;
        private OptimalController m_optimalController;

        public RollOut(CalculationEngine.OptimalValues[][][] J, double[][] paths, int[][] pathIndices, GeometricBrownianMotion gbm,
            int iQ0, OptimalController optimalController)
        {
            m_J = J;
            m_paths = paths;
            m_pathIndices = pathIndices;
            m_gbm = gbm;
            m_iQ0 = iQ0;
            m_optimalController = optimalController;
        }

        public void WriteToFile(string pathToFolder, List<int> simus)
        {
            int nbSteps = m_gbm.NbSteps;            

            foreach (var i_simu in simus)
            {
                var pathRolledOut = new List<OptimalStep>();

                var _Q = m_optimalController.Q(m_iQ0);
                var _iQ = m_iQ0;

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
                engine.WriteFile(pathToFolder + string.Format("\\simulation_{0}.csv", i_simu + 1), pathRolledOut);
            }
        }
    }
}
