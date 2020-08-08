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
            T = 1.0,
            NbTimes = 100,
            Sigma = .3,
            S0 = 1.0,
            R = .02,
            NbSimus = 1000,
            NbDiscr = 100,
            QMin = .0,
            QMax = .5,
            Q0 = .2,
            DeltaQMin = - .1,
            DeltaQMax = +.12,
            NbStepsQ = 100
        };
    }
}
