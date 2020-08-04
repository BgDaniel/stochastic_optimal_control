using CalculationEngine;
using StochasticControl.CommandLineParser;
using System;

namespace StochasticControl
{
    class Program
    {
        static void Main(string[] args)
        {
            Execute(ExampleConfigs.ExampleConfigurations.Geom1);
        }

        private static void Execute(Options options)
        {            
            var gbm = new GeometricBrownianMotion(options.T, options.NbSteps, options.Sigma, options.S0, options.R, options.NbSimus);
            var paths = gbm.Simulate();

            var optimalController = new OptimalController(paths, options.NbDiscr, gbm, options.QMin, options.QMax, options.Qmin,
                options.Qmax, options.StepsQ);
            var J = optimalController.Control();

        }
    }
}
