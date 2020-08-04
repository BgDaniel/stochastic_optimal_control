using MathNet.Numerics.Distributions;
using System;
using System.Text;


namespace CalculationEngine
{
    public class GeometricBrownianMotion
    {
        public double T { private set; get; }
        public double Dt { private set; get; }
        public int NbSteps { private set; get; }
        public double Sigma { private set; get; }
        public double S0 { private set; get; }
        public double R { private set; get; }
        public int NbSimus { private set; get; }

        public GeometricBrownianMotion(double t, int steps_t, double sigma, double s0, double r, int simus)
        {
            T = t;
            Dt = t / steps_t;
            NbSteps = steps_t;
            Sigma = sigma;
            S0 = s0;
            R = r;
            NbSimus = simus;
        }

        public double[][] Simulate()
        {
            var paths = new double[NbSimus][];

            for(int i_simu = 0; i_simu < NbSimus; i_simu++)
            {
                paths[i_simu] = new double[NbSteps];
                paths[i_simu][0] = S0;
            }

            var dZ_t = new Normal(.0, Sigma * Math.Sqrt(Dt));
            
            for (int i_simu = 0; i_simu < NbSimus; i_simu++)
            {
                for (int j_time = 1; j_time < NbSteps; j_time++)
                {
                    paths[i_simu][j_time] = paths[i_simu][j_time - 1] * (1.0 + R * Dt + dZ_t.Sample());
                }
            }

            return paths;
        }
    }
}
