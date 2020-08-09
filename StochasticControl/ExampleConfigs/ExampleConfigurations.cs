using StochasticControl.CommandLineParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace StochasticControl.ExampleConfigs
{
    static class ExampleConfigurations
    {
        public static Options Geom1 = new Options
        {
            T = 2.0,
            NbTimes = 200,
            Sigma = .4,
            S0 = 1.0,
            R = +.02,
            NbSimus = 1,
            NbDiscr = 200,
            QMin = .0,
            QMax = .5,
            Q0 = .2,
            DeltaQMin = - .1,
            DeltaQMax = +.12,
            NbStepsQ = 100
        };
    }
}
