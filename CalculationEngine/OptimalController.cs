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
        private double m_QMin;
        private double m_QMax;
        private double m_qMin;
        private double m_qMax;
        private int m_nbStepsQ;
        private double m_dQ;
        private IStochModel m_model;

        public int[][] PathIndices { private set; get; }

        public double Q(int iQ)
        {
            return m_QMin + iQ * m_dQ; 
        }

        public OptimalController(IStochModel model, QSpace qSpace)
        {
            m_model = model;                      

            m_QMax = qSpace.QMax;
            m_QMin = qSpace.QMin;
            m_qMax = qSpace.DeltaQMax;
            m_qMin = qSpace.DeltaQMin;
            m_dQ = qSpace.Dq;
            m_nbStepsQ = qSpace.NbStepsQ;
        }

        public OptimalValues[][][] Control()
        {
            int nbSteps = m_model.NbSteps;
            double[][] S = m_model.GridS;

            var optimalValues = new OptimalValues[nbSteps][][];

            for (int iTime = 0; iTime < nbSteps; iTime++)
            {
                optimalValues[iTime] = new OptimalValues[m_nbStepsQ][];

                var nbS = m_model.S(iTime).Count();

                for (int jQ = 0; jQ < m_nbStepsQ; jQ++)
                    optimalValues[iTime][jQ] = new OptimalValues[nbS];                
            }

            var nbSLast = m_model.S(nbSteps).Count();

            for (int jQ = 0; jQ < m_nbStepsQ; jQ++)
            {
                for (int iS = 0; iS < nbSLast; iS++)
                    optimalValues[nbSteps - 2][jQ][iS] = new OptimalValues(.0, null, null, null);                
            }
            
            for (int iTime = nbSteps - 2; iTime > 0; iTime--)
            {
                for (int jS = 0; jS < m_model.S(iTime).Count(); jS++)
                {
                    for (int kS = 0; kS < m_nbStepsQ; kS++)
                    {
                        var nextQs = new List<int>();

                        for (int lQ = 0; lQ < m_nbStepsQ; lQ++)
                        {
                            if (m_qMin <= (lQ - kS) * m_dQ && (lQ - kS) * m_dQ <= m_qMax)
                                nextQs.Add(lQ);                            
                        }

                        var optimalStep = new OptimalValues(double.MinValue, null, null, null);

                        foreach (var nextQ in nextQs)
                        {
                            var expectation = .0;

                            foreach(var mS in m_model.SNext(iTime + 1))
                                expectation += m_model.TransitionProbability(iTime, jS, mS) * optimalValues[iTime + 1][nextQ][mS].Value;

                            var value_next = - nextQ * m_dQ * S[iTime][jS] + expectation;
                            
                            if (value_next > optimalStep.Value)
                                optimalStep = new OptimalValues(value_next, m_qMin + nextQ * m_dQ, nextQ, (nextQ - kS) * m_dQ);                                                       
                        }

                        optimalValues[iTime][kS][jS] = optimalStep;
                    }
                }
            }
            return optimalValues;
        }
    }
}
