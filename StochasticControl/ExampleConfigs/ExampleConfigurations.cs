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
            Sigma = .3,
            S0 = 1.0,
            R = .02,
            NbSimus = 1000,
            NbDiscr = 100,
            QMin = .0,
            QMax = .5,
            DeltaQMin = - .1,
            DeltaQMax = +.15,
            NbStepsQ = 100
        };
    }
}
