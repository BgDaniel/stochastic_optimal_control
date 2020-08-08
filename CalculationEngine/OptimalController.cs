using MathNet.Numerics.Distributions;
using StochasticControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;

namespace CalculationEngine
{
    public class OptimalController
    {
        private double[][] m_paths;        
        private double m_Qmin;
        private double m_Qmax;
        private double m_qmin;
        private double m_qmax;
        private int m_stepsQ;
        private double m_dQ;
        private IStochModel m_model;
        private QSpace m_qSpace;

        public int[][] PathIndices { private set; get; }

        public double Q(int iQ)
        {
            return m_Qmin + iQ * m_dQ; 
        }

        public OptimalController(IStochModel model, QSpace qSpace, double[][] paths)
        {
            m_model = model;
            m_qSpace = qSpace;                       
            m_paths = paths;
        }

        public OptimalStep[][][] Control()
        {
            int nbSteps = m_model.NbSteps;
            double[][] S = m_model.GridS;

            var J = new OptimalStep[nbSteps][][];

            for (int iTime = 0; iTime < nbSteps; iTime++)
            {
                J[iTime] = new OptimalStep[m_stepsQ][];

                var nbS = m_model.S(iTime).Count();

                for (int j_Q = 0; j_Q < m_stepsQ; j_Q++)
                {
                    J[iTime][j_Q] = new OptimalStep[nbS];
                }
            }

            var nbSLast = m_model.S(nbSteps).Count();

            for (int j_Q = 0; j_Q < m_stepsQ; j_Q++)
            {
                for (int i_S = 0; i_S < nbSLast; i_S++)
                {
                    J[nbSteps - 2][j_Q][i_S] = new OptimalStep(.0, null, null, null);
                }
            }
            
            for (int iTime = nbSteps - 2; iTime > 0; iTime--)
            {
                for (int jS = 0; jS < m_model.S(iTime).Count(); jS++)
                {
                    for (int k_Q = 0; k_Q < m_stepsQ; k_Q++)
                    {
                        var next_Qs = new List<int>();

                        for (int l_Q = 0; l_Q < m_stepsQ; l_Q++)
                        {
                            if (m_qmin <= (l_Q - k_Q) * m_dQ && (l_Q - k_Q) * m_dQ <= m_qmax)
                            {
                                next_Qs.Add(l_Q);
                            }
                        }

                        var _J = new OptimalStep(double.MinValue, null, null, null);

                        foreach (var next_Q in next_Qs)
                        {
                            var expect_J_next = .0;

                            foreach(var mS in m_model.SNext(iTime + 1))
                                expect_J_next += m_model.TransitionProbability(iTime, jS, mS) * J[iTime + 1][next_Q][mS].Value;

                            var value_next = - next_Q * m_dQ * S[iTime][jS] + expect_J_next;
                            
                            if (value_next > _J.Value)
                            {
                                _J = new OptimalStep(value_next, m_qmin + next_Q * m_dQ, next_Q, (next_Q - k_Q) * m_dQ);
                            }                            
                        }

                        J[iTime][k_Q][jS] = _J;
                    }
                }
            }
            return J;
        }
    }
}
