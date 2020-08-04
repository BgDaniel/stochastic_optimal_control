using MathNet.Numerics.Distributions;
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
        private double[][] m_discretization;
        private double[][][] m_transitionProb;
        private int m_stepsS;
        private GeometricBrownianMotion m_gbm;
        private double m_Qmin;
        private double m_Qmax;
        private double m_qmin;
        private double m_qmax;
        private int m_stepsQ;
        private double m_dQ;

        public OptimalController(double[][] paths, int nbDiscr, GeometricBrownianMotion gbm, double Qmin, double Qmax, double qmin,
            double qmax, int stepsQ)
        {
            m_paths = paths;
            m_stepsS = nbDiscr;
            m_gbm = gbm;
            m_Qmin = Qmin;
            m_Qmax = Qmax;
            m_qmin = qmin;
            m_qmax = qmax;
            m_stepsQ = stepsQ;
            m_dQ = (m_Qmax - m_Qmin) / stepsQ;

            SetDiscretization();
            SetTransitionProb();
        }

        private void SetDiscretization()
        {
            int nbSimus = m_gbm.NbSimus;
            int nbSteps = m_gbm.NbSteps;

            m_discretization = new double[nbSteps][];

            for (int i_time = 1; i_time < nbSteps; i_time++)
            {
                m_discretization[i_time] = new double[m_stepsS + 1];
                
                var pathForTime = Enumerable.Range(0, nbSimus).Select(j_simu => m_paths[j_simu][i_time]);
                var s_min = pathForTime.Min();
                var s_max = pathForTime.Max();

                var delta = (s_max - s_min) / m_stepsS; 

                for (int j_disc = 0; j_disc < m_stepsS + 1; j_disc++)
                {
                    m_discretization[i_time][j_disc] = s_min + j_disc * delta;
                }
            }
        }

        private void SetTransitionProb()
        {
            int nbSteps = m_gbm.NbSteps;
            m_transitionProb = new double[nbSteps - 1][][];

            var norm = new Normal();

            var r = m_gbm.R;
            var sigma = m_gbm.Sigma;
            var dt = m_gbm.Dt;
            var s0 = m_gbm.S0;

            for (int i_time = 0; i_time < nbSteps - 1; i_time++)
            {
                if (i_time == 0)
                {
                    m_transitionProb[i_time] = new double[1][];
                    m_transitionProb[i_time][0] = new double[m_stepsS];

                    for (int j_disc = 0; j_disc < m_stepsS; j_disc++)
                    {
                        var lower = (Math.Log(m_discretization[1][j_disc] / s0) - r * dt + .5 * sigma * sigma * dt) / (sigma * Math.Sqrt(dt));
                        var upper = (Math.Log(m_discretization[1][j_disc + 1] / s0) - r * dt + .5 * sigma * sigma * dt) / (sigma * Math.Sqrt(dt));
                        var prob_lower = norm.CumulativeDistribution(lower);
                        var prob_upper = norm.CumulativeDistribution(upper);
                        m_transitionProb[i_time][0][j_disc] = prob_upper - prob_lower;
                    }
                }
                else
                {
                    m_transitionProb[i_time] = new double[m_stepsS][];

                    for (int j_disc = 0; j_disc < m_stepsS; j_disc++)
                    {
                        m_transitionProb[i_time][j_disc] = new double[m_stepsS];

                        for (int k_disc = 0; k_disc < m_stepsS; k_disc++)
                        {
                            if (j_disc <= k_disc)
                            {
                                double s = .5 * (m_discretization[i_time][j_disc + 1] + m_discretization[i_time][j_disc]);

                                var lower = (Math.Log(m_discretization[i_time + 1][k_disc] / s) - r * dt + .5 * sigma * sigma * dt) / (sigma * Math.Sqrt(dt));
                                var upper = (Math.Log(m_discretization[i_time + 1][k_disc + 1] / s) - r * dt + .5 * sigma * sigma * dt) / (sigma * Math.Sqrt(dt));
                                var prob_lower = norm.CumulativeDistribution(lower);
                                var prob_upper = norm.CumulativeDistribution(upper);
                                m_transitionProb[i_time][j_disc][k_disc] = prob_upper - prob_lower;
                            }
                            else
                            {
                                m_transitionProb[i_time][j_disc][k_disc] = m_transitionProb[i_time][k_disc][j_disc];
                            }                            
                        }
                    }
                }
            }
        }

        public void Control()
        {
            int nbSteps = m_gbm.NbSteps;

            var J = new HJBContainer[nbSteps][][];

            for (int i_time = 0; i_time < nbSteps; i_time++)
            {
                J[i_time] = new HJBContainer[m_stepsQ][];

                for (int j_Q = 0; j_Q < m_stepsQ; j_Q++)
                {
                    J[i_time][j_Q] = new HJBContainer[m_stepsS];
                }
            }

            for (int j_Q = 0; j_Q < m_stepsQ; j_Q++)
            {
                for (int i_S = 0; i_S < m_stepsS; i_S++)
                {
                    J[nbSteps - 2][j_Q][i_S] = new HJBContainer(.0, null, null);
                }
            }
            
            for (int i_time = nbSteps - 2; i_time > 0; i_time--)
            {
                for (int j_S = 0; j_S < m_stepsS; j_S++)
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

                        var _J = new HJBContainer(double.MinValue, null, null);

                        foreach (var next_Q in next_Qs)
                        {
                            var expect_J_next = .0;

                            for (int m_S = 0; m_S < m_stepsS; m_S++)
                            {
                                expect_J_next += m_transitionProb[i_time][j_S][m_S] * J[i_time+1][next_Q][m_S].Value;
                            }

                            var value_next = - next_Q * m_dQ * m_discretization[i_time][j_S] + expect_J_next;
                            
                            if (value_next > _J.Value)
                            {
                                _J = new HJBContainer(value_next, m_qmin + next_Q * m_dQ, next_Q);
                            }                            
                        }

                        J[i_time][k_Q][j_S] = _J;
                    }
                }
            }
        }
    }
}
