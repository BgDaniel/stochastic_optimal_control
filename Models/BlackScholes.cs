using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    class BlackScholes
    {
        /*
        
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

        private void SetDiscretization()
        {
            int nbSimus = m_gbm.NbSimus;
            int nbSteps = m_gbm.NbSteps;

            m_discretization = new double[nbSteps][];
            PathIndices = new int[nbSimus][];

            for (int i_simu = 0; i_simu < nbSimus; i_simu++)
            {
                PathIndices[i_simu] = new int[nbSteps];
            }

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

                for (int i_simu = 0; i_simu < nbSimus; i_simu++)
                {
                    for (int j_disc = 0; j_disc < m_stepsS; j_disc++)
                    {
                        if (m_discretization[i_time][j_disc] <= m_paths[i_simu][i_time]
                            && m_paths[i_simu][i_time] < m_discretization[i_time][j_disc + 1])
                        {
                            PathIndices[i_simu][i_time] = j_disc;
                        }
                    }
                }
            }
        }

        */
    }
}
